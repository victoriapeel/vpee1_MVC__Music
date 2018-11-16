using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace vpee1_MVC__Music.Data.MusicMigrations
{
    public partial class Audit : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                schema: "MO",
                table: "Musicians",
                maxLength: 256,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedOn",
                schema: "MO",
                table: "Musicians",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UpdatedBy",
                schema: "MO",
                table: "Musicians",
                maxLength: 256,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedOn",
                schema: "MO",
                table: "Musicians",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                schema: "MO",
                table: "Albums",
                maxLength: 256,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedOn",
                schema: "MO",
                table: "Albums",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UpdatedBy",
                schema: "MO",
                table: "Albums",
                maxLength: 256,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedOn",
                schema: "MO",
                table: "Albums",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedBy",
                schema: "MO",
                table: "Musicians");

            migrationBuilder.DropColumn(
                name: "CreatedOn",
                schema: "MO",
                table: "Musicians");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                schema: "MO",
                table: "Musicians");

            migrationBuilder.DropColumn(
                name: "UpdatedOn",
                schema: "MO",
                table: "Musicians");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                schema: "MO",
                table: "Albums");

            migrationBuilder.DropColumn(
                name: "CreatedOn",
                schema: "MO",
                table: "Albums");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                schema: "MO",
                table: "Albums");

            migrationBuilder.DropColumn(
                name: "UpdatedOn",
                schema: "MO",
                table: "Albums");
        }
    }
}
