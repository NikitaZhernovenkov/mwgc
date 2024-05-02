// Decompiled with JetBrains decompiler
// Type: mwgc.RealEngine.RealVector2
// Assembly: mwgc, Version=1.2.8468.42059, Culture=neutral, PublicKeyToken=null
// MVID: 82A161B2-3E36-490A-94BF-0C902BEC9901
// Assembly location: D:\Users\nikzh\Downloads\mwgc_vertices.exe

using System.IO;

#nullable disable
namespace mwgc.RealEngine
{
  public struct RealVector2
  {
    public float u;
    public float v;

    public RealVector2(float u, float v)
    {
      this.u = u;
      this.v = v;
    }

    public void Read(BinaryReader reader)
    {
      this.u = reader.ReadSingle();
      this.v = reader.ReadSingle();
    }

    public void Write(BinaryWriter writer)
    {
      writer.Write(this.u);
      writer.Write(this.v);
    }

    public override string ToString()
    {
      return "Vector2 {" + this.u.ToString() + "," + this.v.ToString() + "}";
    }

    public override int GetHashCode()
    {
      int hashCode = this.u.GetHashCode();
      string str1 = hashCode.ToString();
      hashCode = this.v.GetHashCode();
      string str2 = hashCode.ToString();
      return (str1 + ":" + str2).GetHashCode();
    }
  }
}
