using System;
using System.Collections.Generic;
using TrackerLib.Models;
using TrackerLib.Enums;

namespace TrackerLib.Interfaces
{
    public interface ISettings
    {
        bool AppHasBeenSetup { get; set; }

        List<string> Users { get; set; }
        int UserCount { get; }
        string CurrentUser { get; set; }

        string MakeParticipantIdentifierForUser(string user);

        string DeviceModelName { get; }

        TrackingType TrackingType { get; set; }

        DateTimeOffset StopTrackingDate { get; set; }
        
        string UserId { get; set; }
        string ParticipantIdentifier { get;}

        Credentials Credentials { get;}
    }
}
