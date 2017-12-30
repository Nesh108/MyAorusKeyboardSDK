// Decompiled with JetBrains decompiler
// Type: AorusKeyboard.Native
// Assembly: AorusFusion, Version=0.3.6.7, Culture=neutral, PublicKeyToken=null
// MVID: D989AA71-FCE9-4831-9F62-8BDE4C7ACEEE
// Assembly location: C:\Program Files (x86)\AorusFusion\AorusFusion.exe

using System;
using System.Runtime.InteropServices;

namespace AorusKeyboard
{
  public class Native
  {
    public const int INVALID_HANDLE_VALUE = -1;
    public const int DIGCF_PRESENT = 2;
    public const int DIGCF_DEVICEINTERFACE = 16;
    public static Native.Struct_Device[] usbDevices;
    public static Native.KB_Struct_Macro[] StructMacro;
    public static Native.Struct_Profile[] StructProfile;
    public static byte[] macroName;
    public static int[] btnMacroIndex;
    public static bool[] IsMacro;
    public static int[] IndexOfHotkeyMacro;
    public static int CurrentProfile;
    public static int ChangeButtonIndex;
    public static string ChangeButtonTitle;
    public static int realX;
    public static int realY;
    public static string[] TitleStr;
    public static string[] MacroListStr;
    public static string[] HotkeyListStr;
    public static string[] BasicListStr;
    public static string[] BasicListStrKrypton;
    public static string[] BasicListPicStr;
    public static string[] ModeStr;
    public static string[] SDStr;
    public static string[] MouseString;
    public static string[] KeyboardString;
    public static string[] TimeIntervalStr;
    public static string[] KeyboardMouseXYStr;
    public static string[] RunModeStr;
    public static string ImportStr;
    public static string ExportStr;
    public static string[] TimeStr;
    public static string StartStr;
    public static string StopStr;
    public static string DeleteStr;
    public static string DeleteAllStr;
    public static string UsePauseStr;
    public static string NotUsePauseStr;
    public static string MouseInputStr;
    public static string KeyboardInputStr;
    public static string Totallist;
    public static int newIconIndex;
    public static string[] IconPath;
    public static bool isHideWindow;
    public static int newTimeInterval;
    public static string TimeEdit;
    public static string importFileName;
    public static string exportFileName;
    public static string importPicFileName;
    public static double XPos;
    public static double YPos;
    public static int dropOption;
    public static int osdG1;
    public static int osdG2;
    public static int osdG3;
    public static int osdG4;
    public static int osdG5;
    public static string newXPos;
    public static string newYPos;
    public static string iconDefault;
    public static string iconCustom;
    public static bool isMouseLeft;
    public static ushort MakeCode;
    public static ushort Flags;
    public static ushort Reserved;
    public static ushort VKey;
    public static uint Message;
    public static uint ExtraInformation;
    public static Native.KeymappingAddress[] keyaddress;
    public static Native.profileinfo[] profileinfotemp;
    public static Native.profileinfo[] profileinfos;
    public static string DialogType;
    public static int deviceCount;
    public static string[] deviceId;
    public static bool enablemainTab;
    public static int currentId;
    public static int lastId;
    public static string NotebookDevices;
    public static string appStr;
    public static string already;
    public static string CleanA;
    public static string update;
    public static string Error;
    public static string osLanguage;
    public static bool existHoltek;
    public static bool isRecording;
    public static int keyboardonly;
    public static bool Pause2Stop;
    public static bool isslash;
    public static bool isleftshift;
    public static bool isrightshift;
    public static bool isleftctrl;
    public static bool isrightctrl;
    public static bool isleftalt;
    public static bool isrightalt;
    public static bool islwin;
    public static bool isrwin;
    public static bool isenter;
    public static bool isnumenter;
    public static bool isup;
    public static bool isdown;
    public static bool isleft;
    public static bool isright;
    public static bool istap;
    public static bool isesc;
    public static bool isback;
    public static int optionKeyboardMouse;
    public static int optionTimePeriod;
    public static int optionRun;
    public static int optionPause;
    public static int optionStartStop;
    public static bool bTimerStart;
    public static bool isRecordPeriodTime;
    public static int lastSelectedIndex;
    public static int nowSelectedIndex;
    public static int editMacroIndex;
    public static int now25Index;
    public static bool CheckIsRawFlash;
    public static bool CheckIsRawFlashCypress;
    public static string[] keyMapping;
    public static string[] editMacrosTips;
    public static bool profile0;
    public static bool profile1;
    public static bool profile2;
    public static bool profile3;
    public static bool profile4;
    public static bool profile5;
    public static bool MacrokeysEnabled;
    public static bool isWaiting;
    public static bool isExistRecordingDlg;
    public static bool hasChangeMacroDetails;
    public static int NowBasicIndex;
    public static string edit;
    public static int deviceTotal;
    public static string[] kryptonset;
    public static string kryptonbrightness;
    public static string kryptonroll;
    public static string take;
    public static string take2;
    public static string HintClean;
    public static string take1;
    public static string restore;
    public static string clean;
    public static int groupid;
    public static int selectby;
    public static bool doSwitchTab;
    public static string[] kmString;
    public static string line;
    public static string txtFirmwareUpdate;
    public static string updateFirmwareString;
    public static bool firstLoadMacrosList;
    public static bool isKrypton;
    public static bool isWheelUp;
    public static string currentDevice;
    public static string lastDevice;
    public static bool afterAssign;
    public static bool afterEraseMemory;
    public static int PreviousProfile;

    [DllImport("setupapi.dll")]
    public static extern IntPtr SetupDiGetClassDevs(ref Guid classGuid, int enumerator, IntPtr hwndParent, int flags);

    [DllImport("setupapi.dll", CharSet = CharSet.Auto, SetLastError = true)]
    public static extern bool SetupDiEnumDeviceInterfaces(IntPtr DeviceInfoSet, Native.SP_DEVINFO_DATA deviceInfoData, ref Guid interfaceClassGuid, int memberIndex, Native.SP_DEVICE_INTERFACE_DATA deviceInterfaceData);

    [DllImport("setupapi.dll")]
    public static extern bool SetupDiOpenDeviceInfo(IntPtr deviceInfoSet, string deviceInstanceId, IntPtr hwndParent, int openFlags, Native.SP_DEVINFO_DATA deviceInfoData);

    [DllImport("setupapi.dll", CharSet = CharSet.Auto, SetLastError = true)]
    public static extern bool SetupDiGetDeviceInterfaceDetail(IntPtr deviceInfoSet, Native.SP_DEVICE_INTERFACE_DATA deviceInterfaceData, IntPtr deviceInterfaceDetailData, int deiceInterfaceDetailData, ref int requiredSize, Native.SP_DEVINFO_DATA deviceInfoData);

    [DllImport("setupapi.dll", CharSet = CharSet.Auto, SetLastError = true)]
    private static extern bool SetupDiGetDeviceRegistryProperty(IntPtr DeviceInfoSet, Native.SP_DEVINFO_DATA deviceInfoData, int property, out int propertyRegDataType, IntPtr propertyBuffer, int propertyBufferSize, out int requiredSize);

    [DllImport("setupapi.dll")]
    private static extern uint SetupDiDestroyDeviceInfoList(IntPtr deviceInfoSet);

    [DllImport("hid.dll", SetLastError = true)]
    public static extern bool HidD_GetFeature(IntPtr hidDeviceObject, ref byte lpReportBuffer, int reportBufferLength);

    public struct profileinfo
    {
      public bool enable;
      public byte ledstatus;
      public int velocity;
      public byte ledcolor;
      public string colorfile;
    }

    public struct KB_Struct_Macro
    {
      public int HaveMacro;
      public string MacroListString;
      public int ImageIndex;
    }

    public struct Struct_Profile
    {
      public int[] isMacro;
      public long[] MacroRealAddress;
      public int[] FunctionMap;
      public int[] IconPos;
      public string[] Name;
      public byte r;
      public byte g;
      public byte b;
      public int[] Listindex;
    }

    public struct KeymappingAddress
    {
      public byte[] DataBuf;
      public byte[] ismacro;
      public byte[] Macroindex;
    }

    public struct Struct_Device
    {
      public IntPtr m_HandleProfile;
      public int DeviceID;
      public string DeviceString;
      public int iconindex;
    }

    [StructLayout(LayoutKind.Sequential)]
    public class SP_DEVINFO_DATA
    {
      private int cbSize = Marshal.SizeOf(typeof (Native.SP_DEVINFO_DATA));
      private Guid classGuid = Guid.Empty;
      private int devInst;
      private int reserved;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 2)]
    public struct SP_DEVICE_INTERFACE_DETAIL_DATA
    {
      public int cbSize;
      private short devicePath;
    }

    [StructLayout(LayoutKind.Sequential)]
    public class SP_DEVICE_INTERFACE_DATA
    {
      private int cbSize = Marshal.SizeOf(typeof (Native.SP_DEVICE_INTERFACE_DATA));
      private Guid interfaceClassGuid = Guid.Empty;
      private int flags;
      private int reserved;
    }
  }
}
