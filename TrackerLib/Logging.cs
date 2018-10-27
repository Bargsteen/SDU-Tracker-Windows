using System;
using System.IO;
using System.Reflection;
using log4net;
using log4net.Config;

namespace TrackerLib
{
  public class Logging
  {
    private static readonly ILog _log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

    public static void SetupLogging()
    {
      var logRepository = LogManager.GetRepository(Assembly.GetEntryAssembly());
      XmlConfigurator.Configure(logRepository, new FileInfo("log4net.config"));
    }

    public static void LogInfo(string msg) => _log.Info(msg);
    public static void LogDebug(string msg) => _log.Debug(msg);
    public static void LogError(string msg) => _log.Error(msg);
    public static void LogFatal(string msg) => _log.Fatal(msg);


    public static void LogUsage<T>(T usage, UsageLogType usageLogType) where T : Usage
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

  public enum UsageLogType { SentDirectly, SentFromPersistence, Saved }
}