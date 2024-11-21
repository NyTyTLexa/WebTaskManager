using System.Text.Json.Serialization;

namespace Web.Model
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
        public string DateStart { get; set; } = DateTime.Now.ToShortDateString();

        public DateTime StartDate
        {
            get
            {
                return Convert.ToDateTime(DateStart);
            }
            set
            {
                DeadLine = value;
            }
        } 

        [JsonPropertyName("date_end")]
        public string DateEnd { get; set; } = DateTime.Now.ToShortDateString();

        virtual public DateTime DeadLine
        { 
            get
            {
                return Convert.ToDateTime(DateEnd);
            }
            set
            {
                DeadLine = value;
            }
        } 

    }
}
