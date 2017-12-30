using System;
using System.Runtime.InteropServices;
using System.Text;

namespace AorusKeyboard
{
    internal static class Win32
    {
        [DllImport("hid.dll", SetLastError = true)]
        internal static extern bool HidD_GetPreparsedData(IntPtr HidDeviceObject, ref IntPtr PreparsedData);

        [DllImport("hid.dll", SetLastError = true)]
        internal static extern bool HidD_GetFeature(IntPtr hidDeviceObject, ref byte lpReportBuffer, int reportBufferLength);

        [DllImport("hid.dll", SetLastError = true)]
        internal static extern bool HidD_SetFeature(IntPtr hidDeviceObject, ref byte lpReportBuffer, int reportBufferLength);

        [DllImport("hid.dll", SetLastError = true)]
        internal static extern bool HidD_SetOutputReport(IntPtr hidDeviceObject, ref byte lpReportBuffer, int reportBufferLength);

        [DllImport("hid.dll", SetLastError = true)]
        internal static extern int HidP_GetCaps(IntPtr PreparsedData, ref HIDP_CAPS Capabilities);

        [DllImport("hid.dll", SetLastError = true)]
        internal static extern bool HidD_GetAttributes(IntPtr HidDeviceObject, ref HIDD_ATTRIBUTES Attributes);

        [DllImport("hid.dll", SetLastError = true)]
        internal static extern bool HidD_GetSerialNumberString(IntPtr hidDeviceObject, byte[] buffer, uint bufferLength);

        [DllImport("hid.dll", SetLastError = true)]
        internal static extern bool HidD_GetProductString(IntPtr hidDeviceObject, byte[] buffer, uint bufferLength);

        [DllImport("User32.dll")]
        internal static extern bool RegisterRawInputDevices(RAWINPUTDEVICE[] pRawInputDevice, uint uiNumDevices, uint cbSize);

        [DllImport("user32")]
        internal static extern uint GetRawInputData(IntPtr hrawInput, uint command, IntPtr pData, ref uint size, int headerSize);

        [DllImport("kernel32.dll")]
        internal static extern IntPtr LoadLibrary(string dllToLoad);

        [DllImport("kernel32.dll")]
        internal static extern bool FreeLibrary(IntPtr hModule);

        [DllImport("kernel32")]
        internal static extern IntPtr GetProcAddress(IntPtr hModule, string procedureName);

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        internal static extern bool GetCursorPos(out POINTt pt);

        [DllImport("kernel32.dll", EntryPoint = "CreateFileW", CharSet = CharSet.Unicode, SetLastError = true)]
        internal static extern IntPtr CreateFile(string lpFileName, uint dwDesiredAccess, int dwShareMode, IntPtr lpSecurityAttributes, int dwCreationDisposition, int dwFlagsAndAttributes, int hTemplateFile);

        [DllImport("user32.dll")]
        internal static extern int GetKeyNameText(int lParam, StringBuilder lpString, int nSize);

        [DllImport("kernel32.dll", SetLastError = true)]
        internal static extern bool ReadFile(IntPtr hFile, byte[] lpBuffer, uint NumberOfBytesToRead, ref uint pNumberOfBytesRead, IntPtr Overlapped);

        [DllImport("kernel32.dll", SetLastError = true)]
        internal static extern bool WriteFile(IntPtr hFile, byte[] lpBuffer, uint NumberOfBytesToWrite, ref uint lpNumberOfBytesWritten, IntPtr overlapped);

        [DllImport("user32.dll", SetLastError = true)]
        internal static extern bool SystemParametersInfo(int uAction, uint uParam, ref uint lpvParam, int fuWinIni);
    }
}
