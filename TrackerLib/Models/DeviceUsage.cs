using Newtonsoft.Json;
using Realms;
using System;

namespace TrackerLib.Models
{
    public class DeviceUsage : RealmObject, IUsage
    {

        public DeviceUsage() { }
        public DeviceUsage(string participantIdentifier, string deviceModelName,
                           DateTimeOffset timeStamp, int userCount, EventType eventType)
        {
            ParticipantIdentifier = participantIdentifier;
            DeviceModelName = deviceModelName;
            TimeStamp = timeStamp;
            UserCount = userCount;

            EventType = eventType.GetHashCode();
        }
        public string ParticipantIdentifier { get; set; }
        public string DeviceModelName { get; set; }
        public int UserCount { get; set; }

        [JsonProperty("timestamp")]
        public DateTimeOffset TimeStamp { get; set; }

        [JsonProperty("event_type")]
        public int EventType { get; set; }

        public string UsageIdentifier => $"device_{ParticipantIdentifier}_{TimeStamp}_{EventType}";
    }
}
