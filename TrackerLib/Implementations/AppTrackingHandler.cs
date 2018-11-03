using System;
using System.Threading;
using TrackerLib.Interfaces;
using TrackerLib.Models;

namespace TrackerLib.Implementations
{
    public class AppTrackingHandler : IAppTrackingHandler
    {
        private readonly IActiveWindowHandler activeWindowHandler;
        private readonly IDateTimeHandler dateTimeHandler;
        private readonly ISendOrSaveHandler sendOrSaveHandler;
        private readonly ISettings settings;

        private bool isTracking;
        private Thread trackingThread;

        public AppTrackingHandler(IActiveWindowHandler activeWindowHandler, IDateTimeHandler dateTimeHandler, ISendOrSaveHandler sendOrSaveHandler, ISettings settings)
        {
            this.activeWindowHandler = activeWindowHandler;
            this.dateTimeHandler = dateTimeHandler;
            this.sendOrSaveHandler = sendOrSaveHandler;
            this.settings = settings;

            isTracking = false;
        }

        public void StartTracking()
        {
            isTracking = true;

            trackingThread = new Thread(() =>
            {
                while (isTracking)
                {
                    SendOrSaveIfAppHasChanged();
                    Thread.Sleep(1000);
                }
            })
            {
                // Makes it shutdown when the foreground threads have finished.
                IsBackground = true
            };
            trackingThread.Start();
        }

        private AppUsage MakeAppUsage(string package, int duration)
        {
            var participantIdentifier = settings.ParticipantIdentifier;
            var deviceModelName = settings.DeviceModelName;
            var timeStamp = dateTimeHandler.Now;
            var userCount = settings.UserCount;
            return new AppUsage(participantIdentifier, deviceModelName, timeStamp, userCount, package, duration);
        }

        private void SendOrSaveIfAppHasChanged()
        {
            var lastActiveWindow = activeWindowHandler.MaybeGetLastActiveWindow();
            if (lastActiveWindow != null)
            {
                int duration = (int)Math.Round((lastActiveWindow.EndTime - lastActiveWindow.StartTime).TotalMilliseconds);
                var newAppUsage = MakeAppUsage(lastActiveWindow.Identifier, duration);

                sendOrSaveHandler.SendOrSaveUsage(newAppUsage);
            }
        }

        public void StopTracking()
        {
            isTracking = false;
        }
    }
}
