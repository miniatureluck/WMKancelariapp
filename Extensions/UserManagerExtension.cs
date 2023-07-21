using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Rendering;
using WMKancelariapp.Models;

namespace WMKancelariapp.Extensions
{
    public static class UserManagerExtension
    {
        public static IEnumerable<SelectListItem> CreateUsersSelectList(this UserManager<User> userManager)
        {
            var usersSelectList = new List<SelectListItem>();
            
            foreach (var user in userManager.Users)
            {
                usersSelectList.Add(new SelectListItem
                {
                    Text = user.UserName.RemoveEmailDomain(),
                    Value = user.Id
                });
            }

            return usersSelectList;
        }
    }
}
