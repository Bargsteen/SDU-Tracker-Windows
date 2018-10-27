using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using TrackerLib;

namespace Tracker
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Logging.SetupLogging();


            Logging.LogInfo(ActiveWindow.GetActiveWindowTitle());
            //Console.WriteLine("ACTIVE WINDOW: " + ActiveWindow.GetActiveWindowTitle());

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MyApplicationContext());
        }
    }
}
