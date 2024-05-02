// Decompiled with JetBrains decompiler
// Type: mwgc.RealEngine.RealGeometryPartData
// Assembly: mwgc, Version=1.2.8468.42059, Culture=neutral, PublicKeyToken=null
// MVID: 82A161B2-3E36-490A-94BF-0C902BEC9901
// Assembly location: D:\Users\nikzh\Downloads\mwgc_vertices.exe

using System.Collections;
using System.IO;

#nullable disable
namespace mwgc.RealEngine
{
  public struct RealGeometryPartData
  {
    public int Null1;
    public int Null2;
    public int Unk1;
    public int Flags;
    public int GroupCount;
    public int Null2_MW;
    public int VBCount;
    public int Null3;
    public int Null4;
    public int Null5;
    public int Null6;
    private int _indexCount;
    public RealVertex[] Vertices;
    public ushort[] Indices;
    public RealShadingGroup[] Groups;
    public ArrayList Materials;

    public int IndexCount
    {
      get => this._indexCount;
      set => this._indexCount = value;
    }

    public bool HasNormals => true;

    public int VertexCount
    {
      get
      {
        int vertexCount = 0;
        foreach (RealShadingGroup group in this.Groups)
          vertexCount += group.VertexCount;
        return vertexCount;
      }
    }

    public int TriangleCount
    {
      get => this.IndexCount / 3;
      set => this.IndexCount = value * 3;
    }

    public void Read(BinaryReader reader)
    {
      this.Null1 = reader.ReadInt32();
      this.Null2 = reader.ReadInt32();
      this.Unk1 = reader.ReadInt32();
      this.Flags = reader.ReadInt32();
      this.GroupCount = reader.ReadInt32();
      this.Null2_MW = reader.ReadInt32();
      this.VBCount = reader.ReadInt32();
      this.Null3 = reader.ReadInt32();
      this.Null4 = reader.ReadInt32();
      this.Null5 = reader.ReadInt32();
      this.Null6 = reader.ReadInt32();
      this.IndexCount = reader.ReadInt32();
    }

    public void Write(BinaryWriter writer)
    {
      writer.Write(this.Null1);
      writer.Write(this.Null2);
      writer.Write(this.Unk1);
      writer.Write(this.Flags);
      writer.Write(this.GroupCount);
      writer.Write(this.Null2_MW);
      writer.Write(this.VBCount);
      writer.Write(this.Null3);
      writer.Write(this.Null4);
      writer.Write(this.Null5);
      writer.Write(this.Null6);
      writer.Write(this.IndexCount);
    }

    public void ReadVertices(BinaryReader reader)
    {
      bool withNormal = this.HasNormals;
      if (!withNormal)
        withNormal = true;
      this.Vertices = new RealVertex[this.VertexCount];
      for (int index = 0; index < this.VertexCount; ++index)
        this.Vertices[index].Read(reader, withNormal);
    }

    public void WriteVertices(BinaryWriter writer)
    {
      for (int index = 0; index < this.VertexCount; ++index)
        this.Vertices[index].Write(writer);
    }

    public void ReadIndices(BinaryReader reader)
    {
      this.Indices = new ushort[this.TriangleCount * 3];
      for (int index = 0; index < this.TriangleCount * 3; ++index)
        this.Indices[index] = reader.ReadUInt16();
    }

    public void WriteIndices(BinaryWriter writer)
    {
      for (int index = 0; index < this.TriangleCount * 3; ++index)
        writer.Write(this.Indices[index]);
    }

    public void ReadGroups(BinaryReader reader)
    {
      this.Groups = new RealShadingGroup[this.GroupCount];
      for (int index = 0; index < this.GroupCount; ++index)
        this.Groups[index].Read(reader);
    }

    public void WriteGroups(BinaryWriter writer)
    {
      for (int index = 0; index < this.GroupCount; ++index)
        this.Groups[index].Write(writer);
    }

    public void ReadMaterialName(BinaryReader reader)
    {
      if (this.Materials == null)
        this.Materials = new ArrayList();
      this.Materials.Add((object) new FixedLenString(reader));
    }
  }
}
