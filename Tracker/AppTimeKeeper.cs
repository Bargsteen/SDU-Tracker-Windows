using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using TrackerLib.Interfaces;
using TrackerLib.Models;

namespace Tracker
{
    public class AppTimeKeeper : IAppTimeKeeper
    {
        private ActiveWindow currentActiveWindow;
        public AppTimeKeeper()
        {
            currentActiveWindow = null;
        }

        public ActiveWindow MaybeGetLastActiveWindow()
        {
            var activeProcess = GetActiveProcess();
            if (activeProcess != null)
            {
                var now = DateTimeOffset.UtcNow;
                var activeProcessIdentifier = GetIdentifier(activeProcess);
                if (currentActiveWindow != null)
                {
                    if (currentActiveWindow.Identifier != activeProcessIdentifier) // Switched window
                    {
                        // Add endtime to currentActiveWindow
                        currentActiveWindow.EndTime = now;
                        var lastActiveWindow = (ActiveWindow)currentActiveWindow.Clone();

                        // Set new currentActive window.
                        currentActiveWindow = new ActiveWindow(activeProcessIdentifier, now);

                        return lastActiveWindow;
                    }
                }
                else // First current active window
                {
                    currentActiveWindow = new ActiveWindow(activeProcessIdentifier, now);
                }
            }
            return null;
        }

        private string GetIdentifier(Process process)
        {
            var processName = process.ProcessName;
            var windowName = process.MainWindowTitle; // TODO: Remove too specific info from title

            return $"{processName}-{windowName}";
        }

        [DllImport("user32.dll")]
        private static extern IntPtr GetForegroundWindow();

        [DllImport("user32.dll")]
        private static extern int GetWindowThreadProcessId(IntPtr hWnd, out uint lpdwProcessId);

        private Process GetActiveProcess()
        {
            IntPtr hWnd = GetForegroundWindow();
            return hWnd != null ? GetProcessByHandle(hWnd) : null;
        }

        private Process GetProcessByHandle(IntPtr hWnd)
        {
            try
            {
                GetWindowThreadProcessId(hWnd, out uint processID);
                return Process.GetProcessById((int)processID);
            }
            catch
            {
                return null;
            }
        }
    }


}
