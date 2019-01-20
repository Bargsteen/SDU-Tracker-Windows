using Tracker.Models;

namespace Tracker.Interfaces
{
    public interface ISendOrSaveService
    {
        void SendOrSaveUsage<T>(T usage, bool fromPersistence = false)
            where T : Usage;

        void SendSomeUsagesFromPersistence(int limitOfEach = 10);

        void SaveUsage<T>(T usage) where T : Usage;
    }
}
