using System;
using System.Collections.Generic;
using System.Drawing;
using System.Management;
using System.Threading;

namespace MyAorus
{
    internal class MyAorus
    {
        private static readonly int KeyboardProfile = 1;
        private static MyAorusHandler _aorus;
        private static readonly int ThreadDelay = 20000;
        private static Dictionary<AorusKeys, Color> _layout;
        private static readonly bool KeepCurrentBrightness = true;
        private static int _selectedBrightness = 20;
        private static int _previousBatteryBlocks = 0;
        private static bool _previousChargingStatus = false;
        private static readonly int[] BatteryLevels = { 3, 6, 9, 12, 15, 17, 19, 21 };
        private static readonly Color[] BatteryLevelsColors = { Color.Red, Color.OrangeRed, Color.Orange, Color.Yellow, Color.GreenYellow, Color.Green, Color.DodgerBlue, Color.Blue };
        private static readonly Color BatteryChargingColor = Color.Purple;
        private static readonly bool ShowChargingStatus = true;

        private static void Main(string[] args)
        {
            _aorus = new MyAorusHandler();
            BatteryRunner(ref _layout, Color.DarkRed, Color.DarkGreen);
        }

        private static void BatteryRunner(ref Dictionary<AorusKeys, Color> layout, Color dischargedColor, Color chargedColor)
        {
            bool isBatteryCharging = false;
            int batteryValue = 0;
            while (true)
            {
                ObjectQuery query = new ObjectQuery("Select * FROM Win32_Battery");
                ManagementObjectSearcher searcher = new ManagementObjectSearcher(query);

                ManagementObjectCollection collection = searcher.Get();

                foreach (ManagementObject mo in collection)
                {
                    foreach (PropertyData property in mo.Properties)
                    {
                        if (property.Name.Equals("BatteryStatus"))
                        {
                            isBatteryCharging = property.Value.ToString().Equals("2");
                        }

                        if (property.Name.Equals("EstimatedChargeRemaining"))
                        {
                            batteryValue = int.Parse(property.Value.ToString());
                            break;
                        }

                    }
                }

                UpdateKeyboardLayout(isBatteryCharging, batteryValue, dischargedColor, chargedColor);
                Thread.Sleep(ThreadDelay);
            }
        }

        private static void UpdateKeyboardLayout(bool isBatteryCharging, int batteryValue, Color dischargedColor,
            Color chargedColor)
        {
            if (KeepCurrentBrightness)
            {
                _selectedBrightness = _aorus.GetCurrentBrightness();
            }

            int blocks = (batteryValue / 5) + 1;

            Console.WriteLine("Current Battery: {0}% - Status: {1} Charging", batteryValue,
                isBatteryCharging ? "" : "Not");
            if (_previousBatteryBlocks != blocks || _previousChargingStatus != isBatteryCharging)
            {
                _previousBatteryBlocks = blocks;
                _previousChargingStatus = isBatteryCharging;
                int batteryLevel = 0;
                for (int i = 0; i < BatteryLevels.Length; i++)
                {
                    if (BatteryLevels[i] >= blocks)
                    {
                        batteryLevel = i;
                        break;
                    }
                }

                _layout = SingleStaticColor(BatteryLevelsColors[batteryLevel]);

                if (ShowChargingStatus && isBatteryCharging)
                {
                    _layout[AorusKeys.Escape] = BatteryChargingColor;
                }
                else
                {
                    _layout[AorusKeys.Escape] = blocks > 1 ? chargedColor : dischargedColor;
                }
                _layout[AorusKeys.F1] = blocks > 2 ? chargedColor : dischargedColor;
                _layout[AorusKeys.F2] = blocks > 3 ? chargedColor : dischargedColor;
                _layout[AorusKeys.F3] = blocks > 4 ? chargedColor : dischargedColor;
                _layout[AorusKeys.F4] = blocks > 5 ? chargedColor : dischargedColor;
                _layout[AorusKeys.F5] = blocks > 6 ? chargedColor : dischargedColor;
                _layout[AorusKeys.F6] = blocks > 7 ? chargedColor : dischargedColor;
                _layout[AorusKeys.F7] = blocks > 8 ? chargedColor : dischargedColor;
                _layout[AorusKeys.F8] = blocks > 9 ? chargedColor : dischargedColor;
                _layout[AorusKeys.F9] = blocks > 10 ? chargedColor : dischargedColor;
                _layout[AorusKeys.F10] = blocks > 11 ? chargedColor : dischargedColor;
                _layout[AorusKeys.F11] = blocks > 12 ? chargedColor : dischargedColor;
                _layout[AorusKeys.F12] = blocks > 13 ? chargedColor : dischargedColor;
                _layout[AorusKeys.Pause] = blocks > 14 ? chargedColor : dischargedColor;
                _layout[AorusKeys.Delete] = blocks > 15 ? chargedColor : dischargedColor;
                _layout[AorusKeys.Home] = blocks > 16 ? chargedColor : dischargedColor;
                _layout[AorusKeys.PageUp] = blocks > 17 ? chargedColor : dischargedColor;
                _layout[AorusKeys.PageDown] = blocks > 18 ? chargedColor : dischargedColor;
                _layout[AorusKeys.End] = blocks > 19 ? chargedColor : dischargedColor;



                _aorus.SetKeyboard((byte)KeyboardProfile, _layout);

                // + 1 Needed for selecting the correct profile
                _aorus.SelectKeyboardLightLayout(KeyboardProfile + 1, _selectedBrightness);
            }
        }

        private static Dictionary<AorusKeys, Color> CreateRandomizedLayout()
        {
            Dictionary<AorusKeys, Color> myLayout = new Dictionary<AorusKeys, Color>();
            Random r = new Random();
            foreach (AorusKeys k in Enum.GetValues(typeof(AorusKeys)))
            {
                myLayout[k] = Color.FromArgb(r.Next(0, 255), r.Next(0, 255), r.Next(0, 255), r.Next(0, 255));
            }
            return myLayout;
        }

        private static Dictionary<AorusKeys, Color> SingleStaticColor(Color c)
        {
            Dictionary<AorusKeys, Color> myLayout = new Dictionary<AorusKeys, Color>();
            foreach (AorusKeys k in Enum.GetValues(typeof(AorusKeys)))
            {
                myLayout[k] = c;
            }
            return myLayout;
        }

        private static int RandomBrightness()
        {
            return new Random().Next(0, 100);
        }
    }
}