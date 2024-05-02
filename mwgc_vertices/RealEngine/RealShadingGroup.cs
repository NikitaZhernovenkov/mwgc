// Decompiled with JetBrains decompiler
// Type: mwgc.RealEngine.RealShadingGroup
// Assembly: mwgc, Version=1.2.8468.42059, Culture=neutral, PublicKeyToken=null
// MVID: 82A161B2-3E36-490A-94BF-0C902BEC9901
// Assembly location: D:\Users\nikzh\Downloads\mwgc_vertices.exe

using System.IO;

#nullable disable
namespace mwgc.RealEngine
{
  public struct RealShadingGroup
  {
    public RealVector3 BoundsMin;
    public int Length;
    public RealVector3 BoundsMax;
    public int Offset;
    public int Flags;
    public byte TextureIndex0;
    public byte TextureIndex1;
    public byte TextureIndex2;
    public byte TextureIndex3;
    public byte TextureIndex4;
    public byte ShaderIndex0;
    public short Padding1;
    public int Null1;
    public int Null2;
    public int Null3;
    public int Null4;
    public int Unk1;
    public int Null5;
    public int VertexCount;
    public int TriangleCount;
    public int Null6;
    public int Null7;
    public int Null8;
    public int Null9;
    public int Null10;
    public int Null11;
    public int Null12;

    public int TextureIndex
    {
      get => (int) this.TextureIndex0;
      set
      {
      }
    }

    public int ShaderIndex
    {
      get => (int) this.ShaderIndex0;
      set
      {
      }
    }

    public bool UseNormalMap => (this.Flags & 10616832) != 0;

    public bool EnableLighting => (this.Flags & 256) != 0;

    public void Read(BinaryReader reader)
    {
      this.BoundsMin.Read(reader);
      this.BoundsMax.Read(reader);
      this.TextureIndex0 = reader.ReadByte();
      this.TextureIndex1 = reader.ReadByte();
      this.TextureIndex2 = reader.ReadByte();
      this.TextureIndex3 = reader.ReadByte();
      this.TextureIndex4 = reader.ReadByte();
      this.ShaderIndex0 = reader.ReadByte();
      this.Padding1 = reader.ReadInt16();
      this.Null1 = reader.ReadInt32();
      this.Null2 = reader.ReadInt32();
      this.Null3 = reader.ReadInt32();
      this.Null4 = reader.ReadInt32();
      this.Unk1 = reader.ReadInt32();
      this.Null5 = reader.ReadInt32();
      this.Flags = reader.ReadInt32();
      this.VertexCount = reader.ReadInt32();
      this.TriangleCount = reader.ReadInt32();
      this.Offset = reader.ReadInt32();
      this.Null6 = reader.ReadInt32();
      this.Null7 = reader.ReadInt32();
      this.Null8 = reader.ReadInt32();
      this.Null9 = reader.ReadInt32();
      this.Null10 = reader.ReadInt32();
      this.Length = reader.ReadInt32();
      this.Null11 = reader.ReadInt32();
      this.Null12 = reader.ReadInt32();
    }

    public void Write(BinaryWriter writer)
    {
      this.BoundsMin.Write(writer);
      this.BoundsMax.Write(writer);
      writer.Write(this.TextureIndex0);
      writer.Write(this.TextureIndex1);
      writer.Write(this.TextureIndex2);
      writer.Write(this.TextureIndex3);
      writer.Write(this.TextureIndex4);
      writer.Write(this.ShaderIndex0);
      writer.Write(this.Padding1);
      writer.Write(this.Null1);
      writer.Write(this.Null2);
      writer.Write(this.Null3);
      writer.Write(this.Null4);
      writer.Write(this.Unk1);
      writer.Write(this.Null5);
      writer.Write(this.Flags);
      writer.Write(this.VertexCount);
      writer.Write(this.TriangleCount);
      writer.Write(this.Offset);
      writer.Write(this.Null6);
      writer.Write(this.Null7);
      writer.Write(this.Null8);
      writer.Write(this.Null9);
      writer.Write(this.Null10);
      writer.Write(this.Length);
      writer.Write(this.Null11);
      writer.Write(this.Null12);
    }
  }
}
