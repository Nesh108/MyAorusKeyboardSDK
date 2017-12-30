using System;

namespace AorusKeyboard
{
  public struct SECURITY_ATTRIBUTES
  {
    public int nLength;
    public IntPtr lpSecurityDescriptor;
    public int bInheritHandle;
  }
}
