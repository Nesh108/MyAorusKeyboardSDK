// Decompiled with JetBrains decompiler
// Type: AorusKeyboard.HIDP_CAPS
// Assembly: AorusFusion, Version=0.3.6.7, Culture=neutral, PublicKeyToken=null
// MVID: D989AA71-FCE9-4831-9F62-8BDE4C7ACEEE
// Assembly location: C:\Program Files (x86)\AorusFusion\AorusFusion.exe

using System.Runtime.InteropServices;

namespace AorusKeyboard
{
    public struct HIDP_CAPS
    {
        public short Usage;
        public short UsagePage;
        public short InputReportByteLength;
        public short OutputReportByteLength;
        public short FeatureReportByteLength;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 17)]
        private short[] Reserved;
        private short NumberLinkCollectionNodes;
        private short NumberInputButtonCaps;
        private short NumberInputValueCaps;
        private short NumberInputDataIndices;
        private short NumberOutputButtonCaps;
        private short NumberOutputValueCaps;
        private short NumberOutputDataIndices;
        private short NumberFeatureButtonCaps;
        private short NumberFeatureValueCaps;
        private short NumberFeatureDataIndices;
    }
}
