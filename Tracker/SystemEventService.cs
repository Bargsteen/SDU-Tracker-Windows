using System;
using Microsoft.Win32;
using TrackerLib.Interfaces;

namespace Tracker
{
    public class SystemEventService : ISystemEventService
    {
        public event EventHandler SystemSuspended;
        public event EventHandler SystemStartedOrResumed;

        public SystemEventService()
        {
            SystemEvents.PowerModeChanged += InvokeAppropriateSystemEvent;
        }

        public void Dispose()
        {
            // Necessary to avoid memory leaks.
            // As described here: https://docs.microsoft.com/en-us/dotnet/api/microsoft.win32.systemevents.powermodechanged?view=netframework-4.7.2
            SystemEvents.PowerModeChanged -= InvokeAppropriateSystemEvent;
        }

        private void InvokeAppropriateSystemEvent(object sender, PowerModeChangedEventArgs args)
        {
            switch (args.Mode)
            {
                case PowerModes.Resume:
                    SystemStartedOrResumed?.Invoke(this, EventArgs.Empty);
                    break;
                case PowerModes.Suspend:
                    SystemSuspended?.Invoke(this, EventArgs.Empty);
                    break;
                case PowerModes.StatusChange: // Battery related. Not relevant.
                    break;
                default:
                    break;
            }
        }
    }
}