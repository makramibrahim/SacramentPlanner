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
    public class SpeakersController : Controller
    {
        private readonly SacramentMeetingPlannerContext _context;

        public SpeakersController(SacramentMeetingPlannerContext context)
        {
            _context = context;
        }

        // GET: Speakers
        public async Task<IActionResult> Index(string sortOrder, string searchString)
        {
            ViewData["NameSortParm"] = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewData["CurrentFilter"] = searchString;

            var Speakers = from s in _context.Speakers
                          select s;
            if (!String.IsNullOrEmpty(searchString))
            {
                Speakers = Speakers.Where(s => s.Speaker1.Contains(searchString)
                                       || s.Speaker2.Contains(searchString)
                                       || s.Speaker3.Contains(searchString));
            }
            switch (sortOrder)
            {
                case "name_desc":
                    Speakers = Speakers.OrderByDescending(s => s.Speaker1);
                    break;
               default:
                    Speakers = Speakers.OrderBy(s => s.Speaker1);
                    break;
            }
            return View(await Speakers.AsNoTracking().ToListAsync());
        }



        // GET: Speakers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var speakers = await _context.Speakers
                .SingleOrDefaultAsync(m => m.SpeakersID == id);
            if (speakers == null)
            {
                return NotFound();
            }

            return View(speakers);
        }

        // GET: Speakers/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Speakers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("SpeakersID,Speaker1,Speaker2,Speaker3")] Speakers speakers)
        {
            if (ModelState.IsValid)
            {
                _context.Add(speakers);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(speakers);
        }

        // GET: Speakers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var speakers = await _context.Speakers.SingleOrDefaultAsync(m => m.SpeakersID == id);
            if (speakers == null)
            {
                return NotFound();
            }
            return View(speakers);
        }

        // POST: Speakers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("SpeakersID,Speaker1,Speaker2,Speaker3")] Speakers speakers)
        {
            if (id != speakers.SpeakersID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(speakers);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SpeakersExists(speakers.SpeakersID))
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
            return View(speakers);
        }

        // GET: Speakers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var speakers = await _context.Speakers
                .SingleOrDefaultAsync(m => m.SpeakersID == id);
            if (speakers == null)
            {
                return NotFound();
            }

            return View(speakers);
        }

        // POST: Speakers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var speakers = await _context.Speakers.SingleOrDefaultAsync(m => m.SpeakersID == id);
            _context.Speakers.Remove(speakers);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SpeakersExists(int id)
        {
            return _context.Speakers.Any(e => e.SpeakersID == id);
        }
    }
}
