// Decompiled with JetBrains decompiler
// Type: mwgc.RealEngine.RealGeometryPartInfo
// Assembly: mwgc, Version=1.2.8468.42059, Culture=neutral, PublicKeyToken=null
// MVID: 82A161B2-3E36-490A-94BF-0C902BEC9901
// Assembly location: D:\Users\nikzh\Downloads\mwgc_vertices.exe

using System.IO;

#nullable disable
namespace mwgc.RealEngine
{
  public struct RealGeometryPartInfo
  {
    public int Null1;
    public int Null2;
    public int Null3;
    public int Unk1;
    public uint Hash;
    public int TriangleCount;
    public byte Null4;
    public byte TextureCount;
    public byte ShaderCount;
    public byte Null5;
    public int Null6;
    public RealVector4 BoundMin;
    public RealVector4 BoundMax;
    public RealMatrix Transform;
    public int Null7;
    public int Null8;
    public int Unk2;
    public int Unk3;
    public int Null9;
    public int Unk4_MW;
    public float Unk5_MW;
    public float Unk6_MW;
    public FixedLenString PartName;
    public uint[] Textures;
    public uint[] Shaders;
    public RealMountPoint[] MountPoints;

    public void Read(BinaryReader reader)
    {
      this.Null1 = reader.ReadInt32();
      this.Null2 = reader.ReadInt32();
      this.Null3 = reader.ReadInt32();
      this.Unk1 = reader.ReadInt32();
      this.Hash = reader.ReadUInt32();
      this.TriangleCount = reader.ReadInt32();
      this.Null4 = reader.ReadByte();
      this.TextureCount = reader.ReadByte();
      this.ShaderCount = reader.ReadByte();
      this.Null5 = reader.ReadByte();
      this.Null6 = reader.ReadInt32();
      this.BoundMin.Read(reader);
      this.BoundMax.Read(reader);
      this.Transform.Read(reader);
      this.Null7 = reader.ReadInt32();
      this.Null8 = reader.ReadInt32();
      this.Unk2 = reader.ReadInt32();
      this.Unk3 = reader.ReadInt32();
      this.Null9 = reader.ReadInt32();
      this.Unk4_MW = reader.ReadInt32();
      this.Unk5_MW = reader.ReadSingle();
      this.Unk6_MW = reader.ReadSingle();
      this.PartName = new FixedLenString(reader);
    }

    public void Write(BinaryWriter writer)
    {
      writer.Write(this.Null1);
      writer.Write(this.Null2);
      writer.Write(this.Null3);
      writer.Write(this.Unk1);
      writer.Write(this.Hash);
      writer.Write(this.TriangleCount);
      writer.Write(this.Null4);
      writer.Write(this.TextureCount);
      writer.Write(this.ShaderCount);
      writer.Write(this.Null5);
      writer.Write(this.Null6);
      this.BoundMin.Write(writer);
      this.BoundMax.Write(writer);
      this.Transform.Write(writer);
      writer.Write(this.Null7);
      writer.Write(this.Null8);
      writer.Write(this.Unk2);
      writer.Write(this.Unk3);
      writer.Write(this.Null9);
      writer.Write(this.Unk4_MW);
      writer.Write(this.Unk5_MW);
      writer.Write(this.Unk6_MW);
      this.PartName.Write(writer);
    }

    public void ReadTextures(BinaryReader reader)
    {
      this.Textures = new uint[(int) this.TextureCount];
      for (int index = 0; index < (int) this.TextureCount; ++index)
      {
        this.Textures[index] = reader.ReadUInt32();
        reader.ReadInt32();
      }
    }

    public void WriteTextures(BinaryWriter writer)
    {
      for (int index = 0; index < (int) this.TextureCount; ++index)
      {
        writer.Write(this.Textures[index]);
        writer.Write(0);
      }
    }

    public void ReadShaders(BinaryReader reader)
    {
      this.Shaders = new uint[(int) this.ShaderCount];
      for (int index = 0; index < (int) this.ShaderCount; ++index)
      {
        this.Shaders[index] = reader.ReadUInt32();
        reader.ReadInt32();
      }
    }

    public void WriteShaders(BinaryWriter writer)
    {
      for (int index = 0; index < (int) this.ShaderCount; ++index)
      {
        writer.Write(this.Shaders[index]);
        writer.Write(0);
      }
    }

    public void ReadMountPoints(BinaryReader reader, RealChunk chunk)
    {
      int length = (chunk.EndOffset - (int) reader.BaseStream.Position) / 80;
      this.MountPoints = new RealMountPoint[length];
      for (int index = 0; index < length; ++index)
      {
        this.MountPoints[index] = new RealMountPoint();
        this.MountPoints[index].Read(reader);
      }
    }

    public void WriteMountPoints(BinaryWriter writer)
    {
      for (int index = 0; index < this.MountPoints.Length; ++index)
        this.MountPoints[index].Write(writer);
    }
  }
}
