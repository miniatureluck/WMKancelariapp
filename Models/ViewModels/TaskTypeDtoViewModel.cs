using System.ComponentModel;
using Microsoft.Build.Framework;

namespace WMKancelariapp.Models.ViewModels
{
    public class TaskTypeDtoViewModel
    {
        public string TaskTypeId { get; set; }
        [DisplayName("Nazwa")]
        [Required]
        public string Name { get; set; }
        public List<Case>? MostFrequentCase { get; set; } = new List<Case>();

        public List<UserTask>? Tasks { get; set; } = new List<UserTask>();
        public List<HourlyPrice>? HourlyPrices { get; set; } = new List<HourlyPrice>();
    }
}
