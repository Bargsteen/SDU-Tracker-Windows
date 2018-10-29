using Newtonsoft.Json;

namespace TrackerLib.Models
{
    [JsonObject(MemberSerialization.OptIn)]
    public interface IUsage
    {
        [JsonProperty("participant_identifier")]
        string ParticipantIdentifier { get; set; }

        [JsonProperty("device_model_name")]
        string DeviceModelName { get; set; }

        [JsonProperty("user_count")]
        int UserCount { get; set; }

        string UsageIdentifier { get; }
    }
}
