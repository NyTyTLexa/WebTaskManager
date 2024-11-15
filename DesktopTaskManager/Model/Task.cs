using System.Text.Json.Serialization;

namespace WebTaskManager.Model
{
    public class Task
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }
        [JsonPropertyName("name")]
        public string Name { get; set; } = string.Empty;
        [JsonPropertyName("description")]
        public string Description { get; set; } = string.Empty;
        [JsonPropertyName("priority_id")]
        public int PriorityId { get; set; }
        [JsonPropertyName("status_id")]
        public int StatusId { get; set; }
        [JsonPropertyName("date_start")]
        public string DateStart { get; set; } = string.Empty;
        [JsonPropertyName("date_end")]
        public string DateEnd { get; set; } = string.Empty;
        public Status Status = new Status();
        public Priority Priority = new Priority();

        public string StatusName
        {
            get
            {
                return Status.Name;
            }
        }

        public string PriorityName
        {
            get
            {
                return Priority.Name;
            }
        }
    }
}
