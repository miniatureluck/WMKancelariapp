using System.ComponentModel;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace WMKancelariapp.Models.ViewModels
{
    public class CaseDtoViewModel
    {
        public string CaseId { get; set; }
        [DisplayName("Nazwa")]
        public string Name { get; set; }
        [DisplayName("Opiekun")]
        public User? AssignedUser { get; set; }
        [DisplayName("Klient")]
        public Client? Client { get; set; }
        [DisplayName("Opis")]
        public string? Description { get; set; }
        [DisplayName("Czynności")]
        public List<UserTask>? Tasks { get; set; } = new List<UserTask>();



        [DisplayName("Klient")]
        public List<SelectListItem>? AllClientsSelectList { get; set; } = new List<SelectListItem>();

        [DisplayName("Opiekun")]
        public virtual List<SelectListItem>? AllUsersSelectList { get; set; } = new List<SelectListItem>();
    }
}
