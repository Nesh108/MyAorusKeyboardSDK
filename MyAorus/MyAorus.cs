using System.Drawing;
using System.Collections.Generic;
using System;
using System.Management;
using System.Threading;

namespace MyAorus
{
    class MyAorus
    {
        static int keyboardProfile = 1;
        static MyAorusHandler aorus;
        static int ThreadDelay = 60000;
        static Dictionary<AorusKeys, Color> layout;

        static void Main(string[] args)
        {
            aorus = new MyAorusHandler();
            layout = CreateRandomizedLayout();
            BatteryRunner(ref layout, Color.DarkRed, Color.DarkGreen);
        }

        static void BatteryRunner(ref Dictionary<AorusKeys, Color> layout, Color dischargedColor, Color chargedColor)
        {
            while (true)
            {
                ObjectQuery query = new ObjectQuery("Select * FROM Win32_Battery");
                ManagementObjectSearcher searcher = new ManagementObjectSearcher(query);

                ManagementObjectCollection collection = searcher.Get();

                foreach (ManagementObject mo in collection)
                {
                    foreach (PropertyData property in mo.Properties)
                    {
                        if (property.Name.Equals("EstimatedChargeRemaining"))
                        {
                            int batterValue = int.Parse(property.Value.ToString());
                            Console.WriteLine("Current Battery: {0}%", batterValue);
                            int blocks = batterValue / 5;
                            layout[AorusKeys.Escape] = blocks > 1 ? chargedColor : dischargedColor;
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
                            break;
                        }
                    }
                }
                aorus.SetKeyboard((byte)keyboardProfile, layout);

                // + 1 Needed for selecting the correct profile
                aorus.SelectKeyboardLightLayout(keyboardProfile + 1);

                Thread.Sleep(ThreadDelay);
            }
        }
        static Dictionary<AorusKeys, Color> CreateRandomizedLayout()
        {
            Dictionary<AorusKeys, Color> layout = new Dictionary<AorusKeys, Color>();
            Random r = new Random();
            foreach(AorusKeys k in Enum.GetValues(typeof(AorusKeys)))
            {
                layout[k] = Color.FromArgb(r.Next(0, 255), r.Next(0, 255), r.Next(0, 255));
            }
            return layout;
        }
    }
}