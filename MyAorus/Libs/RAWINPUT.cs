// Decompiled with JetBrains decompiler
// Type: AorusKeyboard.RAWINPUT
// Assembly: AorusFusion, Version=0.3.6.7, Culture=neutral, PublicKeyToken=null
// MVID: D989AA71-FCE9-4831-9F62-8BDE4C7ACEEE
// Assembly location: C:\Program Files (x86)\AorusFusion\AorusFusion.exe

using System.Runtime.InteropServices;

namespace AorusKeyboard
{
  [StructLayout(LayoutKind.Explicit)]
  internal struct RAWINPUT
  {
    [FieldOffset(0)]
    public RAWINPUTHEADER Header;
    [FieldOffset(16)]
    public RAWMOUSE Mouse;
    [FieldOffset(16)]
    public RAWKEYBOARD Keyboard;
    [FieldOffset(16)]
    public RAWHID HID;
  }
}
