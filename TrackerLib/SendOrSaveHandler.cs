using Realms;
using System;
using System.Linq;
using TrackerLib.Interfaces;
using TrackerLib.Models;

namespace TrackerLib
{
    public class SendOrSaveHandler : ISendOrSaveHandler
    {
        private readonly IPersistence persistence;
        private readonly ILogging logging;
        private readonly IRequests requests;

        public SendOrSaveHandler(IPersistence persistence, ILogging logging, IRequests requests)
        {
            this.persistence = persistence;
            this.logging = logging;
            this.requests = requests;
        }

        public void SendOrSaveUsage<T>(T usage, Credentials credentials, bool fromPersistence)
          where T : RealmObject, IUsage
        {
            Action onSuccess;
            Action onError;

            if (fromPersistence)
            {
                onSuccess = () =>
                {
                    persistence.DeleteUsage(usage);
                    logging.Usage(usage, UsageLogType.SentFromPersistence);
                };
                onError = () => { }; // Let it stay in persistence.
            }
            else
            {
                onSuccess = () => logging.Usage(usage, UsageLogType.SentDirectly);
                onError = () =>
                {
                    persistence.SaveUsage(usage);
                    logging.Usage(usage, UsageLogType.Saved);
                };
            }

            requests.SendUsageAsync(usage, credentials, onSuccess, onError);

        }

        public void SendSomeUsagesFromPersistence(Credentials credentials, int limitOfEach = 10)
        {
            var appUsages = persistence.FetchAppUsages();
            var deviceUsages = persistence.FetchDeviceUsages();

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