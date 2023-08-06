using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace WMKancelariapp.Models.ViewModels
{
    public class ClientDtoViewModel
    {
        public string ClientId { get; set; }
        [DisplayName("Imię")]
        public string? Name { get; set; }
        [DisplayName("Nazwisko")]
        [Required(ErrorMessage = "Należy podać nazwisko")]
        public string Surname { get; set; }
        [DisplayName("Adres")]
        public string? Address { get; set; }
        [DisplayName("Kod pocztowy")]
        public string? PostCode { get; set; }
        [DisplayName("Miejscowość")]
        public string? Location { get; set; }
        [DisplayName("E-mail")]
        public string? Email { get; set; }
        [DisplayName("NIP")]
        public string? TaxIdNumber { get; set; }
        [DisplayName("Telefon")]
        public string? Phone { get; set; }
        [DisplayName("Opis")]
        public string? Description { get; set; }
        [DisplayName("Sprawy")]
        public List<Case>? Cases { get; set; } = new List<Case>();
        [DisplayName("Czynności")]
        public List<UserTask>? Tasks { get; set; } = new List<UserTask>();
        [DisplayName("Stawki")]
        public List<HourlyPrice>? Prices { get; set; } = new List<HourlyPrice>();
        [DisplayName("Opiekun")]
        public User? AssignedUser { get; set; }

        [DisplayName("Opiekun")]
        public virtual List<SelectListItem>? AllUsersSelectList { get; set; } = new List<SelectListItem>();

        [DisplayName("Sprawa")]
        public virtual List<SelectListItem>? AllCasesSelectList { get; set; } = new List<SelectListItem>();

        public virtual List<string>? SelectedCases { get; set; } = new List<string>();
    }
}
