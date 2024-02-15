using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WMBA_7_2_.Data;
using WMBA_7_2_.Models;

namespace WMBA_7_2_.Controllers
{
    public class DivisionController : Controller
    {
        private readonly WMBAContext _context;

        public DivisionController(WMBAContext context)
        {
            _context = context;
        }

        // GET: Division
        public async Task<IActionResult> Index()
        {

            ViewData["LeagueTypeID"] = new SelectList(_context.Leagues, "ID", "LeagueType");
            return View(await _context.Divisions.ToListAsync());


        }

        // GET: Division/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Divisions == null)
            {
                return NotFound();
            }

            var division = await _context.Divisions
                .FirstOrDefaultAsync(m => m.ID == id);
            if (division == null)
            {
                return NotFound();
            }

            return View(division);
        }

        // GET: Division/Create
        public IActionResult Create()
        {

            ViewData["LeagueTypeID"] = new SelectList(_context.Leagues, "ID", "LeagueType");
            return View();
        }

        // POST: Division/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,DivAge,DivisionTeams,LeagueTypeID")] Division division)
        {
            if (ModelState.IsValid)
            {
                _context.Add(division);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index", "ManageLookup");
            }

            ViewData["LeagueTypeID"] = new SelectList(_context.Leagues, "ID", "LeagueType");
            return View(division);
        }

        // GET: Division/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Divisions == null)
            {
                return NotFound();
            }

            var division = await _context.Divisions.FindAsync(id);
            if (division == null)
            {
                return NotFound();
            }

            ViewData["LeagueTypeID"] = new SelectList(_context.Leagues, "ID", "LeagueType");
            return View(division);
        }

        // POST: Division/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,DivAge,DivisionTeams,LeagueTypeID")] Division division)
        {
            if (id != division.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(division);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DivisionExists(division.ID))
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

            ViewData["LeagueTypeID"] = new SelectList(_context.Leagues, "ID", "LeagueType", division.LeagueTypeID);
            return View(division);
        }

        // GET: Division/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Divisions == null)
            {
                return NotFound();
            }

            var division = await _context.Divisions
                .FirstOrDefaultAsync(m => m.ID == id);
            if (division == null)
            {
                return NotFound();
            }
            ViewData["LeagueTypeID"] = new SelectList(_context.Leagues, "ID", "LeagueType", division.LeagueTypeID);

            return View(division);
        }

        // POST: Division/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Divisions == null)
            {
                return Problem("Entity set 'WMBAContext.Divisions'  is null.");
            }
            var division = await _context.Divisions.FindAsync(id);
            if (division != null)
            {
                _context.Divisions.Remove(division);
            }

            ViewData["LeagueTypeID"] = new SelectList(_context.Leagues, "ID", "LeagueType", division.LeagueTypeID);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index", "ManageLookup");
        }

        private bool DivisionExists(int id)
        {
          return _context.Divisions.Any(e => e.ID == id);
        }
    }
}
