using Microsoft.EntityFrameworkCore.Migrations;

namespace API.Data.Migrations
{
    public partial class TransactionUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Amount",
                table: "Transactions",
                newName: "DebitAmount");

            migrationBuilder.AddColumn<decimal>(
                name: "CreditAmount",
                table: "Transactions",
                type: "TEXT",
                nullable: false,
                defaultValue: 0m);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreditAmount",
                table: "Transactions");

            migrationBuilder.RenameColumn(
                name: "DebitAmount",
                table: "Transactions",
                newName: "Amount");
        }
    }
}
