namespace Web.Model
{
    public class Task
    {
        public int id { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public int priority_id { get; set; }
        public int status_id { get; set; }
        public string date_start { get; set; }
        public string date_end { get; set; }

        public DateTime dateEnd { get
            {
                return Convert.ToDateTime(date_end);
            }
        }
        virtual public string DeadLine { get
            {
                return dateEnd.ToShortDateString();
            } }

    }
}
