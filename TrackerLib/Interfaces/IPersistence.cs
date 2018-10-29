using System.Linq;
using Realms;
using TrackerLib.Models;

namespace TrackerLib.Interfaces
{
    public interface IPersistence
    {
        void SaveUsage<T>(T usage) where T : RealmObject, IUsage;
        void DeleteUsage<T>(T usage) where T : RealmObject, IUsage;
        IQueryable<AppUsage> FetchAppUsages();
        IQueryable<DeviceUsage> FetchDeviceUsages();
    }
}
