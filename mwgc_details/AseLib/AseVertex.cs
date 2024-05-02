// Decompiled with JetBrains decompiler
// Type: mwgc.AseLib.AseVertex
// Assembly: mwgc, Version=1.2.7318.38444, Culture=neutral, PublicKeyToken=null
// MVID: 7B2CC934-3582-440B-A9FF-077D52B1156A
// Assembly location: D:\Users\nikzh\Downloads\mwgc_details.exe

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
