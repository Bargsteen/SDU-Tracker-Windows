using System;
using System.Threading;
using System.Windows.Forms;
using TrackerLib.Interfaces;

namespace Tracker
{
    class MyApplicationContext : ApplicationContext
    {
        private NotifyIcon trayIcon;
        private IAppTrackingHandler appTrackingHandler;

        public MyApplicationContext(IAppTrackingHandler appTrackingHandler)
        {
            this.appTrackingHandler = appTrackingHandler;

            trayIcon = new NotifyIcon()
            {
                Icon = Properties.Resources.AppIcon,

                ContextMenu = new ContextMenu(new MenuItem[]
                {
                    new MenuItem("Start tracking", StartTracking),
                    new MenuItem("Stop tracking", StopTracking),
                    new MenuItem("Exit", Exit)
                }),
                Visible = true
            };
        }

        private void StartTracking(object sender, EventArgs e)
        {
            appTrackingHandler.StartTracking();
        }

        private void StopTracking(object sender, EventArgs e)
        {
            appTrackingHandler.StopTracking();
        }


        private void Exit(object sender, EventArgs e)
        {
            trayIcon.Visible = false;

            Application.Exit();
        }
    }
}
