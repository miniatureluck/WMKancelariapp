using System.ComponentModel;

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
    }
}
