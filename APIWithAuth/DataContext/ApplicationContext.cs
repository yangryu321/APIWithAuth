using APIWithAuth.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace APIWithAuth.DataContext
{
    public class ApplicationContext : IdentityDbContext<User>
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> options) :base(options) {}

        public DbSet<Student> Students { get; set; }
      
    }
}
