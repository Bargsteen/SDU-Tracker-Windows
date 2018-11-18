using System;
using TrackerLib.Interfaces;

namespace TrackerLib.Implementations
{
    public class DateTimeService : IDateTimeService
    {
        public DateTimeOffset CurrentTime => DateTimeOffset.UtcNow;
    }
}
