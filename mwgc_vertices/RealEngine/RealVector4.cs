// Decompiled with JetBrains decompiler
// Type: mwgc.RealEngine.RealVector4
// Assembly: mwgc, Version=1.2.8468.42059, Culture=neutral, PublicKeyToken=null
// MVID: 82A161B2-3E36-490A-94BF-0C902BEC9901
// Assembly location: D:\Users\nikzh\Downloads\mwgc_vertices.exe

using System.IO;

#nullable disable
namespace mwgc.RealEngine
{
  public struct RealVector4
  {
    public float x;
    public float y;
    public float z;
    public float w;

    public RealVector4(float x, float y, float z, float w)
    {
      this.x = x;
      this.y = y;
      this.z = z;
      this.w = w;
    }

    public RealVector4(float x, float y, float z)
    {
      this.x = x;
      this.y = y;
      this.z = z;
      this.w = 0.0f;
    }

    public void Read(BinaryReader reader)
    {
      this.x = reader.ReadSingle();
      this.y = reader.ReadSingle();
      this.z = reader.ReadSingle();
      this.w = reader.ReadSingle();
    }

    public void Write(BinaryWriter writer)
    {
      writer.Write(this.x);
      writer.Write(this.y);
      writer.Write(this.z);
      writer.Write(this.w);
    }

    public override string ToString()
    {
      return "Vector4 {" + this.x.ToString() + "," + this.y.ToString() + "," + this.z.ToString() + "," + this.w.ToString() + "}";
    }
  }
}
