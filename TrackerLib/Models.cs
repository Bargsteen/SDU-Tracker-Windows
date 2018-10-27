using System;
using Newtonsoft.Json;
using Realms;

namespace TrackerLib
{
  [JsonObject(MemberSerialization.OptIn)]
  public interface Usage
  {
    [JsonProperty("participant_identifier")]
    string ParticipantIdentifier { get; set; }

    [JsonProperty("device_model_name")]
    string DeviceModelName { get; set; }

    [JsonProperty("user_count")]
    int UserCount { get; set; }

    string UsageIdentifier { get; }
  }

  public class DeviceUsage : RealmObject, Usage
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

  public class AppUsage : RealmObject, Usage
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

  public enum EventType
  {
    Started = 1,
    Ended = 0
  }

  public static class Extensions
  {
    public static string ToJson(this Usage usage)
    {
      return JsonConvert.SerializeObject(usage);
    }

    public static string EventTypeToString(this int eventType)
    {
      switch (eventType)
      {
        case 1:
          return "Started";
        case 0:
          return "Ended";
        default:
          return "";
      }
    }
  }
}
