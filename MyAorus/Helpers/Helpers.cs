namespace MyAorus
{
    class Helpers
    {
        public static int GetIdFromKey(AorusKeys k)
        {
            switch(k)
            {
                case AorusKeys.LeftCtrl: return 6;
                case AorusKeys.LeftShift: return 7;
                case AorusKeys.CapsLock: return 8;
                case AorusKeys.Tab: return 9;
                case AorusKeys.Tilde: return 10;
                case AorusKeys.Escape: return 11;
                case AorusKeys.Fn: return 12;
                case AorusKeys.SpaceUK: return 13;
                case AorusKeys.A: return 14;
                case AorusKeys.Q: return 15;
                case AorusKeys.D1: return 16;
                case AorusKeys.F1: return 17;
                case AorusKeys.Win: return 18;
                case AorusKeys.Z: return 19;
                case AorusKeys.S: return 20;
                case AorusKeys.W: return 21;
                case AorusKeys.D2: return 22;
                case AorusKeys.F2: return 23;
                case AorusKeys.LeftAlt: return 24;
                case AorusKeys.X: return 25;
                case AorusKeys.D: return 26;
                case AorusKeys.E: return 27;
                case AorusKeys.D3: return 28;
                case AorusKeys.F3: return 29;
                case AorusKeys.C: return 31;
                case AorusKeys.F: return 32;
                case AorusKeys.R: return 33;
                case AorusKeys.D4: return 34;
                case AorusKeys.F4: return 35;
                case AorusKeys.V: return 37;
                case AorusKeys.G: return 38;
                case AorusKeys.T: return 39;
                case AorusKeys.D5: return 40;
                case AorusKeys.F5: return 41;
                case AorusKeys.Space: return 42;
                case AorusKeys.B: return 43;
                case AorusKeys.H: return 44;
                case AorusKeys.Y: return 45;
                case AorusKeys.D6: return 46;
                case AorusKeys.F6: return 47;
                case AorusKeys.N: return 49;
                case AorusKeys.J: return 50;
                case AorusKeys.U: return 51;
                case AorusKeys.D7: return 52;
                case AorusKeys.F7: return 53;
                case AorusKeys.M: return 55;
                case AorusKeys.K: return 56;
                case AorusKeys.I: return 57;
                case AorusKeys.D8: return 58;
                case AorusKeys.F8: return 59;
                case AorusKeys.RightAlt: return 60;
                case AorusKeys.Comma: return 61;
                case AorusKeys.L: return 62;
                case AorusKeys.O: return 63;
                case AorusKeys.D9: return 64;
                case AorusKeys.F9: return 65;
                case AorusKeys.App: return 66;
                case AorusKeys.Fullstop: return 67;
                case AorusKeys.Semi: return 68;
                case AorusKeys.P: return 69;
                case AorusKeys.D0: return 70;
                case AorusKeys.F10: return 71;
                case AorusKeys.RightCtrl: return 72;
                case AorusKeys.NumPadSlash: return 73;
                case AorusKeys.Apostrophe: return 74;
                case AorusKeys.Lsquare: return 75;
                case AorusKeys.Hyphen: return 76;
                case AorusKeys.F11: return 77;
                case AorusKeys.RightSquare: return 81;
                case AorusKeys.Equal: return 82;
                case AorusKeys.F12: return 83;
                case AorusKeys.Left: return 84;
                case AorusKeys.RightShift: return 85;
                case AorusKeys.SharpUK: return 86;
                case AorusKeys.Backslash: return 87;
                case AorusKeys.Pause: return 89;
                case AorusKeys.Down: return 90;
                case AorusKeys.Up: return 91;
                case AorusKeys.Enter: return 92;
                case AorusKeys.EnterUK: return 93;
                case AorusKeys.Backspace: return 94;
                case AorusKeys.Delete: return 95;
                case AorusKeys.Right: return 96;
                case AorusKeys.NumPad1: return 97;
                case AorusKeys.NumPad4: return 98;
                case AorusKeys.NumPad7: return 99;
                case AorusKeys.NumLock: return 100;
                case AorusKeys.Home: return 101;
                case AorusKeys.NumPad0: return 102;
                case AorusKeys.NumPad2: return 103;
                case AorusKeys.NumPad5: return 104;
                case AorusKeys.NumPad8: return 105;
                case AorusKeys.Slash: return 106;
                case AorusKeys.PageUp: return 107;
                case AorusKeys.NumPadDel: return 108;
                case AorusKeys.NumPad3: return 109;
                case AorusKeys.NumPad6: return 110;
                case AorusKeys.NumPad9: return 111;
                case AorusKeys.Asterisk: return 112;
                case AorusKeys.PageDown: return 113;
                case AorusKeys.RightEnter: return 114;
                case AorusKeys.Plus: return 116;
                case AorusKeys.Minus: return 118;
                case AorusKeys.End: return 119;
                default: return 0;
            }
        }
    }
}