using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OneExpense.Business.Models;

namespace OneExpense.Data.Mappings
{
    public class ExpenseMapping : IEntityTypeConfiguration<Expense>
    {
        public void Configure(EntityTypeBuilder<Expense> builder)
        {
            builder.HasKey(p => p.Id);
            builder.Property(p => p.Id)
                .HasDefaultValueSql("uuid_generate_v4()");

            builder.Property(p => p.Description)
                .IsRequired()
                .HasColumnType("varchar(250)");

            builder.Property(p => p.Amount)
                .IsRequired();

            builder.ToTable("Expenses");

            //todo:seed
        }
    }
}
