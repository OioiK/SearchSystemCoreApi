using Newtonsoft.Json;

namespace SearchSystemCoreApi
{
    internal class ResponseModel
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("statusCode")]
        public string StatusCode { get; set; }

        [JsonProperty("time")]
        public string Time { get; set; }
    }
}
