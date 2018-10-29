using Realms;
using TrackerLib.Models;

namespace TrackerLib.Interfaces
{
    public interface ISendOrSaveHandler
    {
        void SendOrSaveUsage<T>(T usage, Credentials credentials, bool fromPersistence)
           where T : RealmObject, IUsage;

        void SendSomeUsagesFromPersistence(Credentials credentials, int limitOfEach = 10);
    }
}
