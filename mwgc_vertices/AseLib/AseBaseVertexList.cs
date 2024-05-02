// Decompiled with JetBrains decompiler
// Type: mwgc.AseLib.AseBaseVertexList
// Assembly: mwgc, Version=1.2.8468.42059, Culture=neutral, PublicKeyToken=null
// MVID: 82A161B2-3E36-490A-94BF-0C902BEC9901
// Assembly location: D:\Users\nikzh\Downloads\mwgc_vertices.exe

namespace mwgc.AseLib
{
  public class AseBaseVertexList : AseNode
  {
    protected AseVertex[] _vertices;

    public int Count
    {
      get => this._vertices.Length;
      set => this._vertices = new AseVertex[value];
    }

    public AseVertex this[int index]
    {
      get => this._vertices[index];
      set => this._vertices[index] = value;
    }
  }
}
