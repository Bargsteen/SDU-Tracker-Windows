namespace TrackerLib.Interfaces
{
    public interface IResendService
    {
        void StartPeriodicResendingOfSavedUsages(int intervalInSeconds, int limitOfEachUsage);
    }
}