using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Drawing;
using System.Management;
using System.Threading;

namespace MyAorus
{
    internal class MyAorus
    {
        public static int ThreadDelay = 20000;
        public static int[] BatteryLevels = { 3, 6, 9, 12, 15, 17, 19, 21 };
        public static Color[] BatteryLevelsColors = { Color.Red, Color.OrangeRed, Color.Orange, Color.Yellow, Color.GreenYellow, Color.Green, Color.DodgerBlue, Color.Blue };
        public static Color BatteryChargingColor = Color.Purple;
        public static Color BatteryChargedColor = Color.DarkGreen;
        public static Color BatteryDischargedColor = Color.DarkRed;
        public static bool ShowChargingStatus = true;
        public static bool KeepCurrentBrightness = true;
        public static ArrayList MovieModeProcesses = new ArrayList(new string[] { });
        public static int AllowedFullscreenOffset = 0;

        private static readonly int KeyboardProfile = 1;
        private static MyAorusHandler _aorus;
        private static Dictionary<AorusKeys, Color> _layout;
        private static int _selectedBrightness = 100;
        private static int _previousBatteryBlocks = 0;
        private static bool _previousChargingStatus = false;
        private static bool _isKeyboardInMovieMode = false;
        private static bool _isBatteryCharging = false;
        private static int _batteryValue = 0;

        private static string _prevMsg = "";

        private static OBSHandler _obsHandler;

        private static void Main()
        {
            Init();
            _aorus = new MyAorusHandler();
            _obsHandler = new OBSHandler();
            _aorus.SelectKeyboardLightLayout(KeyboardProfile + 1, _selectedBrightness);
            BatteryRunner();
        }

        private static void Init()
        {
            _selectedBrightness = int.Parse(ConfigurationManager.AppSettings["default_brightness"]);
            ThreadDelay = int.Parse(ConfigurationManager.AppSettings["thread_delay"]);
            ShowChargingStatus = bool.Parse(ConfigurationManager.AppSettings["show_charging_status"]);
            KeepCurrentBrightness = bool.Parse(ConfigurationManager.AppSettings["keep_current_brightness"]);
            MovieModeProcesses = new ArrayList(ConfigurationManager.AppSettings["movie_mode_processes"].Split(';'));
            AllowedFullscreenOffset = int.Parse(ConfigurationManager.AppSettings["allowed_fullscreen_offset"]);
            BatteryChargingColor = Color.FromName(ConfigurationManager.AppSettings["battery_charging_color"]);
            Console.WriteLine("-----------Configuration Loaded-----------\n" +
                              "- Selected Brightness: {0}\n" +
                              "- Thread Delay: {1}ms\n" +
                              "- Show Charging Status: {2}\n" +
                              "- Keep Current Brightness: {3}\n" +
                              "- Movie Mode Processes: {4}\n" +
                              "- Allowed Fullscreen Offset: {5}px\n" +
                              "- Battery Charging Color: {6}\n" +
                              "------------------------------------------",
                _selectedBrightness, ThreadDelay, ShowChargingStatus, KeepCurrentBrightness, string.Join("|", MovieModeProcesses.ToArray()), AllowedFullscreenOffset, BatteryChargingColor.ToString());
        }

        private static void BatteryRunner()
        {
            while (true)
            {
                if (!_obsHandler.IsConnected)
                {
                    _obsHandler.Init();
                }
                else
                {
                    _obsHandler.CheckStatus();
                }

                MainLoop();
                Thread.Sleep(ThreadDelay);
            }
        }

        public static void MainLoop(bool forceRefresh = false)
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
                        _isBatteryCharging = property.Value.ToString().Equals("2");
                    }

                    if (property.Name.Equals("EstimatedChargeRemaining"))
                    {
                        _batteryValue = int.Parse(property.Value.ToString());
                        break;
                    }
                }
            }
            CheckFullscreenProcesses();
            if (!_isKeyboardInMovieMode)
            {
                UpdateKeyboardLayout(_isBatteryCharging, _batteryValue, BatteryDischargedColor, BatteryChargedColor, forceRefresh);
            }
        }

        private static void CheckFullscreenProcesses()
        {
            // If process is fullscreen, turn off brightness, else turn it back on
            if (FullscreenManager.IsForegroundFullScreen(MovieModeProcesses, AllowedFullscreenOffset))
            {
                if (!_isKeyboardInMovieMode)
                {
                    _aorus.SelectKeyboardLightLayout(KeyboardProfile + 1, 0);
                    _isKeyboardInMovieMode = true;
                }
            }
            else if (_isKeyboardInMovieMode)
            {
                _aorus.SelectKeyboardLightLayout(KeyboardProfile + 1, _selectedBrightness);
                _isKeyboardInMovieMode = false;
            }
        }

        private static void UpdateKeyboardLayout(bool isBatteryCharging, int batteryValue, Color dischargedColor,
            Color chargedColor, bool forceRefresh)
        {
            if (!_isKeyboardInMovieMode)
            {
                if (KeepCurrentBrightness)
                {
                    _selectedBrightness = _aorus.GetCurrentBrightness();
                }

                int blocks = (batteryValue / 5) + 1;
                string msg = String.Format("Current Battery: {0}% - Status: {1} Charging", batteryValue,
                    isBatteryCharging ? "" : "Not");
                if (!_prevMsg.Equals(msg))
                {
                    Console.WriteLine(msg);
                    _prevMsg = msg;
                }
                if (forceRefresh || _previousBatteryBlocks != blocks || _previousChargingStatus != isBatteryCharging)
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

                    // Set color for the whole keyboard
                    batteryLevel = Math.Max(Math.Min(BatteryLevelsColors.Length - 1, batteryLevel), 0);
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
        }

        private static Dictionary<AorusKeys, Color> SingleStaticColor(Color c)
        {
            Dictionary<AorusKeys, Color> myLayout = new Dictionary<AorusKeys, Color>();
            foreach (AorusKeys k in Enum.GetValues(typeof(AorusKeys)))
            {
                myLayout[k] = c;
            }

            if (_obsHandler.IsStreaming)
            {
                myLayout[AorusKeys.NumPad7] = _obsHandler.StreamingColor;
                myLayout[AorusKeys.NumPad8] = _obsHandler.StreamingColor;
                myLayout[AorusKeys.NumPad9] = _obsHandler.StreamingColor;
                myLayout[AorusKeys.NumPad4] = _obsHandler.StreamingColor;
                myLayout[AorusKeys.NumPad5] = _obsHandler.StreamingColor;
                myLayout[AorusKeys.NumPad6] = _obsHandler.StreamingColor;
            }

            if (_obsHandler.IsRecording)
            {
                myLayout[AorusKeys.NumPad1] = _obsHandler.StreamingColor;
                myLayout[AorusKeys.NumPad2] = _obsHandler.StreamingColor;
                myLayout[AorusKeys.NumPad3] = _obsHandler.StreamingColor;
                myLayout[AorusKeys.Right] = _obsHandler.StreamingColor;
                myLayout[AorusKeys.NumPad0] = _obsHandler.StreamingColor;
                myLayout[AorusKeys.NumPadDel] = _obsHandler.StreamingColor;
            }
            return myLayout;
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
    }
}