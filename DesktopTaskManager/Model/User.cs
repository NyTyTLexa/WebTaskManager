namespace WebTaskManager.Model
{
    public class User
    {
        public string Id { get; set; }
        public string Login { get; set; }

        public string Password { get; set; }

        public string FullName { get; set; }
        public string Email { get; set; }
        public string Error { get; set; } = string.Empty;

        public User(string id, string login, string password)
        {
            Id = id;
            Login = login;
            Password = password;
        }

        public User(string error)
        {
            Error = error;
        }
    }
}
