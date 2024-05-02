// Decompiled with JetBrains decompiler
// Type: mwgc.AseLib.AseTextureVertexList
// Assembly: mwgc, Version=1.2.7318.38444, Culture=neutral, PublicKeyToken=null
// MVID: 7B2CC934-3582-440B-A9FF-077D52B1156A
// Assembly location: D:\Users\nikzh\Downloads\mwgc_details.exe

#nullable disable
namespace mwgc.AseLib
{
  public class AseTextureVertexList : AseBaseVertexList
  {
    protected override void ProcessInnerNode(AseReader reader, AseNode parentNode)
    {
      if (!(reader.NodeName == "MESH_TVERT"))
        return;
      AseStringTokenizer aseStringTokenizer = new AseStringTokenizer(reader.NodeData);
      int index = int.Parse(aseStringTokenizer.GetNext());
      this._vertices[index].U = float.Parse(aseStringTokenizer.GetNext());
      this._vertices[index].V = float.Parse(aseStringTokenizer.GetNext());
      this._vertices[index].W = float.Parse(aseStringTokenizer.GetNext());
    }
  }
}
