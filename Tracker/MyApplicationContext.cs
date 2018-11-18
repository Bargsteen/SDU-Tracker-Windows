using System;
using System.Windows.Forms;
using TrackerLib.Interfaces;

namespace Tracker
{
    internal class MyApplicationContext : ApplicationContext
    {
        private NotifyIcon _trayIcon;

        private readonly IRunner _runner;
        private readonly IUserWindow _userWindow;

        public MyApplicationContext(IRunner runner, IUserWindow userWindow)
        {
            SetupMenu();
            Application.ApplicationExit += OnApplicationExit;

            _runner = runner;
            _userWindow = userWindow;

            _runner.Run();
        }

        private void SetupMenu()
        {
            _trayIcon = new NotifyIcon
            {
                Icon = Properties.Resources.AppIcon,

                ContextMenu = new ContextMenu(new[]
                {
                    new MenuItem("Vælg bruger", OpenUserMenuClicked), 
                    new MenuItem("Afslut", Exit)
                }),
                Visible = true
            };
        }

        private void OpenUserMenuClicked(object sender, EventArgs args)
        {
            _userWindow.ShowWindow();
        }

        private void Exit(object sender, EventArgs e)
        {
            _trayIcon.Visible = false;

            Application.Exit();
        }

        private void OnApplicationExit(object sender, EventArgs e)
        {
            _runner.Terminate();
        }
    }
}
