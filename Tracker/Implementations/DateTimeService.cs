using System;
using Tracker.Interfaces;

namespace Tracker.Implementations
{
    public class DateTimeService : IDateTimeService
    {
        public DateTimeOffset CurrentTime => DateTimeOffset.UtcNow;
    }
}
