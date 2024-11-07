namespace Web.Model
{
    public class Task
    {
        public int id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Priority_id { get; set; }
        public int Status_id { get; set; }
        public string date_start { get; set; }
        public string date_end { get; set; }

    }
}
