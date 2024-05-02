// Decompiled with JetBrains decompiler
// Type: mwgc.RawGeometry.RawObject
// Assembly: mwgc, Version=1.2.8468.42059, Culture=neutral, PublicKeyToken=null
// MVID: 82A161B2-3E36-490A-94BF-0C902BEC9901
// Assembly location: D:\Users\nikzh\Downloads\mwgc_vertices.exe

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
