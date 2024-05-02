// Decompiled with JetBrains decompiler
// Type: mwgc.RealEngine.RealGeometryInfo
// Assembly: mwgc, Version=1.2.7318.38444, Culture=neutral, PublicKeyToken=null
// MVID: 7B2CC934-3582-440B-A9FF-077D52B1156A
// Assembly location: D:\Users\nikzh\Downloads\mwgc_details.exe

using System.IO;

#nullable disable
namespace mwgc.RealEngine
{
  public class RealGeometryInfo
  {
    public int Null1;
    public int Null2;
    public int Unk1;
    public int PartCount;
    public FixedLenString RelFilePath;
    public FixedLenString ClassType;
    public int ExtChunkOffs;
    public int ExtChunkLen;
    public int Unk2;
    public int Null3;
    public int Null4;
    public int Null5;
    public int Null6_MW;
    public int Null7_MW;
    public int Null8_MW;
    public int Null9_MW;

    public void Read(BinaryReader reader)
    {
      this.Null1 = reader.ReadInt32();
      this.Null2 = reader.ReadInt32();
      this.Unk1 = reader.ReadInt32();
      this.PartCount = reader.ReadInt32();
      this.RelFilePath = new FixedLenString(reader, 56);
      this.ClassType = new FixedLenString(reader, 32);
      this.ExtChunkOffs = reader.ReadInt32();
      this.ExtChunkLen = reader.ReadInt32();
      this.Unk2 = reader.ReadInt32();
      this.Null3 = reader.ReadInt32();
      this.Null4 = reader.ReadInt32();
      this.Null5 = reader.ReadInt32();
      this.Null6_MW = reader.ReadInt32();
      this.Null7_MW = reader.ReadInt32();
      this.Null8_MW = reader.ReadInt32();
      this.Null9_MW = reader.ReadInt32();
    }

    public void Write(BinaryWriter writer)
    {
      writer.Write(this.Null1);
      writer.Write(this.Null2);
      writer.Write(this.Unk1);
      writer.Write(this.PartCount);
      this.RelFilePath.Write(writer);
      this.ClassType.Write(writer);
      writer.Write(this.ExtChunkOffs);
      writer.Write(this.ExtChunkLen);
      writer.Write(this.Unk2);
      writer.Write(this.Null3);
      writer.Write(this.Null4);
      writer.Write(this.Null5);
      writer.Write(this.Null6_MW);
      writer.Write(this.Null7_MW);
      writer.Write(this.Null8_MW);
      writer.Write(this.Null9_MW);
    }
  }
}
