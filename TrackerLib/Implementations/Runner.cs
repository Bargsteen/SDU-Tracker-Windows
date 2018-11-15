using TrackerLib.Constants;
using TrackerLib.Enums;
using TrackerLib.Interfaces;
using TrackerLib.Models;

namespace TrackerLib.Implementations
{
    public class Runner : IRunner
    {
        private readonly IAlertHandler _alertHandler;
        private readonly IAppTracker _appTracker;
        private readonly IDateTimeHandler _dateTimeHandler;
        private readonly IDeviceTracker _deviceTracker;
        private readonly ILaunchAtLoginHandler _launchAtLoginHandler;
        private readonly ILogger _logger;
        private readonly ISendOrSaveHandler _sendOrSaveHandler;
        private readonly ISettings _settings;
        private readonly IResendHandler _resendHandler;
        private readonly IUsageBuilder _usageBuilder;
        private readonly IUserHandler _userHandler;

        public Runner(IAlertHandler alertHandler, IAppTracker appTracker, IDateTimeHandler dateTimeHandler, 
            IDeviceTracker deviceTracker, ILaunchAtLoginHandler launchAtLoginHandler, ILogger logger, 
            ISendOrSaveHandler sendOrSaveHandler, ISettings settings, IResendHandler resendHandler, 
            IUsageBuilder usageBuilder, IUserHandler userHandler)
        {
            _alertHandler = alertHandler;
            _appTracker = appTracker;
            _dateTimeHandler = dateTimeHandler;
            _deviceTracker = deviceTracker;
            _launchAtLoginHandler = launchAtLoginHandler;
            _logger = logger;
            _sendOrSaveHandler = sendOrSaveHandler;
            _settings = settings;
            _resendHandler = resendHandler;
            _usageBuilder = usageBuilder;
            _userHandler = userHandler;
        }


        public void Run()
        {
            if (!_settings.AppHasBeenSetup)
            {
                _logger.LogInfo(LoggerConstants.AppHasNotBeenSetupText);
                _alertHandler.ShowAlert(AlertsConstants.ReadyForSetupTitle, AlertsConstants.ReadyForSetupMessage,
                    AlertsConstants.OkButtonText, AlertsConstants.LongAlertTime);
            }
            else
            {
                if (_dateTimeHandler.CurrentTime <= _settings.StopTrackingDate)
                {
                    _userHandler.CheckIfUserHasChanged();

                    _resendHandler.StartPeriodicResendingOfSavedUsages();

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
                    _launchAtLoginHandler.LaunchAtLoginIsEnabled = false;
                    _alertHandler.ShowAlert(AlertsConstants.TrackingHasEndedTitle,
                        AlertsConstants.TrackingHasEndedMessage, AlertsConstants.OkButtonText,
                        AlertsConstants.LongAlertTime);
                }
            }
        }

        public void Terminate()
        {
            if (!_settings.AppHasBeenSetup) return;

            var deviceUsage = _usageBuilder.MakeDeviceUsage(EventType.Ended);
            _sendOrSaveHandler.SendOrSaveUsage(deviceUsage);
        }
    }
}