// Decompiled with JetBrains decompiler
// Type: mwgc.RealEngine.RealVector3
// Assembly: mwgc, Version=1.2.7318.38444, Culture=neutral, PublicKeyToken=null
// MVID: 7B2CC934-3582-440B-A9FF-077D52B1156A
// Assembly location: D:\Users\nikzh\Downloads\mwgc_details.exe

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
