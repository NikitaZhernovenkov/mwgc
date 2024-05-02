// Decompiled with JetBrains decompiler
// Type: mwgc.RealEngine.RealMatrix
// Assembly: mwgc, Version=1.2.7318.38444, Culture=neutral, PublicKeyToken=null
// MVID: 7B2CC934-3582-440B-A9FF-077D52B1156A
// Assembly location: D:\Users\nikzh\Downloads\mwgc_details.exe

using System;
using System.IO;

#nullable disable
namespace mwgc.RealEngine
{
  public struct RealMatrix
  {
    public float[] m;

    public float Get(int i, int j) => this.m[i * 4 + j];

    public void Set(int i, int j, float newValue) => this.m[i * 4 + j] = newValue;

    public void Read(BinaryReader reader)
    {
      this.m = new float[16];
      for (int index = 0; index < 16; ++index)
        this.m[index] = reader.ReadSingle();
    }

    public void Write(BinaryWriter writer)
    {
      for (int index = 0; index < 16; ++index)
        writer.Write(this.m[index]);
    }

    public void MakeSingle()
    {
      this.m[0] = 1f;
      this.m[1] = 0.0f;
      this.m[2] = 0.0f;
      this.m[3] = 0.0f;
      this.m[4] = 0.0f;
      this.m[5] = 1f;
      this.m[6] = 0.0f;
      this.m[7] = 0.0f;
      this.m[8] = 0.0f;
      this.m[9] = 0.0f;
      this.m[10] = 1f;
      this.m[11] = 0.0f;
    }

    public void MultMatrix4f(float[] m1, float[] m2)
    {
      float[] numArray = new float[16];
      Array.Copy((Array) m1, (Array) numArray, 16);
      numArray[0] = (float) ((double) m1[0] * (double) m2[0] + (double) m1[1] * (double) m2[4] + (double) m1[2] * (double) m2[8]);
      numArray[1] = (float) ((double) m1[0] * (double) m2[1] + (double) m1[1] * (double) m2[5] + (double) m1[2] * (double) m2[9]);
      numArray[2] = (float) ((double) m1[0] * (double) m2[2] + (double) m1[1] * (double) m2[6] + (double) m1[2] * (double) m2[10]);
      numArray[4] = (float) ((double) m1[4] * (double) m2[0] + (double) m1[5] * (double) m2[4] + (double) m1[6] * (double) m2[8]);
      numArray[5] = (float) ((double) m1[4] * (double) m2[1] + (double) m1[5] * (double) m2[5] + (double) m1[6] * (double) m2[9]);
      numArray[6] = (float) ((double) m1[4] * (double) m2[2] + (double) m1[5] * (double) m2[6] + (double) m1[6] * (double) m2[10]);
      numArray[8] = (float) ((double) m1[8] * (double) m2[0] + (double) m1[9] * (double) m2[4] + (double) m1[10] * (double) m2[8]);
      numArray[9] = (float) ((double) m1[8] * (double) m2[1] + (double) m1[9] * (double) m2[5] + (double) m1[10] * (double) m2[9]);
      numArray[10] = (float) ((double) m1[8] * (double) m2[2] + (double) m1[9] * (double) m2[6] + (double) m1[10] * (double) m2[10]);
      Array.Copy((Array) numArray, (Array) m1, 16);
    }

    public void Rotate(float XAngle, float YAngle, float ZAngle)
    {
      this.MakeSingle();
      float num = 0.0174f;
      float[] numArray1 = new float[16];
      float[] numArray2 = new float[16];
      Array.Copy((Array) this.m, (Array) numArray1, 16);
      Array.Copy((Array) this.m, (Array) numArray2, 16);
      this.m[5] = (float) Math.Cos((double) XAngle * (double) num);
      this.m[6] = (float) Math.Sin((double) XAngle * (double) num);
      this.m[9] = (float) Math.Sin((double) XAngle * (double) num) * -1f;
      this.m[10] = (float) Math.Cos((double) XAngle * (double) num);
      numArray1[0] = (float) Math.Cos((double) YAngle * (double) num);
      numArray1[2] = (float) Math.Sin((double) YAngle * (double) num) * -1f;
      numArray1[8] = (float) Math.Sin((double) YAngle * (double) num);
      numArray1[10] = (float) Math.Cos((double) YAngle * (double) num);
      this.MultMatrix4f(this.m, numArray1);
      numArray2[0] = (float) Math.Cos((double) ZAngle * (double) num);
      numArray2[1] = (float) Math.Sin((double) ZAngle * (double) num);
      numArray2[4] = (float) Math.Sin((double) ZAngle * (double) num) * -1f;
      numArray2[5] = (float) Math.Cos((double) ZAngle * (double) num);
      this.MultMatrix4f(this.m, numArray2);
    }
  }
}
