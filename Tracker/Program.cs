using SimpleInjector;
using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Tracker.Enums;
using Tracker.Implementations;
using Tracker.Interfaces;
using Container = SimpleInjector.Container;

namespace Tracker
{
    internal static class Program
    {
        private static readonly Container Container;

        static Program()
        {
            Container = GetContainer();
        }

      
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        private static void Main(string[] args)
        {
            try
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);

                if (args.Length > 0) // Should be from the setup link
                 {
                     var setupService = Container.GetInstance<ISetupService>();
                     setupService.SetupAppByUri(args[0]);
                 }

                 var trackerApplicationContext = Container.GetInstance<TrackerApplicationContext>();

                 Application.Run(trackerApplicationContext);
            }
            catch(Exception e)
            {
                var logger = Container.GetInstance<ILogger>();
                logger.LogError(e.Message);
            }
        }

        private static Container GetContainer()
        {
            var container = new Container();

            //container.Register<ISettings, Settings>(Lifestyle.Singleton);
            container.RegisterInstance(GetSettings());
            container.Register<ILogger, Logger>(Lifestyle.Singleton);
            container.Register<IActiveWindowService, ActiveWindowService>();
            container.RegisterInstance(GetPersistence());
            container.Register<ISendOrSaveService, SendOrSaveService>();
            container.Register<IAppTracker, AppTracker>();
            container.Register<IRequests, Requests>();
            container.Register<IDateTimeService, DateTimeService>();
            container.Register<IRunner, Runner>();
            container.Register<ISleepService, SleepService>();
            container.Register<ISystemEventService, SystemEventService>();
            container.Register<IDeviceTracker, DeviceTracker>();
            container.Register<IAlertService, AlertService>(Lifestyle.Singleton);
            container.Register<IUsageBuilder, UsageBuilder>();
            container.Register<ILaunchAtLoginService, LaunchAtLoginService>();
            container.Register<IUserService, UserService>(Lifestyle.Singleton);
            container.Register<IResendService, ResendService>();
            container.Register<IUserWindow, UserWindow>(Lifestyle.Singleton);;
            container.Register<TrackerApplicationContext>();
            container.Register<ISetupService, SetupService>();

            return container;
        }

        private static IPersistence GetPersistence()
        {
            return new Persistence();
        }

        private static ISettings GetSettings()
        {
            var settings = new Settings();

            if (!settings.AppHasBeenSetup)
            {
                settings.AppHasBeenSetup = true;
                settings.Users = new List<string> { "Kasper", "Kasper2" };
                settings.UserId = "Bargsteen";
                settings.CurrentUser = "Kasper";
                settings.StopTrackingDate = DateTimeOffset.MaxValue;
                settings.TrackingType = TrackingType.AppAndDevice;
            }

            return settings;
        }
    }
}
