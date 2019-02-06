using System.Diagnostics;
using Tracker.Interfaces;

namespace Tracker.Implementations
{
    public class PowerSettingsService : IPowerSettingsService
    {
        public void SetSleepAfterTimer(int minutes)
        {
            if (minutes < 0) return;
            Process.Start("powercfg", $"/change standby-timeout-ac {minutes}"); // Connected to power
            Process.Start("powercfg", $"/change standby-timeout-dc {minutes}"); // Battery
        }
    }
}