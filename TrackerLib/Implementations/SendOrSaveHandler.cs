using Realms;
using System;
using System.Linq;
using TrackerLib.Interfaces;
using TrackerLib.Models;
using TrackerLib.Enums;

namespace TrackerLib.Implementations
{
    public class SendOrSaveHandler : ISendOrSaveHandler
    {
        private readonly ILogging logging;
        private readonly IPersistence persistence;
        private readonly IRequests requests;
        private readonly ISettings settings;

        private readonly Credentials credentials;

        public SendOrSaveHandler(ILogging logging, IPersistence persistence, IRequests requests, ISettings settings)
        {
            this.logging = logging;
            this.persistence = persistence;
            this.requests = requests;
            this.settings = settings;

            credentials = settings.Credentials;
        }

        public void SendOrSaveUsage<T>(T usage, bool fromPersistence)
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

        public void SendSomeUsagesFromPersistence(int limitOfEach = 10)
        {
            var appUsages = persistence.FetchAppUsages();
            var deviceUsages = persistence.FetchDeviceUsages();

            int appUsagesCount = appUsages.Count();
            int deviceUsagesCount = deviceUsages.Count();

            for (int i = 0; i < limitOfEach && i < appUsagesCount; i++)
            {
                SendOrSaveUsage(appUsages.First(), true);
            }

            for (int i = 0; i < limitOfEach && i < deviceUsagesCount; i++)
            {
                SendOrSaveUsage(deviceUsages.First(), true);
            }
        }
    }
}