using Newtonsoft.Json;
using Realms;
using System;

namespace TrackerLib.Models
{
    public class AppUsage : RealmObject, IUsage
    {
        public AppUsage() { }
        public AppUsage(string participantIdentifier, string deviceModelName,
                        DateTimeOffset timeStamp, int userCount, string package,
                        int duration)
        {
            ParticipantIdentifier = participantIdentifier;
            DeviceModelName = deviceModelName;
            TimeStamp = timeStamp;
            UserCount = userCount;

            Package = package;
            Duration = duration;
        }
        public string ParticipantIdentifier { get; set; }
        public string DeviceModelName { get; set; }
        public int UserCount { get; set; }

        [JsonProperty("date")]
        public DateTimeOffset TimeStamp { get; set; }

        [JsonProperty("package")]
        public string Package { get; set; }

        [JsonProperty("duration")]
        public int Duration { get; set; }

        public string UsageIdentifier => $"app_{ParticipantIdentifier}_{TimeStamp}_{Package}";
    }
}
