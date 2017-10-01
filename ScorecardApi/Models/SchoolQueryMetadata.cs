using Newtonsoft.Json;

namespace ScorecardApi.Models
{
    public class SchoolQueryMetadata
    {
        [JsonProperty("per_page")]
        public long SchoolsPerPage { get; set; }

        [JsonProperty("page")]
        public long Page { get; set; }

        [JsonProperty("total")]
        public long Total { get; set; }
    }
}