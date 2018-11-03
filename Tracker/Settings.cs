using System;
using System.Collections.Generic;
using TrackerLib.Enums;
using TrackerLib.Interfaces;
using TrackerLib.Models;

namespace Tracker
{
    public class Settings : ISettings
    {
        private readonly Properties.Settings settings = Properties.Settings.Default;

        public bool AppHasBeenSetup
        {
            get => settings?.AppHasBeenSetup ?? false;
            set
            {
                settings.AppHasBeenSetup = value;
                settings.Save();
            }
        }

        public List<string> Users => settings?.UserList ?? new List<string>();

        public void AddUser(string nameOfUser)
        {
            if(settings.UserList == null)
            {
                settings.UserList = new List<string>();
            }

            if (!settings.UserList.Contains(nameOfUser))
            {
                settings.UserList.Add(nameOfUser);
                settings.Save();
            }
        }

        public void RemoveUser(string nameOfUser)
        {
            settings?.UserList.Remove(nameOfUser);
            settings.Save();
        }

        public int UserCount => settings.UserList?.Count ?? 1;

        public string CurrentUser
        {
            get => settings?.CurrentUser ?? "ukendt-bruger";
            set
            {
                settings.CurrentUser = value;
                settings.Save();
            }
        }

        public string DeviceModelName => Environment.MachineName;

        public TrackingType TrackingType
        {
            get => settings?.TrackingType ?? TrackingType.Device;
            set
            {
                settings.TrackingType = value;
                settings.Save();
            }
        }

        public DateTimeOffset StopTrackingDate
        {
            get => settings?.StopTrackingDate ?? DateTimeOffset.MaxValue;
            set
            {
                settings.StopTrackingDate = value;
                settings.Save();
            }
        }

        public string UserId
        {
            get => settings?.UserId ?? "ukendt-id";
            set
            {
                settings.UserId = value;
                settings.Save();
            }
        }

        public string ParticipantIdentifier => $"{UserId}:{CurrentUser}";

        public Credentials Credentials => new Credentials(settings.Username, settings.Password);
    }
}
