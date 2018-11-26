using System;

namespace Tracker.Models
{
    public class ActiveWindow : ICloneable
    {
        public string Identifier { get; }
        public DateTimeOffset StartTime { get; }
        public DateTimeOffset EndTime { get; set; }

        public ActiveWindow(string identifier, DateTimeOffset startTime)
        {
            Identifier = identifier;
            StartTime = startTime;
        }

        public object Clone()
        {
            return MemberwiseClone();
        }
    }
}
