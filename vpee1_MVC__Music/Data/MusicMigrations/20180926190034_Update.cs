using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace vpee1_MVC__Music.Data.MusicMigrations
{
    public partial class Update : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "DOB",
                schema: "MO",
                table: "Musicians",
                nullable: false,
                oldClrType: typeof(string));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "DOB",
                schema: "MO",
                table: "Musicians",
                nullable: false,
                oldClrType: typeof(DateTime));
        }
    }
}
