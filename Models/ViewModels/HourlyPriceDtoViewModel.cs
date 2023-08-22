using System.ComponentModel;

namespace WMKancelariapp.Models.ViewModels
{
    public class HourlyPriceDtoViewModel
    {
        public string HourlyPriceId { get; set; }
        
        [DisplayName("Stawka")]
        public int Price { get; set; }
        public User User { get; set; }

        [DisplayName("Czynność")]
        public TaskType TaskType { get; set; }

        [DisplayName("Sprawa")]
        public Case Case { get; set; }

        [DisplayName("Data modyfikacji")]
        public DateTime LastModified { get; set; } = DateTime.Now;

        public List<UserTask> UserTasks { get; set; } = new List<UserTask>();

        public string TaskTypeId { get; set; }
        public string CaseId { get; set; }
    }
}
