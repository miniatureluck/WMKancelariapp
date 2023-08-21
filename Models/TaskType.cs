using System.ComponentModel.DataAnnotations;

namespace WMKancelariapp.Models
{
    public class TaskType : Entity
    {
        [StringLength(25)]
        public string Name { get; set; }

        public List<UserTask>? Tasks { get; set; } = new List<UserTask>();
        public List<HourlyPrice>? HourlyPrices { get; set; } = new List<HourlyPrice>();
    }
}
