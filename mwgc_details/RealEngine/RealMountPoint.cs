// Decompiled with JetBrains decompiler
// Type: mwgc.RealEngine.RealMountPoint
// Assembly: mwgc, Version=1.2.7318.38444, Culture=neutral, PublicKeyToken=null
// MVID: 7B2CC934-3582-440B-A9FF-077D52B1156A
// Assembly location: D:\Users\nikzh\Downloads\mwgc_details.exe

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
