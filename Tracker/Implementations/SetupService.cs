using System;
using Microsoft.Win32;
using TrackerLib.Constants;
using TrackerLib.Interfaces;

namespace Tracker.Implementations
{
    public class SetupService : ISetupService
    {
        public void RegisterUriScheme()
        {
            const string friendlyName = "SDU Tracker";

            using (var key = Registry.CurrentUser.CreateSubKey("SOFTWARE\\Classes\\" + SetupConstants.UriScheme))
            {
                string applicationLocation = typeof(Program).Assembly.Location;

                key?.SetValue("", "URL:" + friendlyName);
                key?.SetValue("URL Protocol", "");


                using (var defaultIcon = key?.CreateSubKey("DefaultIcon"))
                {
                    defaultIcon?.SetValue("", "Tracker.exe" + ",1");
                }

                using (var commandKey = key?.CreateSubKey(@"shell\open\command"))
                {
                    commandKey?.SetValue("", "\"" + applicationLocation + "\" \"%1\"");
                }

            }
        }

        public void SetupAppByUri(Uri uri)
        {
            throw new System.NotImplementedException();
        }
    }
}