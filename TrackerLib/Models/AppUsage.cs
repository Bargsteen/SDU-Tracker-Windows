using Newtonsoft.Json;
using System;

namespace TrackerLib.Models
{
    public class AppUsage : Usage
    {
        protected AppUsage() {}

        public AppUsage(string participantIdentifier, string deviceModelName,
                        DateTimeOffset timeStamp, int userCount, string package,
                        int duration) : base(participantIdentifier, deviceModelName, userCount)
        {
            TimeStamp = timeStamp;
            
            Package = package;
            Duration = duration;
        }

        public AppUsage(string participantIdentifier, string deviceModelName,
            DateTimeOffset timeStamp, int userCount, string package,
            int duration, int id) : base(participantIdentifier, deviceModelName, userCount, id)
        {
            TimeStamp = timeStamp;

            Package = package;
            Duration = duration;
        }

        [JsonProperty("date")]
        public sealed override DateTimeOffset TimeStamp { get; set; }

        [JsonProperty("package")]
        public string Package { get; set; }

        [JsonProperty("duration")]
        public int Duration { get; set; }
    }
}
