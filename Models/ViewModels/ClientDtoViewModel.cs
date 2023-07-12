using System.ComponentModel;

namespace WMKancelariapp.Models.ViewModels
{
    public class ClientDtoViewModel
    {
        public string ClientId { get; set; }
        [DisplayName("Imię")]
        public string Name { get; set; }
        [DisplayName("Nazwisko")]
        public string? Surname { get; set; }
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
        [DisplayName("Stawka")]
        public List<HourlyPrice>? Prices { get; set; } = new List<HourlyPrice>();
        [DisplayName("Opiekun")]
        public User? AssignedUser { get; set; }
    }
}
