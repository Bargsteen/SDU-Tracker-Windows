using SimpleInjector;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;
using Tracker.Implementations;
using TrackerLib.Constants;
using TrackerLib.Enums;
using TrackerLib.Implementations;
using TrackerLib.Interfaces;
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
            
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            var trackerApplicationContext = Container.GetInstance<TrackerApplicationContext>();

            HandleArgs(args);

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

        private static void HandleArgs(IReadOnlyList<string> args)
        {
            var alertService = Container.GetInstance<IAlertService>();
            var setupService = new SetupService();

            if (args.Count <= 0) return;

            if (Uri.TryCreate(args[0], UriKind.Absolute, out var uri) &&
                string.Equals(uri.Scheme, SetupConstants.UriScheme, StringComparison.OrdinalIgnoreCase))
            {
                setupService.SetupAppByUri(uri);
                alertService.ShowAlert("Opened by URL:", uri.ToString(), "Ok");
            }
        }
    }
}
