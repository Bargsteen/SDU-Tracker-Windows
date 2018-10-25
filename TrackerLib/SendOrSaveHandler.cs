using System;
using System.Linq;
using Realms;


namespace TrackerLib
{
  public static class SendOrSaveHandler
  {
    public static void SendOrSaveUsage<T>(T usage, Credentials credentials, bool fromPersistence)
      where T : RealmObject, Usage
    {
      Action onSuccess;
      Action onError;

      if (fromPersistence)
      {
        onSuccess = () => Persistence.DeleteUsage(usage);
        onError = () => { }; // Let it stay in persistence.
      }
      else
      {
        onSuccess = () => { }; // Nothing left to do.
        onError = () => Persistence.SaveUsage(usage);
      }

      Requests.SendUsageAsync(usage, credentials, onSuccess, onError);

    }

    public static void SendSomeUsagesFromPersistence(Credentials credentials, int limitOfEach = 10)
    {
      var appUsages = Persistence.FetchAppUsages();
      var deviceUsages = Persistence.FetchDeviceUsages();
      
      int appUsagesCount = appUsages.Count();
      int deviceUsagesCount = deviceUsages.Count();

      for (int i = 0; i < limitOfEach && i < appUsagesCount; i++)
      {
        Console.WriteLine(appUsagesCount);
        SendOrSaveUsage(appUsages.First(), credentials, true);
      }

      for (int i = 0; i < limitOfEach && i < deviceUsagesCount; i++)
      {
        Console.WriteLine(deviceUsagesCount);
        SendOrSaveUsage(deviceUsages.First(), credentials, true);
      }
    }
  }
}