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
        onSuccess = () =>
        {
          Persistence.DeleteUsage(usage);
          Logging.LogUsage(usage, UsageLogType.SentFromPersistence);
        };
        onError = () => { }; // Let it stay in persistence.
      }
      else
      {
        onSuccess = () => Logging.LogUsage(usage, UsageLogType.SentDirectly);
        onError = () =>
        {
          Persistence.SaveUsage(usage);
          Logging.LogUsage(usage, UsageLogType.Saved);
        };
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
        SendOrSaveUsage(appUsages.First(), credentials, true);
      }

      for (int i = 0; i < limitOfEach && i < deviceUsagesCount; i++)
      {
        SendOrSaveUsage(deviceUsages.First(), credentials, true);
      }
    }
  }
}