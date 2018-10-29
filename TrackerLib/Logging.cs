using log4net;
using log4net.Config;
using System.IO;
using System.Reflection;
using TrackerLib.Interfaces;
using TrackerLib.Models;

namespace TrackerLib
{
    public class Logging : ILogging
    {
        private readonly ILog _log;
        public Logging()
        {
            _log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
            var logRepository = LogManager.GetRepository(Assembly.GetEntryAssembly());
            XmlConfigurator.ConfigureAndWatch(logRepository, new FileInfo("log4net.config"));
        }

        public void Info(string msg) => _log.Info(msg);
        public void Debug(string msg) => _log.Debug(msg);
        public void Error(string msg) => _log.Error(msg);
        public void Fatal(string msg) => _log.Fatal(msg);


        public void Usage<T>(T usage, UsageLogType usageLogType) where T : IUsage
        {
            string logMsg = "";

            switch (usageLogType)
            {
                case UsageLogType.SentDirectly:
                    logMsg += "Sent directly: ";
                    break;
                case UsageLogType.SentFromPersistence:
                    logMsg += "Sent from storage: ";
                    break;
                case UsageLogType.Saved:
                    logMsg += "Saved: ";
                    break;
            }

            switch (usage)
            {
                case DeviceUsage deviceUsage:
                    logMsg += $"[DEVICE] {deviceUsage.EventType.EventTypeToString()}";
                    break;
                case AppUsage appUsage:
                    logMsg += $"[APP] {appUsage.Package} - {appUsage.Duration} ms";
                    break;
            }

            Info(logMsg);
        }
    }

    public enum UsageLogType { SentDirectly, SentFromPersistence, Saved }
}