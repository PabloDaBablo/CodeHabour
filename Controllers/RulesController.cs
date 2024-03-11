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
    public class RulesController : Controller
    {
        private readonly ILogger<RulesController> _logger;
        private readonly WMBAContext _context;

        public RulesController(ILogger<RulesController> logger, WMBAContext context)
        {
            _logger = logger;
            _context = context;
        }

        // GET: Rules
        public IActionResult Index()
        {
            ViewBag.Rules = _context.Rules.ToList();
            return View();
        }

        // GET: Rules/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Rules == null)
            {
                return NotFound();
            }

            var rules = await _context.Rules
                .FirstOrDefaultAsync(m => m.ID == id);
            if (rules == null)
            {
                return NotFound();
            }

            return View(rules);
        }

        // GET: Rules/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Rules/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,RuleName,RuleDescription")] Rules rules)
        {
            if (ModelState.IsValid)
            {
                _context.Add(rules);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(rules);
        }

        // GET: Rules/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Rules == null)
            {
                return NotFound();
            }

            var rules = await _context.Rules.FindAsync(id);
            if (rules == null)
            {
                return NotFound();
            }
            return View(rules);
        }

        // POST: Rules/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,RuleName,RuleDescription")] Rules rules)
        {
            if (id != rules.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(rules);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RulesExists(rules.ID))
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
            return View(rules);
        }

        // GET: Rules/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Rules == null)
            {
                return NotFound();
            }

            var rules = await _context.Rules
                .FirstOrDefaultAsync(m => m.ID == id);
            if (rules == null)
            {
                return NotFound();
            }

            return View(rules);
        }

        // POST: Rules/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Rules == null)
            {
                return Problem("Entity set 'WMBAContext.Rules'  is null.");
            }
            var rules = await _context.Rules.FindAsync(id);
            if (rules != null)
            {
                _context.Rules.Remove(rules);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RulesExists(int id)
        {
          return _context.Rules.Any(e => e.ID == id);
        }
    }
}
