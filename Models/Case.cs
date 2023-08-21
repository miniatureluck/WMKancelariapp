namespace WMKancelariapp.Models
{
    public class Case : Entity
    {
        public string Name { get; set; }
        public User? AssignedUser { get; set; }
        public Client? Client { get; set; }
        public string? Description { get; set; }
        public List<HourlyPrice>? Prices { get; set; } = new List<HourlyPrice>();
        public List<UserTask>? Tasks { get; set; } = new List<UserTask>();

    }
}
