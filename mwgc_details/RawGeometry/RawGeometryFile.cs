﻿// Decompiled with JetBrains decompiler
// Type: mwgc.RawGeometry.RawGeometryFile
// Assembly: mwgc, Version=1.2.7318.38444, Culture=neutral, PublicKeyToken=null
// MVID: 7B2CC934-3582-440B-A9FF-077D52B1156A
// Assembly location: D:\Users\nikzh\Downloads\mwgc_details.exe

using System.IO;

#nullable disable
namespace mwgc.RawGeometry
{
  public class RawGeometryFile
  {
    public RawHeader Header;
    public RawObject[] Objects;

    public void Read(string filename)
    {
      FileStream input = new FileStream(filename, FileMode.Open, FileAccess.Read);
      BinaryReader br = new BinaryReader((Stream) input);
      this.Header = new RawHeader();
      this.Header.Read(br);
      this.Objects = new RawObject[this.Header.NumObjects];
      for (int index = 0; index < this.Header.NumObjects; ++index)
      {
        this.Objects[index] = new RawObject();
        this.Objects[index].Read(br, this.Header.ObjHeaders[index].NumVertices, this.Header.ObjHeaders[index].NumFaces);
      }
      Compiler.VerboseOutput(string.Format("Loaded {0} materials", (object) this.Header.NumMaterials));
      for (int index = 0; index < this.Header.NumMaterials; ++index)
        Compiler.VerboseOutput(string.Format(" + {0}", (object) this.Header.MatNames[index].Data));
      Compiler.VerboseOutput(string.Format("Loaded {0} objects", (object) this.Header.NumObjects));
      for (int index = 0; index < this.Header.NumObjects; ++index)
        Compiler.VerboseOutput(string.Format(" + {0}", (object) this.Header.ObjHeaders[index].ObjName.Data));
      input.Close();
    }
  }
}
