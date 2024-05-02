// Decompiled with JetBrains decompiler
// Type: mwgc.AseLib.AseTextureVertexList
// Assembly: mwgc, Version=1.2.8468.42059, Culture=neutral, PublicKeyToken=null
// MVID: 82A161B2-3E36-490A-94BF-0C902BEC9901
// Assembly location: D:\Users\nikzh\Downloads\mwgc_vertices.exe

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
