namespace WMKancelariapp.Models.ViewModels
{
    public class SettlementDtoViewModel
    {
        public string SettlementId { get; set; }
        public List<UserTask> UserTasks { get; set; }
        public Client Client { get; set; }
        public int TotalPrice { get; set; }
        public DateTime Modified { get; set; } = DateTime.Now;
        public bool IsSettled { get; set; }
    }
}
