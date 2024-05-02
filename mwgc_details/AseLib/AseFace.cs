// Decompiled with JetBrains decompiler
// Type: mwgc.AseLib.AseFace
// Assembly: mwgc, Version=1.2.7318.38444, Culture=neutral, PublicKeyToken=null
// MVID: 7B2CC934-3582-440B-A9FF-077D52B1156A
// Assembly location: D:\Users\nikzh\Downloads\mwgc_details.exe

#nullable disable
namespace mwgc.AseLib
{
  public struct AseFace
  {
    private int _a;
    private int _b;
    private int _c;
    private bool _ab;
    private bool _bc;
    private bool _ca;
    private int[] _smoothing;
    private int _materialId;
    private int _tA;
    private int _tB;
    private int _tC;
    private AseVertex _faceNormal;
    private AseVertex _aN;
    private AseVertex _bN;
    private AseVertex _cN;

    public int A
    {
      get => this._a;
      set => this._a = value;
    }

    public int B
    {
      get => this._b;
      set => this._b = value;
    }

    public int C
    {
      get => this._c;
      set => this._c = value;
    }

    public AseVertex NormalA
    {
      get => this._aN;
      set => this._aN = value;
    }

    public AseVertex NormalB
    {
      get => this._bN;
      set => this._bN = value;
    }

    public AseVertex NormalC
    {
      get => this._cN;
      set => this._cN = value;
    }

    public AseVertex NormalFace
    {
      get => this._faceNormal;
      set => this._faceNormal = value;
    }

    public int TextureA
    {
      get => this._tA;
      set => this._tA = value;
    }

    public int TextureB
    {
      get => this._tB;
      set => this._tB = value;
    }

    public int TextureC
    {
      get => this._tC;
      set => this._tC = value;
    }

    public bool EdgeAB
    {
      get => this._ab;
      set => this._ab = value;
    }

    public bool EdgeBC
    {
      get => this._bc;
      set => this._bc = value;
    }

    public bool EdgeCA
    {
      get => this._ca;
      set => this._ca = value;
    }

    public int SmoothingCount
    {
      get => this._smoothing != null ? this._smoothing.Length : 0;
      set => this._smoothing = new int[value];
    }

    public int this[int index]
    {
      get => this._smoothing[index];
      set => this._smoothing[index] = value;
    }

    public int MaterialID
    {
      get => this._materialId;
      set => this._materialId = value;
    }
  }
}
