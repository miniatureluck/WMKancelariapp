using Microsoft.AspNetCore.Identity;

namespace WMKancelariapp.Models
{
    public class User : IdentityUser
    {
        public string Username { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public List<Client>? Clients { get; set; } = new List<Client>();
        public List<Case>? Cases { get; set; } = new List<Case>();

    }
}
