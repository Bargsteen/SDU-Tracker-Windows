using System;
using Microsoft.Win32;
using Tracker.Interfaces;

namespace Tracker.Implementations
{
    public class SystemEventService : ISystemEventService
    {
        public event EventHandler SystemSuspended;
        public event EventHandler SystemStartedOrResumed;
        private readonly ISleepService _sleepService;

        public SystemEventService(ISleepService sleepService)
        {
            SystemEvents.PowerModeChanged += InvokeAppropriateSystemEvent;
            SystemEvents.SessionEnding += InvokeSessionEndingEvent;
            _sleepService = sleepService;
        }   

        public void Dispose()
        {
            // Necessary to avoid memory leaks.
            // As described here: https://docs.microsoft.com/en-us/dotnet/api/microsoft.win32.systemevents.powermodechanged?view=netframework-4.7.2
            SystemEvents.PowerModeChanged -= InvokeAppropriateSystemEvent;
            SystemEvents.SessionEnding -= InvokeSessionEndingEvent;
        }

        // SessionEnding occurs when the user logs out or shuts down the system
        private void InvokeSessionEndingEvent(object sender, SessionEndingEventArgs e)
        {
            SystemSuspended?.Invoke(this, EventArgs.Empty);
            _sleepService.SleepFor(1);
        }

        // PowerModes.Suspend occurs when the computer goes to sleep (forced or by timer)
        // PowerModes.Resume occurs when the computer wakes (from sleep or login)
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
            }
        }
    }
}