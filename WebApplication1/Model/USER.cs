using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Model
{
    public class User
    {
        [Required]
        public string Id { get; set; }
        [Required]
        public string Login { get; set; }
        [Required]
        public string Password { get; set; }
        
        public string FullName { get; set; }
        public string Email { get; set; }
        public User(string id, string login, string password)
        {
            Id = id;
            Login = login;
            Password = password;
        }

        public User() { }
    }
}
