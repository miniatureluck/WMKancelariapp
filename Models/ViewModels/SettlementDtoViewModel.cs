using Microsoft.AspNetCore.Mvc.Rendering;

namespace WMKancelariapp.Models.ViewModels
{
    public class SettlementDtoViewModel
    {
        public string SettlementId { get; set; }
        public List<UserTask> UserTasks { get; set; } = new List<UserTask>();
        public Client Client { get; set; }
        public int TotalPrice { get; set; }
        public DateTime Modified { get; set; } = DateTime.Now;
        public bool IsSettled { get; set; }


        public List<bool> SelectedUserTasksStatus { get; set; }
        public User? AssignedUser { get; set; }
        public List<SelectListItem> UsersSelectList { get; set; } = new List<SelectListItem>();
        public List<SelectListItem> ClientsSelectList { get; set; } = new List<SelectListItem>();
        

    }
}
