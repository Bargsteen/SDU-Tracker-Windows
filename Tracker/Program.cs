using SimpleInjector;
using System;
using System.Windows.Forms;
using Tracker.Implementations;
using TrackerLib.Enums;
using TrackerLib.Implementations;
using TrackerLib.Interfaces;

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
        private static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            var trackerApplicationContext = Container.GetInstance<TrackerApplicationContext>();
            
            Application.Run(trackerApplicationContext);
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
            container.Register<IAlertService, AlertService>();
            container.Register<IUsageBuilder, UsageBuilder>();
            container.Register<ILaunchAtLoginService, LaunchAtLoginService>();
            container.Register<IUserService, UserService>();
            container.Register<IResendService, ResendService>();
            container.Register<IUserWindow, UserWindow>();
            container.Register<TrackerApplicationContext>();

            return container;
        }

        private static IPersistence GetPersistence()
        {
            return new Persistence();
        }

        private static readonly ISettings Settings = new Settings()
        {
            AppHasBeenSetup = true,
            CurrentUser = "Kasper",
            StopTrackingDate = DateTimeOffset.MaxValue,
            TrackingType = TrackingType.AppAndDevice,
            UserId = "Bargsteen",
            Users = { "Kasper", "Kasper2", "Kasper3" }
        };

        private static ISettings GetSettings()
        {
            return Settings;
        }
    }
}
