using SimpleInjector;
using System;
using System.Windows.Forms;
using TrackerLib.Implementations;
using TrackerLib.Interfaces;

namespace Tracker
{
    internal static class Program
    {
        private static readonly Container Container;

        static Program()
        {
            Container = new Container();
            Container.Register<ISettings, Settings>(Lifestyle.Singleton);
            Container.Register<ILogger, Logger>(Lifestyle.Singleton);
            Container.Register<IActiveWindowHandler, ActiveWindowHandler>();
            Container.Register<IPersistence, Persistence>();
            Container.Register<ISendOrSaveHandler, SendOrSaveHandler>();
            Container.Register<IAppTracker, AppTracker>();
            Container.Register<IRequests, Requests>();
            Container.Register<IDateTimeHandler, DateTimeHandler>();
        }

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        private static void Main()
        {
            var appTrackingHandler = Container.GetInstance<IAppTracker>();
            appTrackingHandler.StartTracking();


            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MyApplicationContext(appTrackingHandler));
        }
    }
}
