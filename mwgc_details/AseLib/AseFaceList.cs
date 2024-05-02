// Decompiled with JetBrains decompiler
// Type: mwgc.AseLib.AseFaceList
// Assembly: mwgc, Version=1.2.7318.38444, Culture=neutral, PublicKeyToken=null
// MVID: 7B2CC934-3582-440B-A9FF-077D52B1156A
// Assembly location: D:\Users\nikzh\Downloads\mwgc_details.exe

#nullable disable
namespace mwgc.AseLib
{
  public class AseFaceList : AseNode
  {
    protected AseFace[] _faces;

    public int Count
    {
      get => this._faces.Length;
      set => this._faces = new AseFace[value];
    }

    public AseFace this[int index]
    {
      get => this._faces[index];
      set => this._faces[index] = value;
    }

    protected override void ProcessInnerNode(AseReader reader, AseNode parentNode)
    {
      if (!(reader.NodeName == "MESH_FACE"))
        return;
      AseStringTokenizer aseStringTokenizer = new AseStringTokenizer(reader.NodeData);
      string s = aseStringTokenizer.GetNext();
      if (s.EndsWith(":"))
        s = s.Substring(0, s.Length - 1).Trim();
      int index1 = int.Parse(s);
      for (int index2 = 0; index2 < 3; ++index2)
      {
        string next = aseStringTokenizer.GetNext();
        int num = int.Parse(aseStringTokenizer.GetNext());
        switch (next)
        {
          case "A:":
            this._faces[index1].A = num;
            break;
          case "B:":
            this._faces[index1].B = num;
            break;
          case "C:":
            this._faces[index1].C = num;
            break;
        }
      }
      for (int index3 = 0; index3 < 3; ++index3)
      {
        string next = aseStringTokenizer.GetNext();
        bool flag = int.Parse(aseStringTokenizer.GetNext()) != 0;
        switch (next)
        {
          case "AB:":
            this._faces[index1].EdgeAB = flag;
            break;
          case "BC:":
            this._faces[index1].EdgeBC = flag;
            break;
          case "CA:":
            this._faces[index1].EdgeCA = flag;
            break;
        }
      }
      while (aseStringTokenizer.HasMore())
      {
        string next = aseStringTokenizer.GetNext();
        if (next.StartsWith("*"))
        {
          switch (next)
          {
            case "*MESH_SMOOTHING":
              if (!aseStringTokenizer.Peek().StartsWith("*"))
              {
                string[] strArray = aseStringTokenizer.GetNext().Split(',');
                this._faces[index1].SmoothingCount = strArray.Length;
                for (int index4 = 0; index4 < strArray.Length; ++index4)
                  this._faces[index1][index4] = int.Parse(strArray[index4]);
                continue;
              }
              continue;
            case "*MESH_MTLID":
              if (!aseStringTokenizer.Peek().StartsWith("*"))
              {
                this._faces[index1].MaterialID = int.Parse(aseStringTokenizer.GetNext());
                continue;
              }
              continue;
            default:
              continue;
          }
        }
      }
    }
  }
}
