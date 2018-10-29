using System;
using System.Collections.Generic;
using TrackerLib.Models;

namespace TrackerLib.Interfaces
{
    public interface ISettings
    {
        List<string> Users { get; set; }
        int UserCount { get; }
        string CurrentUser { get; set; }
        string DeviceModelName { get; }
        bool UseAppTracking { get; set; }
        DateTimeOffset StopTrackingDate { get; set; }
        bool AppHasBeenSetup { get; set; }
        string ParticipantIdentifier { get; set; }
        Credentials Credentials { get; set; }
    }
}
