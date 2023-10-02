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



        private Dictionary<string, bool> _userTaskIdsList;
        public User? AssignedUser { get; set; }
        public List<SelectListItem> UsersSelectList { get; set; } = new List<SelectListItem>();
        public List<SelectListItem> ClientsSelectList { get; set; } = new List<SelectListItem>();
        public Dictionary<string, bool> UserTaskIdsList
        {
            get
            {
                if (_userTaskIdsList != null) return _userTaskIdsList;
                _userTaskIdsList = new Dictionary<string, bool>();
                foreach (var userTask in UserTasks)
                {
                    _userTaskIdsList[userTask.Id] = false;
                }
                return _userTaskIdsList;
            }
        }

    }
}
