﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using OneExpense.Data.Context;

namespace OneExpense.Data.Migrations
{
    [DbContext(typeof(OneExpenseDbContext))]
    [Migration("20200607010557_InitialDB")]
    partial class InitialDB
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Npgsql:PostgresExtension:uuid-ossp", ",,")
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn)
                .HasAnnotation("ProductVersion", "3.1.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            modelBuilder.Entity("OneExpense.Business.Models.Company", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("varchar(250)");

                    b.Property<string>("Trade")
                        .IsRequired()
                        .HasColumnType("varchar(250)");

                    b.HasKey("Id");

                    b.ToTable("Companies");
                });

            modelBuilder.Entity("OneExpense.Business.Models.ExpenseReport", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasDefaultValueSql("uuid_generate_v4()");

                    b.Property<DateTime>("CreateDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("varchar(200)");

                    b.Property<decimal>("Total")
                        .HasColumnType("numeric");

                    b.HasKey("Id");

                    b.ToTable("ExpenseReports");
                });

            modelBuilder.Entity("OneExpense.Business.Models.ExpenseReportDetail", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<decimal>("Amount")
                        .HasColumnType("numeric");

                    b.Property<DateTime>("CreateDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("varchar(200)");

                    b.Property<Guid>("ExpenseId")
                        .HasColumnType("uuid");

                    b.Property<string>("Image")
                        .HasColumnType("varchar(100)");

                    b.Property<string>("Supplier")
                        .IsRequired()
                        .HasColumnType("varchar(100)");

                    b.HasKey("Id");

                    b.HasIndex("ExpenseId");

                    b.ToTable("ExpenseReportDetails");
                });

            modelBuilder.Entity("OneExpense.Business.Models.ExpenseReportDetail", b =>
                {
                    b.HasOne("OneExpense.Business.Models.ExpenseReport", "ExpenseReport")
                        .WithMany("Details")
                        .HasForeignKey("ExpenseId")
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}