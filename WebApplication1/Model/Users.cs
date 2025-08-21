using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace WebApplication1.Model
{
    public class Users
    {
        [Required]
        [JsonPropertyName("Id")]
        public string Id { get; set; }
        [Required]
        [JsonPropertyName("Login")]
        public string Login { get; set; }
        [Required]
        [JsonPropertyName("Password")]
        public string Password { get; set; }
        [JsonPropertyName("FullName")]
        public string FullName { get; set; }
        [JsonPropertyName("Email")]
        public string Email { get; set; }
        public Users(string id, string login, string password)
        {
            Id = id;
            Login = login;
            Password = password;
        }

        public Users() { }
    }
}
