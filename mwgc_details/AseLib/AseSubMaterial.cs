// Decompiled with JetBrains decompiler
// Type: mwgc.AseLib.AseSubMaterial
// Assembly: mwgc, Version=1.2.7318.38444, Culture=neutral, PublicKeyToken=null
// MVID: 7B2CC934-3582-440B-A9FF-077D52B1156A
// Assembly location: D:\Users\nikzh\Downloads\mwgc_details.exe

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
