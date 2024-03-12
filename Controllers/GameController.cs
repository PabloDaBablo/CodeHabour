using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WMBA_7_2_.CustomControllers;
using WMBA_7_2_.Data;
using WMBA_7_2_.Models;

namespace WMBA_7_2_.Controllers
{
    public class GameController : CognizantController
    {
        private readonly WMBAContext _context;

        public GameController(WMBAContext context)
        {
            _context = context;
        }

        // Redirect to Dashboard/Home Page
        public IActionResult RedirectToDashboard()
        {
            return RedirectToAction("Index", "Home");
        }


        // GET: Game
        [HttpGet]
        public async Task<IActionResult> Index(int? divisionID)
        {
            
            var gamesQuery = _context.Games
            .Include(g => g.Team_Games).ThenInclude(g => g.Team).ThenInclude(g => g.Division)
            .Include(g => g.AwayTeam).ThenInclude(g => g.Division)
            .Include(g => g.HomeTeam).ThenInclude(g => g.Division)
            .AsNoTracking();

            var games = await gamesQuery.ToListAsync();

            if (divisionID.HasValue)
            {
                games = games.Where(g => g.HomeTeam.DivisionID == divisionID).ToList();
            }
        

            ViewData["DivisionID"] = new SelectList(_context.Divisions, "ID", "DivAge");
            ViewData["HomeTeam"] = new SelectList(_context.Teams, "ID", "TeamName");
            ViewData["AwayTeam"] = new SelectList(_context.Teams, "ID", "TeamName");

            return View(games);
        }

        // GET: Game/Details/5
        [HttpGet]
        public async Task<IActionResult> Details(int? id)
        {


            if (id == null || _context.Games == null)
            {
                return NotFound();
            }

            var game = await _context.Games
                .Include(g => g.GamePlayers).ThenInclude(p => p.Player)
                .Include(g => g.AwayTeam)
                .Include(g => g.HomeTeam)
                .FirstOrDefaultAsync(m => m.ID == id);

            ViewBag.AvailablePlayers = _context.Players.Where(p => p.IsActive && !game.GamePlayers.Select(gp => gp.PlayerID).Contains(p.ID)).ToList();

            if (game == null)
            {
                return NotFound();
            }

            return View(game);
        }

        // GET: Game/Create
        [HttpGet]
        public IActionResult Create()
        {

            ViewData["HomeTeam"] = new SelectList(_context.Teams, "ID", "TeamName");
            ViewData["AwayTeam"] = new SelectList(_context.Teams, "ID", "TeamName");
            return View();
        }

        // POST: Game/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Division,HomeTeamID,AwayTeamID,GameDate,GameTime,GameLocation")] Game game)
        {

            ViewData["HomeTeam"] = new SelectList(_context.Teams, "ID", "TeamName");
            ViewData["AwayTeam"] = new SelectList(_context.Teams, "ID", "TeamName");
           


            if (ModelState.IsValid)
            {
                game.HomeTeam = _context.Teams.Include(t => t.Players).FirstOrDefault(t => t.ID == game.HomeTeamID);
                game.AwayTeam = _context.Teams.Include(t => t.Players).FirstOrDefault(t => t.ID == game.AwayTeamID);

                FillLineupsWithTeams(game);

                _context.Add(game);
                await _context.SaveChangesAsync();
                // Set success message in TempData
                TempData["SuccessMessage"] = "Game was successfully created.";
				//return RedirectToAction("Details", new { id = game.ID });
				return RedirectToAction("Create", new { id = game.ID });
			}


            return View();
        }



        // GET: Game/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Games == null)
            {
                return NotFound();
            }

            var game = await _context.Games.FindAsync(id);
            if (game == null)
            {
                return NotFound();
            }

            ViewData["HomeTeam"] = new SelectList(_context.Teams, "ID", "TeamName");
            ViewData["AwayTeam"] = new SelectList(_context.Teams, "ID", "TeamName");
            return View(game);
        }

        // POST: Game/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,GameDate,GameTime,HomeTeamID,AwayTeamID,GameLocation")] Game game)
        {


            ViewData["HomeTeam"] = new SelectList(_context.Teams, "ID", "TeamName", game.HomeTeam);
            ViewData["AwayTeam"] = new SelectList(_context.Teams, "ID", "TeamName", game.AwayTeam);

            if (id != game.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(game);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!GameExists(game.ID))
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

            return View(game);
        }

        // GET: Game/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Games == null)
            {
                return NotFound();
            }

            var game = await _context.Games
                .FirstOrDefaultAsync(m => m.ID == id);
            if (game == null)
            {
                return NotFound();
            }

            return View(game);
        }

        // POST: Game/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Games == null)
            {
                return Problem("Entity set 'WMBAContext.Schedules'  is null.");
            }
            var game = await _context.Games.FindAsync(id);
            if (game != null)
            {
                _context.Games.Remove(game);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool GameExists(int id)
        {
          return _context.Games.Any(e => e.ID == id);
        }

        private void FillLineupsWithTeams(Game game)
        {
            foreach (Player player in game.HomeTeam.Players)
            {
                game.GamePlayers.Add(new GamePlayer()
                {
                    PlayerID = player.ID,
                    GameID = game.ID,
                    TeamLineup = TeamLineup.Home
                });
            }

            foreach (Player player in game.AwayTeam.Players)
            {
                game.GamePlayers.Add(new GamePlayer()
                {
                    PlayerID = player.ID,
                    GameID = game.ID,
                    TeamLineup = TeamLineup.Away
                });
            }
        }

        [HttpPost]
        public async Task<IActionResult> RemovePlayerFromGame(int gameId, int playerId)
        {
            var gamePlayer = await _context.GamePlayers.FirstOrDefaultAsync(gp => gp.GameID == gameId && gp.PlayerID == playerId);
            if (gamePlayer != null)
            {
                _context.GamePlayers.Remove(gamePlayer);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Details), new { id = gameId });
        }

        [HttpPost]
        public async Task<IActionResult> AddPlayerToGame(int GameID, int PlayerID, bool isHomeTeam)
        {
            var existingGamePlayer = await _context.GamePlayers.FirstOrDefaultAsync(gp => gp.GameID == GameID && gp.PlayerID == PlayerID);
            if (existingGamePlayer == null)
            {
                var player = await _context.Players.FindAsync(PlayerID);
                if (player != null)
                {
                    var gamePlayer = new GamePlayer
                    {
                        GameID = GameID,
                        PlayerID = PlayerID,
                        TeamLineup = isHomeTeam ? TeamLineup.Home : TeamLineup.Away 
                    };
                    _context.GamePlayers.Add(gamePlayer);
                    await _context.SaveChangesAsync();
                }
            }

            return RedirectToAction(nameof(Details), new { id = GameID });
        }
            private SelectList DivisionSelectList(int? selectedId)
            {
                return new SelectList(_context.Divisions
                    .OrderBy(d => d.DivAge), "ID", "DivAge", selectedId);
            }
            
            private void PopulateDropDownLists(Game game = null)
            {
                ViewData["DivisonID"] = DivisionSelectList(game?.HomeTeamID);
            }

       
    }
}
