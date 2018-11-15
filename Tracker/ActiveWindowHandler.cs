using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using TrackerLib.Interfaces;
using TrackerLib.Models;

namespace Tracker
{
    public class ActiveWindowHandler : IActiveWindowHandler
    {
        private ActiveWindow _currentActiveWindow;
        public ActiveWindowHandler()
        {
            _currentActiveWindow = null;
        }

        public ActiveWindow MaybeGetLastActiveWindow()
        {
            var activeProcess = GetActiveProcess();
            if (activeProcess != null)
            {
                var now = DateTimeOffset.UtcNow;
                var activeProcessIdentifier = GetIdentifier(activeProcess);
                if (_currentActiveWindow != null)
                {
                    if (_currentActiveWindow.Identifier != activeProcessIdentifier) // Switched window
                    {
                        // Add endtime to currentActiveWindow
                        _currentActiveWindow.EndTime = now;
                        var lastActiveWindow = (ActiveWindow)_currentActiveWindow.Clone();

                        // Set new currentActive window.
                        _currentActiveWindow = new ActiveWindow(activeProcessIdentifier, now);

                        return lastActiveWindow;
                    }
                }
                else // First current active window
                {
                    _currentActiveWindow = new ActiveWindow(activeProcessIdentifier, now);
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
                GetWindowThreadProcessId(hWnd, out uint processId);
                return Process.GetProcessById((int)processId);
            }
            catch
            {
                return null;
            }
        }
    }


}
