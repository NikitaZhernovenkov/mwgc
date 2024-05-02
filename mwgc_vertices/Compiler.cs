// Decompiled with JetBrains decompiler
// Type: mwgc.Compiler
// Assembly: mwgc, Version=1.2.8468.42059, Culture=neutral, PublicKeyToken=null
// MVID: 82A161B2-3E36-490A-94BF-0C902BEC9901
// Assembly location: D:\Users\nikzh\Downloads\mwgc_vertices.exe

using mwgc.AseLib;
using mwgc.RawGeometry;
using mwgc.RealEngine;
using System;
using System.IO;
using System.Reflection;

namespace mwgc
{
  internal class Compiler
  {
    public static CompilerOptions Options = new CompilerOptions();

    private static void PrintBanner()
    {
      Console.WriteLine("NFS:MW Geometry Compiler (mwgc) " + Assembly.GetExecutingAssembly().GetName().Version.ToString() + " (Custom Version)");
      Console.WriteLine("Copyright(C) 2005 - 2006, AruTec Inc. (Arushan), All Rights Reserved.");
      Console.WriteLine("Copyright(C) 2018 nlgzrgn.");
      Console.WriteLine("Contact: oneforaru at gmail dot com (bug reports only), nlgzrgn#7138 at Discord");
      Console.WriteLine();
      Console.WriteLine("Disclaimer: This program is provided as is without any warranties of any kind.");
      Console.WriteLine();
    }

    private static void PrintUsage()
    {
      Console.WriteLine("Usage:   mwrc <options> source.mwr [geometry.bin]");
      Console.WriteLine();
      Console.WriteLine("Options: -xname <name>     Set the X-Name for the model");
      Console.WriteLine("         -matlist          Provides a list of materials");
      Console.WriteLine("         -xlink            Provides a list of geometry crosslinks");
      Console.WriteLine("         -verbose          Show verbose output");
      Console.WriteLine("         -nowait           Don't wait after conversion");
      Console.WriteLine("         -help/-h/-?       Display this help screen");
      Console.WriteLine();
    }

    public static void VerboseOutput(string output)
    {
      if (Compiler.Options["verbose"] == null)
        return;
      Console.WriteLine("(verbose) " + output);
    }

    public static void ErrorOutput(string output) => Console.WriteLine("(error)   " + output);

    public static void WarningOutput(string output) => Console.WriteLine("(warning) " + output);

    [STAThread]
    private static int Main(string[] args)
    {
      if (!Compiler.Options.CollectOptions(args))
      {
        Compiler.PrintBanner();
        Compiler.PrintUsage();
        return 1;
      }
      Compiler.PrintBanner();
      Compiler.VerboseOutput("Source: " + Compiler.Options["source"]);
      Compiler.VerboseOutput("Target: " + Compiler.Options["target"]);
      Compiler.VerboseOutput("X-Name: " + Compiler.Options["xname"]);
      Compiler.VerboseOutput("Loading source...");
      DataCollector dataCollector = new DataCollector();
      RealGeometryFile realGeometryFile = (RealGeometryFile) null;
      FileInfo fileInfo = new FileInfo(Compiler.Options["source"]);
      if (fileInfo.Extension.ToLower() == ".mwr")
      {
        RawGeometryFile rawGeom = new RawGeometryFile();
        rawGeom.Read(Compiler.Options["source"]);
        Compiler.VerboseOutput("Collecting data [Most Wanted Raw]...");
        try
        {
          realGeometryFile = dataCollector.Collect(rawGeom);
        }
        catch (Exception ex)
        {
          Compiler.ErrorOutput("Exception: " + ex.Message);
          return 1;
        }
      }
      else if (fileInfo.Extension.ToLower() == ".ase")
      {
        AseFile aseFile = new AseFile();
        try
        {
          aseFile.Open(Compiler.Options["source"]);
        }
        catch (Exception ex)
        {
          Compiler.ErrorOutput("ASE Exception: " + ex.Message);
          return 1;
        }
        Compiler.VerboseOutput("Collecting data [Ascii Scene Export]...");
        try
        {
          realGeometryFile = dataCollector.Collect(aseFile);
        }
        catch (Exception ex)
        {
          Compiler.ErrorOutput("Exception: " + ex.Message);
        }
      }
      else
      {
        Compiler.ErrorOutput("Unsupported source file extension.");
        return 1;
      }
      if (realGeometryFile == null)
      {
        Compiler.ErrorOutput("Failed to collect data.");
        return 1;
      }
      Compiler.VerboseOutput("Writing target file...");
      realGeometryFile.Save(Compiler.Options["target"]);
      Compiler.VerboseOutput("Completed successfully.");
      if (Compiler.Options["nowait"] == null)
      {
        Console.WriteLine();
        Console.WriteLine("Press any key to continue...");
        Console.ReadLine();
      }
      return 0;
    }
  }
}
