using System;
using TrackerLib.Interfaces;
using TrackerLib.Models;

namespace TrackerLib.Implementations
{
    public class UsageBuilder : IUsageBuilder
    {
        private readonly ISettings _settings;
        private readonly IDateTimeHandler _dateTimeHandler;

        public UsageBuilder(ISettings settings, IDateTimeHandler dateTimeHandler)
        {
            _settings = settings;
            _dateTimeHandler = dateTimeHandler;
        }

        public DeviceUsage MakeDeviceUsage(EventType eventType)
        {
            return new DeviceUsage(_settings.ParticipantIdentifier, _settings.DeviceModelName, 
                _dateTimeHandler.CurrentTime, _settings.UserCount, eventType);
        }

        public AppUsage MakeAppUsage(ActiveWindow activeWindow)
        {
            int duration = (int) Math.Round((activeWindow.EndTime - activeWindow.StartTime).TotalMilliseconds);

            return new AppUsage(_settings.ParticipantIdentifier, _settings.DeviceModelName, 
                _dateTimeHandler.CurrentTime, _settings.UserCount, activeWindow.Identifier, duration);
        }
    }
}