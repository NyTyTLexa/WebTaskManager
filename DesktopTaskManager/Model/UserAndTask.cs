namespace WebTaskManager.Model
{
    public class UserAndTask
    {
        public int Id { get; set; }
        public User User { get; set; }

        public Task Task { get; set; }
    }
}
