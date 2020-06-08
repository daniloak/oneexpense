using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace OneExpense.Data.Migrations
{
    public partial class AddUserId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "UserId",
                table: "ExpenseReports",
                nullable: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserId",
                table: "ExpenseReports");
        }
    }
}
