using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Tracker
{
    class MyApplicationContext : ApplicationContext
    {
        private NotifyIcon trayIcon;

        public MyApplicationContext()
        {
            //MenuItem exitMenuItem = new MenuItem("Afslut", new EventHandler(Exit));

            trayIcon = new NotifyIcon()
            {
                Icon = Tracker.Properties.Resources.AppIcon,
                ContextMenu = new ContextMenu(new MenuItem[]
                {
                    new MenuItem("Exit", Exit)
                }),
                Visible = true
            };



        }

        private void Exit(object sender, EventArgs e)
        {
            trayIcon.Visible = false;

            Application.Exit();
        }
    }
}
