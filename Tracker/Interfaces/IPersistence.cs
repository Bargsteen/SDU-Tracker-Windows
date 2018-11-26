using System.Collections.Generic;
using Tracker.Models;

namespace Tracker.Interfaces
{
    public interface IPersistence
    {
        void Save<T>(T usage) where T : Usage;
        void Delete<T>(T usage) where T : Usage;
        List<AppUsage> FetchAppUsages(int upTo);
        List<DeviceUsage> FetchDeviceUsages(int upTo);
    }
}
