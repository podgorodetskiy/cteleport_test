using System;
using System.Collections.Generic;
using CTeleportTest.Core.Contracts.Enums;
using Newtonsoft.Json;

namespace CTeleportTest.Core.Contracts
{
    public class BookingMetadata
    {
        [JsonProperty("custom_fields", NullValueHandling = NullValueHandling.Ignore)]
        public Dictionary<string, string> CustomFields { get; set; }

        [JsonProperty("vessel_name", NullValueHandling = NullValueHandling.Ignore)]
        public string VesselName { get; set; }

        [JsonProperty("vessel_flag", NullValueHandling = NullValueHandling.Ignore)]
        public string VesselFlag { get; set; }

        [JsonProperty("crew_change_member", NullValueHandling = NullValueHandling.Ignore)]
        public CrewChangeMember? CrewChangeMember { get; set; }

        [JsonProperty("crew_change_airport", NullValueHandling = NullValueHandling.Ignore)]
        public string CrewChangeAirport { get; set; }

        [JsonProperty("crew_change_date", NullValueHandling = NullValueHandling.Ignore)]
        public DateTimeOffset? CrewChangeDate { get; set; }

        [JsonProperty("exempt_li_tax", NullValueHandling = NullValueHandling.Ignore)]
        public bool? ExemptLiTax { get; set; }
    }
}