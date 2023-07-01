namespace WMKancelariapp.Models
{
    public class TaskType : Entity
    {
        public string Name { get; set; }
        public List<Case> Cases { get; set; } = new List<Case>();
    }
}
