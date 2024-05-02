// Decompiled with JetBrains decompiler
// Type: mwgc.AseLib.AseRoot
// Assembly: mwgc, Version=1.2.8468.42059, Culture=neutral, PublicKeyToken=null
// MVID: 82A161B2-3E36-490A-94BF-0C902BEC9901
// Assembly location: D:\Users\nikzh\Downloads\mwgc_vertices.exe

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
