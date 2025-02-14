﻿using System;
using Tracker.Enums;
using Tracker.Interfaces;
using Tracker.Models;

namespace Tracker.Implementations
{
    public class UsageBuilder : IUsageBuilder
    {
        private readonly ISettings _settings;
        private readonly IDateTimeService _dateTimeService;

        public UsageBuilder(ISettings settings, IDateTimeService dateTimeService)
        {
            _settings = settings;
            _dateTimeService = dateTimeService;
        }

        public DeviceUsage MakeDeviceUsage(EventType eventType)
        {
            return new DeviceUsage(_settings.ParticipantIdentifier, _settings.DeviceModelName,
                _dateTimeService.CurrentTime, _settings.UserCount, eventType);
        }

        public DeviceUsage MakeDeviceUsage(EventType eventType, string participantIdentifier)
        {
            return new DeviceUsage(participantIdentifier, _settings.DeviceModelName,
                _dateTimeService.CurrentTime, _settings.UserCount, eventType);
        }

        public AppUsage MakeAppUsage(ActiveWindow activeWindow)
        {
            int duration = (int) Math.Round((activeWindow.EndTime - activeWindow.StartTime).TotalMilliseconds);

            return new AppUsage(_settings.ParticipantIdentifier, _settings.DeviceModelName, 
                _dateTimeService.CurrentTime, _settings.UserCount, activeWindow.Identifier, duration);
        }
    }
}