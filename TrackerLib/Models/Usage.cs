using System;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace TrackerLib.Models
{
    [JsonObject(MemberSerialization.OptIn)]
    public abstract class Usage
    {
        protected Usage(){}

        protected Usage(string participantIdentifier, string deviceModelName, int userCount)
        {
            ParticipantIdentifier = participantIdentifier;
            DeviceModelName = deviceModelName;
            UserCount = userCount;
        }

        protected Usage(string participantIdentifier, string deviceModelName, int userCount, int id)
        {
            ParticipantIdentifier = participantIdentifier;
            DeviceModelName = deviceModelName;
            UserCount = userCount;
            Id = id;
        }

        [JsonProperty("participant_identifier")]
        public string ParticipantIdentifier { get; set; }

        [JsonProperty("device_model_name")]
        public string DeviceModelName { get; set; }

        [JsonProperty("user_count")]
        public int UserCount { get; set; }

        public abstract DateTimeOffset TimeStamp { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
    }
}
