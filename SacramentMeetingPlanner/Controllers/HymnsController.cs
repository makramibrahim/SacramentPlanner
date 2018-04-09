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
    public class HymnsController : Controller
    {
        private readonly SacramentMeetingPlannerContext _context;

        public HymnsController(SacramentMeetingPlannerContext context)
        {
            _context = context;
        }

        // GET: Hymns
        public async Task<IActionResult> Index(string sortOrder, string searchString)
        {
            ViewData["NameSortParm"] = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewData["CurrentFilter"] = searchString;

            var Hymns = from s in _context.Hymns
                        select s;
            if (!String.IsNullOrEmpty(searchString))
            {
                Hymns = Hymns.Where(s => s.OpeningHymn.Contains(searchString)
                                       || s.ClosingHymn.Contains(searchString)
                                       || s.SacramentHymn.Contains(searchString));
            }
            switch (sortOrder)
            {
                case "name_desc":
                    Hymns = Hymns.OrderByDescending(s => s.OpeningHymn);
                    break;
                default:
                    Hymns = Hymns.OrderBy(s => s.OpeningHymn);
                    break;
            }
            return View(await Hymns.AsNoTracking().ToListAsync());
        }

        // GET: Hymns/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var hymns = await _context.Hymns
                .SingleOrDefaultAsync(m => m.HymnsID == id);
            if (hymns == null)
            {
                return NotFound();
            }

            return View(hymns);
        }

        // GET: Hymns/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Hymns/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("HymnsID,OpeningHymn,OpeningHyNumber,SacramentHymn,SacrHyNumber,ClosingHymn,ClosingNumber")] Hymns hymns)
        {
            if (ModelState.IsValid)
            {
                _context.Add(hymns);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(hymns);
        }

        // GET: Hymns/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var hymns = await _context.Hymns.SingleOrDefaultAsync(m => m.HymnsID == id);
            if (hymns == null)
            {
                return NotFound();
            }
            return View(hymns);
        }

        // POST: Hymns/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("HymnsID,OpeningHymn,OpeningHyNumber,SacramentHymn,SacrHyNumber,ClosingHymn,ClosingNumber")] Hymns hymns)
        {
            if (id != hymns.HymnsID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(hymns);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!HymnsExists(hymns.HymnsID))
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
            return View(hymns);
        }

        // GET: Hymns/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var hymns = await _context.Hymns
                .SingleOrDefaultAsync(m => m.HymnsID == id);
            if (hymns == null)
            {
                return NotFound();
            }

            return View(hymns);
        }

        // POST: Hymns/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var hymns = await _context.Hymns.SingleOrDefaultAsync(m => m.HymnsID == id);
            _context.Hymns.Remove(hymns);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool HymnsExists(int id)
        {
            return _context.Hymns.Any(e => e.HymnsID == id);
        }
    }
}
