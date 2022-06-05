using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using DiplomEremenko.Models;


namespace DiplomEremenko.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options): base(options)
        {
            Database.EnsureCreated();
        }
       public DbSet<UserLoyalty> UsersLoyaltie { get; set; }
        public DbSet<Loyalty> Loyalties { get; set; }
    }
}