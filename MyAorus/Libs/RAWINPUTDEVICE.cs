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
