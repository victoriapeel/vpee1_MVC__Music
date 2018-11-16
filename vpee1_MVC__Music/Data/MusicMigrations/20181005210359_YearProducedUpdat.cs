using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace vpee1_MVC__Music.Data.MusicMigrations
{
    public partial class YearProducedUpdat : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "YearProduced",
                schema: "MO",
                table: "Albums",
                maxLength: 4,
                nullable: false,
                oldClrType: typeof(DateTime));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "YearProduced",
                schema: "MO",
                table: "Albums",
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 4);
        }
    }
}
