using System;
using System.Windows.Forms;
using Microsoft.VisualBasic.ApplicationServices;
using Tracker.Enums;
using Tracker.Events;
using Tracker.Interfaces;
using Tracker.Properties;

namespace Tracker
{
    internal class TrackerApplicationContext : ApplicationContext, ITrackerApplicationContext
    {
        private readonly IRunner _runner;
        private readonly ISettings _settings;
        private readonly IUserService _userService;

        private readonly ILogger _logger;

        private NotifyIcon _trayIcon;

        public TrackerApplicationContext(IRunner runner, ISettings settings, IUserService userService, ILogger logger)
        {
            _runner = runner;
            _settings = settings;
            _userService = userService;
            _logger = logger;

            SetupMenu();
            
            _userService.OnUserSessionStarted += HandleUserSessionStarted;
            
            Application.ApplicationExit += OnApplicationExit;

            var response = _runner.Run();
            if (response == RunnerResponse.ShouldTerminate)
            {
                Exit(this, null);
            }
        }

        private void SetupMenu()
        {
            _trayIcon = new NotifyIcon
            {
                Icon = Resources.AppIcon,

                ContextMenu = new ContextMenu(new[]
                {
                    new MenuItem(MakeCurrentUserString(_settings.CurrentUser), OpenUserMenuClicked)
                }),
                Visible = true
            };
        }

        private void OpenUserMenuClicked(object sender, EventArgs args)
        {
            _userService.ShowUserWindow();
        }

        private void Exit(object sender, EventArgs e)
        {
            _trayIcon.Visible = false;

            Application.Exit();
        }

        private void OnApplicationExit(object sender, EventArgs e)
        {
            _logger.LogInfo("OnApplicationExit Executed");
            _runner.Terminate();
        }

        private void HandleUserSessionStarted(object sender, UserSessionChangeEventArgs args)
        {
            _trayIcon.ContextMenu.MenuItems[0].Text = MakeCurrentUserString(args.User);
        }

        private static string MakeCurrentUserString(string user)
        {
            return $"Valgte bruger: {user}";
        }
    }
}