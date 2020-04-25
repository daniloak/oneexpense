using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OneExpense.Business.Models;

namespace OneExpense.Data.Mappings
{
    public class ExpenseReportDetailMapping : IEntityTypeConfiguration<ExpenseReportDetail>
    {
        public void Configure(EntityTypeBuilder<ExpenseReportDetail> builder)
        {
            builder.HasKey(p => p.Id);

            builder.Property(p => p.Supplier)
                .IsRequired()
                .HasColumnType("varchar(100)");

            builder.Property(p => p.Description)
                .IsRequired()
                .HasColumnType("varchar(200)");

            builder.Property(p => p.Amount)
                .IsRequired();

            builder.ToTable("ExpenseReportDetails");
        }
    }
}
