namespace WMKancelariapp.Models
{
    public class TaskType : Entity
    {
        public string Name { get; set; }

        public List<UserTask>? Tasks { get; set; } = new List<UserTask>();
        public HourlyPrice? HourlyPrice { get; set; }
    }
}
