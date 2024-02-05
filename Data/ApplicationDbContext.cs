using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using DeckMaster.Models;
using DeckMaster.ViewModels;

namespace DeckMaster.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public DbSet<IPN> IPNs { get; set; }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<MyRegisteredUser> MyRegisteredUsers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
        public DbSet<DeckMaster.ViewModels.RoleVM> RoleVM { get; set; } = default!;
    }
}
