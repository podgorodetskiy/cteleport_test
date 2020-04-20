using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace CTeleportTest.Core.Contracts
{
    public class Leg
    {
        [JsonProperty("origin", NullValueHandling = NullValueHandling.Ignore)]
        public string Origin { get; set; }

        [JsonProperty("destination", NullValueHandling = NullValueHandling.Ignore)]
        public string Destination { get; set; }

        [JsonProperty("departure", NullValueHandling = NullValueHandling.Ignore)]
        [JsonConverter(typeof(UnixDateTimeConverter))]
        public DateTime? Departure { get; set; }

        [JsonProperty("arrival", NullValueHandling = NullValueHandling.Ignore)]
        [JsonConverter(typeof(UnixDateTimeConverter))]
        public DateTime? Arrival { get; set; }

        [JsonProperty("is_direct", NullValueHandling = NullValueHandling.Ignore)]
        public bool? IsDirect { get; set; }
    }
}