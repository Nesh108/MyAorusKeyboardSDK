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
