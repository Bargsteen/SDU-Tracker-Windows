using System.Collections.Generic;
using Realms;
using System.Linq;
using System;

namespace TrackerLib 
{
  public class Persistence 
  {
    public static string RealmFileLocation => Realm.GetInstance().Config.DatabasePath;

    public static void SaveUsage<T>(T usage) where T : RealmObject, Usage
    {
      var realm = Realm.GetInstance();
      realm.Write(() => realm.Add<T>(usage, true));
    }
    
    public static void DeleteUsage<T>(T usage) where T : RealmObject, Usage
    {
      var realm = Realm.GetInstance();

      using(var transaction = realm.BeginWrite())
      {
        realm.Remove(usage);
        transaction.Commit();
      }
    }

    public static IQueryable<AppUsage> FetchAppUsages()
    {
      var realm = Realm.GetInstance();
      var usages = realm.All<AppUsage>().OrderBy(u => u.TimeStamp);
      return usages;
    }
    
    public static IQueryable<DeviceUsage> FetchDeviceUsages()
    {
      var realm = Realm.GetInstance();
      var usages = realm.All<DeviceUsage>().OrderBy(u => u.TimeStamp);
      return usages;
    }
  }
}