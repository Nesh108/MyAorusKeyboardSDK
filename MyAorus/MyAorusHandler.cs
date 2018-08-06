using System;
using System.Collections.Generic;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Threading;
using AorusKeyboard;

namespace MyAorus
{
    class MyAorusHandler
    {
        public byte[] PictureMatrix = new byte[960];
        public IntPtr _deviceHandle_2016 = IntPtr.Zero;

        public MyAorusHandler()
        {
            InitKeyboard();
        }

        public void SetKeyboard(byte index, Dictionary<AorusKeys, Color> layout)
        {
            SetKeyboardLightLayout(index, layout);
        }

        public void SetKeyboardLightLayout(byte index, Dictionary<AorusKeys, Color> layout)
        {
            foreach (var k in layout.Keys)
            {
                ModifySingleKeyLight(Helpers.GetIdFromKey(k), layout[k]);
            }
            SetKeysMatrix(index);
        }

        private void ModifySingleKeyLight(int key, Color c)
        {
            PictureMatrix[key * 4] = (byte)key;
            PictureMatrix[key * 4 + 1] = c.R;
            PictureMatrix[key * 4 + 2] = c.G;
            PictureMatrix[key * 4 + 3] = c.B;
        }

        private int InitKeyboard()
        {
            bool flag = true;
            int num1 = 0;
            Guid guid = new Guid("4d1e55b2-f16f-11cf-88cb-001111000030");
            IntPtr classDevs = Native.SetupDiGetClassDevs(ref guid, 0, IntPtr.Zero, 18);
            classDevs.ToInt32();
            int memberIndex = 0;
            while (flag)
            {
                Native.SP_DEVICE_INTERFACE_DATA deviceInterfaceData = new Native.SP_DEVICE_INTERFACE_DATA();
                flag = Native.SetupDiEnumDeviceInterfaces(classDevs, (Native.SP_DEVINFO_DATA)null, ref guid, memberIndex, deviceInterfaceData);
                if (flag)
                {
                    Native.SP_DEVINFO_DATA deviceInfoData = new Native.SP_DEVINFO_DATA();
                    int requiredSize = 0;
                    Native.SetupDiGetDeviceInterfaceDetail(classDevs, deviceInterfaceData, IntPtr.Zero, 0, ref requiredSize, deviceInfoData);
                    IntPtr num2 = Marshal.AllocHGlobal(requiredSize);
                    Marshal.StructureToPtr((object)new Native.SP_DEVICE_INTERFACE_DETAIL_DATA()
                    {
                        cbSize = (IntPtr.Size != 8 ? 4 + Marshal.SystemDefaultCharSize : 8)
                    }, num2, false);
                    Native.SetupDiGetDeviceInterfaceDetail(classDevs, deviceInterfaceData, num2, requiredSize, ref requiredSize, deviceInfoData);
                    string stringAuto = Marshal.PtrToStringAuto((IntPtr)((int)num2 + Marshal.SizeOf(typeof(int))));
                    Marshal.FreeHGlobal(num2);
                    if (stringAuto.IndexOf("04d9") > 0 && stringAuto.IndexOf("8008") > 0 || stringAuto.IndexOf("1044") > 0 && stringAuto.IndexOf("7a38") > 0 || stringAuto.IndexOf("1044") > 0 && stringAuto.IndexOf("7a39") > 0)
                    {
                        SECURITY_ATTRIBUTES securityAttributes = new SECURITY_ATTRIBUTES();
                        securityAttributes.nLength = Marshal.SizeOf((object)securityAttributes);
                        securityAttributes.lpSecurityDescriptor = IntPtr.Zero;
                        securityAttributes.bInheritHandle = 0;
                        IntPtr file = Win32.CreateFile(stringAuto, 3221225472U, 3, IntPtr.Zero, 3, 0, 0);
                        if (file.ToInt32() != -1)
                        {
                            IntPtr PreparsedData = new IntPtr();
                            Win32.HidD_GetPreparsedData(file, ref PreparsedData);
                            HIDP_CAPS Capabilities = new HIDP_CAPS();
                            Win32.HidP_GetCaps(PreparsedData, ref Capabilities);
                            if ((int)Capabilities.FeatureReportByteLength > 0)
                            {
                                _deviceHandle_2016 = file;
                                break;
                            }
                        }
                    }
                    else if ((stringAuto.IndexOf("1044") <= 0 || stringAuto.IndexOf("7a03") <= 0) && (stringAuto.IndexOf("1044") <= 0 || stringAuto.IndexOf("7a04") <= 0) && stringAuto.IndexOf("1044") > 0)
                        stringAuto.IndexOf("7a06");
                    ++memberIndex;
                }
            }
            return num1;
        }

        public void SetKeysMatrix(byte index)
        {
            int sleep_time = 65;

            byte[] lpBuffer = new byte[70];
            for (int index1 = 0; index1 < 70; ++index1)
                lpBuffer[index1] = (byte)0;
            lpBuffer[1] = (byte)18;
            lpBuffer[2] = (byte)0;
            lpBuffer[3] = index;
            lpBuffer[4] = (byte)8;
            lpBuffer[5] = (byte)0;
            lpBuffer[8] = (byte)0;
            for (int index1 = 1; index1 <= 7; ++index1)
                lpBuffer[8] = (byte)((uint)lpBuffer[8] + (uint)lpBuffer[index1]);
            lpBuffer[8] = (byte)((uint)byte.MaxValue - (uint)lpBuffer[8]);
            Win32.HidD_SetFeature(_deviceHandle_2016, ref lpBuffer[0], 9);
            Thread.Sleep(sleep_time);

            uint lpNumberOfBytesWritten = 0;
            for (int index1 = 0; index1 < 70; ++index1)
                lpBuffer[index1] = (byte)0;
            for (int index1 = 0; index1 < 64; ++index1)
                lpBuffer[index1 + 1] = PictureMatrix[index1];
            if (!Win32.WriteFile(_deviceHandle_2016, lpBuffer, 65U, ref lpNumberOfBytesWritten, IntPtr.Zero))
                Marshal.ThrowExceptionForHR(Marshal.GetHRForLastWin32Error());
            Thread.Sleep(sleep_time);
            for (int index1 = 0; index1 < 64; ++index1)
                lpBuffer[index1 + 1] = PictureMatrix[index1 + 64];
            if (!Win32.WriteFile(_deviceHandle_2016, lpBuffer, 65U, ref lpNumberOfBytesWritten, IntPtr.Zero))
                Marshal.ThrowExceptionForHR(Marshal.GetHRForLastWin32Error());
            Thread.Sleep(sleep_time);
            for (int index1 = 0; index1 < 64; ++index1)
                lpBuffer[index1 + 1] = PictureMatrix[index1 + 128];
            if (!Win32.WriteFile(_deviceHandle_2016, lpBuffer, 65U, ref lpNumberOfBytesWritten, IntPtr.Zero))
                Marshal.ThrowExceptionForHR(Marshal.GetHRForLastWin32Error());
            Thread.Sleep(sleep_time);
            for (int index1 = 0; index1 < 64; ++index1)
                lpBuffer[index1 + 1] = PictureMatrix[index1 + 192];
            if (!Win32.WriteFile(_deviceHandle_2016, lpBuffer, 65U, ref lpNumberOfBytesWritten, IntPtr.Zero))
                Marshal.ThrowExceptionForHR(Marshal.GetHRForLastWin32Error());
            Thread.Sleep(sleep_time);
            for (int index1 = 0; index1 < 64; ++index1)
                lpBuffer[index1 + 1] = PictureMatrix[index1 + 256];
            if (!Win32.WriteFile(_deviceHandle_2016, lpBuffer, 65U, ref lpNumberOfBytesWritten, IntPtr.Zero))
                Marshal.ThrowExceptionForHR(Marshal.GetHRForLastWin32Error());
            Thread.Sleep(sleep_time);
            for (int index1 = 0; index1 < 64; ++index1)
                lpBuffer[index1 + 1] = PictureMatrix[index1 + 320];
            if (!Win32.WriteFile(_deviceHandle_2016, lpBuffer, 65U, ref lpNumberOfBytesWritten, IntPtr.Zero))
                Marshal.ThrowExceptionForHR(Marshal.GetHRForLastWin32Error());
            Thread.Sleep(sleep_time);
            for (int index1 = 0; index1 < 64; ++index1)
                lpBuffer[index1 + 1] = PictureMatrix[index1 + 384];
            if (!Win32.WriteFile(_deviceHandle_2016, lpBuffer, 65U, ref lpNumberOfBytesWritten, IntPtr.Zero))
                Marshal.ThrowExceptionForHR(Marshal.GetHRForLastWin32Error());
            Thread.Sleep(sleep_time);
            for (int index1 = 0; index1 < 64; ++index1)
                lpBuffer[index1 + 1] = PictureMatrix[index1 + 448];
            if (!Win32.WriteFile(_deviceHandle_2016, lpBuffer, 65U, ref lpNumberOfBytesWritten, IntPtr.Zero))
                Marshal.ThrowExceptionForHR(Marshal.GetHRForLastWin32Error());
            Thread.Sleep(sleep_time);
        }

        public void SelectKeyboardLightLayout(int customIndex, int brightnessLevel)
        {
            byte[] numArray = new byte[9];
            for (int index = 0; index < 9; ++index)
                numArray[index] = (byte)0;
            numArray[1] = (byte)8;
            numArray[2] = (byte)0;
            switch (customIndex)
            {
                case 1:
                    numArray[3] = (byte)51;
                    break;
                case 2:
                    numArray[3] = (byte)52;
                    break;
                case 3:
                    numArray[3] = (byte)53;
                    break;
                case 4:
                    numArray[3] = (byte)54;
                    break;
                default:
                    numArray[3] = (byte)55;
                    break;
            }
            numArray[4] = (byte)5;
            numArray[5] = (byte)brightnessLevel;
            numArray[6] = (byte)2;
            numArray[7] = (byte)1;
            numArray[8] = (byte)0;
            for (int index = 1; index <= 7; ++index)
                numArray[8] = (byte)((uint)numArray[8] + (uint)numArray[index]);
            numArray[8] = (byte)((uint)byte.MaxValue - (uint)numArray[8]);
            Win32.HidD_SetFeature(this._deviceHandle_2016, ref numArray[0], 9);
        }

        public void SetDefaultBreathingEffect()
        {
            byte[] numArray = new byte[9];
            for (int index = 0; index < 9; ++index)
                numArray[index] = (byte)0;
            numArray[1] = (byte)8;
            numArray[2] = (byte)0;
            numArray[3] = (byte)2;
            numArray[4] = (byte)5;
            numArray[5] = (byte)50;
            numArray[6] = (byte)8;
            numArray[7] = (byte)1;
            numArray[8] = (byte)0;
            for (int index = 1; index <= 7; ++index)
                numArray[8] = (byte)((uint)numArray[8] + (uint)numArray[index]);
            numArray[8] = (byte)((uint)byte.MaxValue - (uint)numArray[8]);
            Win32.HidD_SetFeature(this._deviceHandle_2016, ref numArray[0], 9);
            Thread.Sleep(65);
        }

        public void SetDefaultWaveEffect()
        {
            byte[] numArray = new byte[70];
            for (int index = 0; index < 70; ++index)
                numArray[index] = (byte)0;
            numArray[1] = (byte)8;
            numArray[2] = (byte)0;
            numArray[3] = (byte)3;
            numArray[4] = (byte)5;
            numArray[5] = (byte)50;
            numArray[6] = (byte)2;
            numArray[7] = (byte)1;
            numArray[8] = (byte)0;
            for (int index = 1; index <= 7; ++index)
                numArray[8] = (byte)((uint)numArray[8] + (uint)numArray[index]);
            numArray[8] = (byte)((uint)byte.MaxValue - (uint)numArray[8]);
            Win32.HidD_SetFeature(this._deviceHandle_2016, ref numArray[0], 9);
            Thread.Sleep(650);
        }

        public void SetDefaultAllGreen()
        {
            byte[] numArray = new byte[9];
            for (int index = 0; index < 9; ++index)
                numArray[index] = (byte)0;
            numArray[1] = (byte)8;
            numArray[2] = (byte)0;
            numArray[3] = (byte)1;
            numArray[4] = (byte)5;
            numArray[5] = (byte)50;
            numArray[6] = (byte)2;
            numArray[7] = (byte)1;
            numArray[8] = (byte)0;
            for (int index = 1; index <= 7; ++index)
                numArray[8] = (byte)((uint)numArray[8] + (uint)numArray[index]);
            numArray[8] = (byte)((uint)byte.MaxValue - (uint)numArray[8]);
            Win32.HidD_SetFeature(this._deviceHandle_2016, ref numArray[0], 9);
        }


        public int GetCurrentBrightness()
        {
            byte[] Buffer = new byte[9];
            for (int index = 0; index < 9; ++index)
            {
                Buffer[index] = (byte)0;
            }
            Win32.HidD_GetFeature(_deviceHandle_2016, ref Buffer[0], 9);
            return Buffer[5];
        }
    }
}
