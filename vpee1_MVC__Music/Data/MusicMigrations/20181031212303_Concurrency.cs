using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace vpee1_MVC__Music.Data.MusicMigrations
{
    public partial class Concurrency : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "SIN",
                schema: "MO",
                table: "Musicians",
                maxLength: 9,
                nullable: false,
                oldClrType: typeof(string));

            migrationBuilder.AddColumn<byte[]>(
                name: "RowVersion",
                schema: "MO",
                table: "Musicians",
                rowVersion: true,
                nullable: true);

            migrationBuilder.AddColumn<byte[]>(
                name: "RowVersion",
                schema: "MO",
                table: "Albums",
                rowVersion: true,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RowVersion",
                schema: "MO",
                table: "Musicians");

            migrationBuilder.DropColumn(
                name: "RowVersion",
                schema: "MO",
                table: "Albums");

            migrationBuilder.AlterColumn<string>(
                name: "SIN",
                schema: "MO",
                table: "Musicians",
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 9);
        }
    }
}
