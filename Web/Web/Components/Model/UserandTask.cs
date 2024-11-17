using System.ComponentModel.DataAnnotations.Schema;

namespace Web.Model
{
    public class UserAndTask
    {
        public int Id { get; set; }
        public string UserId { get; set; } = string.Empty;
        public int TaskId { get; set; }
        public virtual User User { get; set; } = new User();
        public virtual Task Task { get; set; } = new Task();
    }
}
