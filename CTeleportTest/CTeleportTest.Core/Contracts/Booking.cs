using System;
using System.Collections.Generic;
using CTeleportTest.Core.Contracts.Enums;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace CTeleportTest.Core.Contracts
{
    public class Booking
    {
        [JsonProperty("id", NullValueHandling = NullValueHandling.Ignore)]
        public string Id { get; set; }

        [JsonProperty("locator", NullValueHandling = NullValueHandling.Ignore)]
        public string Locator { get; set; }

        [JsonProperty("pax_name", NullValueHandling = NullValueHandling.Ignore)]
        public string PaxName { get; set; }

        [JsonProperty("terms", NullValueHandling = NullValueHandling.Ignore)]
        public BookingTerms Terms { get; set; }

        [JsonProperty("metadata", NullValueHandling = NullValueHandling.Ignore)]
        public BookingMetadata Metadata { get; set; }

        [JsonProperty("state", NullValueHandling = NullValueHandling.Ignore)]
        public State? State { get; set; }

        [JsonProperty("no_show", NullValueHandling = NullValueHandling.Ignore)]
        public bool? NoShow { get; set; }

        [JsonProperty("price", NullValueHandling = NullValueHandling.Ignore)]
        public Price Price { get; set; }

        [JsonProperty("departure_at", NullValueHandling = NullValueHandling.Ignore)]
        [JsonConverter(typeof(UnixDateTimeConverter))]
        public DateTime? DepartureAt { get; set; }

        [JsonProperty("legs", NullValueHandling = NullValueHandling.Ignore)]
        public List<Leg> Legs { get; set; }

        [JsonProperty("created_at", NullValueHandling = NullValueHandling.Ignore)]
        [JsonConverter(typeof(UnixDateTimeConverter))]
        public DateTime? CreatedAt { get; set; }

        [JsonProperty("created_by", NullValueHandling = NullValueHandling.Ignore)]
        public User CreatedBy { get; set; }

        [JsonProperty("tenant_id", NullValueHandling = NullValueHandling.Ignore)]
        public string TenantId { get; set; }

        [JsonProperty("autofilled", NullValueHandling = NullValueHandling.Ignore)]
        public bool? Autofilled { get; set; }
    }
}