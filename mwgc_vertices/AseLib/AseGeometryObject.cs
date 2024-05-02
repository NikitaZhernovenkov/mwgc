// Decompiled with JetBrains decompiler
// Type: mwgc.AseLib.AseGeometryObject
// Assembly: mwgc, Version=1.2.8468.42059, Culture=neutral, PublicKeyToken=null
// MVID: 82A161B2-3E36-490A-94BF-0C902BEC9901
// Assembly location: D:\Users\nikzh\Downloads\mwgc_vertices.exe

namespace mwgc.AseLib
{
  public class AseGeometryObject : AseExtendedNode
  {
    private string _name;
    private int _materialRef;
    private AseTransform _transform;
    private AseMesh _mesh;

    public AseMesh Mesh
    {
      get => this._mesh;
      set => this._mesh = value;
    }

    public AseTransform Transform
    {
      get => this._transform;
      set => this._transform = value;
    }

    public string Name => this._name;

    public int MaterialReference => this._materialRef;

    public AseGeometryObject()
    {
      this.AddNodeParser("NODE_TM", (AseNode) (this._transform = new AseTransform()));
      this.AddNodeParser("MESH", (AseNode) (this._mesh = new AseMesh()));
    }

    protected override void ProcessInnerNode(AseReader reader, AseNode parentNode)
    {
      switch (reader.NodeName)
      {
        case "NODE_NAME":
          this._name = reader.NodeData;
          break;
        case "MATERIAL_REF":
          this._materialRef = int.Parse(reader.NodeData);
          break;
      }
    }

    protected override void ProcessNodePre(AseReader reader, AseNode parentNode)
    {
      (parentNode as AseRoot).AddGeometryObject(this);
    }
  }
}
