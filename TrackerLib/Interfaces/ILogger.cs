using TrackerLib.Enums;
using TrackerLib.Models;

namespace TrackerLib.Interfaces
{
    public interface ILogger
    {
        void LogUsage<T>(T usage, UsageLogType usageLogType) where T : Usage;

        void LogInfo(string msg);
        void LogDebug(string msg);
        void LogError(string msg);
        void LogFatal(string msg);
    }
}