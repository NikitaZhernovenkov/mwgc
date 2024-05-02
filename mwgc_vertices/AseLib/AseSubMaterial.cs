// Decompiled with JetBrains decompiler
// Type: mwgc.AseLib.AseSubMaterial
// Assembly: mwgc, Version=1.2.8468.42059, Culture=neutral, PublicKeyToken=null
// MVID: 82A161B2-3E36-490A-94BF-0C902BEC9901
// Assembly location: D:\Users\nikzh\Downloads\mwgc_vertices.exe

#nullable disable
namespace mwgc.AseLib
{
  public class AseSubMaterial : AseMaterial
  {
    protected override void ProcessNodePre(AseReader reader, AseNode parentNode)
    {
      if (!(parentNode is AseMaterial))
        return;
      (parentNode as AseMaterial)[int.Parse(reader.NodeData)] = this;
    }
  }
}
