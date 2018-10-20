using System;
using Newtonsoft.Json;

namespace TrackerLib
{
  public abstract class Usage
  {
    public Usage(string participantIdentifier,
    string deviceModelName, int userCount)
    {
      ParticipantIdentifier = participantIdentifier;
      DeviceModelName = deviceModelName;
      UserCount = userCount;
    }

    [JsonProperty("participant_identifier")]
    public string ParticipantIdentifier { get; set; }

    [JsonProperty("device_model_name")]
    public string DeviceModelName { get; set; }

    [JsonProperty("user_count")]
    public int UserCount { get; set; }    
  }

  public class DeviceUsage : Usage
  {
    public DeviceUsage(string participantIdentifier, string deviceModelName,
    int userCount, DateTime timeStamp, EventType eventType)
    : base(participantIdentifier, deviceModelName, userCount)
    {
      EventType = eventType;
    }

    [JsonProperty("timestamp")]
    public DateTime TimeStamp { get; set; }

    [JsonProperty("event_type")]
    public EventType EventType { get; set; }
  }

  public class AppUsage : Usage
  {
    public AppUsage(string participantIdentifier,
    string deviceModelName, int userCount, DateTime date, string package, int duration)
    : base(participantIdentifier, deviceModelName, userCount)
    {
      Date = date;
      Package = package;
      Duration = duration;
    }

    [JsonProperty("date")]
    public DateTime Date { get; set; }

    [JsonProperty("package")]
    public string Package { get; set; }

    [JsonProperty("duration")]
    public int Duration { get; set; }
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
