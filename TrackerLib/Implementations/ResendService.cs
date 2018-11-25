using System.Threading;
using TrackerLib.Interfaces;

namespace TrackerLib.Implementations
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
                _sendOrSaveService.SendSomeUsagesFromPersistence(limitOfEachUsage);
                _sleepService.SleepFor(intervalInSeconds);
                
            });
            thread.IsBackground = true;
            thread.Start();
        }
    }
}