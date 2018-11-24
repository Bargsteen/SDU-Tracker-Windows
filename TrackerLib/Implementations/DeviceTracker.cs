using System;
using TrackerLib.Enums;
using TrackerLib.Events;
using TrackerLib.Interfaces;

namespace TrackerLib.Implementations
{
    public class DeviceTracker : IDeviceTracker
    {
        private readonly ISendOrSaveService _sendOrSaveService;
        private readonly ISettings _settings;
        private readonly ISystemEventService _systemEventService;
        private readonly IUsageBuilder _usageBuilder;
        private readonly IUserService _userService;
        private bool hasBeenStarted;

        public DeviceTracker(ISendOrSaveService sendOrSaveService, ISettings settings, 
            ISystemEventService systemEventService, IUsageBuilder usageBuilder, IUserService userService)
        {
            _sendOrSaveService = sendOrSaveService;
            _settings = settings;
            _systemEventService = systemEventService;
            _usageBuilder = usageBuilder;
            _userService = userService;
            hasBeenStarted = false;
        }

        public void StartTracking()
        {
            hasBeenStarted = true;
            _systemEventService.SystemSuspended += HandleSystemSuspended;
            _systemEventService.SystemStartedOrResumed += HandleSystemResumed;
            _userService.OnUserSessionStarted += HandleUserSessionStarted;
            _userService.OnUserSessionEnded += HandleUserSessionEnded;

            _userService.CheckIfUserHasChanged();
        }

        public void StopTracking()
        {
            if (!hasBeenStarted) return;

            _systemEventService.SystemSuspended -= HandleSystemSuspended;
            _systemEventService.SystemStartedOrResumed -= HandleSystemResumed;

            // Necessary to avoid memory leaks
            // Look at the implementation for further detail.
            _systemEventService.Dispose();

            // Send final
            var deviceUsage = _usageBuilder.MakeDeviceUsage(EventType.Ended);
            _sendOrSaveService.SendOrSaveUsage(deviceUsage);
        }

        private void HandleSystemSuspended(object sender, EventArgs e)
        {
            var deviceUsage = _usageBuilder.MakeDeviceUsage(EventType.Ended);
            _sendOrSaveService.SendOrSaveUsage(deviceUsage);
        }

        private void HandleSystemResumed(object sender, EventArgs e)
        {
            _userService.CheckIfUserHasChanged();
        }

        private void HandleUserSessionStarted(object sender, UserSessionChangeEventArgs args)
        {
            string participantIdentifier = _settings.MakeParticipantIdentifierForUser(args.User);
            var deviceStartedUsage = _usageBuilder.MakeDeviceUsage(EventType.Started, participantIdentifier);
            _sendOrSaveService.SendOrSaveUsage(deviceStartedUsage);
        }

        private void HandleUserSessionEnded(object sender, UserSessionChangeEventArgs args)
        {
            string participantIdentifier = _settings.MakeParticipantIdentifierForUser(args.User);
            var deviceEndedUsage = _usageBuilder.MakeDeviceUsage(EventType.Ended, participantIdentifier);
            _sendOrSaveService.SendOrSaveUsage(deviceEndedUsage);
        }
    }
}