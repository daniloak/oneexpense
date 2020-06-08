using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using OneExpense.Business.Models;

namespace OneExpense.Data
{
    public class ApplicationDbContext : IdentityDbContext<CompanyUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<CompanyUser>(b =>
            {
                b.Property(p => p.CompanyId).IsRequired();
                //b.Property(p => p.FirstName).IsRequired()
                //                            .HasMaxLength(50);
                //b.Property(p => p.LastName).IsRequired()
                //                           .HasMaxLength(100);
            });
        }
    }
}
