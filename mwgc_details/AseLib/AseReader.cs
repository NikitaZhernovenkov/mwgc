// Decompiled with JetBrains decompiler
// Type: mwgc.AseLib.AseReader
// Assembly: mwgc, Version=1.2.7318.38444, Culture=neutral, PublicKeyToken=null
// MVID: 7B2CC934-3582-440B-A9FF-077D52B1156A
// Assembly location: D:\Users\nikzh\Downloads\mwgc_details.exe

using System.IO;

#nullable disable
namespace mwgc.AseLib
{
  public class AseReader
  {
    private TextReader _reader;
    private string _line;
    private string _nodeName;
    private string _nodeData;
    private bool _nodeParentStart;
    private bool _nodeParentEnd;

    public TextReader Reader => this._reader;

    public string Line => this._line;

    public string NodeName => this._nodeName;

    public string NodeData => this._nodeData;

    public bool NodeParentStart => this._nodeParentStart;

    public bool NodeParentEnd => this._nodeParentEnd;

    public AseReader(TextReader reader) => this._reader = reader;

    public bool EndOfFile => this._line == null;

    public void ReadNextLine()
    {
      do
      {
        string str = this._reader.ReadLine();
        if (str == null)
        {
          this._line = (string) null;
          return;
        }
        this._line = str.Trim();
        if (this._line == "}")
        {
          this._nodeName = (string) null;
          this._nodeData = (string) null;
          this._nodeParentStart = false;
          this._nodeParentEnd = true;
          return;
        }
      }
      while (!this._line.StartsWith("*"));
      int num1 = this._line.IndexOf(' ');
      int num2 = this._line.IndexOf('\t');
      int startIndex = num1;
      if (startIndex == -1)
      {
        startIndex = num2;
        if (startIndex == -1)
          startIndex = this._line.Length - 1;
      }
      else if (num2 != -1)
        startIndex = num1 < num2 ? num1 : num2;
      this._nodeName = this._line.Substring(1, startIndex - 1);
      this._nodeData = this._line.Substring(startIndex).Trim();
      if (this._nodeData.StartsWith("\"") && this._nodeData.EndsWith("\""))
        this._nodeData = this._nodeData.Substring(1, this._nodeData.Length - 2);
      this._nodeParentStart = this._line.EndsWith("{");
      if (this._nodeParentStart)
        this._nodeData = this._nodeData.Substring(0, this._nodeData.Length - 1).Trim();
      this._nodeParentEnd = false;
    }
  }
}
