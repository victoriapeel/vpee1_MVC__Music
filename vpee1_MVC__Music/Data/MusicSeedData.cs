using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using vpee1_MVC__Music.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace vpee1_MVC__Music.Data
{
    public static class MusicSeedData
    {
        public static void Initialise(IServiceProvider serviceProvider)

        {
            using (var context = new MusicContext(serviceProvider.GetRequiredService<DbContextOptions<MusicContext>>()))
            {
                if (!context.Genres.Any())
                {
                    context.Genres.AddRange(
                        new Genre
                        {
                            Name = "Rock"
                        },

                        new Genre
                        {
                            Name = "Ska"
                        },

                        new Genre
                        {
                            Name = "Rap"
                        },

                        new Genre
                        {
                            Name = "World"
                        },

                        new Genre
                        {
                            Name = "Reggae"
                        },

                        new Genre
                        {
                            Name = "Hard Bop"
                        },

                        new Genre
                        {
                            Name = "Punk"
                        }
                    );
                    context.SaveChanges();
                }

                if (!context.Albums.Any())
                {
                    context.Albums.AddRange(
                        new Album
                        {
                            Name = "The Real Thing",
                            YearProduced = DateTime.Parse("1989-06-20"),
                            Price = 15.99m,
                            GenreID = context.Genres.FirstOrDefault(d => d.Name == "Rock").GenreID
                        },

                         new Album
                         {
                             Name = "Doggy Style",
                             YearProduced = DateTime.Parse("1993-11-23"),
                             Price = 31.99m,
                             GenreID = context.Genres.FirstOrDefault(d => d.Name == "Rap").GenreID
                         },

                         new Album
                         {
                             Name = "Damaged",
                             YearProduced = DateTime.Parse("1981-12-05"),
                             Price = 17.99m,
                             GenreID = context.Genres.FirstOrDefault(d => d.Name == "Punk").GenreID
                         },

                         new Album
                         {
                             Name = "Arkology",
                             YearProduced = DateTime.Parse("1997-07-14"),
                             Price = 21.99m,
                             GenreID = context.Genres.FirstOrDefault(d => d.Name == "Reggae").GenreID
                         },

                         new Album
                         {
                             Name = "Hi-Bop Ska",
                             YearProduced = DateTime.Parse("1994-10-19"),
                             Price = 25.99m,
                             GenreID = context.Genres.FirstOrDefault(d => d.Name == "Ska").GenreID
                         },

                         new Album
                         {
                             Name = "Radio Bemba Sound System",
                             YearProduced = DateTime.Parse("2002-09-17"),
                             Price = 15.99m,
                             GenreID = context.Genres.FirstOrDefault(d => d.Name == "World").GenreID
                         },

                         new Album
                         {
                             Name = "Blue Train",
                             YearProduced = DateTime.Parse("1957-09-15"),
                             Price = 25.99m,
                             GenreID = context.Genres.FirstOrDefault(d => d.Name == "Hard Bop").GenreID
                         }
                       );
                    context.SaveChanges();
                }

                if (!context.Songs.Any())
                {
                    context.Songs.AddRange(
                        new Song
                        {
                            Title = "The Real Thing",
                            GenreID = context.Genres.FirstOrDefault(d => d.Name == "Rock").GenreID,
                            AlbumID = context.Albums.FirstOrDefault(d => d.Name == "The Real Thing").AlbumID
                        },

                        new Song
                        {
                            Title = "Underwater Love",
                            GenreID = context.Genres.FirstOrDefault(d => d.Name == "Rock").GenreID,
                            AlbumID = context.Albums.FirstOrDefault(d => d.Name == "The Real Thing").AlbumID
                        },

                        new Song
                        {
                            Title = "Mr Bobby",
                            GenreID = context.Genres.FirstOrDefault(d => d.Name == "World").GenreID,
                            AlbumID = context.Albums.FirstOrDefault(d => d.Name == "Radio Bemba Sound System").AlbumID
                        },

                        new Song
                        {
                            Title = "Clandestino",
                            GenreID = context.Genres.FirstOrDefault(d => d.Name == "World").GenreID,
                            AlbumID = context.Albums.FirstOrDefault(d => d.Name == "Radio Bemba Sound System").AlbumID
                        },

                        new Song
                        {
                            Title = "Lazy Bird",
                            GenreID = context.Genres.FirstOrDefault(d => d.Name == "Hard Bop").GenreID,
                            AlbumID = context.Albums.FirstOrDefault(d => d.Name == "Blue Train").AlbumID
                        },

                        new Song
                        {
                            Title = "Groovy Dub",
                            GenreID = context.Genres.FirstOrDefault(d => d.Name == "Ska").GenreID,
                            AlbumID = context.Albums.FirstOrDefault(d => d.Name == "Arkology").AlbumID
                        },

                        new Song
                        {
                            Title = "Rise Above",
                            GenreID = context.Genres.FirstOrDefault(d => d.Name == "Punk").GenreID,
                            AlbumID = context.Albums.FirstOrDefault(d => d.Name == "Damaged").AlbumID
                        },

                        new Song
                        {
                            Title = "Padded Cell",
                            GenreID = context.Genres.FirstOrDefault(d => d.Name == "Punk").GenreID,
                            AlbumID = context.Albums.FirstOrDefault(d => d.Name == "Damaged").AlbumID
                        }
                      );
                    context.SaveChanges();
                }

                if (!context.Instruments.Any())
                {
                    context.Instruments.AddRange(
                        new Instrument
                        {
                            Name = "Drums",
                        },

                        new Instrument
                        {
                            Name = "Guitar",
                        },

                        new Instrument
                        {
                            Name = "Vocal",
                        },

                        new Instrument
                        {
                            Name = "Piano",
                        },

                        new Instrument
                        {
                            Name = "Bass",
                        },

                        new Instrument
                        {
                            Name = "Trumpet",
                        },

                        new Instrument
                        {
                            Name = "Trombone",
                        },

                        new Instrument
                        {
                            Name = "Saxophone",
                        }
                 );
                    context.SaveChanges();
                }

                if (!context.Musicians.Any())
                {
                    context.Musicians.AddRange(
                        new Musician
                        {
                            FirstName = "John",
                            MiddleName = "William",
                            LastName = "Coltrane",
                            PhoneNumber = 1235559999,
                            DOB = DateTime.Parse("1926-09-23"),
                            SIN = "123456789",
                            InstrumentID = context.Instruments.FirstOrDefault(d => d.Name == "Saxophone").InstrumentID

                        },

                        new Musician
                        {
                            FirstName = "Jose-Manuel",
                            MiddleName = "Chao",
                            LastName = "Ortega",
                            PhoneNumber = 5552224444,
                            DOB = DateTime.Parse("1961-06-21"),
                            SIN = "124455698",
                            InstrumentID = context.Instruments.FirstOrDefault(d => d.Name == "Guitar").InstrumentID
                        },

                        new Musician
                        {
                            FirstName = "Michael",
                            MiddleName = "Allan",
                            LastName = "Paton",
                            PhoneNumber = 9051234569,
                            DOB = DateTime.Parse("1968-01-27"),
                            SIN = "135469789",
                            InstrumentID = context.Instruments.FirstOrDefault(d => d.Name == "Vocal").InstrumentID

                        },

                        new Musician
                        {
                            FirstName = "Chuck",
                            LastName = "Dukowski",
                            PhoneNumber = 5556669999,
                            DOB = DateTime.Parse("1954-02-01"),
                            SIN = "135478987",
                            InstrumentID = context.Instruments.FirstOrDefault(d => d.Name == "Bass").InstrumentID

                        },

                        new Musician
                        {
                            FirstName = "John",
                            MiddleName = "William",
                            LastName = "Coltrane",
                            PhoneNumber = 1235559999,
                            DOB = DateTime.Parse("1926-09-23"),
                            SIN = "123456788",
                            InstrumentID = context.Instruments.FirstOrDefault(d => d.Name == "Saxophone").InstrumentID
                        }
                    );
                    context.SaveChanges();
                }
            }
        }
    }
}
