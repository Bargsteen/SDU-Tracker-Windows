using System;

namespace TrackerLib.Interfaces
{
    public interface IDateTimeHandler
    {
        DateTimeOffset Now { get; }
    }
}
