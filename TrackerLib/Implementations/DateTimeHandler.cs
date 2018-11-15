using System;
using TrackerLib.Interfaces;

namespace TrackerLib.Implementations
{
    public class DateTimeHandler : IDateTimeHandler
    {
        public DateTimeOffset CurrentTime => DateTimeOffset.UtcNow;
    }
}
