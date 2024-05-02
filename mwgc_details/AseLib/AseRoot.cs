// Decompiled with JetBrains decompiler
// Type: mwgc.AseLib.AseRoot
// Assembly: mwgc, Version=1.2.7318.38444, Culture=neutral, PublicKeyToken=null
// MVID: 7B2CC934-3582-440B-A9FF-077D52B1156A
// Assembly location: D:\Users\nikzh\Downloads\mwgc_details.exe

using System.Collections;

#nullable disable
namespace mwgc.AseLib
{
  public class AseRoot : AseExtendedNode
  {
    private AseMaterialList _materialList;
    private ArrayList _geomObjects;

    public AseGeometryObject this[int index] => this._geomObjects[index] as AseGeometryObject;

    public int ObjectCount => this._geomObjects.Count;

    public void AddGeometryObject(AseGeometryObject obj) => this._geomObjects.Add((object) obj);

    public AseMaterialList MaterialList
    {
      get => this._materialList;
      set => this._materialList = value;
    }

    public AseRoot()
    {
      this._geomObjects = new ArrayList();
      this.AddNodeParser("MATERIAL_LIST", (AseNode) (this._materialList = new AseMaterialList()));
      this.AddNodeParser("GEOMOBJECT", typeof (AseGeometryObject));
    }
  }
}
