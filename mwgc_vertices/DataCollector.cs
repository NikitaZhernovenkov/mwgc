// Decompiled with JetBrains decompiler
// Type: mwgc.DataCollector
// Assembly: mwgc, Version=1.2.8468.42059, Culture=neutral, PublicKeyToken=null
// MVID: 82A161B2-3E36-490A-94BF-0C902BEC9901
// Assembly location: D:\Users\nikzh\Downloads\mwgc_vertices.exe

using mwgc.AseLib;
using mwgc.RawGeometry;
using mwgc.RealEngine;
using System;
using System.Collections;
using System.Globalization;
using System.IO;

namespace mwgc
{
  public class DataCollector
  {
    private Hashtable materialHT;

    private void LoadCrossLinks(RealGeometryFile rgf)
    {
      if (Compiler.Options["xlink"] == null)
        return;
      FileStream fileStream = new FileStream(Compiler.Options["xlink"], FileMode.Open, FileAccess.Read);
      StreamReader streamReader = new StreamReader((Stream) fileStream);
      int num = 0;
      string str1;
      do
      {
        ++num;
        str1 = streamReader.ReadLine();
        if (str1 != null && !str1.StartsWith("#") && !str1.StartsWith("//") && !str1.StartsWith(";") && str1.IndexOf('=') > -1)
        {
          string[] strArray = str1.Split('=');
          string str2 = this.ResolveRealNameForce(strArray[0].Trim());
          uint hash = DataCollector.RealHash(str2);
          string str3 = this.ResolveRealNameForce(strArray[1].Trim());
          int partIndex = rgf.FindPartIndex(DataCollector.RealHash(str3));
          if (partIndex > -1)
          {
            if (rgf.FindPartIndex(hash) > -1)
            {
              Compiler.WarningOutput(string.Format(" + Part already exists: {0} at line {1}", (object) strArray[1], (object) num));
            }
            else
            {
              Compiler.VerboseOutput(string.Format(" + Crosslink from {0} to {1}", (object) strArray[0], (object) strArray[1]));
              RealGeometryPart realGeometryPart = rgf[partIndex];
              RealGeometryPart part = new RealGeometryPart()
              {
                PartData = realGeometryPart.PartData,
                PartInfo = realGeometryPart.PartInfo
              };
              part.PartInfo.PartName = new FixedLenString(str2);
              part.PartInfo.Hash = hash;
              rgf.AddPart(part);
            }
          }
          else
            Compiler.WarningOutput(string.Format(" + No such target part: {0} at line {1}", (object) strArray[1], (object) num));
        }
      }
      while (str1 != null);
      fileStream.Close();
    }

    private void LoadMaterialHT()
    {
      Hashtable hashtable = new Hashtable();
      if (Compiler.Options["matlist"] != null)
      {
        FileStream fileStream = new FileStream(Compiler.Options["matlist"], FileMode.Open, FileAccess.Read);
        StreamReader streamReader = new StreamReader((Stream) fileStream);
        string str;
        do
        {
          str = streamReader.ReadLine();
          if (str != null && !str.StartsWith("#") && !str.StartsWith("//") && !str.StartsWith(";") && str.IndexOf('=') > -1)
          {
            string[] strArray = str.Split('=');
            hashtable.Add((object) strArray[0].Trim(), (object) strArray[1].Trim());
          }
        }
        while (str != null);
        fileStream.Close();
      }
      this.materialHT = hashtable;
    }

    private static uint RealHash(string str)
    {
      uint num = uint.MaxValue;
      foreach (char ch in str)
        num = num * 33U + (uint) ch;
      return num;
    }

    private string ResolveRealNameForce(string precompileName)
    {
      return (Compiler.Options["xname"] + precompileName).ToUpper();
    }

    private string ResolveRealName(string precompileName)
    {
      string upper = Compiler.Options["xname"].ToUpper();
      string str = precompileName;
      if (precompileName.StartsWith("x_"))
        str = upper + precompileName.Substring(1);
      return str.ToUpper();
    }

    public RealGeometryFile Collect(RawGeometryFile rawGeom)
    {
      RealGeometryFile rgf = new RealGeometryFile();
      Hashtable hashtable1 = new Hashtable();
      this.LoadMaterialHT();
      for (int key = 0; key < rawGeom.Header.NumMaterials; ++key)
      {
        DataCollector.MatTexStringPair matTexStringPair = new DataCollector.MatTexStringPair();
        string data = rawGeom.Header.MatNames[key].Data;
        if (this.materialHT.ContainsKey((object) data))
          data = (string) this.materialHT[(object) data];
        string[] strArray = data.Split('/');
        if (strArray.Length < 2)
        {
          Compiler.WarningOutput(string.Format("Material {0} has invalid name.", (object) data));
          matTexStringPair.Material = "DEFAULT";
          matTexStringPair.Texture = "DEFAULT";
        }
        else
        {
          matTexStringPair.Material = strArray[0];
          matTexStringPair.Texture = strArray[1];
        }
        hashtable1.Add((object) key, (object) matTexStringPair);
      }
      ArrayList arrayList1 = new ArrayList();
      ArrayList arrayList2 = new ArrayList();
      ArrayList arrayList3 = new ArrayList();
      ArrayList arrayList4 = new ArrayList();
      ArrayList arrayList5 = new ArrayList();
      ArrayList arrayList6 = new ArrayList();
      ArrayList arrayList7 = new ArrayList();
      for (int index1 = 0; index1 < rawGeom.Header.NumObjects; ++index1)
      {
        Compiler.VerboseOutput(string.Format("Compiling object {0}: {1}", (object) (index1 + 1), (object) rawGeom.Header.ObjHeaders[index1].ObjName));
        if (rawGeom.Header.ObjHeaders[index1].ObjName.Data.StartsWith("#"))
        {
          string str = rawGeom.Header.ObjHeaders[index1].ObjName.Data.Substring(1).ToUpper();
          if (str.IndexOf("[") > -1)
            str = str.Substring(0, str.IndexOf("["));
          uint num = !str.StartsWith("0x") ? DataCollector.RealHash(str) : uint.Parse(str.Substring(2), NumberStyles.HexNumber);
          Compiler.VerboseOutput(string.Format(" + Mount Point Name: {0}", (object) str));
          Compiler.VerboseOutput(string.Format(" + Compiled Hash: 0x{0:x}", (object) num));
          RealMountPoint realMountPoint = new RealMountPoint()
          {
            Hash = num,
            Transform = new RealMatrix()
          };
          realMountPoint.Transform.m = new float[16];
          float[] transform = rawGeom.Header.ObjHeaders[index1].Transform;
          realMountPoint.Transform.m[0] = 1f;
          realMountPoint.Transform.m[5] = 1f;
          realMountPoint.Transform.m[10] = 1f;
          realMountPoint.Transform.m[15] = 1f;
          realMountPoint.Transform.m[12] = transform[14];
          realMountPoint.Transform.m[13] = transform[12];
          realMountPoint.Transform.m[14] = transform[13];
          arrayList1.Add((object) realMountPoint);
        }
        else if (rawGeom.Header.ObjHeaders[index1].ObjName.Data.StartsWith("KIT00_STYLE00_EXHAUST#"))
        {
          string str = rawGeom.Header.ObjHeaders[index1].ObjName.Data.Substring(1).ToUpper();
          if (str.IndexOf("[") > -1)
            str = str.Substring(0, str.IndexOf("["));
          uint num = !str.StartsWith("0x") ? DataCollector.RealHash(str) : uint.Parse(str.Substring(2), NumberStyles.HexNumber);
          Compiler.VerboseOutput(string.Format(" + Mount Point Name: {0}", (object) str));
          Compiler.VerboseOutput(string.Format(" + Compiled Hash: 0x{0:x}", (object) num));
          RealMountPoint realMountPoint = new RealMountPoint()
          {
            Hash = num,
            Transform = new RealMatrix()
          };
          realMountPoint.Transform.m = new float[16];
          float[] transform = rawGeom.Header.ObjHeaders[index1].Transform;
          realMountPoint.Transform.m[0] = 1f;
          realMountPoint.Transform.m[5] = 1f;
          realMountPoint.Transform.m[10] = 1f;
          realMountPoint.Transform.m[15] = 1f;
          realMountPoint.Transform.m[12] = transform[14];
          realMountPoint.Transform.m[13] = transform[12];
          realMountPoint.Transform.m[14] = transform[13];
          arrayList2.Add((object) realMountPoint);
        }
        else if (rawGeom.Header.ObjHeaders[index1].ObjName.Data.StartsWith("KIT00_STYLE01_EXHAUST#"))
        {
          string str = rawGeom.Header.ObjHeaders[index1].ObjName.Data.Substring(1).ToUpper();
          if (str.IndexOf("[") > -1)
            str = str.Substring(0, str.IndexOf("["));
          uint num = !str.StartsWith("0x") ? DataCollector.RealHash(str) : uint.Parse(str.Substring(2), NumberStyles.HexNumber);
          Compiler.VerboseOutput(string.Format(" + Mount Point Name: {0}", (object) str));
          Compiler.VerboseOutput(string.Format(" + Compiled Hash: 0x{0:x}", (object) num));
          RealMountPoint realMountPoint = new RealMountPoint()
          {
            Hash = num,
            Transform = new RealMatrix()
          };
          realMountPoint.Transform.m = new float[16];
          float[] transform = rawGeom.Header.ObjHeaders[index1].Transform;
          realMountPoint.Transform.m[0] = 1f;
          realMountPoint.Transform.m[5] = 1f;
          realMountPoint.Transform.m[10] = 1f;
          realMountPoint.Transform.m[15] = 1f;
          realMountPoint.Transform.m[12] = transform[14];
          realMountPoint.Transform.m[13] = transform[12];
          realMountPoint.Transform.m[14] = transform[13];
          arrayList3.Add((object) realMountPoint);
        }
        else if (rawGeom.Header.ObjHeaders[index1].ObjName.Data.StartsWith("KIT01_STYLE00_EXHAUST#"))
        {
          string str = rawGeom.Header.ObjHeaders[index1].ObjName.Data.Substring(1).ToUpper();
          if (str.IndexOf("[") > -1)
            str = str.Substring(0, str.IndexOf("["));
          uint num = !str.StartsWith("0x") ? DataCollector.RealHash(str) : uint.Parse(str.Substring(2), NumberStyles.HexNumber);
          Compiler.VerboseOutput(string.Format(" + Mount Point Name: {0}", (object) str));
          Compiler.VerboseOutput(string.Format(" + Compiled Hash: 0x{0:x}", (object) num));
          RealMountPoint realMountPoint = new RealMountPoint()
          {
            Hash = num,
            Transform = new RealMatrix()
          };
          realMountPoint.Transform.m = new float[16];
          float[] transform = rawGeom.Header.ObjHeaders[index1].Transform;
          realMountPoint.Transform.m[0] = 1f;
          realMountPoint.Transform.m[5] = 1f;
          realMountPoint.Transform.m[10] = 1f;
          realMountPoint.Transform.m[15] = 1f;
          realMountPoint.Transform.m[12] = transform[14];
          realMountPoint.Transform.m[13] = transform[12];
          realMountPoint.Transform.m[14] = transform[13];
          arrayList4.Add((object) realMountPoint);
        }
        else if (rawGeom.Header.ObjHeaders[index1].ObjName.Data.StartsWith("KIT01_STYLE01_EXHAUST#"))
        {
          string str = rawGeom.Header.ObjHeaders[index1].ObjName.Data.Substring(1).ToUpper();
          if (str.IndexOf("[") > -1)
            str = str.Substring(0, str.IndexOf("["));
          uint num = !str.StartsWith("0x") ? DataCollector.RealHash(str) : uint.Parse(str.Substring(2), NumberStyles.HexNumber);
          Compiler.VerboseOutput(string.Format(" + Mount Point Name: {0}", (object) str));
          Compiler.VerboseOutput(string.Format(" + Compiled Hash: 0x{0:x}", (object) num));
          RealMountPoint realMountPoint = new RealMountPoint()
          {
            Hash = num,
            Transform = new RealMatrix()
          };
          realMountPoint.Transform.m = new float[16];
          float[] transform = rawGeom.Header.ObjHeaders[index1].Transform;
          realMountPoint.Transform.m[0] = 1f;
          realMountPoint.Transform.m[5] = 1f;
          realMountPoint.Transform.m[10] = 1f;
          realMountPoint.Transform.m[15] = 1f;
          realMountPoint.Transform.m[12] = transform[14];
          realMountPoint.Transform.m[13] = transform[12];
          realMountPoint.Transform.m[14] = transform[13];
          arrayList5.Add((object) realMountPoint);
        }
        else
        {
          Hashtable hashtable2 = new Hashtable();
          UniqueList uniqueList1 = new UniqueList();
          UniqueList uniqueList2 = new UniqueList();
          int num1 = !arrayList6.Contains((object) rawGeom.Header.ObjHeaders[index1].ObjName.ToString()) ? -1 : int.MaxValue;
          for (int index2 = 0; index2 < rawGeom.Header.ObjHeaders[index1].NumFaces; ++index2)
          {
            RawFace face = rawGeom.Objects[index1].Faces[index2];
            DataCollector.MatTexPair key = new DataCollector.MatTexPair();
            DataCollector.MatTexStringPair matTexStringPair = (DataCollector.MatTexStringPair) hashtable1[(object) face.MatIndex];
            key.Material = uniqueList2.Add((object) matTexStringPair.Material);
            key.Texture = uniqueList1.Add((object) matTexStringPair.Texture);
            DataCollector.SubMesh subMesh;
            if (hashtable2.ContainsKey((object) key))
            {
              subMesh = hashtable2[(object) key] as DataCollector.SubMesh;
            }
            else
            {
              subMesh = new DataCollector.SubMesh();
              subMesh.TextureId = key.Texture;
              subMesh.MaterialId = key.Material;
              hashtable2.Add((object) key, (object) subMesh);
            }
            RawVertex vertex1 = rawGeom.Objects[index1].Vertices[(int) face.I3];
            RawVertex vertex2 = rawGeom.Objects[index1].Vertices[(int) face.I2];
            RawVertex vertex3 = rawGeom.Objects[index1].Vertices[(int) face.I1];
            RealVertex realVertex = new RealVertex();
            realVertex.Initialize(true, 0);
            realVertex.Position.x = vertex1.Z;
            realVertex.Position.y = vertex1.X;
            realVertex.Position.z = vertex1.Y;
            realVertex.Normal.x = vertex1.nZ;
            realVertex.Normal.y = vertex1.nX;
            realVertex.Normal.z = vertex1.nY;
            realVertex.Diffuse = num1;
            realVertex.UV.u = face.tU3;
            realVertex.UV.v = face.tV3;
            subMesh.IndexList.Add((object) (ushort) subMesh.VertexList.Add((object) realVertex));
            realVertex = new RealVertex();
            realVertex.Initialize(true, 0);
            realVertex.Position.x = vertex2.Z;
            realVertex.Position.y = vertex2.X;
            realVertex.Position.z = vertex2.Y;
            realVertex.Normal.x = vertex2.nZ;
            realVertex.Normal.y = vertex2.nX;
            realVertex.Normal.z = vertex2.nY;
            realVertex.Diffuse = num1;
            realVertex.UV.u = face.tU2;
            realVertex.UV.v = face.tV2;
            subMesh.IndexList.Add((object) (ushort) subMesh.VertexList.Add((object) realVertex));
            realVertex = new RealVertex();
            realVertex.Initialize(true, 0);
            realVertex.Position.x = vertex3.Z;
            realVertex.Position.y = vertex3.X;
            realVertex.Position.z = vertex3.Y;
            realVertex.Normal.x = vertex3.nZ;
            realVertex.Normal.y = vertex3.nX;
            realVertex.Normal.z = vertex3.nY;
            realVertex.Diffuse = num1;
            realVertex.UV.u = face.tU1;
            realVertex.UV.v = face.tV1;
            subMesh.IndexList.Add((object) (ushort) subMesh.VertexList.Add((object) realVertex));
          }
          Compiler.VerboseOutput(string.Format(" + Compiled into {0} submeshes", (object) hashtable2.Count));
          DataCollector.SubMesh[] subMeshArray = new DataCollector.SubMesh[hashtable2.Count];
          hashtable2.Values.CopyTo((Array) subMeshArray, 0);
          for (int index3 = 0; index3 < hashtable2.Count; ++index3)
          {
            DataCollector.SubMesh subMesh = subMeshArray[index3];
            Compiler.VerboseOutput(string.Format(" + Submesh {0}:", (object) (index3 + 1)));
            Compiler.VerboseOutput(string.Format("    + Material:  {0}", uniqueList2[subMesh.MaterialId]));
            Compiler.VerboseOutput(string.Format("    + Texture:   {0}", uniqueList1[subMesh.TextureId]));
            Compiler.VerboseOutput(string.Format("    + Vertices:  {0}", (object) subMesh.VertexList.Count));
            Compiler.VerboseOutput(string.Format("    + Triangles: {0}", (object) (subMesh.IndexList.Count / 3)));
          }
          Compiler.VerboseOutput(string.Format("Creating part data for binary object file..."));
          RealGeometryPart part = new RealGeometryPart();
          if (rawGeom.Header.ObjHeaders[index1].ObjName.Data.ToUpper().StartsWith("BASE_"))
            arrayList7.Add((object) part);
          if (rawGeom.Header.ObjHeaders[index1].ObjName.Data.ToUpper().StartsWith("KIT00_STYLE00_EXHAUST"))
            arrayList2.Add((object) part);
          if (rawGeom.Header.ObjHeaders[index1].ObjName.Data.ToUpper().StartsWith("KIT00_STYLE01_EXHAUST"))
            arrayList3.Add((object) part);
          if (rawGeom.Header.ObjHeaders[index1].ObjName.Data.ToUpper().StartsWith("KIT01_STYLE00_EXHAUST"))
            arrayList4.Add((object) part);
          if (rawGeom.Header.ObjHeaders[index1].ObjName.Data.ToUpper().StartsWith("KIT01_STYLE01_EXHAUST"))
            arrayList5.Add((object) part);
          string str1 = this.ResolveRealNameForce(rawGeom.Header.ObjHeaders[index1].ObjName.Data);
          part.PartInfo.Hash = DataCollector.RealHash(str1);
          part.PartInfo.PartName = new FixedLenString(str1);
          part.PartInfo.ShaderCount = (byte) uniqueList2.Count;
          part.PartInfo.Shaders = new uint[uniqueList2.Count];
          for (int index4 = 0; index4 < uniqueList2.Count; ++index4)
          {
            string str2 = uniqueList2[index4] as string;
            part.PartInfo.Shaders[index4] = !str2.StartsWith("0x") ? DataCollector.RealHash(str2) : uint.Parse(str2.Substring(2), NumberStyles.HexNumber);
          }
          part.PartInfo.TextureCount = (byte) uniqueList1.Count;
          part.PartInfo.Textures = new uint[uniqueList1.Count];
          for (int index5 = 0; index5 < uniqueList1.Count; ++index5)
          {
            string precompileName = uniqueList1[index5] as string;
            part.PartInfo.Textures[index5] = !precompileName.StartsWith("0x") ? DataCollector.RealHash(this.ResolveRealName(precompileName)) : uint.Parse(precompileName.Substring(2), NumberStyles.HexNumber);
          }
          part.PartInfo.Transform.m = rawGeom.Header.ObjHeaders[index1].Transform;
          part.PartInfo.TriangleCount = rawGeom.Header.ObjHeaders[index1].NumFaces;
          part.PartInfo.Unk1 = 4194328;
          part.PartInfo.Unk2 = 959824;
          part.PartInfo.Unk3 = 959824;
          part.PartInfo.Unk4_MW = 0;
          part.PartInfo.Unk5_MW = 1f;
          part.PartInfo.Unk6_MW = (float) part.PartInfo.TriangleCount;
          part.PartData.Flags = 16512;
          part.PartData.GroupCount = subMeshArray.Length;
          part.PartData.Groups = new RealShadingGroup[subMeshArray.Length];
          int length1 = 0;
          RealVector4 realVector4_1;
          for (int index6 = 0; index6 < subMeshArray.Length; ++index6)
          {
            DataCollector.SubMesh subMesh = subMeshArray[index6];
            part.PartData.Groups[index6] = new RealShadingGroup();
            realVector4_1 = new RealVector4();
            RealVector4 realVector4_2 = new RealVector4();
            if (subMesh.VertexList.Count > 0)
            {
              RealVector3 position = ((RealVertex) subMesh.VertexList[0]).Position;
              realVector4_1.x = position.x;
              realVector4_1.y = position.y;
              realVector4_1.z = position.z;
              realVector4_2 = realVector4_1;
            }
            for (int index7 = 1; index7 < subMesh.VertexList.Count; ++index7)
            {
              RealVector3 position = ((RealVertex) subMesh.VertexList[index7]).Position;
              if ((double) position.x > (double) realVector4_2.x)
                realVector4_2.x = position.x;
              if ((double) position.y > (double) realVector4_2.y)
                realVector4_2.y = position.y;
              if ((double) position.z > (double) realVector4_2.z)
                realVector4_2.z = position.z;
              if ((double) position.x < (double) realVector4_1.x)
                realVector4_1.x = position.x;
              if ((double) position.y < (double) realVector4_1.y)
                realVector4_1.y = position.y;
              if ((double) position.z < (double) realVector4_1.z)
                realVector4_1.z = position.z;
            }
            part.PartData.Groups[index6].BoundsMax = new RealVector3(realVector4_2.x, realVector4_2.y, realVector4_2.z);
            part.PartData.Groups[index6].BoundsMin = new RealVector3(realVector4_1.x, realVector4_1.y, realVector4_1.z);
            part.PartData.Groups[index6].Length = subMesh.IndexList.Count;
            part.PartData.Groups[index6].TextureIndex0 = (byte) subMesh.TextureId;
            part.PartData.Groups[index6].TextureIndex1 = (byte) subMesh.TextureId;
            part.PartData.Groups[index6].TextureIndex2 = (byte) subMesh.TextureId;
            part.PartData.Groups[index6].TextureIndex3 = (byte) subMesh.TextureId;
            part.PartData.Groups[index6].TextureIndex4 = (byte) subMesh.TextureId;
            part.PartData.Groups[index6].ShaderIndex0 = (byte) subMesh.MaterialId;
            part.PartData.Groups[index6].Unk1 = 4;
            part.PartData.Groups[index6].Flags = part.PartData.Flags;
            part.PartData.Groups[index6].VertexCount = subMesh.VertexList.Count;
            part.PartData.Groups[index6].TriangleCount = subMesh.IndexList.Count / 3;
            part.PartData.Groups[index6].Offset = length1;
            length1 += subMesh.IndexList.Count;
          }
          realVector4_1 = new RealVector4();
          RealVector4 realVector4_3 = new RealVector4();
          if (part.PartData.Groups.Length != 0)
          {
            RealVector3 boundsMin = part.PartData.Groups[0].BoundsMin;
            realVector4_1.x = boundsMin.x;
            realVector4_1.y = boundsMin.y;
            realVector4_1.z = boundsMin.z;
            RealVector3 boundsMax = part.PartData.Groups[0].BoundsMax;
            realVector4_3.x = boundsMax.x;
            realVector4_3.y = boundsMax.y;
            realVector4_3.z = boundsMax.z;
          }
          for (int index8 = 1; index8 < part.PartData.Groups.Length; ++index8)
          {
            RealVector3 boundsMax = part.PartData.Groups[index8].BoundsMax;
            if ((double) boundsMax.x > (double) realVector4_3.x)
              realVector4_3.x = boundsMax.x;
            if ((double) boundsMax.y > (double) realVector4_3.y)
              realVector4_3.y = boundsMax.y;
            if ((double) boundsMax.z > (double) realVector4_3.z)
              realVector4_3.z = boundsMax.z;
            RealVector3 boundsMin = part.PartData.Groups[index8].BoundsMin;
            if ((double) boundsMin.x < (double) realVector4_1.x)
              realVector4_1.x = boundsMin.x;
            if ((double) boundsMin.y < (double) realVector4_1.y)
              realVector4_1.y = boundsMin.y;
            if ((double) boundsMin.z < (double) realVector4_1.z)
              realVector4_1.z = boundsMin.z;
          }
          part.PartInfo.BoundMax = realVector4_3;
          part.PartInfo.BoundMin = realVector4_1;
          part.PartData.IndexCount = length1;
          part.PartData.Indices = new ushort[length1];
          int length2 = 0;
          int num2 = 0;
          for (int index9 = 0; index9 < subMeshArray.Length; ++index9)
          {
            for (int index10 = 0; index10 < subMeshArray[index9].IndexList.Count; ++index10)
              part.PartData.Indices[num2++] = (ushort) ((uint) (ushort) subMeshArray[index9].IndexList[index10] + (uint) length2);
            length2 += subMeshArray[index9].VertexList.Count;
          }
          part.PartData.TriangleCount = num2 / 3;
          part.PartData.Unk1 = 18;
          part.PartData.VBCount = 1;
          part.PartData.Vertices = new RealVertex[length2];
          int num3 = 0;
          for (int index11 = 0; index11 < subMeshArray.Length; ++index11)
          {
            for (int index12 = 0; index12 < subMeshArray[index11].VertexList.Count; ++index12)
              part.PartData.Vertices[num3++] = (RealVertex) subMeshArray[index11].VertexList[index12];
          }
          rgf.AddPart(part);
        }
        Compiler.VerboseOutput(" + Complete.");
      }
      if (arrayList1.Count > 0)
      {
        Compiler.VerboseOutput("Merging mount points into base parts...");
        if (arrayList7.Count == 0)
        {
          Compiler.WarningOutput("Mount points provided without any base parts! Ignoring mount points.");
        }
        else
        {
          RealMountPoint[] realMountPointArray = new RealMountPoint[arrayList1.Count];
          arrayList1.CopyTo((Array) realMountPointArray);
          foreach (RealGeometryPart realGeometryPart in arrayList7)
            realGeometryPart.PartInfo.MountPoints = realMountPointArray;
        }
        Compiler.VerboseOutput(" + Complete.");
      }
      if (arrayList2.Count > 0)
      {
        Compiler.VerboseOutput("Merging mount points into base parts...");
        if (arrayList7.Count == 0)
        {
          Compiler.WarningOutput("Mount points provided without any base parts! Ignoring mount points.");
        }
        else
        {
          RealMountPoint[] realMountPointArray = new RealMountPoint[arrayList2.Count];
          arrayList2.CopyTo((Array) realMountPointArray);
          foreach (RealGeometryPart realGeometryPart in arrayList2)
            realGeometryPart.PartInfo.MountPoints = realMountPointArray;
        }
        Compiler.VerboseOutput(" + Complete.");
      }
      if (arrayList3.Count > 0)
      {
        Compiler.VerboseOutput("Merging mount points into base parts...");
        if (arrayList7.Count == 0)
        {
          Compiler.WarningOutput("Mount points provided without any base parts! Ignoring mount points.");
        }
        else
        {
          RealMountPoint[] realMountPointArray = new RealMountPoint[arrayList3.Count];
          arrayList3.CopyTo((Array) realMountPointArray);
          foreach (RealGeometryPart realGeometryPart in arrayList3)
            realGeometryPart.PartInfo.MountPoints = realMountPointArray;
        }
        Compiler.VerboseOutput(" + Complete.");
      }
      if (arrayList4.Count > 0)
      {
        Compiler.VerboseOutput("Merging mount points into base parts...");
        if (arrayList7.Count == 0)
        {
          Compiler.WarningOutput("Mount points provided without any base parts! Ignoring mount points.");
        }
        else
        {
          RealMountPoint[] realMountPointArray = new RealMountPoint[arrayList4.Count];
          arrayList4.CopyTo((Array) realMountPointArray);
          foreach (RealGeometryPart realGeometryPart in arrayList4)
            realGeometryPart.PartInfo.MountPoints = realMountPointArray;
        }
        Compiler.VerboseOutput(" + Complete.");
      }
      if (arrayList5.Count > 0)
      {
        Compiler.VerboseOutput("Merging mount points into base parts...");
        if (arrayList7.Count == 0)
        {
          Compiler.WarningOutput("Mount points provided without any base parts! Ignoring mount points.");
        }
        else
        {
          RealMountPoint[] realMountPointArray = new RealMountPoint[arrayList5.Count];
          arrayList5.CopyTo((Array) realMountPointArray);
          foreach (RealGeometryPart realGeometryPart in arrayList5)
            realGeometryPart.PartInfo.MountPoints = realMountPointArray;
        }
        Compiler.VerboseOutput(" + Complete.");
      }
      Compiler.VerboseOutput("Collecting part cross links...");
      this.LoadCrossLinks(rgf);
      rgf.GeometryInfo.PartCount = rgf.PartCount;
      rgf.GeometryInfo.Unk1 = 29;
      rgf.GeometryInfo.Unk2 = 128;
      rgf.GeometryInfo.ClassType = new FixedLenString("DEFAULT", 32);
      rgf.GeometryInfo.RelFilePath = new FixedLenString("GEOMETRY.BIN", 56);
      Compiler.VerboseOutput("Data successfully collected.");
      return rgf;
    }

    private DataCollector.MatTexStringPair GetMatTexPair(string mat)
    {
      DataCollector.MatTexStringPair matTexPair = new DataCollector.MatTexStringPair();
      if (this.materialHT.ContainsKey((object) mat))
        mat = (string) this.materialHT[(object) mat];
      string[] strArray = mat.Split('/');
      if (strArray.Length < 2)
      {
        Compiler.WarningOutput(string.Format("Material {0} has invalid name.", (object) mat));
        matTexPair.Material = "DEFAULT";
        matTexPair.Texture = "DEFAULT";
      }
      else
      {
        matTexPair.Material = strArray[0];
        matTexPair.Texture = strArray[1];
      }
      return matTexPair;
    }

    public RealGeometryFile Collect(AseFile aseFile)
    {
      RealGeometryFile rgf = new RealGeometryFile();
      Hashtable hashtable1 = new Hashtable();
      this.LoadMaterialHT();
      for (int index1 = 0; index1 < aseFile.MaterialList.Count; ++index1)
      {
        hashtable1.Add((object) index1.ToString(), (object) this.GetMatTexPair(aseFile.MaterialList[index1].Name));
        if (aseFile.MaterialList[index1].HasSubMaterials)
        {
          for (int index2 = 0; index2 < aseFile.MaterialList[index1].SubMaterialCount; ++index2)
            hashtable1.Add((object) string.Format("{0}/{1}", (object) index1, (object) index2), (object) this.GetMatTexPair(aseFile.MaterialList[index1][index2].Name));
        }
      }
      ArrayList arrayList1 = new ArrayList();
      ArrayList arrayList2 = new ArrayList();
      ArrayList arrayList3 = new ArrayList();
      for (int index3 = 0; index3 < aseFile.ObjectCount; ++index3)
      {
        Compiler.VerboseOutput(string.Format("Compiling object {0}: {1}", (object) (index3 + 1), (object) aseFile[index3].Name));
        if (aseFile[index3].Name.StartsWith("#"))
        {
          string str = aseFile[index3].Name.Substring(1).ToUpper();
          if (str.IndexOf("[") > -1)
            str = str.Substring(0, str.IndexOf("["));
          uint num = !str.StartsWith("0x") ? DataCollector.RealHash(str) : uint.Parse(str.Substring(2), NumberStyles.HexNumber);
          Compiler.VerboseOutput(string.Format(" + Mount Point Name: {0}", (object) str));
          Compiler.VerboseOutput(string.Format(" + Compiled Hash: 0x{0:x}", (object) num));
          RealMountPoint realMountPoint = new RealMountPoint()
          {
            Hash = num,
            Transform = new RealMatrix()
          };
          realMountPoint.Transform.m = new float[16];
          float[] matrix = aseFile[index3].Transform.Matrix;
          realMountPoint.Transform.m[0] = 1f;
          realMountPoint.Transform.m[5] = 1f;
          realMountPoint.Transform.m[10] = 1f;
          realMountPoint.Transform.m[15] = 1f;
          realMountPoint.Transform.m[12] = -matrix[13];
          realMountPoint.Transform.m[13] = matrix[12];
          realMountPoint.Transform.m[14] = matrix[14];
          arrayList1.Add((object) realMountPoint);
        }
        else
        {
          Hashtable hashtable2 = new Hashtable();
          UniqueList uniqueList1 = new UniqueList();
          UniqueList uniqueList2 = new UniqueList();
          int num1 = !arrayList2.Contains((object) aseFile[index3].Name) ? -1 : int.MaxValue;
          for (int index4 = 0; index4 < aseFile[index3].Mesh.FaceList.Count; ++index4)
          {
            AseFace face = aseFile[index3].Mesh.FaceList[index4];
            string key1 = aseFile[index3].MaterialReference.ToString();
            if (aseFile.MaterialList[aseFile[index3].MaterialReference].HasSubMaterials)
              key1 += string.Format("/{0}", (object) face.MaterialID);
            DataCollector.MatTexPair key2 = new DataCollector.MatTexPair();
            DataCollector.MatTexStringPair matTexStringPair = (DataCollector.MatTexStringPair) hashtable1[(object) key1];
            key2.Material = uniqueList2.Add((object) matTexStringPair.Material);
            key2.Texture = uniqueList1.Add((object) matTexStringPair.Texture);
            DataCollector.SubMesh subMesh;
            if (hashtable2.ContainsKey((object) key2))
            {
              subMesh = hashtable2[(object) key2] as DataCollector.SubMesh;
            }
            else
            {
              subMesh = new DataCollector.SubMesh();
              subMesh.TextureId = key2.Texture;
              subMesh.MaterialId = key2.Material;
              hashtable2.Add((object) key2, (object) subMesh);
            }
            AseVertex vertex1 = aseFile[index3].Mesh.VertexList[face.A];
            AseVertex vertex2 = aseFile[index3].Mesh.VertexList[face.B];
            AseVertex vertex3 = aseFile[index3].Mesh.VertexList[face.C];
            AseVertex normalA = face.NormalA;
            AseVertex normalB = face.NormalB;
            AseVertex normalC = face.NormalC;
            AseVertex textureVertex1 = aseFile[index3].Mesh.TextureVertexList[face.TextureA];
            AseVertex textureVertex2 = aseFile[index3].Mesh.TextureVertexList[face.TextureB];
            AseVertex textureVertex3 = aseFile[index3].Mesh.TextureVertexList[face.TextureC];
            RealVertex realVertex = new RealVertex();
            realVertex.Initialize(true, 0);
            realVertex.Position.x = -vertex1.Y;
            realVertex.Position.y = vertex1.X;
            realVertex.Position.z = vertex1.Z;
            realVertex.Normal.x = -normalA.Y;
            realVertex.Normal.y = normalA.X;
            realVertex.Normal.z = normalA.Z;
            realVertex.Diffuse = num1;
            realVertex.UV.u = textureVertex1.U;
            realVertex.UV.v = textureVertex1.V;
            subMesh.IndexList.Add((object) (ushort) subMesh.VertexList.Add((object) realVertex));
            realVertex = new RealVertex();
            realVertex.Initialize(true, 0);
            realVertex.Position.x = -vertex2.Y;
            realVertex.Position.y = vertex2.X;
            realVertex.Position.z = vertex2.Z;
            realVertex.Normal.x = -normalB.Y;
            realVertex.Normal.y = normalB.X;
            realVertex.Normal.z = normalB.Z;
            realVertex.Diffuse = num1;
            realVertex.UV.u = textureVertex2.U;
            realVertex.UV.v = textureVertex2.V;
            subMesh.IndexList.Add((object) (ushort) subMesh.VertexList.Add((object) realVertex));
            realVertex = new RealVertex();
            realVertex.Initialize(true, 0);
            realVertex.Position.x = -vertex3.Y;
            realVertex.Position.y = vertex3.X;
            realVertex.Position.z = vertex3.Z;
            realVertex.Normal.x = -normalC.Y;
            realVertex.Normal.y = normalC.X;
            realVertex.Normal.z = normalC.Z;
            realVertex.Diffuse = num1;
            realVertex.UV.u = textureVertex3.U;
            realVertex.UV.v = textureVertex3.V;
            subMesh.IndexList.Add((object) (ushort) subMesh.VertexList.Add((object) realVertex));
          }
          Compiler.VerboseOutput(string.Format(" + Compiled into {0} submeshes", (object) hashtable2.Count));
          DataCollector.SubMesh[] subMeshArray = new DataCollector.SubMesh[hashtable2.Count];
          hashtable2.Values.CopyTo((Array) subMeshArray, 0);
          for (int index5 = 0; index5 < hashtable2.Count; ++index5)
          {
            DataCollector.SubMesh subMesh = subMeshArray[index5];
            Compiler.VerboseOutput(string.Format(" + Submesh {0}:", (object) (index5 + 1)));
            Compiler.VerboseOutput(string.Format("    + Material:  {0}", uniqueList2[subMesh.MaterialId]));
            Compiler.VerboseOutput(string.Format("    + Texture:   {0}", uniqueList1[subMesh.TextureId]));
            Compiler.VerboseOutput(string.Format("    + Vertices:  {0}", (object) subMesh.VertexList.Count));
            Compiler.VerboseOutput(string.Format("    + Triangles: {0}", (object) (subMesh.IndexList.Count / 3)));
          }
          Compiler.VerboseOutput(string.Format("Creating part data for binary object file..."));
          RealGeometryPart part = new RealGeometryPart();
          if (aseFile[index3].Name.ToUpper().StartsWith("BASE_"))
            arrayList3.Add((object) part);
          string str1 = this.ResolveRealNameForce(aseFile[index3].Name);
          part.PartInfo.Hash = DataCollector.RealHash(str1);
          part.PartInfo.PartName = new FixedLenString(str1);
          part.PartInfo.ShaderCount = (byte) uniqueList2.Count;
          part.PartInfo.Shaders = new uint[uniqueList2.Count];
          for (int index6 = 0; index6 < uniqueList2.Count; ++index6)
          {
            string str2 = uniqueList2[index6] as string;
            part.PartInfo.Shaders[index6] = !str2.StartsWith("0x") ? DataCollector.RealHash(str2) : uint.Parse(str2.Substring(2), NumberStyles.HexNumber);
          }
          part.PartInfo.TextureCount = (byte) uniqueList1.Count;
          part.PartInfo.Textures = new uint[uniqueList1.Count];
          for (int index7 = 0; index7 < uniqueList1.Count; ++index7)
          {
            string precompileName = uniqueList1[index7] as string;
            part.PartInfo.Textures[index7] = !precompileName.StartsWith("0x") ? DataCollector.RealHash(this.ResolveRealName(precompileName)) : uint.Parse(precompileName.Substring(2), NumberStyles.HexNumber);
          }
          part.PartInfo.Transform.m = aseFile[index3].Transform.Matrix;
          part.PartInfo.TriangleCount = aseFile[index3].Mesh.FaceList.Count;
          part.PartInfo.Unk1 = 4194328;
          part.PartInfo.Unk2 = 959824;
          part.PartInfo.Unk3 = 959824;
          part.PartInfo.Unk4_MW = 0;
          part.PartInfo.Unk5_MW = 1f;
          part.PartInfo.Unk6_MW = (float) part.PartInfo.TriangleCount;
          part.PartData.Flags = 16512;
          part.PartData.GroupCount = subMeshArray.Length;
          part.PartData.Groups = new RealShadingGroup[subMeshArray.Length];
          int length1 = 0;
          RealVector4 realVector4_1;
          RealVector4 realVector4_2;
          for (int index8 = 0; index8 < subMeshArray.Length; ++index8)
          {
            DataCollector.SubMesh subMesh = subMeshArray[index8];
            part.PartData.Groups[index8] = new RealShadingGroup();
            realVector4_1 = new RealVector4();
            realVector4_2 = new RealVector4();
            if (subMesh.VertexList.Count > 0)
            {
              RealVector3 position = ((RealVertex) subMesh.VertexList[0]).Position;
              realVector4_1.x = position.x;
              realVector4_1.y = position.y;
              realVector4_1.z = position.z;
              realVector4_2 = realVector4_1;
            }
            for (int index9 = 1; index9 < subMesh.VertexList.Count; ++index9)
            {
              RealVector3 position = ((RealVertex) subMesh.VertexList[index9]).Position;
              if ((double) position.x > (double) realVector4_2.x)
                realVector4_2.x = position.x;
              if ((double) position.y > (double) realVector4_2.y)
                realVector4_2.y = position.y;
              if ((double) position.z > (double) realVector4_2.z)
                realVector4_2.z = position.z;
              if ((double) position.x < (double) realVector4_1.x)
                realVector4_1.x = position.x;
              if ((double) position.y < (double) realVector4_1.y)
                realVector4_1.y = position.y;
              if ((double) position.z < (double) realVector4_1.z)
                realVector4_1.z = position.z;
            }
            part.PartData.Groups[index8].BoundsMax = new RealVector3(realVector4_2.x, realVector4_2.y, realVector4_2.z);
            part.PartData.Groups[index8].BoundsMin = new RealVector3(realVector4_1.x, realVector4_1.y, realVector4_1.z);
            part.PartData.Groups[index8].Length = subMesh.IndexList.Count;
            part.PartData.Groups[index8].TextureIndex0 = (byte) subMesh.TextureId;
            part.PartData.Groups[index8].TextureIndex1 = (byte) subMesh.TextureId;
            part.PartData.Groups[index8].TextureIndex2 = (byte) subMesh.TextureId;
            part.PartData.Groups[index8].TextureIndex3 = (byte) subMesh.TextureId;
            part.PartData.Groups[index8].TextureIndex4 = (byte) subMesh.TextureId;
            part.PartData.Groups[index8].ShaderIndex0 = (byte) subMesh.MaterialId;
            part.PartData.Groups[index8].Unk1 = 4;
            part.PartData.Groups[index8].Flags = part.PartData.Flags;
            part.PartData.Groups[index8].VertexCount = subMesh.VertexList.Count;
            part.PartData.Groups[index8].TriangleCount = subMesh.IndexList.Count / 3;
            part.PartData.Groups[index8].Offset = length1;
            length1 += subMesh.IndexList.Count;
          }
          realVector4_1 = new RealVector4();
          realVector4_2 = new RealVector4();
          if (part.PartData.Groups.Length != 0)
          {
            RealVector3 boundsMin = part.PartData.Groups[0].BoundsMin;
            realVector4_1.x = boundsMin.x;
            realVector4_1.y = boundsMin.y;
            realVector4_1.z = boundsMin.z;
            RealVector3 boundsMax = part.PartData.Groups[0].BoundsMax;
            realVector4_2.x = boundsMax.x;
            realVector4_2.y = boundsMax.y;
            realVector4_2.z = boundsMax.z;
          }
          for (int index10 = 1; index10 < part.PartData.Groups.Length; ++index10)
          {
            RealVector3 boundsMax = part.PartData.Groups[index10].BoundsMax;
            if ((double) boundsMax.x > (double) realVector4_2.x)
              realVector4_2.x = boundsMax.x;
            if ((double) boundsMax.y > (double) realVector4_2.y)
              realVector4_2.y = boundsMax.y;
            if ((double) boundsMax.z > (double) realVector4_2.z)
              realVector4_2.z = boundsMax.z;
            RealVector3 boundsMin = part.PartData.Groups[index10].BoundsMin;
            if ((double) boundsMin.x < (double) realVector4_1.x)
              realVector4_1.x = boundsMin.x;
            if ((double) boundsMin.y < (double) realVector4_1.y)
              realVector4_1.y = boundsMin.y;
            if ((double) boundsMin.z < (double) realVector4_1.z)
              realVector4_1.z = boundsMin.z;
          }
          part.PartInfo.BoundMax = realVector4_2;
          part.PartInfo.BoundMin = realVector4_1;
          part.PartData.IndexCount = length1;
          part.PartData.Indices = new ushort[length1];
          int length2 = 0;
          int num2 = 0;
          for (int index11 = 0; index11 < subMeshArray.Length; ++index11)
          {
            for (int index12 = 0; index12 < subMeshArray[index11].IndexList.Count; ++index12)
              part.PartData.Indices[num2++] = (ushort) ((uint) (ushort) subMeshArray[index11].IndexList[index12] + (uint) length2);
            length2 += subMeshArray[index11].VertexList.Count;
          }
          part.PartData.TriangleCount = num2 / 3;
          part.PartData.Unk1 = 18;
          part.PartData.VBCount = 1;
          part.PartData.Vertices = new RealVertex[length2];
          int num3 = 0;
          for (int index13 = 0; index13 < subMeshArray.Length; ++index13)
          {
            for (int index14 = 0; index14 < subMeshArray[index13].VertexList.Count; ++index14)
              part.PartData.Vertices[num3++] = (RealVertex) subMeshArray[index13].VertexList[index14];
          }
          rgf.AddPart(part);
        }
        Compiler.VerboseOutput(" + Complete.");
      }
      if (arrayList1.Count > 0)
      {
        Compiler.VerboseOutput("Merging mount points into base parts...");
        if (arrayList3.Count == 0)
        {
          Compiler.WarningOutput("Mount points provided without any base parts! Ignoring mount points.");
        }
        else
        {
          RealMountPoint[] realMountPointArray = new RealMountPoint[arrayList1.Count];
          arrayList1.CopyTo((Array) realMountPointArray);
          foreach (RealGeometryPart realGeometryPart in arrayList3)
            realGeometryPart.PartInfo.MountPoints = realMountPointArray;
        }
        Compiler.VerboseOutput(" + Complete.");
      }
      Compiler.VerboseOutput("Collecting part cross links...");
      this.LoadCrossLinks(rgf);
      rgf.GeometryInfo.PartCount = rgf.PartCount;
      rgf.GeometryInfo.Unk1 = 29;
      rgf.GeometryInfo.Unk2 = 128;
      rgf.GeometryInfo.ClassType = new FixedLenString("DEFAULT", 32);
      rgf.GeometryInfo.RelFilePath = new FixedLenString("GEOMETRY.BIN", 56);
      Compiler.VerboseOutput("Data successfully collected.");
      return rgf;
    }

    private struct MatTexStringPair
    {
      public string Material;
      public string Texture;
    }

    private struct MatTexPair
    {
      public int Material;
      public int Texture;

      public override int GetHashCode()
      {
        int hashCode = this.Material.GetHashCode();
        string str1 = hashCode.ToString();
        hashCode = this.Texture.GetHashCode();
        string str2 = hashCode.ToString();
        return (str1 + ":" + str2).GetHashCode();
      }
    }

    private class SubMesh
    {
      public int MaterialId;
      public int TextureId;
      public UniqueList VertexList;
      public ArrayList IndexList;

      public SubMesh()
      {
        this.VertexList = new UniqueList();
        this.IndexList = new ArrayList();
      }
    }
  }
}
