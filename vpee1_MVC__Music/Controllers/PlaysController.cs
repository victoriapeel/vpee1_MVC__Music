using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using vpee1_MVC__Music.Data;
using vpee1_MVC__Music.Models;

namespace vpee1_MVC__Music.Controllers
{
    public class PlaysController : Controller
    {
        private readonly MusicContext _context;

        public PlaysController(MusicContext context)
        {
            _context = context;
        }

        // GET: Plays
        public async Task<IActionResult> Index()
        {
            var musicContext = _context.Plays.Include(p => p.Instrument).Include(p => p.Musician);
            return View(await musicContext.ToListAsync());
        }

        // GET: Plays/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var plays = await _context.Plays
                .Include(p => p.Instrument)
                .Include(p => p.Musician)
                .FirstOrDefaultAsync(m => m.InstrumentID == id);
            if (plays == null)
            {
                return NotFound();
            }

            return View(plays);
        }

        // GET: Plays/Create
        public IActionResult Create()
        {
            ViewData["InstrumentID"] = new SelectList(_context.Instruments, "InstrumentID", "Name");
            ViewData["MusicianID"] = new SelectList(_context.Musicians, "MusicianID", "FirstName");
            return View();
        }

        // POST: Plays/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("InstrumentID,MusicianID")] Plays plays)
        {
            if (ModelState.IsValid)
            {
                _context.Add(plays);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["InstrumentID"] = new SelectList(_context.Instruments, "InstrumentID", "Name", plays.InstrumentID);
            ViewData["MusicianID"] = new SelectList(_context.Musicians, "MusicianID", "FirstName", plays.MusicianID);
            return View(plays);
        }

        // GET: Plays/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var plays = await _context.Plays.FindAsync(id);
            if (plays == null)
            {
                return NotFound();
            }
            ViewData["InstrumentID"] = new SelectList(_context.Instruments, "InstrumentID", "Name", plays.InstrumentID);
            ViewData["MusicianID"] = new SelectList(_context.Musicians, "MusicianID", "FirstName", plays.MusicianID);
            return View(plays);
        }

        // POST: Plays/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("InstrumentID,MusicianID")] Plays plays)
        {
            if (id != plays.InstrumentID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(plays);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PlaysExists(plays.InstrumentID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["InstrumentID"] = new SelectList(_context.Instruments, "InstrumentID", "Name", plays.InstrumentID);
            ViewData["MusicianID"] = new SelectList(_context.Musicians, "MusicianID", "FirstName", plays.MusicianID);
            return View(plays);
        }

        // GET: Plays/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var plays = await _context.Plays
                .Include(p => p.Instrument)
                .Include(p => p.Musician)
                .FirstOrDefaultAsync(m => m.InstrumentID == id);
            if (plays == null)
            {
                return NotFound();
            }

            return View(plays);
        }

        // POST: Plays/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var plays = await _context.Plays.FindAsync(id);
            _context.Plays.Remove(plays);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PlaysExists(int id)
        {
            return _context.Plays.Any(e => e.InstrumentID == id);
        }
    }
}
