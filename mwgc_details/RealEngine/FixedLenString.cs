// Decompiled with JetBrains decompiler
// Type: mwgc.RealEngine.FixedLenString
// Assembly: mwgc, Version=1.2.7318.38444, Culture=neutral, PublicKeyToken=null
// MVID: 7B2CC934-3582-440B-A9FF-077D52B1156A
// Assembly location: D:\Users\nikzh\Downloads\mwgc_details.exe

using System.IO;
using System.Text;

#nullable disable
namespace mwgc.RealEngine
{
  public struct FixedLenString
  {
    private int _length;
    private string _string;

    public FixedLenString(string data)
    {
      this._length = data.Length + 4 - data.Length % 4;
      this._string = data;
    }

    public FixedLenString(string data, int length)
    {
      this._length = length;
      this._string = data;
    }

    public FixedLenString(BinaryReader br, int length)
    {
      this._length = 0;
      this._string = "";
      this.Read(br, length);
    }

    public FixedLenString(BinaryReader br)
    {
      this._length = 0;
      this._string = "";
      this.Read(br);
    }

    public void Read(BinaryReader br, int length)
    {
      this._length = length;
      this._string = Encoding.ASCII.GetString(br.ReadBytes(length)).TrimEnd(new char[1]);
    }

    public void Read(BinaryReader br)
    {
      this._length = 0;
      this._string = "";
      string str;
      do
      {
        str = Encoding.ASCII.GetString(br.ReadBytes(4));
        this._string += str;
        this._length += 4;
      }
      while (str.IndexOf(char.MinValue) < 0);
      this._string = this._string.TrimEnd(new char[1]);
    }

    public void Write(BinaryWriter bw)
    {
      byte[] bytes = Encoding.ASCII.GetBytes(this._string.PadRight(this._length, char.MinValue));
      bw.Write(bytes);
    }

    public override string ToString() => this._string;
  }
}
