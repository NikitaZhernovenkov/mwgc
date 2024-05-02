// Decompiled with JetBrains decompiler
// Type: mwgc.AseLib.AseFile
// Assembly: mwgc, Version=1.2.8468.42059, Culture=neutral, PublicKeyToken=null
// MVID: 82A161B2-3E36-490A-94BF-0C902BEC9901
// Assembly location: D:\Users\nikzh\Downloads\mwgc_vertices.exe

using System.IO;

namespace mwgc.AseLib
{
  public class AseFile : AseRoot
  {
    public AseFile()
    {
    }

    public AseFile(string filename) => this.Open(filename);

    public void Open(string filename)
    {
      StreamReader reader1 = new StreamReader(filename);
      AseReader reader2 = new AseReader((TextReader) reader1);
      reader2.ReadNextLine();
      if (reader2.NodeName != "3DSMAX_ASCIIEXPORT")
        throw new AseException("Not a valid ASE file.");
      if (int.Parse(reader2.NodeData) != 200)
        throw new AseException("The version of the ASE file is not supported.");
      this.ProcessNode(reader2, (AseNode) null);
      reader1.Close();
    }
  }
}
