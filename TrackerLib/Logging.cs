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
   
  }
}