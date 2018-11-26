using System;

namespace Tracker.Interfaces
{
    public interface ISystemEventService : IDisposable
    {
        event EventHandler SystemSuspended;
        event EventHandler SystemStartedOrResumed;
    }
}