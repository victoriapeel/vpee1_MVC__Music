using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using vpee1_MVC__Music.Models;

namespace vpee1_MVC__Music.Data
{
    public class MusicContext : DbContext
    {
        public MusicContext (DbContextOptions<MusicContext> options)
            : base(options)
        {
            UserName = "SeedData";
        }

        public MusicContext(DbContextOptions<MusicContext> options, IHttpContextAccessor httpContextAccessor)
            : base(options)
        {
            _httpContextAccessor = httpContextAccessor;
            UserName = _httpContextAccessor.HttpContext?.User.Identity.Name;
            //UserName = (UserName == null) ? "Unknown" : UserName;
            UserName = UserName ?? "Unknown";
        }

        //To give access to IHttpContextAccessor for Audit Data with IAuditable
        private readonly IHttpContextAccessor _httpContextAccessor;

        //Property to hold the UserName value
        public string UserName
        {
            get; private set;
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

            modelBuilder.Entity<Album>()
                .HasIndex(a => new { a.YearProduced, a.Name})
                .IsUnique();

            modelBuilder.Entity<Performance>()
                .HasKey(t => new { t.SongID, t.MusicianID });

            modelBuilder.Entity<Plays>()
                .HasKey(t => new { t.InstrumentID, t.MusicianID });
        }

        public override int SaveChanges(bool acceptAllChangesOnSuccess)
        {
            OnBeforeSaving();
            return base.SaveChanges(acceptAllChangesOnSuccess);
        }

        public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default(CancellationToken))
        {
            OnBeforeSaving();
            return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        }

        private void OnBeforeSaving()
        {
            var entries = ChangeTracker.Entries();
            foreach (var entry in entries)
            {
                if (entry.Entity is IAuditable trackable)
                {
                    var now = DateTime.UtcNow;
                    switch (entry.State)
                    {
                        case EntityState.Modified:
                            trackable.UpdatedOn = now;
                            trackable.UpdatedBy = UserName;
                            break;

                        case EntityState.Added:
                            trackable.CreatedOn = now;
                            trackable.CreatedBy = UserName;
                            trackable.UpdatedOn = now;
                            trackable.UpdatedBy = UserName;
                            break;
                    }
                }
            }
        }
    }
}
