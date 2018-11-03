using SimpleInjector;
using System;
using System.Windows.Forms;
using TrackerLib.Implementations;
using TrackerLib.Interfaces;

namespace Tracker
{
    internal static class Program
    {
        private static readonly Container container;

        static Program()
        {
            container = new Container();
            container.Register<ISettings, Settings>(Lifestyle.Singleton);
            container.Register<ILogging, Logging>(Lifestyle.Singleton);
            container.Register<IActiveWindowHandler, ActiveWindowHandler>();
            container.Register<IPersistence, Persistence>();
            container.Register<ISendOrSaveHandler, SendOrSaveHandler>();
            container.Register<IAppTrackingHandler, AppTrackingHandler>();
            container.Register<IRequests, Requests>();
            container.Register<IDateTimeHandler, DateTimeHandler>();
        }

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        private static void Main()
        {
            var appTrackingHandler = container.GetInstance<IAppTrackingHandler>();
            appTrackingHandler.StartTracking();


            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MyApplicationContext(appTrackingHandler));
        }
    }
}
