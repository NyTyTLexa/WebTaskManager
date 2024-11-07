using System.Text.Json.Serialization;

namespace WebTaskManager.Model
{
    public class Task
    {
        [JsonPropertyName("id")]
        public int id { get; set; }
        [JsonPropertyName("name")]
        public string Name { get; set; } = string.Empty;
        [JsonPropertyName("description")]
        public string Description { get; set; } = string.Empty;
        [JsonPropertyName("priority_id")]
        public int Priority_id { get; set; }
        [JsonPropertyName("status_id")]
        public int Status_id { get; set; }
        [JsonPropertyName("date_start")]
        public string date_start { get; set; } = string.Empty;
        [JsonPropertyName("date_end")]
        public string date_end { get; set; } = string.Empty;

    }
}
