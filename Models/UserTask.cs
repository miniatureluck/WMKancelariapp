namespace WMKancelariapp.Models
{
    public class UserTask : Entity
    {
        public User User { get; set; }
        public DateTime? StartTime { get; set; }
        public DateTime? EndTime { get; set; }
        public long? Duration { get; set; }
        public string? Description { get; set; }
        public int Value { get; set; }

        public Client? Client { get; set; }
        public Case? Case { get; set; }
        public TaskType TaskType { get; set; }
        public Settlement? Settlement { get; set; }
        public string? SettlementId { get; set; }
    }
}
