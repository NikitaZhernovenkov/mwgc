// Decompiled with JetBrains decompiler
// Type: mwgc.RawGeometry.RawFace
// Assembly: mwgc, Version=1.2.7318.38444, Culture=neutral, PublicKeyToken=null
// MVID: 7B2CC934-3582-440B-A9FF-077D52B1156A
// Assembly location: D:\Users\nikzh\Downloads\mwgc_details.exe

using System.IO;

#nullable disable
namespace mwgc.RawGeometry
{
  public struct RawFace
  {
    public int MatIndex;
    public short I1;
    public short I2;
    public short I3;
    public float tU1;
    public float tU2;
    public float tU3;
    public float tV1;
    public float tV2;
    public float tV3;

    public void Read(BinaryReader br)
    {
      this.MatIndex = br.ReadInt32();
      this.I1 = br.ReadInt16();
      this.I2 = br.ReadInt16();
      this.I3 = br.ReadInt16();
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
