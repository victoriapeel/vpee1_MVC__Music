﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using vpee1_MVC__Music.Data;

namespace vpee1_MVC__Music.Data.MusicMigrations
{
    [DbContext(typeof(MusicContext))]
    [Migration("20181109213414_MusicianUpdate")]
    partial class MusicianUpdate
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasDefaultSchema("MO")
                .HasAnnotation("ProductVersion", "2.1.4-rtm-31024")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("vpee1_MVC__Music.Models.Album", b =>
                {
                    b.Property<int>("AlbumID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("CreatedBy")
                        .HasMaxLength(256);

                    b.Property<DateTime?>("CreatedOn");

                    b.Property<int>("GenreID");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<decimal>("Price");

                    b.Property<byte[]>("RowVersion")
                        .IsConcurrencyToken()
                        .ValueGeneratedOnAddOrUpdate();

                    b.Property<string>("UpdatedBy")
                        .HasMaxLength(256);

                    b.Property<DateTime?>("UpdatedOn");

                    b.Property<string>("YearProduced")
                        .IsRequired()
                        .HasMaxLength(4);

                    b.HasKey("AlbumID");

                    b.HasIndex("GenreID");

                    b.HasIndex("YearProduced", "Name")
                        .IsUnique();

                    b.ToTable("Albums");
                });

            modelBuilder.Entity("vpee1_MVC__Music.Models.Genre", b =>
                {
                    b.Property<int>("GenreID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.HasKey("GenreID");

                    b.ToTable("Genres");
                });

            modelBuilder.Entity("vpee1_MVC__Music.Models.Instrument", b =>
                {
                    b.Property<int>("InstrumentID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.HasKey("InstrumentID");

                    b.ToTable("Instruments");
                });

            modelBuilder.Entity("vpee1_MVC__Music.Models.Musician", b =>
                {
                    b.Property<int>("MusicianID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("CreatedBy")
                        .HasMaxLength(256);

                    b.Property<DateTime?>("CreatedOn");

                    b.Property<DateTime>("DOB");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(30);

                    b.Property<int>("InstrumentID");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<string>("MiddleName")
                        .HasMaxLength(30);

                    b.Property<long>("PhoneNumber");

                    b.Property<byte[]>("RowVersion")
                        .IsConcurrencyToken()
                        .ValueGeneratedOnAddOrUpdate();

                    b.Property<string>("SIN")
                        .IsRequired()
                        .HasMaxLength(9);

                    b.Property<string>("UpdatedBy")
                        .HasMaxLength(256);

                    b.Property<DateTime?>("UpdatedOn");

                    b.HasKey("MusicianID");

                    b.HasIndex("InstrumentID");

                    b.HasIndex("SIN")
                        .IsUnique();

                    b.ToTable("Musicians");
                });

            modelBuilder.Entity("vpee1_MVC__Music.Models.Performance", b =>
                {
                    b.Property<int>("SongID");

                    b.Property<int>("MusicianID");

                    b.HasKey("SongID", "MusicianID");

                    b.HasIndex("MusicianID");

                    b.ToTable("Performances");
                });

            modelBuilder.Entity("vpee1_MVC__Music.Models.Plays", b =>
                {
                    b.Property<int>("InstrumentID");

                    b.Property<int>("MusicianID");

                    b.HasKey("InstrumentID", "MusicianID");

                    b.HasIndex("MusicianID");

                    b.ToTable("Plays");
                });

            modelBuilder.Entity("vpee1_MVC__Music.Models.Song", b =>
                {
                    b.Property<int>("SongID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("AlbumID");

                    b.Property<int>("GenreID");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(80);

                    b.HasKey("SongID");

                    b.HasIndex("AlbumID");

                    b.HasIndex("GenreID");

                    b.ToTable("Songs");
                });

            modelBuilder.Entity("vpee1_MVC__Music.Models.Album", b =>
                {
                    b.HasOne("vpee1_MVC__Music.Models.Genre", "Genre")
                        .WithMany("Albums")
                        .HasForeignKey("GenreID")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("vpee1_MVC__Music.Models.Musician", b =>
                {
                    b.HasOne("vpee1_MVC__Music.Models.Instrument", "Instrument")
                        .WithMany("Musicians")
                        .HasForeignKey("InstrumentID")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("vpee1_MVC__Music.Models.Performance", b =>
                {
                    b.HasOne("vpee1_MVC__Music.Models.Musician", "Musician")
                        .WithMany("Performances")
                        .HasForeignKey("MusicianID")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("vpee1_MVC__Music.Models.Song", "Song")
                        .WithMany("Performances")
                        .HasForeignKey("SongID")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("vpee1_MVC__Music.Models.Plays", b =>
                {
                    b.HasOne("vpee1_MVC__Music.Models.Instrument", "Instrument")
                        .WithMany("Plays")
                        .HasForeignKey("InstrumentID")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("vpee1_MVC__Music.Models.Musician", "Musician")
                        .WithMany("Plays")
                        .HasForeignKey("MusicianID")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("vpee1_MVC__Music.Models.Song", b =>
                {
                    b.HasOne("vpee1_MVC__Music.Models.Album", "Album")
                        .WithMany("Songs")
                        .HasForeignKey("AlbumID")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("vpee1_MVC__Music.Models.Genre", "Genre")
                        .WithMany("Songs")
                        .HasForeignKey("GenreID")
                        .OnDelete(DeleteBehavior.Restrict);
                });
#pragma warning restore 612, 618
        }
    }
}
