using Microsoft.EntityFrameworkCore;
using OneExpense.Business.Models;
using System.Linq;

namespace OneExpense.Data.Context
{
    public class OneExpenseDbContext : DbContext
    {
        public OneExpenseDbContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<Expense> Expenses { get; set; }
        public DbSet<ExpenseDetails> ExpenseDetails { get; set; }

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
    }
}
