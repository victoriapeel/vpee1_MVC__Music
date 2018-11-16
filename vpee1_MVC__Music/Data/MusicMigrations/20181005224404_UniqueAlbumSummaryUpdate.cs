using Microsoft.EntityFrameworkCore.Migrations;

namespace vpee1_MVC__Music.Data.MusicMigrations
{
    public partial class UniqueAlbumSummaryUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Albums_YearProduced_Name",
                schema: "MO",
                table: "Albums",
                columns: new[] { "YearProduced", "Name" },
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Albums_YearProduced_Name",
                schema: "MO",
                table: "Albums");
        }
    }
}
