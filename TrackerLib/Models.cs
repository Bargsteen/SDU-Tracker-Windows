using System;
using Newtonsoft.Json;

namespace TrackerLib
{
    public abstract class Usage
    {
        public Usage(string participantIdentifier, string timeStamp, 
        int userCount, string deviceModelName)
        {
            ParticipantIdentifier = participantIdentifier;
            TimeStamp = timeStamp;
            UserCount = userCount;
            DeviceModelName = deviceModelName;
        }

        [JsonProperty("participant_identifier")]
        public string ParticipantIdentifier {get;set;}

        [JsonProperty("date")]
        public string TimeStamp {get; set;}

        [JsonProperty("user_count")]
        public int UserCount {get; set;}

        [JsonProperty("device_model_name")]
        public string DeviceModelName {get; set;}
    }

    public class DeviceUsage : Usage 
    {
        public DeviceUsage(string participantIdentifier, string timeStamp, 
        int userCount, string deviceModelName, EventType eventType) 
        : base(participantIdentifier, timeStamp, userCount, deviceModelName)
        {
            EventType = eventType;
        }

        [JsonProperty("event_type")]
        public EventType EventType {get; set;}
    }

    public class AppUsage : Usage
    {
        public AppUsage(string participantIdentifier, string timeStamp, 
        int userCount, string deviceModelName, string package, int duration) 
        : base(participantIdentifier, timeStamp, userCount, deviceModelName)
        {
            Package = package;
            Duration = duration;
        }

        [JsonProperty("package")]
        public string Package {get; set;}
        
        [JsonProperty("duration")]
        public int Duration {get; set;}
    }

    public enum EventType 
    {
        Started = 1,
        Ended = 0
    }

    public static class UsageExtensions
    {
        public static string ToJson(this Usage usage)
        {
            return JsonConvert.SerializeObject(usage);
        }
    }
}
