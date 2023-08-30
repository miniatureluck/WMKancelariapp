using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace WMKancelariapp.Models.ViewModels
{
    public class CaseDtoViewModel
    {
        public string? CaseId { get; set; }

        [DisplayName("Nazwa")]
        [Required(ErrorMessage = "Należy podać nazwę")]
        public string Name { get; set; }

        [DisplayName("Opiekun")]
        public User? AssignedUser { get; set; }

        [DisplayName("Klient")]
        public Client? Client { get; set; }
        public string? ClientId { get; set; }

        [DisplayName("Opis")]
        public string? Description { get; set; }

        [DisplayName("Stawki")]
        public List<HourlyPrice>? Prices { get; set; } = new List<HourlyPrice>();

        [DisplayName("Czynności")]
        public List<UserTask>? Tasks { get; set; } = new List<UserTask>();
        
        [DisplayName("Terminy")]
        public List<Deadline>? Deadlines { get; set; } = new List<Deadline>();

        public int SpecifiedPrices { get; set; }
        public int PricesMaxCount { get; set; }
        public string PricesCompletionFraction => $"{SpecifiedPrices}/{PricesMaxCount}";

        [DisplayName("Klient")]
        public List<SelectListItem>? AllClientsSelectList { get; set; } = new List<SelectListItem>();

        [DisplayName("Opiekun")]
        public virtual List<SelectListItem>? AllUsersSelectList { get; set; } = new List<SelectListItem>();
    }
}
