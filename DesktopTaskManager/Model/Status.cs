using System.Text.Json.Serialization;

namespace WebTaskManager.Model
{
    public class Status
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }
        [JsonPropertyName("name")]
        public string Name { get; set; } = string.Empty;
    }
}
