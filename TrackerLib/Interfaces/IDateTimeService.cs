using System;

namespace TrackerLib.Interfaces
{
    public interface IDateTimeService
    {
        DateTimeOffset CurrentTime { get; }
    }
}
