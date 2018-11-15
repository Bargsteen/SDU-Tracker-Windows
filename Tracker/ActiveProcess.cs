using System;
using System.Runtime.InteropServices;
using System.Diagnostics;

namespace Tracker
{
    public class ActiveProcess
    {

        [DllImport("user32.dll")]
        private static extern IntPtr GetForegroundWindow();

        [DllImport("user32.dll")]
        private static extern Int32 GetWindowThreadProcessId(IntPtr hWnd, out uint lpdwProcessId);

        public static Process GetActiveProcess()
        {
            IntPtr hWnd = GetForegroundWindow();
            return hWnd != null ? GetProcessByHandle(hWnd) : null;
        }

        private static Process GetProcessByHandle(IntPtr hWnd)
        {
            try
            {
                uint processId;
                GetWindowThreadProcessId(hWnd, out processId);
                return Process.GetProcessById((int)processId);
            } catch
            {
                return null;
            }
        }

    }
}
