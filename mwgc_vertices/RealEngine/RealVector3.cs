// Decompiled with JetBrains decompiler
// Type: mwgc.RealEngine.RealVector3
// Assembly: mwgc, Version=1.2.8468.42059, Culture=neutral, PublicKeyToken=null
// MVID: 82A161B2-3E36-490A-94BF-0C902BEC9901
// Assembly location: D:\Users\nikzh\Downloads\mwgc_vertices.exe

using System.IO;

#nullable disable
namespace mwgc.RealEngine
{
  public struct RealVector3
  {
    public float x;
    public float y;
    public float z;

    public RealVector3(float x, float y, float z)
    {
      this.x = x;
      this.y = y;
      this.z = z;
    }

    public void Read(BinaryReader reader)
    {
      this.x = reader.ReadSingle();
      this.y = reader.ReadSingle();
      this.z = reader.ReadSingle();
    }

    public void Write(BinaryWriter writer)
    {
      writer.Write(this.x);
      writer.Write(this.y);
      writer.Write(this.z);
    }

    public override string ToString()
    {
      return "Vector3 {" + this.x.ToString() + "," + this.y.ToString() + "," + this.z.ToString() + "}";
    }

    public override int GetHashCode()
    {
      return (this.x.GetHashCode().ToString() + ":" + this.y.GetHashCode().ToString() + ":" + this.z.GetHashCode().ToString()).GetHashCode();
    }
  }
}
