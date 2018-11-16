using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using vpee1_MVC__Music.Data;
using vpee1_MVC__Music.Models;
using vpee1_MVC__Music.ViewModels;

namespace vpee1_MVC__Music.Controllers
{
    public class SongsController : Controller
    {
        private readonly MusicContext _context;

        public SongsController(MusicContext context)
        {
            _context = context;
        }

        // GET: Songs
        public async Task<IActionResult> Index()
        {
            var songs = from s in _context.Songs
                .Include(s => s.Performances).ThenInclude(s => s.Musician)
                          select s;
            return View(await songs.ToListAsync());
        }

        // GET: Songs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var song = await _context.Songs
                .Include(s => s.Album)
                .Include(s => s.Genre)
                .FirstOrDefaultAsync(m => m.SongID == id);
            if (song == null)
            {
                return NotFound();
            }

            return View(song);
        }

        // GET: Songs/Create
        public IActionResult Create()
        {
            Song song = new Song();
            PopulateAssignedPerformanceData(song);
            ViewData["AlbumID"] = new SelectList(_context.Albums.OrderBy(a => a.Name), "AlbumID", "Name");
            ViewData["GenreID"] = new SelectList(_context.Genres.OrderBy(g => g.Name), "GenreID", "Name");
            return View();
        }

        // POST: Songs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("SongID,Title,GenreID,AlbumID")] Song song, string[] selectedOptions)
        {
            try
            {
                UpdateSongMusicians(selectedOptions, song);
                if (ModelState.IsValid)
                {
                    _context.Add(song);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
            }
            catch (Exception)
            {
                ModelState.AddModelError("", "Unable to save changes. Try again, if the problem persists contact administrator");
            }
            ViewData["AlbumID"] = new SelectList(_context.Albums, "AlbumID", "Name", song.AlbumID);
            ViewData["GenreID"] = new SelectList(_context.Genres, "GenreID", "Name", song.GenreID);
            PopulateAssignedPerformanceData(song);
            return View(song);
        }

        // GET: Songs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var song = await _context.Songs
                .Include(m => m.Performances).ThenInclude(m => m.Musician)
                .AsNoTracking()
                .SingleOrDefaultAsync(m=> m.SongID == id);
            if (song == null)
            {
                return NotFound();
            }
            ViewData["AlbumID"] = new SelectList(_context.Albums, "AlbumID", "Name", song.AlbumID);
            ViewData["GenreID"] = new SelectList(_context.Genres, "GenreID", "Name", song.GenreID);
            PopulateAssignedPerformanceData(song);
            return View(song);
        }

        // POST: Songs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit( Song song, int id, string[] selectedOptions)
        {
            var songToUpdate = await _context.Songs
                  .Include(m => m.Performances).ThenInclude(m => m.Musician)
                  .SingleOrDefaultAsync(m => m.SongID == id);


            if (songToUpdate == null)
            {
                return NotFound();
            }
            UpdateSongMusicians(selectedOptions, songToUpdate);

            if (await TryUpdateModelAsync<Song>(songToUpdate, "",
                m => m.Title))
            {

                try
                {
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch (RetryLimitExceededException)
                {
                    ModelState.AddModelError("", "Unable to save changes after multiple attempts. Try again, and if the problem persists, see your system administrator.");
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SongExists(songToUpdate.SongID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                catch (DbUpdateException)
                {
                    ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists see your system administrator.");
                }
                
            }
            ViewData["AlbumID"] = new SelectList(_context.Albums, "AlbumID", "Name", song.AlbumID);
            ViewData["GenreID"] = new SelectList(_context.Genres, "GenreID", "Name", song.GenreID);
            PopulateAssignedPerformanceData(songToUpdate);
            return View(songToUpdate);
        }
        

        // GET: Songs/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var song = await _context.Songs
                .Include(s => s.Album)
                .Include(s => s.Genre)
                .FirstOrDefaultAsync(m => m.SongID == id);
            if (song == null)
            {
                return NotFound();
            }

            return View(song);
        }

        // POST: Songs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var song = await _context.Songs.FindAsync(id);
            _context.Songs.Remove(song);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private void PopulateAssignedPerformanceData(Song song)
        {
            var allPerformances = _context.Musicians;
            var sonPerformances = new HashSet<int>(song.Performances.Select(p => p.MusicianID));
            var selected = new List<PerformanceVM>();
            var available = new List<PerformanceVM>();
            foreach (var p in allPerformances)
            {
                if (sonPerformances.Contains(p.MusicianID))
                {
                    selected.Add(new PerformanceVM
                    {
                        PerformanceID = p.MusicianID,
                        PerformanceName = p.FullName
                    });
                }
                else
                {
                    available.Add(new PerformanceVM
                    {
                        PerformanceID = p.MusicianID,
                        PerformanceName = p.FullName
                    });
                }
            }

            ViewData["selOpts"] = new MultiSelectList(selected.OrderBy(p => p.PerformanceName), "MusicianID", "Name");
            ViewData["availOpts"] = new MultiSelectList(available.OrderBy(p => p.PerformanceName), "MusicianID", "Name");
        }

        private void UpdateSongMusicians(string[] selectedOptions, Song songToUpdate)
        {
            if (selectedOptions == null)
            {
                songToUpdate.Performances = new List<Performance>();
                return;
            }

            var selectedOptionsHS = new HashSet<string>(selectedOptions);
            var sonMusicians = new HashSet<int>(songToUpdate.Performances.Select(m =>m.MusicianID));
            foreach (var m in _context.Musicians)
            {
                if (selectedOptionsHS.Contains(m.MusicianID.ToString()))
                {
                    if (!sonMusicians.Contains(m.MusicianID))
                    {
                        songToUpdate.Performances.Add(new Performance
                        {
                            MusicianID = m.MusicianID,
                            SongID = songToUpdate.SongID
                        });
                    }
                }
                else
                {
                    if (sonMusicians.Contains(m.MusicianID))
                    {
                        Performance specToRemove = songToUpdate.Performances.SingleOrDefault(p => p.MusicianID == p.MusicianID);
                        _context.Remove(specToRemove);
                    }
                }
            }
        }

        private bool SongExists(int id)
        {
            return _context.Songs.Any(e => e.SongID == id);
        }
    }
}
