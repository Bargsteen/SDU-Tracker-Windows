using Newtonsoft.Json;
using System;
using TrackerLib.Enums;

namespace TrackerLib.Models
{
    public class DeviceUsage : Usage
    {
        public DeviceUsage(){}

        public DeviceUsage(string participantIdentifier, string deviceModelName,
                           DateTimeOffset timeStamp, int userCount, EventType eventType)
         : base(participantIdentifier, deviceModelName, userCount)
        {
            TimeStamp = timeStamp;

            EventType = eventType.GetHashCode();
        }

        public DeviceUsage(string participantIdentifier, string deviceModelName,
            DateTimeOffset timeStamp, int userCount, EventType eventType, int id)
            : base(participantIdentifier, deviceModelName, userCount, id)
        {
            TimeStamp = timeStamp;

            EventType = eventType.GetHashCode();
        }

        [JsonProperty("timestamp")]
        public sealed override DateTimeOffset TimeStamp { get; set; }

        [JsonProperty("event_type")]
        public int EventType { get; set; }
    }
}
