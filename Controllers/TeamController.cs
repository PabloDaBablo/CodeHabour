using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using WMBA_7_2_.Data;
using WMBA_7_2_.Models;
using WMBA_7_2_.ViewModels;

namespace WMBA_7_2_.Controllers
{
    public class TeamController : Controller
    {
        private readonly WMBAContext _context;

        public TeamController(WMBAContext context)
        {
            _context = context;
        }

        // GET: Team
        public async Task<IActionResult> Index(string sortOrder)
        {
            var wMBAContext = _context.Teams
                .Include(t => t.Coach)
                .Include(p => p.Players)
            .AsNoTracking();

            ViewData["NameSortParm"] = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewData["CoachSortParm"] = sortOrder == "Coach" ? "coach_desc" : "Coach";

            var teams = from t in _context.Teams.Include(t => t.Coach).Include(t => t.Players)
            select t;

            switch (sortOrder)
            {
                case "name_desc":
                    teams = teams.OrderByDescending(t => t.TeamName);
                    break;
                case "Coach":
                    teams = teams.OrderBy(t => t.Coach.CoachName);
                    break;
                case "coach_desc":
                    teams = teams.OrderByDescending(t => t.Coach.CoachName);
                    break;
                default:
                    teams = teams.OrderBy(t => t.TeamName);
                    break;
            }

            return View(await teams.AsNoTracking().ToListAsync());
        }

        // GET: Team/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Teams == null)
            {
                return NotFound();
            }

            var team = await _context.Teams
                .Include(t => t.Coach)
                .Include(t => t.Players)
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.ID == id);
            if (team == null)
            {
                return NotFound();
            }

            return View(team);
        }

        // GET: Team/Create
        public IActionResult Create()
        {

            var viewModel = new TeamVM
            {
            };
            ViewData["CoachID"] = new SelectList(_context.Coaches, "ID", "CoachName");
            ViewData["PlayerID"] = new SelectList(_context.Players, "ID", "PlayerFullName");
            return View();
        }

        // POST: Team/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(TeamVM viewModel)
        {
            if (ModelState.IsValid)
            {
                var team = new Team
                {
                    
                };

                _context.Teams.Add(team);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }

            ViewData["PlayerID"] = new SelectList(_context.Players, "ID", "PlayerFullName");
            return View(viewModel);
        }

        // GET: Team/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Teams == null)
            {
                return NotFound();
            }

            var team = await _context.Teams
                             .Include(t => t.Players)
                             .Include(t => t.Coach)
                             .FirstOrDefaultAsync(t => t.ID == id);

            if (team == null)
            {
                return NotFound();
            }
            ViewData["CoachID"] = new SelectList(_context.Coaches, "ID", "CoachName", team.CoachID);
            ViewData["PlayerID"] = new SelectList(_context.Players, "ID", "PlayerFullName");

            return View();
        }

        // POST: Team/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,TeamName,PlayerID,CoachID")] Team team)
        {
            if (id != team.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(team);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TeamExists(team.ID))
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
            ViewData["CoachID"] = new SelectList(_context.Coaches, "ID", "CoachName", team.CoachID);
            ViewData["PlayerID"] = new SelectList(_context.Players, "ID", "PlayerFullName");

            return View(team);
        }

        // GET: Team/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Teams == null)
            {
                return NotFound();
            }

            var team = await _context.Teams
                .Include(t => t.Coach)
                .Include(t => t.Players)
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.ID == id);
            if (team == null)
            {
                return NotFound();
            }

            return View(team);
        }

        // POST: Team/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Teams == null)
            {
                return Problem("Entity set 'WMBAContext.Teams'  is null.");
            }
            var team = await _context.Teams.FindAsync(id);
            if (team != null)
            {
                _context.Teams.Remove(team);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TeamExists(int id)
        {
          return _context.Teams.Any(e => e.ID == id);
        }
    }
}
