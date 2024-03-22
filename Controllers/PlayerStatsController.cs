using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WMBA_7_2_.Data;
using WMBA_7_2_.Models;
using WMBA_7_2_.Utilities;
using static System.Runtime.InteropServices.JavaScript.JSType;

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
        [Authorize(Roles = "Admin, Coaches, Convenor, Scorekeeper")]
        public async Task<IActionResult> Index(int? page, string SearchString, string actionButton, string sortDirection = "asc", string sortField = "Runs")
        {
            var wMBAContext = _context.PlayerStats.Include(p => p.Player).AsNoTracking();

            if (!string.IsNullOrEmpty(SearchString))
            {
                wMBAContext = wMBAContext.Where(p => p.Player.PlayerFirstName.ToUpper().Contains(SearchString.ToUpper()) || p.Player.PlayerLastName.ToUpper().Contains(SearchString.ToUpper()));
            }
            //List of sort options.
            string[] sortOptions = new[] { "Runs", "Hits", "Home Runs", "RBI" };

            if (!System.String.IsNullOrEmpty(sortDirection))
            {
                //String on its own has an ambiguity error so I added System,
                //the sort works, I will deal with it with Stovell later. 
                //~donaven
                page = 1;
                if (sortOptions.Contains(actionButton))
                {
                    if (actionButton == sortField)
                    {
                        sortDirection = sortDirection == "asc" ? "desc" : "asc";
                    }
                    sortField = actionButton;
                }
            }

            if (sortField == "Runs")
            {
                if (sortDirection == "asc")
                {
                    wMBAContext = wMBAContext
                        .OrderBy(p => p.Runs);
                }
                else
                {
                    wMBAContext = wMBAContext
                        .OrderByDescending(p => p.Runs);
                }
            }
            else if (sortField == "Hits")
            {
                if (sortDirection == "asc")
                {
                    wMBAContext = wMBAContext
                        .OrderByDescending(p => p.Hits);
                }
                else
                {
                    wMBAContext = wMBAContext
                        .OrderBy(p => p.Hits);
                }
            }
            else if (sortField == "Home Runs")
            {
                if (sortDirection == "asc")
                {
                    wMBAContext = wMBAContext
                        .OrderBy(p => p.HR);
                }
                else
                {
                    wMBAContext = wMBAContext
                        .OrderByDescending(p => p.HR);
                }
            }
            else
            {
                if (sortDirection == "asc")
                {
                    wMBAContext = wMBAContext
                        .OrderBy(p => p.RBI);
                }
                else
                {
                    wMBAContext = wMBAContext
                        .OrderByDescending(p => p.RBI);
                }
            }
            
            //ViewData["playerNameFilter"] = SearchString;

            //Set sort for next time
            ViewData["sortField"] = sortField;
            ViewData["sortDirection"] = sortDirection;
            
            int pageSize = 15;//Change as required
            var pagedData = await PaginatedList<PlayerStats>.CreateAsync(wMBAContext.AsNoTracking(), page ?? 1, pageSize);
            return View(pagedData);
            
            //return View(await wMBAContext.ToListAsync());
        }


        private bool PlayerStatsExists(int id)
        {
          return _context.PlayerStats.Any(e => e.ID == id);
        }
    }
}
