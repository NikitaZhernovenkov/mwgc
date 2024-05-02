// Decompiled with JetBrains decompiler
// Type: mwgc.RawGeometry.RawHeader
// Assembly: mwgc, Version=1.2.8468.42059, Culture=neutral, PublicKeyToken=null
// MVID: 82A161B2-3E36-490A-94BF-0C902BEC9901
// Assembly location: D:\Users\nikzh\Downloads\mwgc_vertices.exe

using System.IO;

#nullable disable
namespace mwgc.RawGeometry
{
  public struct RawHeader
  {
    public int Magic;
    public int NumMaterials;
    public int NumObjects;
    public RawString[] MatNames;
    public RawObjectHeader[] ObjHeaders;

    public void Read(BinaryReader br)
    {
      this.Magic = br.ReadInt32();
      this.NumMaterials = br.ReadInt32();
      this.NumObjects = br.ReadInt32();
      this.MatNames = new RawString[this.NumMaterials];
      for (int index = 0; index < this.NumMaterials; ++index)
        this.MatNames[index] = new RawString(br);
      this.ObjHeaders = new RawObjectHeader[this.NumObjects];
      for (int index = 0; index < this.NumObjects; ++index)
      {
        this.ObjHeaders[index] = new RawObjectHeader();
        this.ObjHeaders[index].Read(br);
      }
    }
  }
}
