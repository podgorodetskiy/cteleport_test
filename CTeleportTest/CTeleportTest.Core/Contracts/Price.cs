using CTeleportTest.Core.Contracts.Enums;
using Newtonsoft.Json;

namespace CTeleportTest.Core.Contracts
{
    public class Price
    {
        [JsonProperty("total", NullValueHandling = NullValueHandling.Ignore)]
        public double? Total { get; set; }

        [JsonProperty("per_mile", NullValueHandling = NullValueHandling.Ignore)]
        public double? PerMile { get; set; }

        [JsonProperty("ccy", NullValueHandling = NullValueHandling.Ignore)]
        public Currency? Ccy { get; set; }
    }
}