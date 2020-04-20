using Newtonsoft.Json;

namespace CTeleportTest.Core.Contracts
{
    public class Leg
    {
        [JsonProperty("origin", NullValueHandling = NullValueHandling.Ignore)]
        public string Origin { get; set; }

        [JsonProperty("destination", NullValueHandling = NullValueHandling.Ignore)]
        public string Destination { get; set; }

        [JsonProperty("departure", NullValueHandling = NullValueHandling.Ignore)]
        public long? Departure { get; set; }

        [JsonProperty("arrival", NullValueHandling = NullValueHandling.Ignore)]
        public long? Arrival { get; set; }

        [JsonProperty("is_direct", NullValueHandling = NullValueHandling.Ignore)]
        public bool? IsDirect { get; set; }
    }
}