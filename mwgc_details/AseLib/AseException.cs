// Decompiled with JetBrains decompiler
// Type: mwgc.AseLib.AseException
// Assembly: mwgc, Version=1.2.7318.38444, Culture=neutral, PublicKeyToken=null
// MVID: 7B2CC934-3582-440B-A9FF-077D52B1156A
// Assembly location: D:\Users\nikzh\Downloads\mwgc_details.exe

using System;

#nullable disable
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
