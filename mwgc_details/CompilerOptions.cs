// Decompiled with JetBrains decompiler
// Type: mwgc.CompilerOptions
// Assembly: mwgc, Version=1.2.7318.38444, Culture=neutral, PublicKeyToken=null
// MVID: 7B2CC934-3582-440B-A9FF-077D52B1156A
// Assembly location: D:\Users\nikzh\Downloads\mwgc_details.exe

using System;
using System.Collections;

#nullable disable
namespace mwgc
{
  internal class CompilerOptions
  {
    private Hashtable options = new Hashtable();

    public string this[string key]
    {
      get
      {
        return this.options.ContainsKey((object) key) ? this.options[(object) key].ToString() : (string) null;
      }
      set
      {
        if (this.options.ContainsKey((object) key))
          this.options[(object) key] = (object) value;
        else
          this.options.Add((object) key, (object) value);
      }
    }

    public bool CollectOptions(string[] args)
    {
      Queue queue = new Queue((ICollection) args);
      bool flag = false;
      ArrayList arrayList = new ArrayList((ICollection) new string[5]
      {
        "xname",
        "matlist",
        "xlink",
        "source",
        "target"
      });
      try
      {
        do
        {
          string str = "";
          if (queue.Count > 0)
            str = queue.Dequeue().ToString();
          else
            flag = true;
          if (!flag && (str[0] == '-' || str[0] == '/'))
          {
            string key = str.Substring(1);
            if (key == "-")
              flag = true;
            else if (arrayList.Contains((object) key))
              this.options.Add((object) key, (object) queue.Dequeue().ToString());
            else
              this.options.Add((object) key, (object) 1);
          }
          else
          {
            flag = true;
            if (queue.Count > 1)
              return false;
            if (!this.options.ContainsKey((object) "source"))
              this.options.Add((object) "source", (object) str);
            if (queue.Count > 0)
              this.options.Add((object) "target", (object) queue.Dequeue().ToString());
          }
        }
        while (queue.Count > 0);
        if (!this.options.ContainsKey((object) "target"))
          this.options.Add((object) "target", (object) "geometry.bin");
        if (!this.options.ContainsKey((object) "source") || !this.options.ContainsKey((object) "target") || this.options.ContainsKey((object) "help") || this.options.ContainsKey((object) "h"))
          return false;
        if (this.options.ContainsKey((object) "?"))
          return false;
      }
      catch
      {
        return false;
      }
      return true;
    }

    public void CollectExtraOptions()
    {
      if (this["xname"] != null)
        return;
      Console.Write("X-Name: ");
      this["xname"] = Console.ReadLine();
    }
  }
}
