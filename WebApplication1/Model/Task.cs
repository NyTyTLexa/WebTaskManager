using System.Text.Json.Serialization;

namespace WebApplication1.Model
{
    public class Task
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }
        [JsonPropertyName("Name")]
        public string Name { get; set; }
        [JsonPropertyName("Description")]
        public string Description { get; set; }
        [JsonPropertyName("Priority_id")]
        public int PriorityId { get; set; }
        [JsonPropertyName("Status_id")]
        public int StatusId { get; set; }
        [JsonPropertyName("date_start")]
        public string DateStart { get; set; }
        [JsonPropertyName("date_end")]
        public string DateEnd { get; set; }

    }
}
