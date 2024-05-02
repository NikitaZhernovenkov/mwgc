// Decompiled with JetBrains decompiler
// Type: mwgc.RealEngine.RealVertex
// Assembly: mwgc, Version=1.2.7318.38444, Culture=neutral, PublicKeyToken=null
// MVID: 7B2CC934-3582-440B-A9FF-077D52B1156A
// Assembly location: D:\Users\nikzh\Downloads\mwgc_details.exe

using System.IO;

#nullable disable
namespace mwgc.RealEngine
{
  public struct RealVertex
  {
    public RealVector3 Position;
    public RealVector3 Normal;
    public int Diffuse;
    public RealVector2 UV;
    public RealVector2 UV1;
    public RealVector2 UV2;
    public RealVector2 UV3;
    private bool _withNormal;
    private int _maxUV;

    public void Initialize(bool withNormal, int maxUV)
    {
      this._withNormal = withNormal;
      this._maxUV = maxUV;
    }

    public void Read(BinaryReader reader, bool withNormal) => this.Read(reader, withNormal, 0);

    public void Read(BinaryReader reader, bool withNormal, int maxUV)
    {
      this._withNormal = withNormal;
      this._maxUV = maxUV;
      this.Position.Read(reader);
      if (this._withNormal)
        this.Normal.Read(reader);
      this.Diffuse = reader.ReadInt32();
      this.UV.Read(reader);
      if (this._maxUV > 0)
        this.UV1.Read(reader);
      if (this._maxUV > 1)
        this.UV2.Read(reader);
      if (this._maxUV <= 2)
        return;
      this.UV3.Read(reader);
    }

    public void Write(BinaryWriter writer)
    {
      this.Position.Write(writer);
      if (this._withNormal)
        this.Normal.Write(writer);
      writer.Write(this.Diffuse);
      this.UV.Write(writer);
      if (this._maxUV > 0)
        this.UV1.Write(writer);
      if (this._maxUV > 1)
        this.UV2.Write(writer);
      if (this._maxUV <= 2)
        return;
      this.UV3.Write(writer);
    }

    public override int GetHashCode()
    {
      return (this.Position.GetHashCode().ToString() + ":" + this.Normal.GetHashCode().ToString() + ":" + this.Diffuse.GetHashCode().ToString() + ":" + this.UV.GetHashCode().ToString() + ":" + this.UV1.GetHashCode().ToString() + ":" + this.UV2.GetHashCode().ToString() + ":" + this.UV3.GetHashCode().ToString()).GetHashCode();
    }
  }
}
