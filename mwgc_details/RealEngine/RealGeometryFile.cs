// Decompiled with JetBrains decompiler
// Type: mwgc.RealEngine.RealGeometryFile
// Assembly: mwgc, Version=1.2.7318.38444, Culture=neutral, PublicKeyToken=null
// MVID: 7B2CC934-3582-440B-A9FF-077D52B1156A
// Assembly location: D:\Users\nikzh\Downloads\mwgc_details.exe

using System.Collections;
using System.IO;

#nullable disable
namespace mwgc.RealEngine
{
  public class RealGeometryFile : RealFile, IEnumerable
  {
    private RealGeometryInfo _desc;
    private ArrayList _parts;
    private int _partIndex;

    public RealGeometryFile()
    {
      this._parts = (ArrayList) null;
      this._desc = new RealGeometryInfo();
    }

    public RealGeometryInfo GeometryInfo
    {
      get => this._desc;
      set => this._desc = value;
    }

    public RealGeometryPart this[int index]
    {
      get => this._parts[index] as RealGeometryPart;
      set => this._parts[index] = (object) value;
    }

    public RealGeometryPart this[uint hash]
    {
      get
      {
        foreach (RealGeometryPart part in this._parts)
        {
          if ((int) part.PartInfo.Hash == (int) hash)
            return part;
        }
        return (RealGeometryPart) null;
      }
    }

    public int PartCount => this._parts.Count;

    public void AddPart(RealGeometryPart part)
    {
      if (this._parts == null)
        this._parts = new ArrayList();
      this._partIndex = this._parts.Count;
      this._parts.Add((object) part);
    }

    public int FindPartIndex(uint hash)
    {
      for (int index = 0; index < this._parts.Count; ++index)
      {
        if ((int) (this._parts[index] as RealGeometryPart).PartInfo.Hash == (int) hash)
          return index;
      }
      return -1;
    }

    private void ProcessParentChunk(RealChunk parentChunk)
    {
      while (this._stream.Position < (long) parentChunk.EndOffset)
      {
        RealChunk parentChunk1 = this.NextChunk();
        if (parentChunk1.IsParent)
        {
          switch (parentChunk1.Type)
          {
            case RealType.Geometry:
              this.ProcessParentChunk(parentChunk1);
              continue;
            case RealType.GeometryParts:
              this._parts = new ArrayList();
              this.ProcessParentChunk(parentChunk1);
              continue;
            case RealType.GeometryPartsEmpty:
              this.ProcessParentChunk(parentChunk1);
              continue;
            case RealType.GeometryPart:
              this._partIndex = this._parts.Count;
              this._parts.Add((object) new RealGeometryPart());
              this.ProcessParentChunk(parentChunk1);
              continue;
            case RealType.GeometryPartData:
              this.ProcessParentChunk(parentChunk1);
              continue;
            default:
              continue;
          }
        }
        else
          this.ProcessChildChunk(parentChunk1);
      }
    }

    private void ProcessChildChunk(RealChunk parentChunk)
    {
      switch (parentChunk.Type)
      {
        case RealType.GeometryPartsDesc:
          this._desc.Read(this._br);
          this._parts = new ArrayList();
          break;
        case RealType.GeometryPartDesc:
          this.NextAlignment(16);
          this[this._partIndex].PartInfo.Read(this._br);
          break;
        case RealType.GeometryPartTextures:
          this[this._partIndex].PartInfo.ReadTextures(this._br);
          break;
        case RealType.GeometryPartShaders:
          this[this._partIndex].PartInfo.ReadShaders(this._br);
          break;
        case RealType.GeometryPartMountPoints:
          this.NextAlignment(16);
          this[this._partIndex].PartInfo.ReadMountPoints(this._br, parentChunk);
          break;
        case RealType.GeometryPartDataDesc:
          this.NextAlignment(16);
          this[this._partIndex].PartData.Read(this._br);
          break;
        case RealType.GeometryPartDataVertices:
          this.NextAlignment(128);
          this[this._partIndex].PartData.ReadVertices(this._br);
          break;
        case RealType.GeometryPartDataGroups:
          this.NextAlignment(16);
          this[this._partIndex].PartData.ReadGroups(this._br);
          break;
        case RealType.GeometryPartDataIndices:
          this.NextAlignment(16);
          this[this._partIndex].PartData.ReadIndices(this._br);
          break;
        case RealType.GeometryPartDataMaterialName:
          this[this._partIndex].PartData.ReadMaterialName(this._br);
          break;
      }
      this.SkipChunk(parentChunk);
    }

    protected override void ProcessOpen()
    {
      while (this._stream.Position < this._stream.Length)
      {
        RealChunk parentChunk = this.NextChunk();
        if (parentChunk.IsParent)
          this.ProcessParentChunk(parentChunk);
        else
          this.ProcessChildChunk(parentChunk);
      }
    }

    private void PaddingAlignment(int padding)
    {
      if (this._stream.Position % (long) padding == 0L)
        return;
      this.BeginChunk(RealType.Null);
      if (this._stream.Position % (long) padding != 0L)
        this._stream.Seek((long) (int) ((long) padding - this._stream.Position % (long) padding), SeekOrigin.Current);
      this.EndChunk();
    }

    protected override void ProcessSave()
    {
      RealChunk[] realChunkArray = new RealChunk[this._parts.Count];
      ArrayList arrayList = new ArrayList();
      this.BeginChunk(RealType.Geometry);
      this.PaddingAlignment(16);
      this.BeginChunk(RealType.GeometryParts);
      this.BeginChunk(RealType.GeometryPartsDesc);
      this._desc.Write(this._bw);
      this.EndChunk();
      this.BeginChunk(RealType.GeometryPartsHash);
      for (int index = 0; index < this._parts.Count; ++index)
      {
        RealGeometryPart part = this._parts[index] as RealGeometryPart;
        arrayList.Add((object) part.PartInfo.Hash);
      }
      arrayList.Sort();
      for (int index = 0; index < arrayList.Count; ++index)
      {
        this._bw.Write((uint) arrayList[index]);
        this._bw.Write(0U);
      }
      this.EndChunk();
      RealChunk realChunk = this.BeginChunk(RealType.Null);
      this._stream.Seek((long) (this._parts.Count * 4 * 6), SeekOrigin.Current);
      this.EndChunk();
      this.BeginChunk(RealType.GeometryPartsEmpty);
      this.EndChunk();
      this.EndChunk();
      for (int index = 0; index < this._parts.Count; ++index)
      {
        this.PaddingAlignment(128);
        RealGeometryPart part = this._parts[index] as RealGeometryPart;
        realChunkArray[index] = this.BeginChunk(RealType.GeometryPart);
        this.BeginChunk(RealType.GeometryPartDesc);
        this.NextAlignment(16);
        part.PartInfo.Write(this._bw);
        this.EndChunk();
        this.BeginChunk(RealType.GeometryPartTextures);
        part.PartInfo.WriteTextures(this._bw);
        this.EndChunk();
        this.BeginChunk(RealType.GeometryPartShaders);
        part.PartInfo.WriteShaders(this._bw);
        this.EndChunk();
        if (part.PartInfo.MountPoints != null && part.PartInfo.MountPoints.Length != 0)
        {
          this.BeginChunk(RealType.GeometryPartMountPoints);
          this.NextAlignment(16);
          part.PartInfo.WriteMountPoints(this._bw);
          this.EndChunk();
        }
        this.BeginChunk(RealType.GeometryPartData);
        this.BeginChunk(RealType.GeometryPartDataDesc);
        this.NextAlignment(16);
        part.PartData.Write(this._bw);
        this.EndChunk();
        this.BeginChunk(RealType.GeometryPartDataGroups);
        this.NextAlignment(16);
        part.PartData.WriteGroups(this._bw);
        this.EndChunk();
        this.BeginChunk(RealType.GeometryPartDataIndices);
        this.NextAlignment(16);
        part.PartData.WriteIndices(this._bw);
        this.EndChunk();
        this.BeginChunk(RealType.GeometryPartDataVertices);
        this.NextAlignment(128);
        part.PartData.WriteVertices(this._bw);
        this.EndChunk();
        this.EndChunk();
        this.EndChunk();
      }
      this.EndChunk();
      this._stream.Seek((long) realChunk.Offset, SeekOrigin.Begin);
      this.BeginChunk(RealType.GeometryPartsOffset);
      for (int index = 0; index < this._parts.Count; ++index)
      {
        RealGeometryPart part = this._parts[index] as RealGeometryPart;
        int num = arrayList.IndexOf((object) part.PartInfo.Hash);
        this._stream.Seek((long) (realChunk.Offset + 8 + num * 4 * 6), SeekOrigin.Begin);
        this._bw.Write(part.PartInfo.Hash);
        this._bw.Write(realChunkArray[index].Offset);
        this._bw.Write(realChunkArray[index].Length + 8);
        this._bw.Write(realChunkArray[index].Length + 8);
        this._bw.Write(0U);
        this._bw.Write(0U);
      }
      this._stream.Seek((long) realChunk.EndOffset, SeekOrigin.Begin);
      this.EndChunk();
      this._stream.Seek(this._stream.Length, SeekOrigin.Begin);
      this.PaddingAlignment(4096);
      this._stream.SetLength(this._stream.Position);
    }

    public IEnumerator GetEnumerator() => this._parts.GetEnumerator();
  }
}
