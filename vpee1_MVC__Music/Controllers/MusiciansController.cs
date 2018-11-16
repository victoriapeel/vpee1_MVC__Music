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

namespace solution_MVC_Music.Controllers
{
    public class MusiciansController : Controller
    {
        private readonly MusicContext _context;

        public MusiciansController(MusicContext context)
        {
            _context = context;
        }

        // GET: Musicians
        public async Task<IActionResult> Index(int? InstrumentID, string sortDirection, string sortField, string actionButton, string SearchString)
        {
            ViewData["SongID"] = new SelectList(_context.Songs.OrderBy(c => c.Title), "ID", "Title");
            PopulateDropDownLists();
            ViewData["Filtering"] = "";


            var musicians = from m in _context.Musicians
                .Include(m => m.Instrument)
                .Include(m => m.Plays).ThenInclude(p => p.Instrument)
            select m;
        
            if (InstrumentID.HasValue)
            {
                musicians = musicians.Where(p => p.InstrumentID == InstrumentID);
                ViewData["Filtering"] = " in";
            }
           
            if (!String.IsNullOrEmpty(SearchString))
            {
                musicians = musicians.Where(p => p.LastName.ToUpper().Contains(SearchString.ToUpper())
                                       || p.FirstName.ToUpper().Contains(SearchString.ToUpper()));
                ViewData["Filtering"] = " in";
            }
            
            if (!String.IsNullOrEmpty(actionButton)) 
            {
                if (actionButton != "Filter")
                {
                    if (actionButton == sortField) 
                    {
                        sortDirection = String.IsNullOrEmpty(sortDirection) ? "desc" : "";
                    }
                    sortField = actionButton;
                }
            }
           
            if (sortField == "Musician")
            {
                if (String.IsNullOrEmpty(sortDirection))
                {
                    musicians = musicians
                        .OrderBy(p => p.LastName)
                        .ThenBy(p => p.FirstName);
                }
                else
                {
                    musicians = musicians
                        .OrderByDescending(p => p.LastName)
                        .ThenByDescending(p => p.FirstName);
                }
            }
            else if (sortField == "Phone Number")
            {
                if (String.IsNullOrEmpty(sortDirection))
                {
                    musicians = musicians
                        .OrderBy(p => p.PhoneNumber);
                }
                else
                {
                    musicians = musicians
                        .OrderByDescending(p => p.PhoneNumber);
                }
            }
            else if (sortField == "Age")
            {
                if (String.IsNullOrEmpty(sortDirection))
                {
                    musicians = musicians
                        .OrderBy(p => p.Age);
                }
                else
                {
                    musicians = musicians
                        .OrderByDescending(p => p.Age);
                }
            }
            else 
            {
                if (String.IsNullOrEmpty(sortDirection))
                {
                    musicians = musicians.OrderBy(p => p.Instrument.Name);
                }
                else
                {
                    musicians = musicians.OrderByDescending(p => p.Instrument.Name);
                }
            }
            //Set sort for next time
            ViewData["sortField"] = sortField;
            ViewData["sortDirection"] = sortDirection;

            return View(await musicians.ToListAsync());
        }


        // GET: Musicians/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var musician = await _context.Musicians
                .Include(m => m.Instrument)
                .Include(m => m.Plays).ThenInclude(p => p.Instrument)
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.MusicianID == id);
            if (musician == null)
            {
                return NotFound();
            }

            return View(musician);
        }

        // GET: Musicians/Create
        public IActionResult Create()
        {
            var musician = new Musician();
            musician.Plays = new List<Plays>();
            PopulateAssignedInstrumentData(musician);
            PopulateDropDownLists();
            return View();
        }

        // POST: Musicians/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,FirstName,MiddleName,LastName,Phone,DOB,SIN,InstrumentID")] Musician musician, string []selectedInstruments)
        {
            try
            {
                if (selectedInstruments != null)
                {
                    musician.Plays = new List<Plays>();
                    foreach (var inst in selectedInstruments)
                    {
                        var instToAdd = new Plays { MusicianID = musician.MusicianID, InstrumentID = int.Parse(inst) };
                        musician.Plays.Add(instToAdd);
                    }
                }
                if (ModelState.IsValid)
                {
                    _context.Add(musician);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
            }
            catch (RetryLimitExceededException)
            {
                ModelState.AddModelError("", "Unable to save changes after multiple attempts. Try again, and if the problem persists, see your system administrator.");
            }
            catch (DbUpdateException dex)
            {
                if (dex.InnerException.Message.Contains("IX_Musicians_SIN"))
                {
                    ModelState.AddModelError("", "Unable to save changes. Remember, you cannot have duplicate SIN numbers.");
                }
                else
                {
                    ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists see your system administrator.");
                }
            }

            PopulateDropDownLists(musician);
            return View(musician);
        }

        // GET: Musicians/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var musician = await _context.Musicians
                .Include(m => m.Instrument)
                .Include(m => m.Plays).ThenInclude(p => p.Instrument)
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.MusicianID == id);

            if (musician == null)
            {
                return NotFound();
            }
            PopulateDropDownLists(musician);
            return View(musician);
        }

        // POST: Musicians/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int? id, string[] selectedInstruments, Byte[] RowVersion)//, [Bind("ID,FirstName,MiddleName,LastName,Phone,DOB,SIN,InstrumentID")] Musician musician)
        {
            var musicianToUpdate = await _context.Musicians
                .Include(m => m.Instrument)
                .Include(m => m.Plays).ThenInclude(p => p.Instrument)
                .FirstOrDefaultAsync(m => m.MusicianID == id);

            if (musicianToUpdate == null)
            {
                return NotFound();
            }

            UpdateMusicianInstruments(selectedInstruments, musicianToUpdate);

            //Try updating it with the values posted
            if (await TryUpdateModelAsync<Musician>(musicianToUpdate, "",
                p => p.SIN, p => p.FirstName, p => p.MiddleName, p => p.LastName, p => p.DOB,
                p => p.PhoneNumber, p => p.InstrumentID))
            {
                try
                {
                    _context.Entry(musicianToUpdate).Property("RowVersion").OriginalValue = RowVersion;
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch (RetryLimitExceededException)
                {
                    ModelState.AddModelError("", "Unable to save changes after multiple attempts. Try again, and if the problem persists, see your system administrator.");
                }
                catch (DbUpdateConcurrencyException ex)// Added for concurrency
                {
                    var exceptionEntry = ex.Entries.Single();
                    var clientValues = (Musician)exceptionEntry.Entity;
                    var databaseEntry = exceptionEntry.GetDatabaseValues();
                    if (databaseEntry == null)
                    {
                        ModelState.AddModelError("",
                            "Unable to save changes. The Musician was deleted by another user.");
                    }
                    else
                    {
                        var databaseValues = (Musician)databaseEntry.ToObject();
                        if (databaseValues.FirstName != clientValues.FirstName)
                            ModelState.AddModelError("FirstName", "Current value: "
                                + databaseValues.FirstName);
                        if (databaseValues.MiddleName != clientValues.MiddleName)
                            ModelState.AddModelError("MiddleName", "Current value: "
                                + databaseValues.MiddleName);
                        if (databaseValues.LastName != clientValues.LastName)
                            ModelState.AddModelError("LastName", "Current value: "
                                + databaseValues.LastName);
                        if (databaseValues.SIN != clientValues.SIN)
                            ModelState.AddModelError("SI Number", "Current value: "
                                + databaseValues.SIN);
                        if (databaseValues.DOB != clientValues.DOB)
                            ModelState.AddModelError("DOB", "Current value: "
                                + String.Format("{0:d}", databaseValues.DOB));
                        if (databaseValues.PhoneNumber != clientValues.PhoneNumber)
                            ModelState.AddModelError("Phone", "Current value: "
                                + String.Format("{0:(###) ###-####}", databaseValues.PhoneNumber));
                        if (databaseValues.InstrumentID != clientValues.InstrumentID)
                        {
                            Instrument databaseInstrument = await _context.Instruments.SingleOrDefaultAsync(i => i.InstrumentID == databaseValues.InstrumentID);
                            ModelState.AddModelError("InstrumentID", $"Current value: {databaseInstrument?.Name}");
                        }
                        ModelState.AddModelError(string.Empty, "The record you attempted to edit "
                                + "was modified by another user after you received your values. The "
                                + "edit operation was canceled and the current values in the database "
                                + "have been displayed. If you still want to save your version of this record, click "
                                + "the Save button again. Otherwise click the 'Back to List' hyperlink.");
                        musicianToUpdate.RowVersion = (byte[])databaseValues.RowVersion;
                        ModelState.Remove("RowVersion");
                    }
                }
                catch (DbUpdateException dex)
                {
                    if (dex.InnerException.Message.Contains("IX_Musicians_SIN"))
                    {
                        ModelState.AddModelError("", "Unable to save changes. Remember, you cannot have duplicate SIN numbers.");
                    }
                    else
                    {
                        ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists see your system administrator.");
                    }
                }
            }

            PopulateDropDownLists(musicianToUpdate);
            PopulateAssignedInstrumentData(musicianToUpdate);
            return View(musicianToUpdate);
        }

        // GET: Musicians/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var musician = await _context.Musicians
                .Include(m => m.Instrument)
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.MusicianID == id);
            if (musician == null)
            {
                return NotFound();
            }

            return View(musician);
        }

        // POST: Musicians/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var musician = await _context.Musicians
                .Include(m => m.Instrument)
                .FirstOrDefaultAsync(m => m.MusicianID == id);
            try
            {
                _context.Musicians.Remove(musician);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch (DbUpdateException dex)
            {
                if (dex.InnerException.Message.Contains("FK_Performances_Musicians_MusicianID"))
                {
                    ModelState.AddModelError("", "Unable to save changes. You cannot delete a Musician who performed on any songs.");
                }
                else
                {
                    ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists see your system administrator.");
                }
            }
            return View(musician);
        }

        private void PopulateAssignedInstrumentData(Musician musician)
        {
            var allInstruments = _context.Instruments;
            var pInstruments = new HashSet<int>(musician.Plays.Select(b => b.InstrumentID));
            var viewModel = new List<AssignedPlayVM>();
            foreach (var con in allInstruments)
            {
                viewModel.Add(new AssignedPlayVM
                {
                    PlayID = con.InstrumentID,
                    PlayName = con.Name,
                    Assigned = pInstruments.Contains(con.InstrumentID)
                });
            }
            ViewData["Instruments"] = viewModel;
        }

        private void UpdateMusicianInstruments(string[] selectedInstruments, Musician musicianToUpdate)
        {
            if (selectedInstruments == null)
            {
                musicianToUpdate.Plays = new List<Plays>();
                return;
            }

            var selectedInstrumentsHS = new HashSet<string>(selectedInstruments);
            var musicianInst = new HashSet<int>
                (musicianToUpdate.Plays.Select(c => c.InstrumentID));
            foreach (var inst in _context.Instruments)
            {
                if (selectedInstrumentsHS.Contains(inst.InstrumentID.ToString()))
                {
                    if (!musicianInst.Contains(inst.InstrumentID))
                    {
                        musicianToUpdate.Plays.Add(new Plays { MusicianID = musicianToUpdate.MusicianID, InstrumentID = inst.InstrumentID });
                    }
                }
                else
                {
                    if (musicianInst.Contains(inst.InstrumentID))
                    {
                        Plays instrumentToRemove = musicianToUpdate.Plays.SingleOrDefault(c => c.InstrumentID == inst.InstrumentID);
                        _context.Remove(instrumentToRemove);
                    }
                }
            }
        }
        private SelectList InstrumentSelectList(int? id)
        {
            var dQuery = from d in _context.Instruments
                         orderby d.Name
                         select d;
            return new SelectList(dQuery, "InstrumentID", "Name", id);
        }

        private void PopulateDropDownLists(Musician musician = null)
        {
            ViewData["InstrumentID"] = InstrumentSelectList(musician?.InstrumentID);
        }

        [HttpGet]
        public JsonResult GetInstruments(int? id)
        {
            return Json(InstrumentSelectList(id));
        }

        private bool MusicianExists(int id)
        {
            return _context.Musicians.Any(e => e.MusicianID == id);
        }
    }
}
