// Decompiled with JetBrains decompiler
// Type: mwgc.AseLib.AseTransform
// Assembly: mwgc, Version=1.2.7318.38444, Culture=neutral, PublicKeyToken=null
// MVID: 7B2CC934-3582-440B-A9FF-077D52B1156A
// Assembly location: D:\Users\nikzh\Downloads\mwgc_details.exe

#nullable disable
namespace mwgc.AseLib
{
  public class AseTransform : AseNode
  {
    protected float[] _matrix;

    public AseTransform() => this._matrix = new float[16];

    public float[] Matrix => this._matrix;

    public float this[int i, int j]
    {
      get => this._matrix[i * 4 + j];
      set => this._matrix[i * 4 + j] = value;
    }

    protected override void ProcessInnerNode(AseReader reader, AseNode parentNode)
    {
      switch (reader.NodeName)
      {
        case "TM_ROW0":
          AseStringTokenizer aseStringTokenizer1 = new AseStringTokenizer(reader.NodeData);
          this[0, 0] = float.Parse(aseStringTokenizer1.GetNext());
          this[0, 1] = float.Parse(aseStringTokenizer1.GetNext());
          this[0, 2] = float.Parse(aseStringTokenizer1.GetNext());
          this[0, 3] = 0.0f;
          break;
        case "TM_ROW1":
          AseStringTokenizer aseStringTokenizer2 = new AseStringTokenizer(reader.NodeData);
          this[1, 0] = float.Parse(aseStringTokenizer2.GetNext());
          this[1, 1] = float.Parse(aseStringTokenizer2.GetNext());
          this[1, 2] = float.Parse(aseStringTokenizer2.GetNext());
          this[1, 3] = 0.0f;
          break;
        case "TM_ROW2":
          AseStringTokenizer aseStringTokenizer3 = new AseStringTokenizer(reader.NodeData);
          this[2, 0] = float.Parse(aseStringTokenizer3.GetNext());
          this[2, 1] = float.Parse(aseStringTokenizer3.GetNext());
          this[2, 2] = float.Parse(aseStringTokenizer3.GetNext());
          this[2, 3] = 0.0f;
          break;
        case "TM_ROW3":
          AseStringTokenizer aseStringTokenizer4 = new AseStringTokenizer(reader.NodeData);
          this[3, 0] = float.Parse(aseStringTokenizer4.GetNext());
          this[3, 1] = float.Parse(aseStringTokenizer4.GetNext());
          this[3, 2] = float.Parse(aseStringTokenizer4.GetNext());
          this[3, 3] = 1f;
          break;
      }
    }
  }
}
