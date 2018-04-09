using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SacramentMeetingPlanner.Models;

namespace SacramentMeetingPlanner.Controllers
{
    public class PrayersController : Controller
    {
        private readonly SacramentMeetingPlannerContext _context;

        public PrayersController(SacramentMeetingPlannerContext context)
        {
            _context = context;
        }

        // GET: Prayers
        public async Task<IActionResult> Index(string sortOrder, string searchString)
        {
            ViewData["NameSortParm"] = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewData["CurrentFilter"] = searchString;

            var Prayers = from s in _context.Prayers
                           select s;
            if (!String.IsNullOrEmpty(searchString))
            {
                Prayers = Prayers.Where(s => s.OpeningPrayers.Contains(searchString)
                                       || s.ClosingPrayers.Contains(searchString));
            }
            switch (sortOrder)
            {
                case "name_desc":
                    Prayers = Prayers.OrderByDescending(s => s.OpeningPrayers);
                    break;
                default:
                    Prayers = Prayers.OrderBy(s => s.OpeningPrayers);
                    break;
            }
            return View(await Prayers.AsNoTracking().ToListAsync());
        }



        // GET: Prayers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var prayers = await _context.Prayers
                .SingleOrDefaultAsync(m => m.PrayersID == id);
            if (prayers == null)
            {
                return NotFound();
            }

            return View(prayers);
        }

        // GET: Prayers/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Prayers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PrayersID,OpeningPrayers,ClosingPrayers")] Prayers prayers)
        {
            if (ModelState.IsValid)
            {
                _context.Add(prayers);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(prayers);
        }

        // GET: Prayers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var prayers = await _context.Prayers.SingleOrDefaultAsync(m => m.PrayersID == id);
            if (prayers == null)
            {
                return NotFound();
            }
            return View(prayers);
        }

        // POST: Prayers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("PrayersID,OpeningPrayers,ClosingPrayers")] Prayers prayers)
        {
            if (id != prayers.PrayersID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(prayers);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PrayersExists(prayers.PrayersID))
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
            return View(prayers);
        }

        // GET: Prayers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var prayers = await _context.Prayers
                .SingleOrDefaultAsync(m => m.PrayersID == id);
            if (prayers == null)
            {
                return NotFound();
            }

            return View(prayers);
        }

        // POST: Prayers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var prayers = await _context.Prayers.SingleOrDefaultAsync(m => m.PrayersID == id);
            _context.Prayers.Remove(prayers);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PrayersExists(int id)
        {
            return _context.Prayers.Any(e => e.PrayersID == id);
        }
    }
}
