using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace WebApplication1.Model
{
    public class UserAndTask
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }
        [JsonPropertyName("Userid")]
        public string UserId { get; set; }
        [JsonPropertyName("TaskId")]
        public int TaskId { get; set; }
        public virtual Users user { get; set; }
        public virtual Task task { get; set; }
    }
}
