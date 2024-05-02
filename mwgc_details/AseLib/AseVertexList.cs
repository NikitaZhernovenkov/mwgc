// Decompiled with JetBrains decompiler
// Type: mwgc.AseLib.AseVertexList
// Assembly: mwgc, Version=1.2.7318.38444, Culture=neutral, PublicKeyToken=null
// MVID: 7B2CC934-3582-440B-A9FF-077D52B1156A
// Assembly location: D:\Users\nikzh\Downloads\mwgc_details.exe

#nullable disable
namespace mwgc.AseLib
{
  public class AseVertexList : AseBaseVertexList
  {
    protected override void ProcessInnerNode(AseReader reader, AseNode parentNode)
    {
      if (!(reader.NodeName == "MESH_VERTEX"))
        return;
      AseStringTokenizer aseStringTokenizer = new AseStringTokenizer(reader.NodeData);
      int index = int.Parse(aseStringTokenizer.GetNext());
      this._vertices[index].X = float.Parse(aseStringTokenizer.GetNext());
      this._vertices[index].Y = float.Parse(aseStringTokenizer.GetNext());
      this._vertices[index].Z = float.Parse(aseStringTokenizer.GetNext());
    }
  }
}
