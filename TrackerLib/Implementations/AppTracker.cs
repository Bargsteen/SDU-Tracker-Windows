using System;
using System.Threading;
using TrackerLib.Constants;
using TrackerLib.Interfaces;
using TrackerLib.Models;

namespace TrackerLib.Implementations
{
    public class AppTracker : IAppTracker
    {
        private readonly IActiveWindowHandler _activeWindowHandler;
        private readonly IDateTimeHandler _dateTimeHandler;
        private readonly ISendOrSaveHandler _sendOrSaveHandler;
        private readonly ISettings _settings;
        private readonly ISleepHandler _sleepHandler;

        private Thread _trackingThread;

        public AppTracker(IActiveWindowHandler activeWindowHandler, IDateTimeHandler dateTimeHandler, ISendOrSaveHandler sendOrSaveHandler, ISettings settings, ISleepHandler sleepHandler)
        {
            _activeWindowHandler = activeWindowHandler;
            _dateTimeHandler = dateTimeHandler;
            _sendOrSaveHandler = sendOrSaveHandler;
            _settings = settings;
            _sleepHandler = sleepHandler;
        }

        public void StartTracking()
        {

            _trackingThread = new Thread(() =>
            {
                while (true)
                {
                    SendOrSaveIfAppHasChanged();
                    _sleepHandler.SleepFor(TrackingConstants.SecondsBetweenActiveWindowChecks);
                }
            })
            {
                // Makes it shutdown when the foreground threads have finished.
                IsBackground = true
            };
            _trackingThread.Start();
        }

        private AppUsage MakeAppUsage(string package, int duration)
        {
            string participantIdentifier = _settings.ParticipantIdentifier;
            string deviceModelName = _settings.DeviceModelName;
            var timeStamp = _dateTimeHandler.CurrentTime;
            int userCount = _settings.UserCount;
            return new AppUsage(participantIdentifier, deviceModelName, timeStamp, userCount, package, duration);
        }

        private void SendOrSaveIfAppHasChanged()
        {
            var lastActiveWindow = _activeWindowHandler.MaybeGetLastActiveWindow();

            if (lastActiveWindow != null)
            {
                int duration = (int)Math.Round((lastActiveWindow.EndTime - lastActiveWindow.StartTime).TotalMilliseconds);
                var newAppUsage = MakeAppUsage(lastActiveWindow.Identifier, duration);

                _sendOrSaveHandler.SendOrSaveUsage(newAppUsage);
            }
        }
    }
}
