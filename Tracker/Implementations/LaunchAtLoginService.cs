using System.Windows.Forms;
using Microsoft.Win32;
using Tracker.Interfaces;

namespace Tracker.Implementations
{
    public class LaunchAtLoginService : ILaunchAtLoginService
    {
        private readonly RegistryKey _registryKey;

        public LaunchAtLoginService()
        {
            _registryKey = Registry.CurrentUser.OpenSubKey
                ("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);
        }

        public bool LaunchAtLoginIsEnabled
        {
            get => _registryKey.GetValue(AppIdentifier) != null;
            set
            {
                if (value)
                {
                    _registryKey.SetValue(AppIdentifier, Application.ExecutablePath);
                }
                else
                {
                    _registryKey.DeleteValue(AppIdentifier, false);
                }
            }
        }

        private static string AppIdentifier => Application.ProductName;  
    }
}