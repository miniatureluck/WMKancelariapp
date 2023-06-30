namespace WMKancelariapp.Models
{
    public class Client : Entity
    {
        public string Name { get; set; }
        public string? Surname { get; set; }
        public string? Email { get; set; }
        public string? Phone { get; set; }
        public string? Description { get; set; }
        public List<Case> Cases { get; set; } = new List<Case>();
    }
}
