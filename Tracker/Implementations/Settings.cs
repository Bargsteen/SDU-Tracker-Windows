using System;
using System.Collections.Generic;
using TrackerLib.Enums;
using TrackerLib.Interfaces;
using TrackerLib.Models;

namespace Tracker.Implementations
{
    public class Settings : ISettings
    {
        private readonly Properties.Settings _settings = Properties.Settings.Default;

        public bool AppHasBeenSetup
        {
            get => _settings?.AppHasBeenSetup ?? false;
            set
            {
                _settings.AppHasBeenSetup = value;
                _settings.Save();
            }
        }

        public List<string> Users { get; set; } = new List<string>();

        public int UserCount => _settings.UserList?.Count ?? 1;

        public string CurrentUser
        {
            get => _settings?.CurrentUser ?? "ukendt-bruger";
            set
            {
                _settings.CurrentUser = value;
                _settings.Save();
            }
        }

        public string MakeParticipantIdentifierForUser(string user)
        {
            return $"{UserId}:{user}";
        }

        public string DeviceModelName => Environment.MachineName;

        public TrackingType TrackingType
        {
            get => _settings?.TrackingType ?? TrackingType.Device;
            set
            {
                _settings.TrackingType = value;
                _settings.Save();
            }
        }

        public DateTimeOffset StopTrackingDate
        {
            get => _settings?.StopTrackingDate ?? DateTimeOffset.MaxValue;
            set
            {
                _settings.StopTrackingDate = value;
                _settings.Save();
            }
        }

        public string UserId
        {
            get => _settings?.UserId ?? "ukendt-id";
            set
            {
                _settings.UserId = value;
                _settings.Save();
            }
        }

        public string ParticipantIdentifier => MakeParticipantIdentifierForUser(CurrentUser);

        public Credentials Credentials => new Credentials(_settings.Username, _settings.Password);
    }
}
