// Decompiled with JetBrains decompiler
// Type: mwgc.RawGeometry.RawObjectHeader
// Assembly: mwgc, Version=1.2.8468.42059, Culture=neutral, PublicKeyToken=null
// MVID: 82A161B2-3E36-490A-94BF-0C902BEC9901
// Assembly location: D:\Users\nikzh\Downloads\mwgc_vertices.exe

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
