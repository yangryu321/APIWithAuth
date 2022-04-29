using APIWithAuth.DataContext;
using Microsoft.AspNetCore.Identity;

namespace APIWithAuth.Models
{
    //coustom user class
    public class User : IdentityUser
    {
        public string Gender { get; set; } = string.Empty;
    }
}
