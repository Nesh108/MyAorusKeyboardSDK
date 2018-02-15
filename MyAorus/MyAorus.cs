using System;
using System.Collections.Generic;
using System.Drawing;
using System.Management;
using System.Threading;

namespace MyAorus
{
    internal class MyAorus
    {
        private static int keyboardProfile = 1;
        private static MyAorusHandler aorus;
        private static int threadDelay = 60000;
        private static Dictionary<AorusKeys, Color> layout;
        private static bool keepCurrentBrightness = true;
        private static int selectedBrightness = 20;
        private static int previousBatteryBlocks = 0;
        private static bool previousChargingStatus = false;
        private static int[] batteryLevels = new int[] { 4, 10, 17, 21 };
        private static Color[] batteryLevelsColors = new Color[] { Color.Red, Color.Orange, Color.Green, Color.Blue };
        private static bool showChargingStatus = true;

        private static void Main(string[] args)
        {
            aorus = new MyAorusHandler();
            BatteryRunner(ref layout, Color.DarkRed, Color.DarkGreen);
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
                Thread.Sleep(threadDelay);
            }
        }

        private static void UpdateKeyboardLayout(bool isBatteryCharging, int batteryValue, Color dischargedColor,
            Color chargedColor)
        {
            if (keepCurrentBrightness)
            {
                selectedBrightness = aorus.GetCurrentBrightness();
            }

            int blocks = (batteryValue / 5) + 1;

            Console.WriteLine("Current Battery: {0}% - Status: {1} Charging", batteryValue,
                isBatteryCharging ? "" : "Not");
            if (previousBatteryBlocks != blocks || previousChargingStatus != isBatteryCharging)
            {
                previousBatteryBlocks = blocks;
                previousChargingStatus = isBatteryCharging;
                int batteryLevel = 0;
                for (int i = 0; i < batteryLevels.Length; i++)
                {
                    if (batteryLevels[i] >= blocks)
                    {
                        batteryLevel = i;
                        break;
                    }
                }

                layout = SingleStaticColor(batteryLevelsColors[batteryLevel]);

                if (showChargingStatus && isBatteryCharging)
                {
                    layout[AorusKeys.Escape] = Color.Indigo;
                }
                else
                {
                    layout[AorusKeys.Escape] = blocks > 1 ? chargedColor : dischargedColor;
                }
                layout[AorusKeys.F1] = blocks > 2 ? chargedColor : dischargedColor;
                layout[AorusKeys.F2] = blocks > 3 ? chargedColor : dischargedColor;
                layout[AorusKeys.F3] = blocks > 4 ? chargedColor : dischargedColor;
                layout[AorusKeys.F4] = blocks > 5 ? chargedColor : dischargedColor;
                layout[AorusKeys.F5] = blocks > 6 ? chargedColor : dischargedColor;
                layout[AorusKeys.F6] = blocks > 7 ? chargedColor : dischargedColor;
                layout[AorusKeys.F7] = blocks > 8 ? chargedColor : dischargedColor;
                layout[AorusKeys.F8] = blocks > 9 ? chargedColor : dischargedColor;
                layout[AorusKeys.F9] = blocks > 10 ? chargedColor : dischargedColor;
                layout[AorusKeys.F10] = blocks > 11 ? chargedColor : dischargedColor;
                layout[AorusKeys.F11] = blocks > 12 ? chargedColor : dischargedColor;
                layout[AorusKeys.F12] = blocks > 13 ? chargedColor : dischargedColor;
                layout[AorusKeys.Pause] = blocks > 14 ? chargedColor : dischargedColor;
                layout[AorusKeys.Delete] = blocks > 15 ? chargedColor : dischargedColor;
                layout[AorusKeys.Home] = blocks > 16 ? chargedColor : dischargedColor;
                layout[AorusKeys.PageUp] = blocks > 17 ? chargedColor : dischargedColor;
                layout[AorusKeys.PageDown] = blocks > 18 ? chargedColor : dischargedColor;
                layout[AorusKeys.End] = blocks > 19 ? chargedColor : dischargedColor;



                aorus.SetKeyboard((byte)keyboardProfile, layout);

                // + 1 Needed for selecting the correct profile
                aorus.SelectKeyboardLightLayout(keyboardProfile + 1, selectedBrightness);
            }
        }

        private static Dictionary<AorusKeys, Color> CreateRandomizedLayout()
        {
            Dictionary<AorusKeys, Color> layout = new Dictionary<AorusKeys, Color>();
            Random r = new Random();
            foreach (AorusKeys k in Enum.GetValues(typeof(AorusKeys)))
            {
                layout[k] = Color.FromArgb(r.Next(0, 255), r.Next(0, 255), r.Next(0, 255), r.Next(0, 255));
            }
            return layout;
        }

        private static Dictionary<AorusKeys, Color> SingleStaticColor(Color c)
        {
            Dictionary<AorusKeys, Color> layout = new Dictionary<AorusKeys, Color>();
            foreach (AorusKeys k in Enum.GetValues(typeof(AorusKeys)))
            {
                layout[k] = c;
            }
            return layout;
        }

        private static int RandomBrightness()
        {
            return new Random().Next(0, 100);
        }
    }
}