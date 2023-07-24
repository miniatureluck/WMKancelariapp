using System.ComponentModel;

namespace WMKancelariapp.Models.ViewModels
{
    public class TaskTypeDtoViewModel
    {
        public string TaskTypeId { get; set; }
        [DisplayName("Nazwa")]
        public string Name { get; set; }

        public List<UserTask>? Tasks { get; set; } = new List<UserTask>();
        public List<HourlyPrice>? HourlyPrices { get; set; } = new List<HourlyPrice>();
    }
}
