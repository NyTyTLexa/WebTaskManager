namespace WebTaskManager.Model
{
    public class StatusTask
    {
        public Status Status { get; set; } = new Status();
        public List<Task> Tasks { get; set; } = new List<Task> { };
    }
}
