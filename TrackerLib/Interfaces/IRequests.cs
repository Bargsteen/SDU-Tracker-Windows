using System;
using TrackerLib.Models;

namespace TrackerLib.Interfaces
{
    public interface IRequests
    {
        void SendUsageAsync(IUsage usage, Credentials credentials, Action onSuccess, Action onError);
    }
}
