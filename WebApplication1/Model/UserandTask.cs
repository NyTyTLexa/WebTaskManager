using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication1.Model
{
    public class UserandTask
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public int TaskId { get; set; }
        public virtual User User { get; set; }
        public virtual Task Task { get; set; }
    }
}
