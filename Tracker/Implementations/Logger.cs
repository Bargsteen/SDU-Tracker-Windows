using System;
using System.IO;
using System.Reflection;
using log4net;
using log4net.Config;
using Tracker.Enums;
using Tracker.Interfaces;
using Tracker.Models;

namespace Tracker.Implementations
{
    public class Logger : ILogger
    {
        private readonly ILog _log;
        public Logger()
        {
            _log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
            var logRepository = LogManager.GetRepository(Assembly.GetEntryAssembly());

            // When Tracker is run as a service, its working dir is system32.
            // So we need to provide the absolute path to the config file
            var configFilePath = $"{AppDomain.CurrentDomain.BaseDirectory}log4net.config";

            XmlConfigurator.ConfigureAndWatch(logRepository, new FileInfo(configFilePath));
        }

        public void LogInfo(string msg) => _log.Info(msg);
        public void LogDebug(string msg) => _log.Debug(msg);
        public void LogError(string msg) => _log.Error(msg);
        public void LogFatal(string msg) => _log.Fatal(msg);


        public void LogUsage<T>(T usage, UsageLogType usageLogType) where T : Usage
        {
            var logMsg = "";

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
                default:
                    throw new ArgumentOutOfRangeException(nameof(usageLogType), usageLogType, null);
            }

            switch (usage)
            {
                case DeviceUsage deviceUsage:
                    logMsg += $"[DEVICE] - {deviceUsage.ParticipantIdentifier} - {deviceUsage.EventType.EventTypeToString()}";
                    break;
                case AppUsage appUsage:
                    logMsg += $"[APP] - {appUsage.ParticipantIdentifier} - {appUsage.Package} - {appUsage.Duration} ms";
                    break;
            }

            LogInfo(logMsg);
        }
    }
}