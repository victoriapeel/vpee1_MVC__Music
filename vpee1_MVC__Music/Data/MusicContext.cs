using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using vpee1_MVC__Music.Models;

namespace vpee1_MVC__Music.Data
{
    public class MusicContext : DbContext
    {
        public MusicContext (DbContextOptions<MusicContext> options)
            : base(options)
        {
        }

        public DbSet<Album> Albums { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<Instrument> Instruments { get; set; }
        public DbSet<Musician> Musicians { get; set; }
        public DbSet<Performance> Performances { get; set; }
        public DbSet<Plays> Plays { get; set; }
        public DbSet<Song> Songs { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("MO");

            modelBuilder.Entity<Genre>()
                .HasMany<Album>(d => d.Albums)
                .WithOne(p => p.Genre)
                .HasForeignKey(p => p.GenreID)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Genre>()
                .HasMany<Song>(d => d.Songs)
                .WithOne(p => p.Genre)
                .HasForeignKey(p => p.GenreID)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Album>()
                .HasMany<Song>(d => d.Songs)
                .WithOne(p => p.Album)
                .HasForeignKey(p => p.AlbumID)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Instrument>()
                .HasMany<Plays>(d => d.Plays)
                .WithOne(p => p.Instrument)
                .HasForeignKey(p => p.InstrumentID)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Instrument>()
               .HasMany<Musician>(d => d.Musicians)
               .WithOne(p => p.Instrument)
               .HasForeignKey(p => p.InstrumentID)
               .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Musician>()
              .HasMany<Performance>(d => d.Performances)
              .WithOne(p => p.Musician)
              .HasForeignKey(p => p.MusicianID)
              .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Musician>()
                .HasIndex(p => p.SIN)
                .IsUnique();

            modelBuilder.Entity<Performance>()
                .HasKey(t => new { t.SongID, t.MusicianID });

            modelBuilder.Entity<Plays>()
                .HasKey(t => new { t.InstrumentID, t.MusicianID });
        }
    }
}
