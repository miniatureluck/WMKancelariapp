using System.ComponentModel;
using Microsoft.AspNetCore.Mvc.Rendering;

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

        public string TaskTypeId { get; set; }
        public string CaseId { get; set; }


        public List<SelectListItem> CasesSelectList { get; set; } = new List<SelectListItem>();
        public List<SelectListItem> TaskTypesPriceList { get; set; } = new List<SelectListItem>();
    }
}
