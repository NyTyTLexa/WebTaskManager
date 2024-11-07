using System.ComponentModel.DataAnnotations;

namespace Web.Model
{
    public class USER
    {
        [Required]
        public string Id { get; set; }
        [Required]
        public string Login { get; set; }
        [Required]
        public string Password { get; set; }
        
        public string FullName { get; set; }
        public string Email { get; set; }

    }
}
