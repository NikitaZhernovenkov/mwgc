// Decompiled with JetBrains decompiler
// Type: mwgc.UniqueList
// Assembly: mwgc, Version=1.2.7318.38444, Culture=neutral, PublicKeyToken=null
// MVID: 7B2CC934-3582-440B-A9FF-077D52B1156A
// Assembly location: D:\Users\nikzh\Downloads\mwgc_details.exe

using System.Collections;

#nullable disable
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
