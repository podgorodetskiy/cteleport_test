using CTeleportTest.Core.Contracts.Enums;
using Newtonsoft.Json;

namespace CTeleportTest.Core.Contracts
{
    public class BookingTerms
    {
        [JsonProperty("fare_type", NullValueHandling = NullValueHandling.Ignore)]
        public FareType? FareType { get; set; }

        [JsonProperty("conditions", NullValueHandling = NullValueHandling.Ignore)]
        public Conditions? Conditions { get; set; }

        [JsonProperty("splitting", NullValueHandling = NullValueHandling.Ignore)]
        public bool? Splitting { get; set; }

        [JsonProperty("no_show_conditions", NullValueHandling = NullValueHandling.Ignore)]
        public Conditions? NoShowConditions { get; set; }

        [JsonProperty("can_cancel", NullValueHandling = NullValueHandling.Ignore)]
        public bool? CanCancel { get; set; }
    }
}