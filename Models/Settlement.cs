namespace WMKancelariapp.Models
{
    public class Settlement : Entity
    {
        public UserTask UserTask { get; set; }
        public string UserTaskId { get; set; }
        public HourlyPrice? HourlyRate { get; set; }
        public int TotalPrice { get; set; }
        public DateTime Modified { get; set; } = DateTime.Now;
        public bool IsSettled { get; set; }
    }
}
