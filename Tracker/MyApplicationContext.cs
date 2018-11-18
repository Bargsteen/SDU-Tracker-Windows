using System;
using System.Windows.Forms;
using TrackerLib.Interfaces;

namespace Tracker
{
    internal class MyApplicationContext : ApplicationContext
    {
        private NotifyIcon _trayIcon;

        private readonly IRunner _runner;

        public MyApplicationContext(IRunner runner)
        {
            SetupMenu();
            Application.ApplicationExit += OnApplicationExit;

            _runner = runner;
            _runner.Run();
        }

        private void SetupMenu()
        {
            _trayIcon = new NotifyIcon()
            {
                Icon = Properties.Resources.AppIcon,

                ContextMenu = new ContextMenu(new[]
                {
                    new MenuItem("Exit", Exit)
                }),
                Visible = true
            };
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
