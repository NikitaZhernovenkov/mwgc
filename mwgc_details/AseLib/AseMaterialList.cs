// Decompiled with JetBrains decompiler
// Type: mwgc.AseLib.AseMaterialList
// Assembly: mwgc, Version=1.2.7318.38444, Culture=neutral, PublicKeyToken=null
// MVID: 7B2CC934-3582-440B-A9FF-077D52B1156A
// Assembly location: D:\Users\nikzh\Downloads\mwgc_details.exe

#nullable disable
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
      switch (reader.NodeName)
      {
        case "MATERIAL_COUNT":
          this._materials = new AseMaterial[int.Parse(reader.NodeData)];
          break;
      }
    }

    protected override void ProcessNodePre(AseReader reader, AseNode parentNode)
    {
      (parentNode as AseRoot).MaterialList = this;
    }
  }
}
