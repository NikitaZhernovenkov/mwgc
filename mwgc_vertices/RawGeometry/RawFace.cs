// Decompiled with JetBrains decompiler
// Type: mwgc.RawGeometry.RawFace
// Assembly: mwgc, Version=1.2.8468.42059, Culture=neutral, PublicKeyToken=null
// MVID: 82A161B2-3E36-490A-94BF-0C902BEC9901
// Assembly location: D:\Users\nikzh\Downloads\mwgc_vertices.exe

using System.IO;

#nullable disable
namespace mwgc.RawGeometry
{
  public struct RawFace
  {
    public int MatIndex;
    public ushort I1;
    public ushort I2;
    public ushort I3;
    public float tU1;
    public float tU2;
    public float tU3;
    public float tV1;
    public float tV2;
    public float tV3;

    public void Read(BinaryReader br)
    {
      this.MatIndex = br.ReadInt32();
      this.I1 = br.ReadUInt16();
      this.I2 = br.ReadUInt16();
      this.I3 = br.ReadUInt16();
      int num = (int) br.ReadInt16();
      this.tU1 = br.ReadSingle();
      this.tU2 = br.ReadSingle();
      this.tU3 = br.ReadSingle();
      this.tV1 = br.ReadSingle();
      this.tV2 = br.ReadSingle();
      this.tV3 = br.ReadSingle();
    }
  }
}
