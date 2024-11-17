using System.Text.Json.Serialization;

namespace Web.Model
{
    public class Priority
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }
        [JsonPropertyName("name")]
        public string Name { get; set; } = string.Empty;
    }
}
