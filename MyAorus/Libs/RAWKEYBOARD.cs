// Decompiled with JetBrains decompiler
// Type: AorusKeyboard.RAWKEYBOARD
// Assembly: AorusFusion, Version=0.3.6.7, Culture=neutral, PublicKeyToken=null
// MVID: D989AA71-FCE9-4831-9F62-8BDE4C7ACEEE
// Assembly location: C:\Program Files (x86)\AorusFusion\AorusFusion.exe

using System.Runtime.InteropServices;

namespace AorusKeyboard
{
  public struct RAWKEYBOARD
  {
    [MarshalAs(UnmanagedType.U2)]
    public ushort MakeCode;
    [MarshalAs(UnmanagedType.U2)]
    public ushort Flags;
    [MarshalAs(UnmanagedType.U2)]
    public ushort Reserved;
    [MarshalAs(UnmanagedType.U2)]
    public ushort VKey;
    [MarshalAs(UnmanagedType.U4)]
    public uint Message;
    [MarshalAs(UnmanagedType.U4)]
    public uint ExtraInformation;
  }
}
