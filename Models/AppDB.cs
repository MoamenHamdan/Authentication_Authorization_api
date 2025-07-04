using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Authentication_Authorization_api.Models
{
    public class AppDB : IdentityDbContext
    {
        public AppDB(DbContextOptions<AppDB> options) : base(options)
        {
            
        }
        public DbSet<AppUser> AppUsers { get; set; }


    }
}
