// Decompiled with JetBrains decompiler
// Type: mwgc.AseLib.AseBaseVertexList
// Assembly: mwgc, Version=1.2.7318.38444, Culture=neutral, PublicKeyToken=null
// MVID: 7B2CC934-3582-440B-A9FF-077D52B1156A
// Assembly location: D:\Users\nikzh\Downloads\mwgc_details.exe

#nullable disable
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
