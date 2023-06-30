namespace WMKancelariapp.Models
{
    public class Case : Entity
    {
        public string Name { get; set; }
        public User? AssignedUser { get; set; }
        public Client? Client { get; set; }
        public string? Description { get; set; }

    }
}
