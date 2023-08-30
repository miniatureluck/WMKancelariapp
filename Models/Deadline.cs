namespace WMKancelariapp.Models
{
    public class Deadline : Entity
    {
        public string Description { get; set; }
        public Case Case { get; set; }
        public DateTime Date { get; set; }
        public User User { get; set; }
        public bool IsCompleted { get; set; } = false;
    }
}
