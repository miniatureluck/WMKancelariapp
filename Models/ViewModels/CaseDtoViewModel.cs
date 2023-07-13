namespace WMKancelariapp.Models.ViewModels
{
    public class CaseDtoViewModel
    {
        public string CaseId { get; set; }
        public string Name { get; set; }
        public User? AssignedUser { get; set; }
        public Client? Client { get; set; }
        public string? Description { get; set; }
        public List<UserTask>? Tasks { get; set; } = new List<UserTask>();
    }
}
