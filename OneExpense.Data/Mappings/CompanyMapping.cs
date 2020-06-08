using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OneExpense.Business.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace OneExpense.Data.Mappings
{
    public class CompanyMapping : IEntityTypeConfiguration<Company>
    {
        public void Configure(EntityTypeBuilder<Company> builder)
        {
            builder.HasKey(p => p.Id);

            builder.Property(p => p.Name)
                   .IsRequired()
                   .HasColumnType("varchar(250)");

            builder.Property(p => p.Trade)
                   .IsRequired()
                   .HasColumnType("varchar(250)");
        }
    }
}
