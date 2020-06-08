using Microsoft.EntityFrameworkCore;
using OneExpense.Business.Models;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace OneExpense.Data.Context
{
    public class OneExpenseDbContext : DbContext
    {
        public OneExpenseDbContext(DbContextOptions<OneExpenseDbContext> options) : base(options)
        {

        }

        public DbSet<Company> Companies { get; set; }
        public DbSet<ExpenseReport> ExpenseReports { get; set; }
        public DbSet<ExpenseReportDetail> ExpenseReportDetails { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            foreach (var property in modelBuilder.Model.GetEntityTypes()
                .SelectMany(e => e.GetProperties())
                .Where(p => p.ClrType == typeof(string)))
                property.SetColumnType("varchar(100)");

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(OneExpenseDbContext).Assembly);

            foreach (var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
                relationship.DeleteBehavior = DeleteBehavior.ClientSetNull;

            modelBuilder.HasPostgresExtension("uuid-ossp");

            base.OnModelCreating(modelBuilder);
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            foreach (var entry in ChangeTracker.Entries().Where(entry => entry.Entity.GetType().GetProperty("CreateDate") != null))
            {
                if (entry.State == EntityState.Added)
                {
                    entry.Property("CreateDate").CurrentValue = DateTime.Now;
                }

                if (entry.State == EntityState.Modified)
                {
                    entry.Property("CreateDate").IsModified = false;
                    entry.Property("UserId").IsModified = false;
                }
            }

            return base.SaveChangesAsync(cancellationToken);
        }
    }
}
