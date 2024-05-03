// Decompiled with JetBrains decompiler
// Type: mwgc.UniqueList
// Assembly: mwgc, Version=1.2.8468.42059, Culture=neutral, PublicKeyToken=null
// MVID: 82A161B2-3E36-490A-94BF-0C902BEC9901
// Assembly location: D:\Users\nikzh\Downloads\mwgc_vertices.exe

using System.Collections;

namespace mwgc
{
  public class UniqueList
  {
    private Hashtable ht;
    private ArrayList list;

    public UniqueList()
    {
      this.ht = new Hashtable();
      this.list = new ArrayList();
    }

    public int Add(object item)
    {
      if (this.ht.ContainsKey(item))
        return (int) this.ht[item];
      int num = this.list.Add(item);
      this.ht.Add(item, (object) num);
      return num;
    }

    public int Count => this.list.Count;

    public object this[int index]
    {
      get => this.list[index];
      set
      {
        bool flag = false;
        if (this.ht.ContainsKey(this.list[index]))
          flag = true;
        if (flag)
          this.ht.Remove(this.list[index]);
        this.list.RemoveAt(index);
        this.list.Insert(index, value);
        if (!flag)
          return;
        this.ht.Add(value, (object) index);
      }
    }
  }
}
