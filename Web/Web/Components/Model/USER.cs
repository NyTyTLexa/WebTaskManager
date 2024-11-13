using System.ComponentModel.DataAnnotations;

namespace Web.Model
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

        virtual public string Error { get; set; } = string.Empty;
        public User(string id, string login, string password)
        {
            Id = id;
            Login = login;
            Password = password;
        }
        public User(string error) 
        {
        this.Error = error;
        }
        public User()
        {

        }
    }
}
