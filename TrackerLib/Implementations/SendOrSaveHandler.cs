using System;
using TrackerLib.Interfaces;
using TrackerLib.Models;
using TrackerLib.Enums;

namespace TrackerLib.Implementations
{
    public class SendOrSaveHandler : ISendOrSaveHandler
    {
        private readonly ILogger _logger;
        private readonly IPersistence _persistence;
        private readonly IRequests _requests;

        private readonly Credentials _credentials;

        public SendOrSaveHandler(ILogger logger, IPersistence persistence, IRequests requests, ISettings settings)
        {
            _logger = logger;
            _persistence = persistence;
            _requests = requests;

            _credentials = settings.Credentials;
        }

        public void SendOrSaveUsage<T>(T usage, bool fromPersistence)
          where T : Usage
        {
            Action onSuccess;
            Action onError;

            if (fromPersistence)
            {
                onSuccess = () =>
                {
                    _persistence.Delete(usage);
                    _logger.LogUsage(usage, UsageLogType.SentFromPersistence);
                };
                onError = () => { }; // Let it stay in persistence.
            }
            else
            {
                onSuccess = () => _logger.LogUsage(usage, UsageLogType.SentDirectly);
                onError = () =>
                {
                    _persistence.Save(usage);
                    _logger.LogUsage(usage, UsageLogType.Saved);
                };
            }

            _requests.SendUsageAsync(usage, _credentials, onSuccess, onError);

        }

        public void SendSomeUsagesFromPersistence(int limitOfEach = 10)
        {
            var appUsages = _persistence.FetchAppUsages(upTo: limitOfEach);
            var deviceUsages = _persistence.FetchDeviceUsages(upTo: limitOfEach);

            appUsages.ForEach(appUsage => SendOrSaveUsage(appUsage, true));
            deviceUsages.ForEach(deviceUsage => SendOrSaveUsage(deviceUsage, true)); 
        }
    }
}