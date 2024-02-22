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
    public class Line_UpController : Controller
    {
        private readonly WMBAContext _context;

        public Line_UpController(WMBAContext context)
        {
            _context = context;
        }

        // GET: Line_Up
        public async Task<IActionResult> Index()
        {
              return View(await _context.Line_Ups.ToListAsync());
        }

        // GET: Line_Up/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Line_Ups == null)
            {
                return NotFound();
            }

            var line_Up = await _context.Line_Ups
                .FirstOrDefaultAsync(m => m.ID == id);
            if (line_Up == null)
            {
                return NotFound();
            }

            return View(line_Up);
        }

        // GET: Line_Up/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Line_Up/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID")] Line_Up line_Up)
        {
            if (ModelState.IsValid)
            {
                _context.Add(line_Up);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(line_Up);
        }

        // GET: Line_Up/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Line_Ups == null)
            {
                return NotFound();
            }

            var line_Up = await _context.Line_Ups.FindAsync(id);
            if (line_Up == null)
            {
                return NotFound();
            }
            return View(line_Up);
        }

        // POST: Line_Up/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID")] Line_Up line_Up)
        {
            if (id != line_Up.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(line_Up);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!Line_UpExists(line_Up.ID))
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
            return View(line_Up);
        }

        // GET: Line_Up/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Line_Ups == null)
            {
                return NotFound();
            }

            var line_Up = await _context.Line_Ups
                .FirstOrDefaultAsync(m => m.ID == id);
            if (line_Up == null)
            {
                return NotFound();
            }

            return View(line_Up);
        }

        // POST: Line_Up/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Line_Ups == null)
            {
                return Problem("Entity set 'WMBAContext.Line_Ups'  is null.");
            }
            var line_Up = await _context.Line_Ups.FindAsync(id);
            if (line_Up != null)
            {
                _context.Line_Ups.Remove(line_Up);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool Line_UpExists(int id)
        {
          return _context.Line_Ups.Any(e => e.ID == id);
        }
    }
}
