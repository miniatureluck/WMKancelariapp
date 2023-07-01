namespace WMKancelariapp.Models
{
    public class Task : Entity
    {
        public User User { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime? EndTime { get; set; }
        public TimeSpan? Duration { get; set; }
        public Client? Client { get; set; }
        public Case Case { get; set; }
        public TaskType TaskType { get; set; }
        public string Description { get; set; }
        public HourlyPrice Price { get; set; }
    }
}
