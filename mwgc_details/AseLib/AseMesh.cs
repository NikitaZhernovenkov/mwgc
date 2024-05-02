// Decompiled with JetBrains decompiler
// Type: mwgc.AseLib.AseMesh
// Assembly: mwgc, Version=1.2.7318.38444, Culture=neutral, PublicKeyToken=null
// MVID: 7B2CC934-3582-440B-A9FF-077D52B1156A
// Assembly location: D:\Users\nikzh\Downloads\mwgc_details.exe

#nullable disable
namespace mwgc.AseLib
{
  public class AseMesh : AseExtendedNode
  {
    private AseVertexList _vertexList;
    private AseTextureVertexList _texVertexList;
    private AseFaceList _faceList;

    public AseVertexList VertexList
    {
      get => this._vertexList;
      set => this._vertexList = value;
    }

    public AseTextureVertexList TextureVertexList
    {
      get => this._texVertexList;
      set => this._texVertexList = value;
    }

    public AseFaceList FaceList
    {
      get => this._faceList;
      set => this._faceList = value;
    }

    public AseMesh()
    {
      this.AddNodeParser("MESH_VERTEX_LIST", (AseNode) (this._vertexList = new AseVertexList()));
      this.AddNodeParser("MESH_FACE_LIST", (AseNode) (this._faceList = new AseFaceList()));
      this.AddNodeParser("MESH_TVERTLIST", (AseNode) (this._texVertexList = new AseTextureVertexList()));
      this.AddNodeParser("MESH_TFACELIST", (AseNode) AseTextureFaceList.Instance);
      this.AddNodeParser("MESH_NORMALS", (AseNode) AseNormals.Instance);
    }

    protected override void ProcessInnerNode(AseReader reader, AseNode parentNode)
    {
      string nodeName = reader.NodeName;
      switch (nodeName)
      {
        case null:
          break;
        case "TIMEVALUE":
          if (int.Parse(reader.NodeData) == 0)
            break;
          throw new AseException("Only models without any animation is supported.");
        case "MESH_NUMVERTEX":
          this._vertexList.Count = int.Parse(reader.NodeData);
          break;
        case "MESH_NUMFACES":
          this._faceList.Count = int.Parse(reader.NodeData);
          break;
        case "MESH_NUMTVERTEX":
          this._texVertexList.Count = int.Parse(reader.NodeData);
          break;
        default:
          int num = nodeName == "MESH_NUMTVFACES" ? 1 : 0;
          break;
      }
    }
  }
}
