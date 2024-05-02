// Decompiled with JetBrains decompiler
// Type: mwgc.RawGeometry.RawObjectHeader
// Assembly: mwgc, Version=1.2.7318.38444, Culture=neutral, PublicKeyToken=null
// MVID: 7B2CC934-3582-440B-A9FF-077D52B1156A
// Assembly location: D:\Users\nikzh\Downloads\mwgc_details.exe

using System.IO;

#nullable disable
namespace mwgc.RawGeometry
{
  public struct RawObjectHeader
  {
    public RawString ObjName;
    public int NumVertices;
    public int NumFaces;
    public float[] Transform;

    public void Read(BinaryReader br)
    {
      this.ObjName = new RawString(br);
      this.NumVertices = br.ReadInt32();
      this.NumFaces = br.ReadInt32();
      this.Transform = new float[16];
      for (int index = 0; index < 16; ++index)
        this.Transform[index] = br.ReadSingle();
    }
  }
}
