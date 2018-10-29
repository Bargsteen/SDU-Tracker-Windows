using System;
using System.Threading;
using System.Windows.Forms;
using TrackerLib.Interfaces;

namespace Tracker
{
    class MyApplicationContext : ApplicationContext
    {
        private NotifyIcon trayIcon;
        private IAppTimeKeeper appTimeKeeper;

        public MyApplicationContext(IAppTimeKeeper appTimeKeeper)
        {
            this.appTimeKeeper = appTimeKeeper;

            trayIcon = new NotifyIcon()
            {
                Icon = Properties.Resources.AppIcon,

                ContextMenu = new ContextMenu(new MenuItem[]
                {
                    new MenuItem("Exit", Exit)
                }),
                Visible = true
            };
            
            PrintActiveWindowChanges();
        }

        private void Exit(object sender, EventArgs e)
        {
            trayIcon.Visible = false;

            Application.Exit();
        }

        private void PrintActiveWindowChanges()
        {
            var thread = new Thread(() =>
               {
                   while (true)
                   {
                       var lastActiveWindow = appTimeKeeper.MaybeGetLastActiveWindow();
                       if (lastActiveWindow != null)
                       {
                           var duration = Math.Round((lastActiveWindow.EndTime - lastActiveWindow.StartTime).TotalMilliseconds);

                           Console.WriteLine($"{lastActiveWindow.Identifier} - {duration} ms");
                       }
                       Thread.Sleep(1000);
                   }
               })
            {
                // Makes it shutdown when the foreground threads have finished.
                IsBackground = true 
            };
            thread.Start();
            
        }
    }
}
