// Decompiled with JetBrains decompiler
// Type: mwgc.AseLib.AseTextureFaceList
// Assembly: mwgc, Version=1.2.8468.42059, Culture=neutral, PublicKeyToken=null
// MVID: 82A161B2-3E36-490A-94BF-0C902BEC9901
// Assembly location: D:\Users\nikzh\Downloads\mwgc_vertices.exe

#nullable disable
namespace mwgc.AseLib
{
  public class AseTextureFaceList : AseNode
  {
    public static readonly AseTextureFaceList Instance = new AseTextureFaceList();

    protected override void ProcessInnerNode(AseReader reader, AseNode parentNode)
    {
      AseMesh aseMesh = parentNode as AseMesh;
      if (!(reader.NodeName == "MESH_TFACE"))
        return;
      AseStringTokenizer aseStringTokenizer = new AseStringTokenizer(reader.NodeData);
      int index = int.Parse(aseStringTokenizer.GetNext());
      AseFace face = aseMesh.FaceList[index] with
      {
        TextureA = int.Parse(aseStringTokenizer.GetNext()),
        TextureB = int.Parse(aseStringTokenizer.GetNext()),
        TextureC = int.Parse(aseStringTokenizer.GetNext())
      };
      aseMesh.FaceList[index] = face;
    }
  }
}
