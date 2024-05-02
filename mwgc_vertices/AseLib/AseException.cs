// Decompiled with JetBrains decompiler
// Type: mwgc.AseLib.AseException
// Assembly: mwgc, Version=1.2.8468.42059, Culture=neutral, PublicKeyToken=null
// MVID: 82A161B2-3E36-490A-94BF-0C902BEC9901
// Assembly location: D:\Users\nikzh\Downloads\mwgc_vertices.exe

using System;

namespace mwgc.AseLib
{
  public class AseException : Exception
  {
    public AseException(string message)
      : base(message)
    {
    }

    public AseException(string message, Exception innerException)
      : base(message, innerException)
    {
    }
  }
}
