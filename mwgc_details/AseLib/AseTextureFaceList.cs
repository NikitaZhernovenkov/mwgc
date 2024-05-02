// Decompiled with JetBrains decompiler
// Type: mwgc.AseLib.AseTextureFaceList
// Assembly: mwgc, Version=1.2.7318.38444, Culture=neutral, PublicKeyToken=null
// MVID: 7B2CC934-3582-440B-A9FF-077D52B1156A
// Assembly location: D:\Users\nikzh\Downloads\mwgc_details.exe

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
