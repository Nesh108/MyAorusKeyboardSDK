using System;
using System.Collections;
using System.Runtime.InteropServices;
using System.Windows.Forms;

class FullscreenManager
{
    [StructLayout(LayoutKind.Sequential)]
    public struct Rect
    {
        public int left;
        public int top;
        public int right;
        public int bottom;
    }

    [DllImport("user32.dll", SetLastError = true)]
    public static extern uint GetWindowThreadProcessId(IntPtr hWnd, out uint lpdwProcessId);

    [DllImport("user32.dll")]
    private static extern bool GetWindowRect(HandleRef hWnd, [In, Out] ref Rect rect);

    [DllImport("user32.dll")]
    private static extern IntPtr GetForegroundWindow();

    public static bool IsForegroundFullScreen(ArrayList processesName, int allowedOffset)
    {
        return IsForegroundFullScreen(processesName, allowedOffset, Screen.PrimaryScreen);
    }

    public static bool IsForegroundFullScreen(ArrayList processesName, int allowedOffset, Screen screen)
    {
        // Get Process Window Rectangle
        Rect rect = new Rect();
        IntPtr hWnd = GetForegroundWindow();
        GetWindowRect(new HandleRef(null, hWnd), ref rect);

        // Get Process Info
        GetWindowThreadProcessId(hWnd, out var procId);
        var proc = System.Diagnostics.Process.GetProcessById((int)procId);

        // Check if the process is within the ones provided and if it is fullscreen
        return processesName.Contains(proc.ProcessName) && IsProcessRectFullscreen(rect, screen, allowedOffset);
    }

    private static bool IsProcessRectFullscreen(Rect rect, Screen screen, int allowedOffset)
    {
        return ((rect.right - rect.left) + allowedOffset >= screen.WorkingArea.Width &&
         (rect.bottom - rect.top) + allowedOffset >= screen.WorkingArea.Height);
    }
}