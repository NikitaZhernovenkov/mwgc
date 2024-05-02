// Decompiled with JetBrains decompiler
// Type: mwgc.RawGeometry.RawHeader
// Assembly: mwgc, Version=1.2.7318.38444, Culture=neutral, PublicKeyToken=null
// MVID: 7B2CC934-3582-440B-A9FF-077D52B1156A
// Assembly location: D:\Users\nikzh\Downloads\mwgc_details.exe

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
