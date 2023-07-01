namespace WMKancelariapp.Models
{
    public class HourlyPrice : Entity
    {
        public int Price { get; set; }
        public User User { get; set; }
        public Task Task { get; set; }
        public Client Client { get; set; }
    }
}
