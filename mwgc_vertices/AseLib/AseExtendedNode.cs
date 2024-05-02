// Decompiled with JetBrains decompiler
// Type: mwgc.AseLib.AseExtendedNode
// Assembly: mwgc, Version=1.2.8468.42059, Culture=neutral, PublicKeyToken=null
// MVID: 82A161B2-3E36-490A-94BF-0C902BEC9901
// Assembly location: D:\Users\nikzh\Downloads\mwgc_vertices.exe

using System;
using System.Collections;

namespace mwgc.AseLib
{
  public abstract class AseExtendedNode : AseNode
  {
    protected Hashtable _nodeParsers;

    public AseExtendedNode() => this._nodeParsers = new Hashtable();

    protected void AddNodeParser(string nodeName, Type parser)
    {
      this._nodeParsers.Add((object) nodeName, (object) parser);
    }

    protected void AddNodeParser(string nodeName, AseNode parser)
    {
      this._nodeParsers.Add((object) nodeName, (object) parser);
    }

    protected AseNode GetParser(string nodeName)
    {
      object nodeParser = this._nodeParsers[(object) nodeName];
      if (nodeParser == null)
        return (AseNode) null;
      return (object) (nodeParser as Type) != null ? (nodeParser as Type).GetConstructor(Type.EmptyTypes).Invoke((object[]) null) as AseNode : nodeParser as AseNode;
    }

    public override void ProcessNode(AseReader reader, AseNode parentNode)
    {
      this.ProcessNodePre(reader, parentNode);
      reader.ReadNextLine();
      while (!reader.EndOfFile && !reader.NodeParentEnd)
      {
        AseNode parser = this.GetParser(reader.NodeName);
        if (parser == null)
        {
          if (reader.NodeParentStart)
            this.TraverseUnhandledNodes(reader);
          else
            this.ProcessInnerNode(reader, parentNode);
        }
        else
          parser.ProcessNode(reader, (AseNode) this);
        reader.ReadNextLine();
      }
      this.ProcessNodePost(reader, parentNode);
    }
  }
}
