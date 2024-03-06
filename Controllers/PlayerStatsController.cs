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


        private bool PlayerStatsExists(int id)
        {
          return _context.PlayerStats.Any(e => e.ID == id);
        }
    }
}
