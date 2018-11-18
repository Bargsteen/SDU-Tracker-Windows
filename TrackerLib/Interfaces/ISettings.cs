using System;
using System.Collections.Generic;
using TrackerLib.Models;
using TrackerLib.Enums;
using TrackerLib.Events;

namespace TrackerLib.Interfaces
{
    public interface ISettings
    {
        bool AppHasBeenSetup { get; set; }

        List<string> Users { get; set; }
        int UserCount { get; }
        string CurrentUser { get; set; }

        event ParticipantIdentifierChangedHandler OnParticipantIdentifierChanged;

        string DeviceModelName { get; }

        TrackingType TrackingType { get; set; }

        DateTimeOffset StopTrackingDate { get; set; }
        
        string UserId { get; set; }
        string ParticipantIdentifier { get;}

        Credentials Credentials { get;}
    }
}
