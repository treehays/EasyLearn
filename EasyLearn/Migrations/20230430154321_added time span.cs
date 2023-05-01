using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EasyLearn.Migrations
{
    public partial class addedtimespan : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<TimeSpan>(
                name: "ModuleDuration",
                table: "Modules",
                type: "time(6)",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "double");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<double>(
                name: "ModuleDuration",
                table: "Modules",
                type: "double",
                nullable: false,
                oldClrType: typeof(TimeSpan),
                oldType: "time(6)");
        }
    }
}
