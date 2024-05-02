// Decompiled with JetBrains decompiler
// Type: mwgc.AseLib.AseMaterialList
// Assembly: mwgc, Version=1.2.8468.42059, Culture=neutral, PublicKeyToken=null
// MVID: 82A161B2-3E36-490A-94BF-0C902BEC9901
// Assembly location: D:\Users\nikzh\Downloads\mwgc_vertices.exe

namespace mwgc.AseLib
{
  public class AseMaterialList : AseExtendedNode
  {
    private AseMaterial[] _materials;

    public AseMaterial this[int index]
    {
      get => this._materials[index];
      set => this._materials[index] = value;
    }

    public int Count => this._materials.Length;

    public AseMaterialList() => this.AddNodeParser("MATERIAL", typeof (AseMaterial));

    protected override void ProcessInnerNode(AseReader reader, AseNode parentNode)
    {
      if (!(reader.NodeName == "MATERIAL_COUNT"))
        return;
      this._materials = new AseMaterial[int.Parse(reader.NodeData)];
    }

    protected override void ProcessNodePre(AseReader reader, AseNode parentNode)
    {
      (parentNode as AseRoot).MaterialList = this;
    }
  }
}
