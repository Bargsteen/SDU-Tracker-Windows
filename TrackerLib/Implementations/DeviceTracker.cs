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

        public DeviceTracker(ISendOrSaveService sendOrSaveService, ISettings settings, 
            ISystemEventService systemEventService, IUsageBuilder usageBuilder, IUserService userService)
        {
            _sendOrSaveService = sendOrSaveService;
            _settings = settings;
            _systemEventService = systemEventService;
            _usageBuilder = usageBuilder;
            _userService = userService;

            _settings.OnParticipantIdentifierChanged += HandleParticipantIdentifierChanged;
        }

        public void StartTracking()
        {
            _systemEventService.SystemSuspended += HandleSystemSuspended;
            _systemEventService.SystemStartedOrResumed += HandleSystemResumed;

            // Send initial
            var deviceUsage = _usageBuilder.MakeDeviceUsage(EventType.Started);
            _sendOrSaveService.SendOrSaveUsage(deviceUsage);
        }

        public void StopTracking()
        {
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

        private void HandleParticipantIdentifierChanged(object sender, ParticipantIdentifierChangedEventArgs args)
        {
            var endedUsage = _usageBuilder.MakeDeviceUsage(EventType.Ended, args.PreviousParticipantIdentifier);
            var startedUsage = _usageBuilder.MakeDeviceUsage(EventType.Started, args.NewParticipantIdentifier);
            _sendOrSaveService.SendOrSaveUsage(endedUsage);
            _sendOrSaveService.SendOrSaveUsage(startedUsage);
        }
    }
}