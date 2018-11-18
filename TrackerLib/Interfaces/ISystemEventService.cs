using System;

namespace TrackerLib.Interfaces
{
    public interface ISystemEventService : IDisposable
    {
        event EventHandler SystemSuspended;
        event EventHandler SystemStartedOrResumed;
    }
}