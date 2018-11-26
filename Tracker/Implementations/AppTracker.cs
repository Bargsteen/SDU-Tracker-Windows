using System.Threading;
using Tracker.Constants;
using Tracker.Interfaces;

namespace Tracker.Implementations
{
    public class AppTracker : IAppTracker
    {
        private readonly IActiveWindowService _activeWindowService;
        private readonly ISendOrSaveService _sendOrSaveService;
        private readonly ISleepService _sleepService;
        private readonly IUsageBuilder _usageBuilder;

        private Thread _trackingThread;

        public AppTracker(IActiveWindowService activeWindowService, ISendOrSaveService sendOrSaveService, 
            ISleepService sleepService, IUsageBuilder usageBuilder)
        {
            _activeWindowService = activeWindowService;
            _sendOrSaveService = sendOrSaveService;
            _sleepService = sleepService;
            _usageBuilder = usageBuilder;
        }

        public void StartTracking()
        {
            _trackingThread = new Thread(() =>
            {
                while (true)
                {
                    SendOrSaveIfAppHasChanged();
                    _sleepService.SleepFor(TrackingConstants.SecondsBetweenActiveWindowChecks);
                }
            })
            {
                // Makes it shutdown when the foreground threads have finished.
                IsBackground = true
            };
            _trackingThread.Start();
        }

        public void StopTracking()
        {
             // Nothing to be done, since it is a background thread.
        }

        private void SendOrSaveIfAppHasChanged()
        {
            var lastActiveWindow = _activeWindowService.MaybeGetLastActiveWindow();

            if (lastActiveWindow != null)
            {
                var newAppUsage = _usageBuilder.MakeAppUsage(lastActiveWindow);

                _sendOrSaveService.SendOrSaveUsage(newAppUsage);
            }
        }
    }
}
