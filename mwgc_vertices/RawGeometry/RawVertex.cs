// Decompiled with JetBrains decompiler
// Type: mwgc.RawGeometry.RawVertex
// Assembly: mwgc, Version=1.2.8468.42059, Culture=neutral, PublicKeyToken=null
// MVID: 82A161B2-3E36-490A-94BF-0C902BEC9901
// Assembly location: D:\Users\nikzh\Downloads\mwgc_vertices.exe

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
