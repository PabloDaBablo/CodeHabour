using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using WMBA_7_2_.CustomControllers;
using WMBA_7_2_.Data;
using WMBA_7_2_.Models;
using WMBA_7_2_.ViewModels;

namespace WMBA_7_2_.Controllers
{
    [AllowAnonymous]
    public class HomeController : CognizantController
    {
        private readonly WMBAContext _context;

        public HomeController(WMBAContext context)
        {
            _context = context;
        }

        // GET: Game
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var games = await _context.Games
                .Include(g => g.Team_Games)
                .ThenInclude(g => g.Team)
                .Include(g => g.AwayTeam)
                .Include(g => g.HomeTeam)
                .AsNoTracking()
                .ToListAsync();

            var gameViewModels = games.Select(game => new DashboardViewModel(game)).ToList();

            ViewData["HomeTeam"] = new SelectList(_context.Teams, "ID", "TeamName");
            ViewData["AwayTeam"] = new SelectList(_context.Teams, "ID", "TeamName");

            return View(gameViewModels);
        }

        // GET: Game/Details/5
        [HttpGet]
        public async Task<IActionResult> Details(int? id)
        {


            if (id == null || _context.Games == null)
            {
                return NotFound();
            }

            var gameEntity = await _context.Games
                .Include(g => g.GamePlayers).ThenInclude(p => p.Player)
                .Include(g => g.AwayTeam)
                .Include(g => g.HomeTeam)
                .FirstOrDefaultAsync(m => m.ID == id);

            if (gameEntity == null)
            {
                return NotFound();
            }

            var gameViewModel = new DashboardViewModel(gameEntity);

            ViewBag.AvailablePlayers = _context.Players
                .Where(p => p.IsActive && !gameEntity.GamePlayers.Select(gp => gp.PlayerID).Contains(p.ID))
                .ToList();

            return View(gameViewModel);
        }

        // Method to redirect to the Game Create view
        public IActionResult RedirectToCreateGame()
        {
            return RedirectToAction("Create", "Game");
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
    }
}