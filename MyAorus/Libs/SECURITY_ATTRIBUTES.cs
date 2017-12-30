// Decompiled with JetBrains decompiler
// Type: AorusKeyboard.SECURITY_ATTRIBUTES
// Assembly: AorusFusion, Version=0.3.6.7, Culture=neutral, PublicKeyToken=null
// MVID: D989AA71-FCE9-4831-9F62-8BDE4C7ACEEE
// Assembly location: C:\Program Files (x86)\AorusFusion\AorusFusion.exe

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
