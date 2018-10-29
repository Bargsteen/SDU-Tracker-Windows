using Realms;
using System.Linq;
using TrackerLib.Interfaces;
using TrackerLib.Models;


namespace TrackerLib
{
    public class Persistence : IPersistence
    {
        public void SaveUsage<T>(T usage) where T : RealmObject, IUsage
        {
            var realm = Realm.GetInstance();
            realm.Write(() => realm.Add(usage, true));
        }

        public void DeleteUsage<T>(T usage) where T : RealmObject, IUsage
        {
            var realm = Realm.GetInstance();

            using (var transaction = realm.BeginWrite())
            {
                realm.Remove(usage);
                transaction.Commit();
            }
        }

        public IQueryable<AppUsage> FetchAppUsages()
        {
            var realm = Realm.GetInstance();
            var usages = realm.All<AppUsage>().OrderBy(u => u.TimeStamp);
            return usages;
        }

        public IQueryable<DeviceUsage> FetchDeviceUsages()
        {
            var realm = Realm.GetInstance();
            var usages = realm.All<DeviceUsage>().OrderBy(u => u.TimeStamp);
            return usages;
        }
    }
}