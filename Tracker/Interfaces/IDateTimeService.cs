using System;

namespace Tracker.Interfaces
{
    public interface IDateTimeService
    {
        DateTimeOffset CurrentTime { get; }
    }
}
