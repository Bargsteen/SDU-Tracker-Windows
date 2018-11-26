using System.Threading;
using Tracker.Interfaces;

namespace Tracker.Implementations
{
    public class ResendService : IResendService
    {
        private readonly ISendOrSaveService _sendOrSaveService;
        private readonly ISleepService _sleepService;

        public ResendService(ISendOrSaveService sendOrSaveService, ISleepService sleepService)
        {
            _sendOrSaveService = sendOrSaveService;
            _sleepService = sleepService;
        }


        public void StartPeriodicResendingOfSavedUsages(int intervalInSeconds, int limitOfEachUsage)
        {
            var thread = new Thread(() =>
            {
                while (true)
                {
                    _sendOrSaveService.SendSomeUsagesFromPersistence(limitOfEachUsage);
                    _sleepService.SleepFor(intervalInSeconds);
                }
            }) {IsBackground = true};
            thread.Start();
        }
    }
}