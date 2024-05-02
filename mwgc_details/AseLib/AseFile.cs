// Decompiled with JetBrains decompiler
// Type: mwgc.AseLib.AseFile
// Assembly: mwgc, Version=1.2.7318.38444, Culture=neutral, PublicKeyToken=null
// MVID: 7B2CC934-3582-440B-A9FF-077D52B1156A
// Assembly location: D:\Users\nikzh\Downloads\mwgc_details.exe

using System.IO;

#nullable disable
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
