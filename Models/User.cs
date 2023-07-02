using Microsoft.AspNetCore.Identity;

namespace WMKancelariapp.Models
{
    public class User : IdentityUser
    {
        public List<Client>? Clients { get; set; } = new List<Client>();
        public List<Case>? Cases { get; set; } = new List<Case>();
        public List<HourlyPrice>? HourlyPrices { get; set; } = new List<HourlyPrice>();
        public List<UserTask>? Tasks { get; set; } = new List<UserTask>();

    }
}
