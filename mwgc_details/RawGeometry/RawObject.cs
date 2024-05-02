// Decompiled with JetBrains decompiler
// Type: mwgc.RawGeometry.RawObject
// Assembly: mwgc, Version=1.2.7318.38444, Culture=neutral, PublicKeyToken=null
// MVID: 7B2CC934-3582-440B-A9FF-077D52B1156A
// Assembly location: D:\Users\nikzh\Downloads\mwgc_details.exe

using System.IO;

#nullable disable
namespace mwgc.RawGeometry
{
  public class RawObject
  {
    public RawVertex[] Vertices;
    public RawFace[] Faces;

    public void Read(BinaryReader br, int numVertices, int numFaces)
    {
      this.Vertices = new RawVertex[numVertices];
      for (int index = 0; index < numVertices; ++index)
        this.Vertices[index].Read(br);
      this.Faces = new RawFace[numFaces];
      for (int index = 0; index < numFaces; ++index)
        this.Faces[index].Read(br);
    }
  }
}
