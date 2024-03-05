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
    public class PlayerStatsController : Controller
    {
        private readonly WMBAContext _context;

        public PlayerStatsController(WMBAContext context)
        {
            _context = context;
        }

        // GET: PlayerStats
        public async Task<IActionResult> Index()
        {
            var wMBAContext = _context.PlayerStats.Include(p => p.Player);
            return View(await wMBAContext.ToListAsync());
        }

        // GET: PlayerStats/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.PlayerStats == null)
            {
                return NotFound();
            }

            var playerStats = await _context.PlayerStats
                .Include(p => p.Player)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (playerStats == null)
            {
                return NotFound();
            }

            return View(playerStats);
        }

        // GET: PlayerStats/Create
        public IActionResult Create()
        {
            ViewData["PlayerID"] = new SelectList(_context.Players, "ID", "PlayerFirstName");
            return View();
        }

        // POST: PlayerStats/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,PA,Runs,Hits,B1,B2,B3,HR,RBI,BB,K,SB,SAC,PlayerID")] PlayerStats playerStats)
        {
            if (ModelState.IsValid)
            {
                _context.Add(playerStats);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["PlayerID"] = new SelectList(_context.Players, "ID", "PlayerFirstName", playerStats.PlayerID);
            return View(playerStats);
        }

        // GET: PlayerStats/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.PlayerStats == null)
            {
                return NotFound();
            }

            var playerStats = await _context.PlayerStats.FindAsync(id);
            if (playerStats == null)
            {
                return NotFound();
            }
            ViewData["PlayerID"] = new SelectList(_context.Players, "ID", "PlayerFirstName", playerStats.PlayerID);
            return View(playerStats);
        }

        // POST: PlayerStats/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,PA,Runs,Hits,B1,B2,B3,HR,RBI,BB,K,SB,SAC,PlayerID")] PlayerStats playerStats)
        {
            if (id != playerStats.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(playerStats);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PlayerStatsExists(playerStats.ID))
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
            ViewData["PlayerID"] = new SelectList(_context.Players, "ID", "PlayerFirstName", playerStats.PlayerID);
            return View(playerStats);
        }

        // GET: PlayerStats/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.PlayerStats == null)
            {
                return NotFound();
            }

            var playerStats = await _context.PlayerStats
                .Include(p => p.Player)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (playerStats == null)
            {
                return NotFound();
            }

            return View(playerStats);
        }

        // POST: PlayerStats/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.PlayerStats == null)
            {
                return Problem("Entity set 'WMBAContext.PlayerStats'  is null.");
            }
            var playerStats = await _context.PlayerStats.FindAsync(id);
            if (playerStats != null)
            {
                _context.PlayerStats.Remove(playerStats);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PlayerStatsExists(int id)
        {
          return _context.PlayerStats.Any(e => e.ID == id);
        }
    }
}
