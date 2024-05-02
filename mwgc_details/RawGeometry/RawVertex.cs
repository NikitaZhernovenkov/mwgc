// Decompiled with JetBrains decompiler
// Type: mwgc.RawGeometry.RawVertex
// Assembly: mwgc, Version=1.2.7318.38444, Culture=neutral, PublicKeyToken=null
// MVID: 7B2CC934-3582-440B-A9FF-077D52B1156A
// Assembly location: D:\Users\nikzh\Downloads\mwgc_details.exe

using System.IO;

#nullable disable
namespace mwgc.RawGeometry
{
  public struct RawVertex
  {
    public float X;
    public float Y;
    public float Z;
    public float nX;
    public float nY;
    public float nZ;

    public void Read(BinaryReader br)
    {
      this.X = br.ReadSingle();
      this.Y = br.ReadSingle();
      this.Z = br.ReadSingle();
      this.nX = br.ReadSingle();
      this.nY = br.ReadSingle();
      this.nZ = br.ReadSingle();
    }
  }
}
