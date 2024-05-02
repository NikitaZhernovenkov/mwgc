// Decompiled with JetBrains decompiler
// Type: mwgc.AseLib.AseNode
// Assembly: mwgc, Version=1.2.7318.38444, Culture=neutral, PublicKeyToken=null
// MVID: 7B2CC934-3582-440B-A9FF-077D52B1156A
// Assembly location: D:\Users\nikzh\Downloads\mwgc_details.exe

#nullable disable
namespace mwgc.AseLib
{
  public abstract class AseNode
  {
    protected void TraverseUnhandledNodes(AseReader reader)
    {
      reader.ReadNextLine();
      while (!reader.NodeParentEnd)
      {
        if (reader.NodeParentStart)
          this.TraverseUnhandledNodes(reader);
        reader.ReadNextLine();
      }
    }

    public virtual void ProcessNode(AseReader reader, AseNode parentNode)
    {
      this.ProcessNodePre(reader, parentNode);
      reader.ReadNextLine();
      while (!reader.EndOfFile && !reader.NodeParentEnd)
      {
        if (reader.NodeParentStart)
          this.TraverseUnhandledNodes(reader);
        else
          this.ProcessInnerNode(reader, parentNode);
        reader.ReadNextLine();
      }
      this.ProcessNodePost(reader, parentNode);
    }

    protected virtual void ProcessNodePre(AseReader reader, AseNode parentNode)
    {
    }

    protected virtual void ProcessNodePost(AseReader reader, AseNode parentNode)
    {
    }

    protected virtual void ProcessInnerNode(AseReader reader, AseNode parentNode)
    {
    }
  }
}
