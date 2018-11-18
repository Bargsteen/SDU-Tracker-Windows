using System;
using TrackerLib.Interfaces;
using TrackerLib.Models;

namespace TrackerLib.Implementations
{
    public class DeviceTracker : IDeviceTracker
    {
        private readonly ISendOrSaveService _sendOrSaveService;
        private readonly ISystemEventService _systemEventService;
        private readonly IUsageBuilder _usageBuilder;

        public DeviceTracker(ISendOrSaveService sendOrSaveService, ISystemEventService systemEventService, 
            IUsageBuilder usageBuilder)
        {
            _sendOrSaveService = sendOrSaveService;
            _systemEventService = systemEventService;
            _usageBuilder = usageBuilder;
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
            var deviceUsage = _usageBuilder.MakeDeviceUsage(EventType.Started);
            _sendOrSaveService.SendOrSaveUsage(deviceUsage);
        }
    }
}