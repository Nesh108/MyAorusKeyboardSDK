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
