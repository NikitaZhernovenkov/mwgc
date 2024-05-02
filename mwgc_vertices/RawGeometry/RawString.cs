// Decompiled with JetBrains decompiler
// Type: mwgc.RawGeometry.RawString
// Assembly: mwgc, Version=1.2.8468.42059, Culture=neutral, PublicKeyToken=null
// MVID: 82A161B2-3E36-490A-94BF-0C902BEC9901
// Assembly location: D:\Users\nikzh\Downloads\mwgc_vertices.exe

using System.IO;
using System.Text;

#nullable disable
namespace mwgc.RawGeometry
{
  public struct RawString
  {
    public int Length;
    public string Data;

    public void Read(BinaryReader br)
    {
      this.Length = br.ReadInt32();
      this.Data = Encoding.ASCII.GetString(br.ReadBytes(this.Length)).Split(new char[1])[0];
    }

    public RawString(BinaryReader br)
    {
      this.Length = 0;
      this.Data = "";
      this.Read(br);
    }

    public override string ToString() => this.Data;
  }
}
