using System;
using System.Collections.Generic;
using TrackerLib;
using TrackerLib.Interfaces;
using TrackerLib.Models;

namespace Tracker
{
    public class Settings : ISettings
    {
        public List<string> Users { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public int UserCount => throw new NotImplementedException();

        public string CurrentUser { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public string DeviceModelName => throw new NotImplementedException();

        public bool UseAppTracking { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public DateTimeOffset StopTrackingDate { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public bool AppHasBeenSetup { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public string ParticipantIdentifier { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public Credentials Credentials { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    }
}
