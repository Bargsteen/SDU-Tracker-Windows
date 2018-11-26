using System;
using Tracker.Models;

namespace Tracker.Interfaces
{
    public interface IRequests
    {
        void SendUsageAsync(Usage usage, Credentials credentials, Action onSuccess, Action onError);
    }
}
