using Newtonsoft.Json;

namespace ScorecardApi.Models
{
    public class SchoolQueryResult
    {
        [JsonProperty("metadata")]
        public SchoolQueryMetadata Metadata { get; set; }

        [JsonProperty("results", NullValueHandling = NullValueHandling.Ignore)]
        public QuerySchool[] QuerySchools { get; set; }

        [JsonProperty("schools", NullValueHandling = NullValueHandling.Ignore)]
        public ResultSchool[] ResultSchools { get; set; }

        public static SchoolQueryResult FromJson(string json)
        {
            return JsonConvert.DeserializeObject<SchoolQueryResult>(json, new JsonSerializerSettings
            {
                MetadataPropertyHandling = MetadataPropertyHandling.Ignore,
                DateParseHandling = DateParseHandling.None,
            });
        }
    }

    public static class Serialize
    {
        public static string ToJson(this SchoolQueryResult self)
        {
            return JsonConvert.SerializeObject(self, new JsonSerializerSettings
            {
                MetadataPropertyHandling = MetadataPropertyHandling.Ignore,
                DateParseHandling = DateParseHandling.None
            });
        }
    }
}