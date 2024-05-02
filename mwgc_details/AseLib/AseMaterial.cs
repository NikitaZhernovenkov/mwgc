// Decompiled with JetBrains decompiler
// Type: mwgc.AseLib.AseMaterial
// Assembly: mwgc, Version=1.2.7318.38444, Culture=neutral, PublicKeyToken=null
// MVID: 7B2CC934-3582-440B-A9FF-077D52B1156A
// Assembly location: D:\Users\nikzh\Downloads\mwgc_details.exe

#nullable disable
namespace mwgc.AseLib
{
  public class AseMaterial : AseExtendedNode
  {
    protected string _name;
    protected AseSubMaterial[] _subMaterials;

    public string Name => this._name;

    public AseSubMaterial this[int index]
    {
      get => this._subMaterials[index];
      set => this._subMaterials[index] = value;
    }

    public bool HasSubMaterials => this._subMaterials != null;

    public int SubMaterialCount => this._subMaterials.Length;

    public AseMaterial()
    {
      this._subMaterials = (AseSubMaterial[]) null;
      this.AddNodeParser("SUBMATERIAL", typeof (AseSubMaterial));
    }

    protected override void ProcessInnerNode(AseReader reader, AseNode parentNode)
    {
      switch (reader.NodeName)
      {
        case "MATERIAL_NAME":
          this._name = reader.NodeData;
          break;
        case "NUMSUBMTLS":
          if (parentNode is AseSubMaterial)
            throw new AseException("Internal Sanity Check: SubMaterial within SubMaterial!");
          this._subMaterials = new AseSubMaterial[int.Parse(reader.NodeData)];
          break;
      }
    }

    protected override void ProcessNodePre(AseReader reader, AseNode parentNode)
    {
      if (!(parentNode is AseMaterialList))
        return;
      (parentNode as AseMaterialList)[int.Parse(reader.NodeData)] = this;
    }
  }
}
