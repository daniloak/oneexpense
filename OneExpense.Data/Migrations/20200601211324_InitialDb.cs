using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace OneExpense.Data.Migrations
{
    public partial class InitialDb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("Npgsql:PostgresExtension:uuid-ossp", ",,");

            migrationBuilder.CreateTable(
                name: "ExpenseReports",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false, defaultValueSql: "uuid_generate_v4()"),
                    CreateDate = table.Column<DateTime>(nullable: false),
                    Description = table.Column<string>(type: "varchar(200)", nullable: false),
                    Total = table.Column<decimal>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExpenseReports", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ExpenseReportDetails",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    ExpenseId = table.Column<Guid>(nullable: false),
                    Supplier = table.Column<string>(type: "varchar(100)", nullable: false),
                    Description = table.Column<string>(type: "varchar(200)", nullable: false),
                    Amount = table.Column<decimal>(nullable: false),
                    Image = table.Column<string>(type: "varchar(100)", nullable: true),
                    CreateDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExpenseReportDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ExpenseReportDetails_ExpenseReports_ExpenseId",
                        column: x => x.ExpenseId,
                        principalTable: "ExpenseReports",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ExpenseReportDetails_ExpenseId",
                table: "ExpenseReportDetails",
                column: "ExpenseId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ExpenseReportDetails");

            migrationBuilder.DropTable(
                name: "ExpenseReports");
        }
    }
}
