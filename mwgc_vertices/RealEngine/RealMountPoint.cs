// Decompiled with JetBrains decompiler
// Type: mwgc.RealEngine.RealMountPoint
// Assembly: mwgc, Version=1.2.8468.42059, Culture=neutral, PublicKeyToken=null
// MVID: 82A161B2-3E36-490A-94BF-0C902BEC9901
// Assembly location: D:\Users\nikzh\Downloads\mwgc_vertices.exe

using System.IO;

#nullable disable
namespace mwgc.RealEngine
{
  public struct RealMountPoint
  {
    public uint Hash;
    public int Null1;
    public int Null2;
    public int Null3;
    public RealMatrix Transform;
    public string AttachTo;

    public void Read(BinaryReader reader)
    {
      this.Hash = reader.ReadUInt32();
      this.Null1 = reader.ReadInt32();
      this.Null2 = reader.ReadInt32();
      this.Null3 = reader.ReadInt32();
      this.Transform.Read(reader);
    }

    public void Write(BinaryWriter writer)
    {
      writer.Write(this.Hash);
      writer.Write(this.Null1);
      writer.Write(this.Null2);
      writer.Write(this.Null3);
      this.Transform.Write(writer);
    }
  }
}
