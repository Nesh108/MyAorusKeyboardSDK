// Decompiled with JetBrains decompiler
// Type: AorusKeyboard.RAWMOUSE
// Assembly: AorusFusion, Version=0.3.6.7, Culture=neutral, PublicKeyToken=null
// MVID: D989AA71-FCE9-4831-9F62-8BDE4C7ACEEE
// Assembly location: C:\Program Files (x86)\AorusFusion\AorusFusion.exe

using System.Runtime.InteropServices;

namespace AorusKeyboard
{
  [StructLayout(LayoutKind.Explicit)]
  public struct RAWMOUSE
  {
    [MarshalAs(UnmanagedType.U2)]
    [FieldOffset(0)]
    public ushort usFlags;
    [MarshalAs(UnmanagedType.U4)]
    [FieldOffset(4)]
    public uint ulButtons;
    [FieldOffset(4)]
    public ushort usButtonFlags;
    [FieldOffset(6)]
    public ushort usButtonData;
    [MarshalAs(UnmanagedType.U4)]
    [FieldOffset(8)]
    public uint ulRawButtons;
    [FieldOffset(12)]
    public int lLastX;
    [FieldOffset(16)]
    public int lLastY;
    [MarshalAs(UnmanagedType.U4)]
    [FieldOffset(20)]
    public uint ulExtraInformation;
  }
}
