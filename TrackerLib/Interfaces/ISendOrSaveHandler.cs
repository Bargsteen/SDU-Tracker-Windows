using Realms;
using TrackerLib.Models;

namespace TrackerLib.Interfaces
{
    public interface ISendOrSaveHandler
    {
        void SendOrSaveUsage<T>(T usage, bool fromPersistence = false)
           where T : RealmObject, IUsage;

        void SendSomeUsagesFromPersistence(int limitOfEach = 10);
    }
}
