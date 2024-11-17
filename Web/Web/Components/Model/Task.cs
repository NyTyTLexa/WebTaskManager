﻿using System.Text.Json.Serialization;

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
        public string DateStart { get; set; } = string.Empty;
        [JsonPropertyName("date_end")]
        public string DateEnd { get; set; } = string.Empty;

        virtual public string DeadLine
        { 
            get
            {
                return Convert.ToDateTime(date_end);
            }
        }

    }
}
