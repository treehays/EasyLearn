using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EasyLearn.Migrations
{
    public partial class changebancode : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "BankName",
                table: "PaymentDetails",
                newName: "BankCode");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "BankCode",
                table: "PaymentDetails",
                newName: "BankName");
        }
    }
}
