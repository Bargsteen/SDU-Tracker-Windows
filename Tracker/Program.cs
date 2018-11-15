using SimpleInjector;
using System;
using System.Windows.Forms;
using TrackerLib.Implementations;
using TrackerLib.Interfaces;
using TrackerLib.Models;

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
            Container.RegisterInstance(GetPersistence());
            Container.Register<ISendOrSaveHandler, SendOrSaveHandler>();
            Container.Register<IAppTracker, AppTracker>();
            Container.Register<IRequests, Requests>();
            Container.Register<IDateTimeHandler, DateTimeHandler>();
        }

        static IPersistence GetPersistence()
        {
            return new Persistence();
        }

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        private static void Main()
        {
            //var appTrackingHandler = Container.GetInstance<IAppTracker>();
            //appTrackingHandler.StartTracking();
            var persistence = Container.GetInstance<IPersistence>();
            var appUsage = new AppUsage("parti", "mbp", DateTimeOffset.Now, 1, "package", 1);

            //persistence.Save(appUsage);

            var fetchedUsages = persistence.FetchAppUsages(upTo: 10);

            fetchedUsages.ForEach(Console.WriteLine);
            

            /*
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MyApplicationContext(appTrackingHandler));*/
        }
    }
}
