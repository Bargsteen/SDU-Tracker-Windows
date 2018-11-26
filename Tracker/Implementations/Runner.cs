using Tracker.Constants;
using Tracker.Enums;
using Tracker.Interfaces;

namespace Tracker.Implementations
{
    public class Runner : IRunner
    {
        private readonly IAlertService _alertService;
        private readonly IAppTracker _appTracker;
        private readonly IDateTimeService _dateTimeService;
        private readonly IDeviceTracker _deviceTracker;
        private readonly ILaunchAtLoginService _launchAtLoginService;
        private readonly ILogger _logger;
        private readonly IResendService _resendService;
        private readonly ISettings _settings;


        public Runner(IAlertService alertService, IAppTracker appTracker, IDateTimeService dateTimeService, 
            IDeviceTracker deviceTracker, ILaunchAtLoginService launchAtLoginService, ILogger logger, 
            IResendService resendService, ISettings settings)
        {
            _alertService = alertService;
            _appTracker = appTracker;
            _dateTimeService = dateTimeService;
            _deviceTracker = deviceTracker;
            _launchAtLoginService = launchAtLoginService;
            _logger = logger;
            _resendService = resendService;
            _settings = settings;
        }

        public void Run()
        {
            if (!_settings.AppHasBeenSetup)
            {
                _logger.LogInfo(LoggerConstants.AppHasNotBeenSetupText);
                _alertService.ShowAlert(AlertConstants.ReadyForSetupTitle, AlertConstants.ReadyForSetupMessage);
            }
            else
            {
                if (_dateTimeService.CurrentTime <= _settings.StopTrackingDate)
                {
                    _resendService.StartPeriodicResendingOfSavedUsages(TrackingConstants.SecondsBetweenResendChecks, TrackingConstants.LimitOfEachUsage);

                    if (_settings.TrackingType == TrackingType.AppAndDevice)
                    {
                        _appTracker.StartTracking();
                        _deviceTracker.StartTracking();
                    }
                    else
                    {
                        _deviceTracker.StartTracking();
                    }
                }
                else
                {
                    // TODO: Launch at login should remove the shortcut
                    _launchAtLoginService.LaunchAtLoginIsEnabled = false;
                    _alertService.ShowAlert(AlertConstants.TrackingHasEndedTitle,
                        AlertConstants.TrackingHasEndedMessage);
                }
            }
        }

        public void Terminate()
        {
            if (!_settings.AppHasBeenSetup) return;

            if (_settings.TrackingType == TrackingType.AppAndDevice)
            {
                _appTracker.StopTracking();
                _deviceTracker.StopTracking();
            }
            else
            {
                _deviceTracker.StopTracking();
            }
        }
    }
}