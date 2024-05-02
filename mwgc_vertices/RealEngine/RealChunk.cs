// Decompiled with JetBrains decompiler
// Type: mwgc.RealEngine.RealChunk
// Assembly: mwgc, Version=1.2.8468.42059, Culture=neutral, PublicKeyToken=null
// MVID: 82A161B2-3E36-490A-94BF-0C902BEC9901
// Assembly location: D:\Users\nikzh\Downloads\mwgc_vertices.exe

using System.IO;

#nullable disable
namespace mwgc.RealEngine
{
  public class RealChunk
  {
    protected int _offset;
    protected RealType _type;
    protected int _length;

    public bool IsParent => ((ulong) (int) this._type & 2147483648UL) > 0UL;

    public RealType Type
    {
      get => this._type;
      set => this._type = value;
    }

    public int EndOffset
    {
      get => this._offset + this._length + 8;
      set => this._length = value - this._offset - 8;
    }

    public int Offset
    {
      get => this._offset;
      set => this._offset = value;
    }

    public int Length
    {
      get => this._length;
      set => this._length = value;
    }

    public int RawLength => this.Length + 8;

    public void Read(BinaryReader br)
    {
      this._offset = (int) br.BaseStream.Position;
      this._type = (RealType) br.ReadUInt32();
      this._length = br.ReadInt32();
    }

    public void Write(BinaryWriter bw)
    {
      long position = bw.BaseStream.Position;
      bw.BaseStream.Seek((long) this._offset, SeekOrigin.Begin);
      bw.Write((uint) this._type);
      bw.Write(this._length);
      bw.BaseStream.Seek(position, SeekOrigin.Begin);
    }

    public void GoToStart(Stream fs) => fs.Seek((long) (this._offset + 8), SeekOrigin.Begin);

    public void Skip(Stream fs) => fs.Seek((long) this.EndOffset, SeekOrigin.Begin);

    public override string ToString()
    {
      return "RealChunk {\n\tType=" + string.Format("{0:X}", (object) this._type) + ",\n\tOffset=" + string.Format("{0:X}", (object) this._offset) + ",\n\tLength=" + string.Format("{0:X}", (object) this._length) + "\n}";
    }
  }
}
