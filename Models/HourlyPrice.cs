namespace WMKancelariapp.Models
{
    public class HourlyPrice : Entity
    {
        public int Price { get; set; }
        public User User { get; set; }
        public TaskType TaskType { get; set; }
        public Client Client { get; set; }

        public UserTask UserTask { get; set; }
    }
}
