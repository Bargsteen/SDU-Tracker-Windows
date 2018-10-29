using SimpleInjector;
using System;
using System.Windows.Forms;
using TrackerLib;
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
            container.Register<IAppTimeKeeper, AppTimeKeeper>();
            container.Register<IPersistence, Persistence>();
            container.Register<ISendOrSaveHandler, SendOrSaveHandler>();
        }

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        private static void Main()
        {
            var appTimeKeeper = container.GetInstance<IAppTimeKeeper>();


            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MyApplicationContext(appTimeKeeper));
        }
    }
}
