// Decompiled with JetBrains decompiler
// Type: AorusKeyboard.RAWINPUTDEVICE
// Assembly: AorusFusion, Version=0.3.6.7, Culture=neutral, PublicKeyToken=null
// MVID: D989AA71-FCE9-4831-9F62-8BDE4C7ACEEE
// Assembly location: C:\Program Files (x86)\AorusFusion\AorusFusion.exe

using System;
using System.Runtime.InteropServices;

namespace AorusKeyboard
{
  public struct RAWINPUTDEVICE
  {
    [MarshalAs(UnmanagedType.U2)]
    public ushort usUsagePage;
    [MarshalAs(UnmanagedType.U2)]
    public ushort usUsage;
    [MarshalAs(UnmanagedType.U4)]
    public int dwFlags;
    public IntPtr hwndTarget;
  }
}
