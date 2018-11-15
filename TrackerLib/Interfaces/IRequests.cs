using System;
using TrackerLib.Models;

namespace TrackerLib.Interfaces
{
    public interface IRequests
    {
        void SendUsageAsync(Usage usage, Credentials credentials, Action onSuccess, Action onError);
    }
}
