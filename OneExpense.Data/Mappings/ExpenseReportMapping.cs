using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OneExpense.Business.Models;

namespace OneExpense.Data.Mappings
{
    public class ExpenseReportMapping : IEntityTypeConfiguration<ExpenseReport>
    {
        public void Configure(EntityTypeBuilder<ExpenseReport> builder)
        {
            builder.HasKey(p => p.Id);
            builder.Property(p => p.Id).HasDefaultValueSql("uuid_generate_v4()");

            builder.Property(p => p.Description)
                .IsRequired()
                .HasColumnType("varchar(200)");

            builder.HasMany(f => f.Details)
                .WithOne(p => p.ExpenseReport)
                .HasForeignKey(p => p.ExpenseId)
                .IsRequired();

            builder.Property(p => p.Total)
                .IsRequired();

            builder.ToTable("ExpenseReports");

            //todo:seed
        }
    }
}
