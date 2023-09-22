namespace WMKancelariapp.Models
{
    public class HourlyPrice : Entity
    {
        public int Price { get; set; }
        public User User { get; set; }
        public TaskType TaskType { get; set; }
        public Case Case { get; set; }
        public DateTime LastModified { get; set; } = DateTime.Now;
        public Settlement? Settlement { get; set; }

        

        public string TaskTypeId { get; set; }
        public string CaseId { get; set; }
        public string? SettlementId { get; set; }
    }
}
