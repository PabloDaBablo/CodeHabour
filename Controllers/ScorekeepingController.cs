using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SQLitePCL;
using WMBA_7_2_.Data;

namespace WMBA_7_2_.Controllers
{
    public class ScorekeepingController : Controller
    {
        private readonly WMBAContext _context;

        public ScorekeepingController(WMBAContext context)
        {
            _context = context;
        }


        public IActionResult Index(int gameId)
        {
            var game = _context.Games
                               .Include(g => g.GamePlayers)
                               .ThenInclude(gp => gp.Player)
                               .FirstOrDefault(g => g.ID == gameId);

            ViewData["GameId"] = gameId;
            return View(game);
        }

        public JsonResult GetPlayers(int gameId)
        {
            var awayTeamID = _context.Games
                                     .Where(g => g.ID == gameId)
                                     .Select(g => g.AwayTeamID)
                                     .FirstOrDefault();//loads the away team lineup onto the scorekeeping page. -Michael

            var players = _context.Players
                .Where(p => p.GamePlayers.Any(gp => gp.GameID == gameId) && p.TeamID == awayTeamID) // Ensure player is part of the home team.
                .Include(c => c.Division)
                .Include(c => c.Team)
                .Include(c => c.PlayerPositions).ThenInclude(pp => pp.Position)
                .Include(c => c.PlayerGameStats)
                .Include(c => c.GamePlayers).ThenInclude(gp => gp.Game)
                .Select(c => new
                {
                    id = c.ID,
                    playerNumber = c.PlayerNumber,
                    playerFirstName = c.PlayerFirstName,
                    playerLastName = c.PlayerLastName,
                    division = c.Division.DivAge,
                    team = c.Team.TeamName,
                    position = c.PlayerPositions.FirstOrDefault().Position.PlayerPosName,
                    games = c.GamePlayers.Where(gp => gp.GameID == gameId)
                                         .Select(gp => new { gp.GameID, gp.PlayerID }).ToList(),
                    stats = c.PlayerGameStats.Select(pgs => new { pgs.AVG, pgs.OBP, pgs.SLG, pgs.OPS }).ToList()
                })
                .ToList();

            return Json(players);
        }


    }
}