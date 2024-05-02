// Decompiled with JetBrains decompiler
// Type: mwgc.AseLib.AseNormals
// Assembly: mwgc, Version=1.2.8468.42059, Culture=neutral, PublicKeyToken=null
// MVID: 82A161B2-3E36-490A-94BF-0C902BEC9901
// Assembly location: D:\Users\nikzh\Downloads\mwgc_vertices.exe

namespace mwgc.AseLib
{
  public class AseNormals : AseNode
  {
    public static readonly AseNormals Instance = new AseNormals();

    protected override void ProcessInnerNode(AseReader reader, AseNode parentNode)
    {
      AseMesh aseMesh = parentNode as AseMesh;
      if (!(reader.NodeName == "MESH_FACENORMAL"))
        return;
      AseStringTokenizer aseStringTokenizer1 = new AseStringTokenizer(reader.NodeData);
      int index1 = int.Parse(aseStringTokenizer1.GetNext());
      AseFace face = aseMesh.FaceList[index1] with
      {
        NormalFace = new AseVertex(float.Parse(aseStringTokenizer1.GetNext()), float.Parse(aseStringTokenizer1.GetNext()), float.Parse(aseStringTokenizer1.GetNext()))
      };
      for (int index2 = 0; index2 < 3; ++index2)
      {
        reader.ReadNextLine();
        AseStringTokenizer aseStringTokenizer2 = new AseStringTokenizer(reader.NodeData);
        if (reader.NodeName == "MESH_VERTEXNORMAL")
        {
          int num = int.Parse(aseStringTokenizer2.GetNext());
          if (num == aseMesh.FaceList[index1].A)
            face.NormalA = new AseVertex(float.Parse(aseStringTokenizer2.GetNext()), float.Parse(aseStringTokenizer2.GetNext()), float.Parse(aseStringTokenizer2.GetNext()));
          else if (num == aseMesh.FaceList[index1].B)
            face.NormalB = new AseVertex(float.Parse(aseStringTokenizer2.GetNext()), float.Parse(aseStringTokenizer2.GetNext()), float.Parse(aseStringTokenizer2.GetNext()));
          else if (num == aseMesh.FaceList[index1].C)
            face.NormalC = new AseVertex(float.Parse(aseStringTokenizer2.GetNext()), float.Parse(aseStringTokenizer2.GetNext()), float.Parse(aseStringTokenizer2.GetNext()));
        }
        else
          --index2;
      }
      aseMesh.FaceList[index1] = face;
    }
  }
}
