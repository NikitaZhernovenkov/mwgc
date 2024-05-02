// Decompiled with JetBrains decompiler
// Type: mwgc.AseLib.AseStringTokenizer
// Assembly: mwgc, Version=1.2.7318.38444, Culture=neutral, PublicKeyToken=null
// MVID: 7B2CC934-3582-440B-A9FF-077D52B1156A
// Assembly location: D:\Users\nikzh\Downloads\mwgc_details.exe

using System.Collections;

#nullable disable
namespace mwgc.AseLib
{
  public class AseStringTokenizer
  {
    private Queue _tokens;

    public AseStringTokenizer(string str)
      : this(str, ' ', '\t')
    {
    }

    public AseStringTokenizer(string str, params char[] split)
    {
      string[] strArray = str.Split(split);
      this._tokens = new Queue();
      foreach (string str1 in strArray)
      {
        if (str1 != "")
          this._tokens.Enqueue((object) str1);
      }
    }

    public string Peek() => this._tokens.Peek() as string;

    public string GetNext() => this._tokens.Dequeue() as string;

    public bool HasMore() => this._tokens.Count > 0;
  }
}
