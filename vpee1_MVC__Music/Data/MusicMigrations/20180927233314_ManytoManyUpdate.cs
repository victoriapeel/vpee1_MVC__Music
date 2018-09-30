using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace vpee1_MVC__Music.Data.MusicMigrations
{
    public partial class ManytoManyUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Plays",
                schema: "MO",
                table: "Plays");

            migrationBuilder.DropIndex(
                name: "IX_Plays_InstrumentID",
                schema: "MO",
                table: "Plays");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Performances",
                schema: "MO",
                table: "Performances");

            migrationBuilder.DropIndex(
                name: "IX_Performances_SongID",
                schema: "MO",
                table: "Performances");

            migrationBuilder.DropColumn(
                name: "PlaysID",
                schema: "MO",
                table: "Plays");

            migrationBuilder.DropColumn(
                name: "PerformanceID",
                schema: "MO",
                table: "Performances");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Plays",
                schema: "MO",
                table: "Plays",
                columns: new[] { "InstrumentID", "MusicianID" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_Performances",
                schema: "MO",
                table: "Performances",
                columns: new[] { "SongID", "MusicianID" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Plays",
                schema: "MO",
                table: "Plays");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Performances",
                schema: "MO",
                table: "Performances");

            migrationBuilder.AddColumn<int>(
                name: "PlaysID",
                schema: "MO",
                table: "Plays",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            migrationBuilder.AddColumn<int>(
                name: "PerformanceID",
                schema: "MO",
                table: "Performances",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Plays",
                schema: "MO",
                table: "Plays",
                column: "PlaysID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Performances",
                schema: "MO",
                table: "Performances",
                column: "PerformanceID");

            migrationBuilder.CreateIndex(
                name: "IX_Plays_InstrumentID",
                schema: "MO",
                table: "Plays",
                column: "InstrumentID");

            migrationBuilder.CreateIndex(
                name: "IX_Performances_SongID",
                schema: "MO",
                table: "Performances",
                column: "SongID");
        }
    }
}
