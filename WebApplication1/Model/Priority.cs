using System.Text.Json.Serialization;

namespace WebApplication1.Model
{
    public class Priority
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }
    }
}
