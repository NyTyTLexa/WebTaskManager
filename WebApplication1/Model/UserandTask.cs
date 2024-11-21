using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication1.Model
{
    public class UserandTask
    {
        public int id { get; set; }
        public string Userid { get; set; }
        public int TaskId { get; set; }
        public virtual User user { get; set; }
        public virtual Task task { get; set; }
    }
}
