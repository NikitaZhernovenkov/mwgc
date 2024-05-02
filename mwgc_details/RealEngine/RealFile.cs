// Decompiled with JetBrains decompiler
// Type: mwgc.RealEngine.RealFile
// Assembly: mwgc, Version=1.2.7318.38444, Culture=neutral, PublicKeyToken=null
// MVID: 7B2CC934-3582-440B-A9FF-077D52B1156A
// Assembly location: D:\Users\nikzh\Downloads\mwgc_details.exe

using System.Collections;
using System.IO;

#nullable disable
namespace mwgc.RealEngine
{
  public abstract class RealFile
  {
    protected Stream _stream;
    protected BinaryReader _br;
    protected BinaryWriter _bw;
    protected Stack _chunkStack;

    protected void NextAlignment(int alignment)
    {
      if (this._stream.Position % (long) alignment == 0L)
        return;
      this._stream.Position += (long) alignment - this._stream.Position % (long) alignment;
    }

    protected void SkipChunk(RealChunk chunk) => chunk.Skip(this._stream);

    protected RealChunk NextChunk()
    {
      RealChunk realChunk = new RealChunk();
      realChunk.Read(this._br);
      return realChunk;
    }

    protected RealChunk BeginChunk(RealType type)
    {
      RealChunk realChunk = new RealChunk();
      realChunk.Offset = (int) this._stream.Position;
      realChunk.Type = type;
      this._chunkStack.Push((object) realChunk);
      this._stream.Seek(8L, SeekOrigin.Current);
      return realChunk;
    }

    protected void EndChunk()
    {
      RealChunk realChunk = this._chunkStack.Pop() as RealChunk;
      realChunk.EndOffset = (int) this._stream.Position;
      realChunk.Write(this._bw);
    }

    public void Open(string filename)
    {
      this.Open((Stream) new FileStream(filename, FileMode.Open, FileAccess.Read));
    }

    public void Save(string filename)
    {
      FileStream output = new FileStream(filename, FileMode.Create, FileAccess.Write);
      this._stream = (Stream) output;
      this._bw = new BinaryWriter((Stream) output);
      this._chunkStack = new Stack();
      this.ProcessSave();
      this.Close();
    }

    public void Open(Stream stream)
    {
      if (this._stream != null)
        this.Close();
      this._stream = stream;
      this._stream.Seek(0L, SeekOrigin.Begin);
      this._br = new BinaryReader(this._stream);
      this.ProcessOpen();
      this.Close();
    }

    private void Close()
    {
      if (this._br != null)
        this._br.Close();
      if (this._bw != null)
        this._bw.Close();
      this._stream.Close();
      this._chunkStack = (Stack) null;
      this._br = (BinaryReader) null;
      this._bw = (BinaryWriter) null;
      this._stream = (Stream) null;
    }

    protected abstract void ProcessOpen();

    protected abstract void ProcessSave();
  }
}
