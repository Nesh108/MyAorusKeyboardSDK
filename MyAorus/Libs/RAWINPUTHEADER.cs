// Decompiled with JetBrains decompiler
// Type: AorusKeyboard.RAWINPUTHEADER
// Assembly: AorusFusion, Version=0.3.6.7, Culture=neutral, PublicKeyToken=null
// MVID: D989AA71-FCE9-4831-9F62-8BDE4C7ACEEE
// Assembly location: C:\Program Files (x86)\AorusFusion\AorusFusion.exe

using System;
using System.Runtime.InteropServices;

namespace AorusKeyboard
{
  internal struct RAWINPUTHEADER
  {
    [MarshalAs(UnmanagedType.U4)]
    public int dwType;
    [MarshalAs(UnmanagedType.U4)]
    public int dwSize;
    public IntPtr hDevice;
    [MarshalAs(UnmanagedType.U4)]
    public int wParam;
  }
}
