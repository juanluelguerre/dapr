using Newtonsoft.Json;

namespace Models
{
    public class Data
    {
        [JsonProperty("id")]
        public int Id { get; set; }
        [JsonProperty("name")]
        public string Name { get; set; }
    }
}
