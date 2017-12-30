using System.Drawing;
using System.Collections.Generic;
using System;

namespace MyAorus
{
    class MyAorus
    {
        static int keyboardProfile = 1;

        static void Main(string[] args)
        {
            MyAorusHandler aorus = new MyAorusHandler();
            aorus.SetKeyboard((byte)keyboardProfile, CreateRandomizedLayout());
            
            // + 1 Needed for selecting the correct profile
            aorus.SelectKeyboardLightLayout(keyboardProfile + 1);
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