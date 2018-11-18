using TrackerLib.Models;

namespace TrackerLib.Interfaces
{
    public interface ISendOrSaveService
    {
        void SendOrSaveUsage<T>(T usage, bool fromPersistence = false)
            where T : Usage;

        void SendSomeUsagesFromPersistence(int limitOfEach = 10);
    }
}
