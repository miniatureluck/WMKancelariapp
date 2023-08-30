using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace WMKancelariapp.Models.ViewModels
{
    public class UserTaskDtoViewModel
    {
        public string? UserTaskId { get; set; }

        [DisplayName("Opiekun")]
        public User User { get; set; }

        [DisplayName("Rozpoczęto")]
        public DateTime? StartTime { get; set; }

        [DisplayName("Zakończono")]
        public DateTime? EndTime { get; set; }

        [DisplayName("Czas")]
        public long? Duration { get; set; }

        [DisplayName("Opis")]
        public string? Description { get; set; }


        [DisplayName("Klient")]
        public Client? Client { get; set; }

        [DisplayName("Sprawa")]
        public Case? Case { get; set; }

        [DisplayName("Kategoria")]
        public TaskType TaskType { get; set; }

        [DisplayName("Czas trwania")]
        public string? DurationMinutes { get; set; }
        
        [DisplayName("Kategoria")]
        public List<SelectListItem>? AllTaskTypesSelectList { get; set; } = new List<SelectListItem>();

        [DisplayName("Klient")]
        public List<SelectListItem>? AllClientsSelectList { get; set; } = new List<SelectListItem>();

        [DisplayName("Wykonawca")]
        public List<SelectListItem>? AllUsersSelectList { get; set; } = new List<SelectListItem>();

        [DisplayName("Sprawa")]
        public virtual List<SelectListItem>? AllCasesSelectList { get; set; } = new List<SelectListItem>();
    }
}
