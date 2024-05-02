// Decompiled with JetBrains decompiler
// Type: mwgc.RealEngine.RealVector4
// Assembly: mwgc, Version=1.2.7318.38444, Culture=neutral, PublicKeyToken=null
// MVID: 7B2CC934-3582-440B-A9FF-077D52B1156A
// Assembly location: D:\Users\nikzh\Downloads\mwgc_details.exe

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
