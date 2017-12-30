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
