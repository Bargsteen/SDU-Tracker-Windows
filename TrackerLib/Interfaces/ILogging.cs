using TrackerLib.Models;
using TrackerLib.Enums;

namespace TrackerLib.Interfaces
{
    public interface ILogging
    {
        void Usage<T>(T usage, UsageLogType usageLogType) where T : IUsage;

        void Info(string msg);
        void Debug(string msg);
        void Error(string msg);
        void Fatal(string msg);
    }
}