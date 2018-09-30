using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace vpee1_MVC__Music.Data.MusicMigrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "MO");

            migrationBuilder.CreateTable(
                name: "Genres",
                schema: "MO",
                columns: table => new
                {
                    GenreID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Genres", x => x.GenreID);
                });

            migrationBuilder.CreateTable(
                name: "Instruments",
                schema: "MO",
                columns: table => new
                {
                    InstrumentID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Instruments", x => x.InstrumentID);
                });

            migrationBuilder.CreateTable(
                name: "Albums",
                schema: "MO",
                columns: table => new
                {
                    AlbumID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(maxLength: 50, nullable: false),
                    YearProduced = table.Column<DateTime>(nullable: false),
                    Price = table.Column<decimal>(nullable: false),
                    GenreID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Albums", x => x.AlbumID);
                    table.ForeignKey(
                        name: "FK_Albums_Genres_GenreID",
                        column: x => x.GenreID,
                        principalSchema: "MO",
                        principalTable: "Genres",
                        principalColumn: "GenreID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Musicians",
                schema: "MO",
                columns: table => new
                {
                    MusicianID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    FirstName = table.Column<string>(maxLength: 30, nullable: false),
                    MiddleName = table.Column<string>(maxLength: 30, nullable: true),
                    LastName = table.Column<string>(maxLength: 50, nullable: false),
                    PhoneNumber = table.Column<long>(nullable: false),
                    DOB = table.Column<DateTime>(nullable: false),
                    SIN = table.Column<string>(nullable: false),
                    InstrumentID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Musicians", x => x.MusicianID);
                    table.ForeignKey(
                        name: "FK_Musicians_Instruments_InstrumentID",
                        column: x => x.InstrumentID,
                        principalSchema: "MO",
                        principalTable: "Instruments",
                        principalColumn: "InstrumentID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Songs",
                schema: "MO",
                columns: table => new
                {
                    SongID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Title = table.Column<string>(maxLength: 80, nullable: false),
                    GenreID = table.Column<int>(nullable: false),
                    AlbumID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Songs", x => x.SongID);
                    table.ForeignKey(
                        name: "FK_Songs_Albums_AlbumID",
                        column: x => x.AlbumID,
                        principalSchema: "MO",
                        principalTable: "Albums",
                        principalColumn: "AlbumID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Songs_Genres_GenreID",
                        column: x => x.GenreID,
                        principalSchema: "MO",
                        principalTable: "Genres",
                        principalColumn: "GenreID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Plays",
                schema: "MO",
                columns: table => new
                {
                    PlaysID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    InstrumentID = table.Column<int>(nullable: false),
                    MusicianID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Plays", x => x.PlaysID);
                    table.ForeignKey(
                        name: "FK_Plays_Instruments_InstrumentID",
                        column: x => x.InstrumentID,
                        principalSchema: "MO",
                        principalTable: "Instruments",
                        principalColumn: "InstrumentID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Plays_Musicians_MusicianID",
                        column: x => x.MusicianID,
                        principalSchema: "MO",
                        principalTable: "Musicians",
                        principalColumn: "MusicianID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Performances",
                schema: "MO",
                columns: table => new
                {
                    PerformanceID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    SongID = table.Column<int>(nullable: false),
                    MusicianID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Performances", x => x.PerformanceID);
                    table.ForeignKey(
                        name: "FK_Performances_Musicians_MusicianID",
                        column: x => x.MusicianID,
                        principalSchema: "MO",
                        principalTable: "Musicians",
                        principalColumn: "MusicianID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Performances_Songs_SongID",
                        column: x => x.SongID,
                        principalSchema: "MO",
                        principalTable: "Songs",
                        principalColumn: "SongID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Albums_GenreID",
                schema: "MO",
                table: "Albums",
                column: "GenreID");

            migrationBuilder.CreateIndex(
                name: "IX_Musicians_InstrumentID",
                schema: "MO",
                table: "Musicians",
                column: "InstrumentID");

            migrationBuilder.CreateIndex(
                name: "IX_Musicians_SIN",
                schema: "MO",
                table: "Musicians",
                column: "SIN",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Performances_MusicianID",
                schema: "MO",
                table: "Performances",
                column: "MusicianID");

            migrationBuilder.CreateIndex(
                name: "IX_Performances_SongID",
                schema: "MO",
                table: "Performances",
                column: "SongID");

            migrationBuilder.CreateIndex(
                name: "IX_Plays_InstrumentID",
                schema: "MO",
                table: "Plays",
                column: "InstrumentID");

            migrationBuilder.CreateIndex(
                name: "IX_Plays_MusicianID",
                schema: "MO",
                table: "Plays",
                column: "MusicianID");

            migrationBuilder.CreateIndex(
                name: "IX_Songs_AlbumID",
                schema: "MO",
                table: "Songs",
                column: "AlbumID");

            migrationBuilder.CreateIndex(
                name: "IX_Songs_GenreID",
                schema: "MO",
                table: "Songs",
                column: "GenreID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Performances",
                schema: "MO");

            migrationBuilder.DropTable(
                name: "Plays",
                schema: "MO");

            migrationBuilder.DropTable(
                name: "Songs",
                schema: "MO");

            migrationBuilder.DropTable(
                name: "Musicians",
                schema: "MO");

            migrationBuilder.DropTable(
                name: "Albums",
                schema: "MO");

            migrationBuilder.DropTable(
                name: "Instruments",
                schema: "MO");

            migrationBuilder.DropTable(
                name: "Genres",
                schema: "MO");
        }
    }
}
