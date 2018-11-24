using log4net;
using log4net.Config;
using System.IO;
using System.Reflection;
using TrackerLib.Interfaces;
using TrackerLib.Models;
using TrackerLib.Enums;

namespace TrackerLib.Implementations
{
    public class Logger : ILogger
    {
        private readonly ILog _log;
        public Logger()
        {
            _log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
            var logRepository = LogManager.GetRepository(Assembly.GetEntryAssembly());
            XmlConfigurator.ConfigureAndWatch(logRepository, new FileInfo("log4net.config"));
            logRepository.Shutdown();
        }

        public void LogInfo(string msg) => _log.Info(msg);
        public void LogDebug(string msg) => _log.Debug(msg);
        public void LogError(string msg) => _log.Error(msg);
        public void LogFatal(string msg) => _log.Fatal(msg);


        public void LogUsage<T>(T usage, UsageLogType usageLogType) where T : Usage
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

            LogInfo(logMsg);
        }
    }
}