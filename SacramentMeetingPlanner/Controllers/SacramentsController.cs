﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SacramentMeetingPlanner.Models;

namespace SacramentMeetingPlanner.Controllers
{
    public class SacramentsController : Controller
    {
        private readonly SacramentMeetingPlannerContext _context;

        public SacramentsController(SacramentMeetingPlannerContext context)
        {
            _context = context;
        }

        // GET: Sacraments
        public async Task<IActionResult> Index(string sortOrder, string searchString)
        {
            ViewData["NameSortParm"] = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewData["DateSortParm"] = sortOrder == "Date" ? "date_desc" : "Date";
            ViewData["CurrentFilter"] = searchString;

            var Sacrament = from s in _context.Sacrament
                           select s;
            if (!String.IsNullOrEmpty(searchString))
            {
                Sacrament = Sacrament.Where(s => s.Subjects.Contains(searchString)
                                       || s.Conducting.Contains(searchString));
            }
            switch (sortOrder)
            {
                case "name_desc":
                    Sacrament = Sacrament.OrderByDescending(s => s.Subjects);
                    break;
                case "Date":
                    Sacrament = Sacrament.OrderBy(s => s.MeetingDate);
                    break;
                case "date_desc":
                    Sacrament = Sacrament.OrderByDescending(s => s.MeetingDate);
                    break;
                default:
                    Sacrament = Sacrament.OrderBy(s => s.Conducting);
                    break;
            }
            return View(await Sacrament.AsNoTracking().ToListAsync());
        }

        // GET: Sacraments/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sacrament = await _context.Sacrament
                .SingleOrDefaultAsync(m => m.ID == id);
            if (sacrament == null)
            {
                return NotFound();
            }

            return View(sacrament);
        }

        // GET: Sacraments/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Sacraments/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,MeetingDate,Conducting,Subjects")] Sacrament sacrament)
        {
            if (ModelState.IsValid)
            {
                _context.Add(sacrament);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(sacrament);
        }

        // GET: Sacraments/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sacrament = await _context.Sacrament.SingleOrDefaultAsync(m => m.ID == id);
            if (sacrament == null)
            {
                return NotFound();
            }
            return View(sacrament);
        }

        // POST: Sacraments/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,MeetingDate,Conducting,Subjects")] Sacrament sacrament)
        {
            if (id != sacrament.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(sacrament);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SacramentExists(sacrament.ID))
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
            return View(sacrament);
        }

        // GET: Sacraments/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sacrament = await _context.Sacrament
                .SingleOrDefaultAsync(m => m.ID == id);
            if (sacrament == null)
            {
                return NotFound();
            }

            return View(sacrament);
        }

        // POST: Sacraments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var sacrament = await _context.Sacrament.SingleOrDefaultAsync(m => m.ID == id);
            _context.Sacrament.Remove(sacrament);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SacramentExists(int id)
        {
            return _context.Sacrament.Any(e => e.ID == id);
        }
    }
}
