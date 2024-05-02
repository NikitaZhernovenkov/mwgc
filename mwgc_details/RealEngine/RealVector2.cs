// Decompiled with JetBrains decompiler
// Type: mwgc.RealEngine.RealVector2
// Assembly: mwgc, Version=1.2.7318.38444, Culture=neutral, PublicKeyToken=null
// MVID: 7B2CC934-3582-440B-A9FF-077D52B1156A
// Assembly location: D:\Users\nikzh\Downloads\mwgc_details.exe

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
