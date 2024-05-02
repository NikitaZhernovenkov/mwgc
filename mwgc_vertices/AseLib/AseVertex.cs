// Decompiled with JetBrains decompiler
// Type: mwgc.AseLib.AseVertex
// Assembly: mwgc, Version=1.2.8468.42059, Culture=neutral, PublicKeyToken=null
// MVID: 82A161B2-3E36-490A-94BF-0C902BEC9901
// Assembly location: D:\Users\nikzh\Downloads\mwgc_vertices.exe

#nullable disable
namespace mwgc.AseLib
{
  public struct AseVertex
  {
    private float _x;
    private float _y;
    private float _z;

    public AseVertex(float x, float y, float z)
    {
      this._x = x;
      this._y = y;
      this._z = z;
    }

    public float X
    {
      get => this._x;
      set => this._x = value;
    }

    public float Y
    {
      get => this._y;
      set => this._y = value;
    }

    public float Z
    {
      get => this._z;
      set => this._z = value;
    }

    public float U
    {
      get => this._x;
      set => this._x = value;
    }

    public float V
    {
      get => this._y;
      set => this._y = value;
    }

    public float W
    {
      get => this._z;
      set => this._z = value;
    }
  }
}
