using System.Threading;
using TrackerLib.Constants;
using TrackerLib.Interfaces;

namespace TrackerLib.Implementations
{
    public class AppTracker : IAppTracker
    {
        private readonly IActiveWindowHandler _activeWindowHandler;
        private readonly ISendOrSaveHandler _sendOrSaveHandler;
        private readonly ISleepHandler _sleepHandler;
        private readonly IUsageBuilder _usageBuilder;

        private Thread _trackingThread;

        public AppTracker(IActiveWindowHandler activeWindowHandler, ISendOrSaveHandler sendOrSaveHandler, 
            ISleepHandler sleepHandler, IUsageBuilder usageBuilder)
        {
            _activeWindowHandler = activeWindowHandler;
            _sendOrSaveHandler = sendOrSaveHandler;
            _sleepHandler = sleepHandler;
            _usageBuilder = usageBuilder;
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

        public void StopTracking()
        {
            // Nothing needs to be done.
        }

        private void SendOrSaveIfAppHasChanged()
        {
            var lastActiveWindow = _activeWindowHandler.MaybeGetLastActiveWindow();

            if (lastActiveWindow != null)
            {
                var newAppUsage = _usageBuilder.MakeAppUsage(lastActiveWindow);

                _sendOrSaveHandler.SendOrSaveUsage(newAppUsage);
            }
        }
    }
}
