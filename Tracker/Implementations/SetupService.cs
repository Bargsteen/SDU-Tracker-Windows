using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Win32;
using Newtonsoft.Json;
using Tracker.Constants;
using Tracker.Enums;
using Tracker.Interfaces;

namespace Tracker.Implementations
{
    public class SetupService : ISetupService
    {
        private readonly IAlertService _alertService;
        private readonly IDateTimeService _dateTimeService;
        private readonly ILaunchAtLoginService _launchAtLoginService;
        private readonly ISettings _settings;
        
        public SetupService(IAlertService alertService, IDateTimeService dateTimeService, ILaunchAtLoginService launchAtLoginService, ISettings settings)
        {
            _alertService = alertService;
            _dateTimeService = dateTimeService;
            _launchAtLoginService = launchAtLoginService;
            _settings = settings;
        }

        public void SetupAppByUri(string maybeUri)
        {
            if (Uri.TryCreate(maybeUri, UriKind.Absolute, out var uri) &&
                string.Equals(uri.Scheme, SetupConstants.UriScheme, StringComparison.OrdinalIgnoreCase))
            {
                string encodedData = uri.Query.Split(new [] {'='}, 2).LastOrDefault();
                if (encodedData != null)
                {
                    // Decode to dictionary
                    byte[] data = Convert.FromBase64String(encodedData);
                    string jsonString = Encoding.UTF8.GetString(data);
                    var dict = JsonConvert.DeserializeObject<Dictionary<string, string>>(jsonString);

                    try
                    {
                        // Extract values from dictionary        
                        List<string> userList = ToUserList(dict["users"]); 
                        string userId = dict["user_id"];
                        TrackingType trackingType =
                            dict["tracking_type"] == "1" ? TrackingType.AppAndDevice : TrackingType.Device;
                        int measurementDays = int.Parse(dict["measurement_days"]);

                        // Save to settings
                        _settings.Users = userList;
                        _settings.CurrentUser = _settings.Users.First();
                        _settings.UserId = userId;
                        _settings.TrackingType = trackingType;
                        _settings.StopTrackingDate = _dateTimeService.CurrentTime.AddDays(measurementDays);

                        // Success => Prompt user with alert
                        _settings.AppHasBeenSetup = true;
                        _launchAtLoginService.LaunchAtLoginIsEnabled = true;
                        _alertService.ShowAlert(AlertConstants.SetupByUriSuccessTitle, AlertConstants.SetupByUriSuccessMessage);
                    }
                    catch (Exception) // TODO: This could be handled without exceptions.
                    {
                        _alertService.ShowAlert(AlertConstants.SetupByUriErrorTitle, AlertConstants.SetupByUriErrorMessage);
                    }
                }
            }
            else
            {
                _alertService.ShowAlert(AlertConstants.SetupByUriErrorTitle, AlertConstants.SetupByUriErrorMessage);
            }
        }

        private static List<string> ToUserList(string input)
        {
            return input
                .Split(new[] {'[', ']', ','}, StringSplitOptions.RemoveEmptyEntries)
                .Select(s => s.Trim())
                .ToList();
        }
    }
}