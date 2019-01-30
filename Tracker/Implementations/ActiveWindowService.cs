using System;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using Tracker.Interfaces;
using Tracker.Models;

namespace Tracker.Implementations
{
    public class ActiveWindowService : IActiveWindowService
    {
        private ActiveWindow _currentActiveWindow;
        public ActiveWindowService()
        {
            _currentActiveWindow = null;
        }

        public ActiveWindow MaybeGetLastActiveWindow()
        {
            var activeProcess = GetActiveProcess();

            if (activeProcess == null) return null;

            var now = DateTimeOffset.UtcNow;
            string activeProcessIdentifier = GetIdentifier(activeProcess);

            if (_currentActiveWindow != null)
            {
                if (_currentActiveWindow.Identifier != activeProcessIdentifier) // Switched window
                {
                    // Add end time to currentActiveWindow
                    _currentActiveWindow.EndTime = now;
                    var lastActiveWindow = (ActiveWindow)_currentActiveWindow.Clone();

                    // Set new currentActive window.
                    _currentActiveWindow = new ActiveWindow(activeProcessIdentifier, now);

                    // "Idle" runs when nothing else does, and "LockApp" is the lock screen.
                    if (lastActiveWindow.Identifier == "LockApp" || lastActiveWindow.Identifier == "Idle")
                    {
                        return null;
                    }

                    return lastActiveWindow;
                }
            }
            else // First current active window
            {
                _currentActiveWindow = new ActiveWindow(activeProcessIdentifier, now);
            }
            return null;
        }

        private static string GetIdentifier(Process process)
        {
            string processName = process.ProcessName;
            string windowName = process.MainWindowTitle; // TODO: Remove too specific info from title

            return MakeIdentifier(processName, windowName);
        }

        private static string MakeIdentifier(string processName, string windowName)
        {
            string identifier = $"{processName}";

            string cleanedWindowName = windowName.Split(new[] {"?-", "-", "–" }, StringSplitOptions.RemoveEmptyEntries)
                .LastOrDefault()?
                .Trim();

            if (!string.IsNullOrEmpty(cleanedWindowName))
            {
                identifier += $" - {cleanedWindowName}";
            }
            return identifier;
        }

        [DllImport("user32.dll")]
        private static extern IntPtr GetForegroundWindow();

        [DllImport("user32.dll")]
        private static extern int GetWindowThreadProcessId(IntPtr hWnd, out uint lpdwProcessId);

        private static Process GetActiveProcess()
        {
            IntPtr hWnd = GetForegroundWindow();
            return GetProcessByHandle(hWnd);
        }

        private static Process GetProcessByHandle(IntPtr hWnd)
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
