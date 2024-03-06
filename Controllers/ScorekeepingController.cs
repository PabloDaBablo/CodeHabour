using Microsoft.AspNetCore.Mvc;
using Microsoft.DotNet.MSIdentity.Shared;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using SQLitePCL;
using WMBA_7_2_.Data;
using WMBA_7_2_.Models;

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
                                     .FirstOrDefault();

            var players = _context.Players
                .Where(p => p.GamePlayers.Any(gp => gp.GameID == gameId) && p.TeamID == awayTeamID) 
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

        public JsonResult GetPlayersHome(int gameId)
        {
            var homeTeamID = _context.Games
                .Where(g => g.ID == gameId)
				.Select(g => g.HomeTeamID)
				.FirstOrDefault();

            var players = _context.Players
                .Where(p => p.GamePlayers.Any(gp => gp.GameID == gameId) && p.TeamID == homeTeamID) 
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

        public class PlayerScoredDto
        {
            public int PlayerId { get; set; }
        }

        [HttpPost]
        public async Task<IActionResult> PlayerScored([FromBody] PlayerScoredDto dto)
        {
            if (dto == null)
            {
                return BadRequest("Invalid request");
            }

            var playerExists = await _context.Players.AnyAsync(p => p.ID == dto.PlayerId);
            if (!playerExists)
            {
                return NotFound($"Player with ID {dto.PlayerId} not found.");
            }

           
            var playerStats = await _context.PlayerStats
                                           .FirstOrDefaultAsync(ps => ps.PlayerID == dto.PlayerId);

            if (playerStats == null)
            {

                playerStats = new PlayerStats
                {
                    PlayerID = dto.PlayerId,
                    Runs = 1
                };
                _context.PlayerStats.Add(playerStats);
            }
            else
            {
                playerStats.Runs = (playerStats.Runs ?? 0) + 1;
            }

            try
            {
                await _context.SaveChangesAsync();
                return Json(new { success = true });
            }
            catch (Exception ex)
            {
               
                return Json(new { success = false, message = ex.Message });
            }
        }

        [HttpPost]
        public async Task<IActionResult> PlayerStruckOut([FromBody] PlayerScoredDto dto)
        {
            if (dto == null)
            {
                return BadRequest("Invalid request");
            }

            var playerExists = await _context.Players.AnyAsync(p => p.ID == dto.PlayerId);
            if (!playerExists)
            {
                return NotFound($"Player with ID {dto.PlayerId} not found.");
            }

            var playerStats = await _context.PlayerStats
                                           .FirstOrDefaultAsync(ps => ps.PlayerID == dto.PlayerId);

            if (playerStats == null)
            {
                
                playerStats = new PlayerStats
                {
                    PlayerID = dto.PlayerId,
                    K = 1 
                };
                _context.PlayerStats.Add(playerStats);
            }
            else
            {
                
                playerStats.K++;
                
            }

            try
            {
                await _context.SaveChangesAsync();
                return Json(new { success = true });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }



        [HttpGet]
        public async Task<IActionResult> GetGameDetails(int gameId)
        {
            var game = await _context.Games
                .Include(g => g.HomeTeam)
                .Include(g => g.AwayTeam)
                .FirstOrDefaultAsync(g => g.ID == gameId);

            if (game == null)
            {
                return NotFound();
            }

            return Json(new
            {
                homeTeamName = game.HomeTeam.TeamName, 
                awayTeamName = game.AwayTeam.TeamName
            });
        }

        public class PlayerBaseHitDto
        {
            public int PlayerId { get; set; }
            public string BaseHitType { get; set; } 
        }

		[HttpPost]
		public async Task<IActionResult> PlayerBaseHit([FromBody] PlayerBaseHitDto dto)
		{
			if (dto == null || !new[] { "1B", "2B", "3B", "HR" }.Contains(dto.BaseHitType))
			{
				return BadRequest("Invalid request");
			}

			var player = await _context.Players.FindAsync(dto.PlayerId);
			if (player == null)
			{
				return NotFound($"Player with ID {dto.PlayerId} not found.");
			}

			var playerStats = await _context.PlayerStats.FirstOrDefaultAsync(ps => ps.PlayerID == dto.PlayerId) ?? new PlayerStats { PlayerID = dto.PlayerId };

			// Increment the appropriate statistic based on the base hit type
			switch (dto.BaseHitType)
			{
				case "1B":
					playerStats.B1 = (playerStats.B1 ?? 0) + 1;
					break;
				case "2B":
					playerStats.B2 = (playerStats.B2 ?? 0) + 1;
					break;
				case "3B":
					playerStats.B3 = (playerStats.B3 ?? 0) + 1;
					break;
				case "HR":
					playerStats.HR = (playerStats.HR ?? 0) + 1;
					break;
			}

			// If it's a new record, add it to the DbSet
			if (playerStats.ID == 0)
			{
				_context.PlayerStats.Add(playerStats);
			}

			try
			{
				await _context.SaveChangesAsync();
				return Json(new { success = true });
			}
			catch (Exception ex)
			{
				return Json(new { success = false, message = ex.Message });
			}
		}


	}
}