using Humanizer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.DotNet.MSIdentity.Shared;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
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

        //Add score to away team
        [HttpPost]
        public async Task<IActionResult> AddAwayTeamRun([FromBody] GameScoreUpdateRequest request)
        {
            var game = await _context.Games.FindAsync(request.GameId);
            if (game == null)
            {
                return Json(new { success = false, message = "Game not found." });
            }

            game.AwayTeamScore += 1; // Increment the away team score
            await _context.SaveChangesAsync();

            return Json(new { success = true });
        }
        
        //Subtract score from away team
        [HttpPost]
        public async Task<IActionResult> DecrementAwayTeamRun([FromBody] GameScoreUpdateRequest request)
        {
            var game = await _context.Games.FindAsync(request.GameId);
            if (game == null)
            {
                return Json(new { success = false, message = "Game not found." });
            }

            game.AwayTeamScore = Math.Max(0, game.AwayTeamScore - 1); // Decrement and ensure non-negative
            await _context.SaveChangesAsync();

            return Json(new { success = true });
        }

        public class GameScoreUpdateRequest
        {
            public int GameId { get; set; }
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
            if (dto == null || dto.PlayerId <= 0)
            {
                return BadRequest("Invalid request");
            }

            var playerExists = await _context.Players.AnyAsync(p => p.ID == dto.PlayerId);
            if (!playerExists)
            {
                return NotFound($"Player with ID {dto.PlayerId} not found.");
            }

            var result = await _context.Database.ExecuteSqlRawAsync(
                "UPDATE PlayerStats SET K = COALESCE(K, 0) + 1 WHERE PlayerID = {0};",
                dto.PlayerId);

            if (result > 0)
            {
                return Json(new { success = true, message = "Strikeout updated successfully." });
            }
            else
            {
                var newStats = new PlayerStats
                {
                    PlayerID = dto.PlayerId,
                    K = 1
                };
                await _context.PlayerStats.AddAsync(newStats);
                await _context.SaveChangesAsync();
                return Json(new { success = true, message = "Player stats created and strikeout recorded." });
            }
        }

        public async Task<IActionResult> GamesPlayed([FromBody] PlayerScoredDto dto)
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
                    GP = 1 
                };
                _context.PlayerStats.Add(playerStats);
            }
            else
            {
                playerStats.GP++;
                _context.PlayerStats.Update(playerStats); 
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
        public async Task<IActionResult> PlateAppearances([FromBody] PlayerScoredDto dto)
        {
            if (dto == null || dto.PlayerId <= 0)
            {
                return BadRequest("Invalid request");
            }

            var playerExists = await _context.Players.AnyAsync(p => p.ID == dto.PlayerId);
            if (!playerExists)
            {
                return NotFound($"Player with ID {dto.PlayerId} not found.");
            }

            var result = await _context.Database.ExecuteSqlRawAsync(
                "UPDATE PlayerStats SET PA = COALESCE(PA, 0) + 1 WHERE PlayerID = {0};",
                dto.PlayerId);

            if (result > 0)
            {
                return Json(new { success = true, message = "Plate Appearances updated successfully." });
            }
            else
            {
                var newStats = new PlayerStats
                {
                    PlayerID = dto.PlayerId,
                    PA = 1
                };
                await _context.PlayerStats.AddAsync(newStats);
                await _context.SaveChangesAsync();
                return Json(new { success = true, message = "Player stats created and plate appearances recorded." });
            }
        }//doesnt work. tried to use SQL to update the PA but it did not work. gonna need instructor help for this one idk why it wont work ):

        [HttpPost]
        public async Task<IActionResult> RunsBattedIn([FromBody] PlayerScoredDto dto)
        {
            if (dto == null || dto.PlayerId <= 0)
            {
                return BadRequest("Invalid request");
            }

            var playerExists = await _context.Players.AnyAsync(p => p.ID == dto.PlayerId);
            if (!playerExists)
            {
                return NotFound($"Player with ID {dto.PlayerId} not found.");
            }

            var result = await _context.Database.ExecuteSqlRawAsync(
                "UPDATE PlayerStats SET RBI = COALESCE(RBI, 0) + 1 WHERE PlayerID = {0};",
                dto.PlayerId);

            if (result > 0)
            {
                return Json(new { success = true, message = "RBI updated successfully." });
            }
            else
            {
                var newStats = new PlayerStats
                {
                    PlayerID = dto.PlayerId,
                    RBI = 1
                };
                await _context.PlayerStats.AddAsync(newStats);
                await _context.SaveChangesAsync();
                return Json(new { success = true, message = "Player stats created and RBI recorded." });
            }
        }

        [HttpPost]
        public async Task<IActionResult> Sacrifice([FromBody] PlayerScoredDto dto)
        {
            if (dto == null || dto.PlayerId <= 0)
            {
                return BadRequest("Invalid request");
            }

            var playerExists = await _context.Players.AnyAsync(p => p.ID == dto.PlayerId);
            if (!playerExists)
            {
                return NotFound($"Player with ID {dto.PlayerId} not found.");
            }

            var result = await _context.Database.ExecuteSqlRawAsync(
                               "UPDATE PlayerStats SET SAC = COALESCE(SAC, 0) + 1 WHERE PlayerID = {0};",
                                              dto.PlayerId);

            if (result > 0)
            {
                return Json(new { success = true, message = "Sacrifice updated successfully." });
            }
            else
            {
                var newStats = new PlayerStats
                {
                    PlayerID = dto.PlayerId,
                    SAC = 1
                };
                await _context.PlayerStats.AddAsync(newStats);
                await _context.SaveChangesAsync();
                return Json(new { success = true, message = "Player stats created and Sacrifice recorded." });
            }
        }

        [HttpPost]
        public async Task<IActionResult> StolenBase([FromBody] PlayerScoredDto dto)
        {
            if (dto == null || dto.PlayerId <= 0)
            {
                return BadRequest("Invalid request");
            }

            var playerExists = await _context.Players.AnyAsync(p => p.ID == dto.PlayerId);
            if (!playerExists)
            {
                return NotFound($"Player with ID {dto.PlayerId} not found.");
            }

            var result = await _context.Database.ExecuteSqlRawAsync(
                               "UPDATE PlayerStats SET SB = COALESCE(SB, 0) + 1 WHERE PlayerID = {0};",
                                              dto.PlayerId);

            if (result > 0)
            {
                return Json(new { success = true, message = "Sacrifice updated successfully." });
            }
            else
            {
                var newStats = new PlayerStats
                {
                    PlayerID = dto.PlayerId,
                    SB = 1
                };
                await _context.PlayerStats.AddAsync(newStats);
                await _context.SaveChangesAsync();
                return Json(new { success = true, message = "Player stats created and Sacrifice recorded." });
            }
        }

        [HttpPost]
        public async Task<IActionResult> BaseOnBalls([FromBody] PlayerScoredDto dto)
        {
            if (dto == null || dto.PlayerId <= 0)
            {
                return BadRequest("Invalid request");
            }

            var playerExists = await _context.Players.AnyAsync(p => p.ID == dto.PlayerId);
            if (!playerExists)
            {
                return NotFound($"Player with ID {dto.PlayerId} not found.");
            }

            var result = await _context.Database.ExecuteSqlRawAsync(
                               "UPDATE PlayerStats SET BB = COALESCE(BB, 0) + 1 WHERE PlayerID = {0};",
                                              dto.PlayerId);

            if (result > 0)
            {
                return Json(new { success = true, message = "Sacrifice updated successfully." });
            }
            else
            {
                var newStats = new PlayerStats
                {
                    PlayerID = dto.PlayerId,
                    BB = 1
                };
                await _context.PlayerStats.AddAsync(newStats);
                await _context.SaveChangesAsync();
                return Json(new { success = true, message = "Player stats created and Sacrifice recorded." });
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

            switch (dto.BaseHitType)
            {
                case "1B":
                    playerStats.B1 = (playerStats.B1 ?? 0) + 1;
                    playerStats.Hits = (playerStats.Hits ?? 0) + 1;
                    break;
                case "2B":
                    playerStats.B2 = (playerStats.B2 ?? 0) + 1;
                    playerStats.Hits = (playerStats.Hits ?? 0) + 1;
                    break;
                case "3B":
                    playerStats.B3 = (playerStats.B3 ?? 0) + 1;
                    playerStats.Hits = (playerStats.Hits ?? 0) + 1;
                    break;
                case "HR":
                    playerStats.HR = (playerStats.HR ?? 0) + 1;
                    playerStats.RBI = (playerStats.RBI ?? 0) + 1;
                    playerStats.Runs = (playerStats.Runs ?? 0) + 1;
                    playerStats.Hits = (playerStats.Hits ?? 0) + 1;
                    break;
            }

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

        public class PlayerOutDto
        {
            public int PlayerId { get; set; }
            public string OutType { get; set; }
        }

        [HttpPost]
        public async Task<IActionResult> PlayerOut([FromBody] PlayerOutDto dto)
        {
            if (dto == null || !new[] { "GO", "PO", "FO" }.Contains(dto.OutType))
            {
                return BadRequest("Invalid request");
            }

            var playerStats = await _context.PlayerStats.FirstOrDefaultAsync(ps => ps.PlayerID == dto.PlayerId)
                ?? new PlayerStats { PlayerID = dto.PlayerId };

            switch (dto.OutType)
            {
                case "GO":
                    playerStats.GO = (playerStats.GO ?? 0) + 1;
                    break;
                case "PO":
                    playerStats.PO = (playerStats.PO ?? 0) + 1;
                    break;
                case "FO":
                    playerStats.FO = (playerStats.FO ?? 0) + 1;
                    break;
            }

            if (_context.Entry(playerStats).State == EntityState.Detached)
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

        public class ActionLogDto
        {
            public string ActionType { get; set; }
            public string Data { get; set; }
            public int GameID { get; set; }
        }

        [HttpPost]
        public ActionResult LogAction([FromBody] ActionLogDto actionLogDto)
        {
            var actionLog = new ActionLog
            {
                ActionType = actionLogDto.ActionType,
                Data = actionLogDto.Data,
                Timestamp = DateTime.Now,
                GameID = actionLogDto.GameID
            };
            _context.ActionLogs.Add(actionLog);
            _context.SaveChanges();

            var actionData = JsonConvert.DeserializeObject<PlayerActionDto>(actionLogDto.Data);

            return Json(new { success = true });
        }

        public class PlayerActionDto
        {
            public int PlayerId { get; set; }
        }

        [HttpPost]
        public async Task<IActionResult> UndoLastAction()
        {
            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                var lastAction = await _context.ActionLogs.OrderByDescending(a => a.Timestamp).FirstOrDefaultAsync();
                if (lastAction == null)
                {
                    return Json(new { success = false, message = "No actions to undo." });
                }

                var actionData = JsonConvert.DeserializeObject<PlayerActionDto>(lastAction.Data);
                if (actionData == null)
                {
                    return Json(new { success = false, message = "Failed to parse action data." });
                }

                switch (lastAction.ActionType)
                {
                    case "Run":
                        {
                            var playerId = actionData.PlayerId;
                            var playerStat = await _context.PlayerStats.FirstOrDefaultAsync(ps => ps.PlayerID == actionData.PlayerId);
                            if (playerStat == null)
                            {
                                return Json(new { success = false, message = $"Player stat not found for playerId {playerId}." });
                            }

                            if (playerStat.Runs > 0)
                            {
                                playerStat.Runs -= 1;
                                
                                _context.PlayerStats.Update(playerStat);
                                await _context.SaveChangesAsync();
                                await transaction.CommitAsync();
                                return Json(new { success = true });
                            }
                            else
                            {
                                return Json(new { success = false, message = "Invalid stat count; cannot decrement." });
                            }
                        }
                    case "Single":
                        {
                            var playerId = actionData.PlayerId;
                            var playerStat = await _context.PlayerStats.FirstOrDefaultAsync(ps => ps.PlayerID == actionData.PlayerId);
                            if (playerStat == null)
                            {
                                return Json(new { success = false, message = $"Player stat not found for playerId {playerId}." });
                            }

                            if (playerStat.B1 > 0)
                            {
                                playerStat.B1 -= 1;
                                playerStat.Hits -= 1;
                                _context.PlayerStats.Update(playerStat);
                                await _context.SaveChangesAsync();
                                await transaction.CommitAsync();
                                return Json(new { success = true });
                            }
                            else
                            {
                                return Json(new { success = false, message = "Invalid stat count; cannot decrement." });
                            }
                        }
                    case "Double":
                        {
                            var playerId = actionData.PlayerId;
                            var playerStat = await _context.PlayerStats.FirstOrDefaultAsync(ps => ps.PlayerID == actionData.PlayerId);
                            if (playerStat == null)
                            {
                                return Json(new { success = false, message = $"Player stat not found for playerId {playerId}." });
                            }

                            if (playerStat.B2 > 0)
                            {
                                playerStat.B2 -= 1;
                                playerStat.Hits -= 1;
                                _context.PlayerStats.Update(playerStat);
                                await _context.SaveChangesAsync();
                                await transaction.CommitAsync();
                                return Json(new { success = true });
                            }
                            else
                            {
                                return Json(new { success = false, message = "Invalid stat count; cannot decrement." });
                            }
                        }
                    case "Triple":
                        {
                            var playerId = actionData.PlayerId;
                            var playerStat = await _context.PlayerStats.FirstOrDefaultAsync(ps => ps.PlayerID == actionData.PlayerId);
                            if (playerStat == null)
                            {
                                return Json(new { success = false, message = $"Player stat not found for playerId {playerId}." });
                            }

                            if (playerStat.B3 > 0)
                            {
                                playerStat.B3 -= 1;
                                playerStat.Hits -= 1;
                                _context.PlayerStats.Update(playerStat);
                                await _context.SaveChangesAsync();
                                await transaction.CommitAsync();
                                return Json(new { success = true });
                            }
                            else
                            {
                                return Json(new { success = false, message = "Invalid stat count; cannot decrement." });
                            }
                        }
                    case "HomeRun":
                        {
                            var playerId = actionData.PlayerId;
                            var playerStat = await _context.PlayerStats.FirstOrDefaultAsync(ps => ps.PlayerID == actionData.PlayerId);
                            if (playerStat == null)
                            {
                                return Json(new { success = false, message = $"Player stat not found for playerId {playerId}." });
                            }

                            if (playerStat.HR > 0)
                            {
                                playerStat.HR -= 1;
                                playerStat.Hits -= 1;
                                playerStat.RBI -= 1;
                                playerStat.Runs -= 1;
                                _context.PlayerStats.Update(playerStat);
                                await _context.SaveChangesAsync();
                                await transaction.CommitAsync();
                                return Json(new { success = true });
                            }
                            else
                            {
                                return Json(new { success = false, message = "Invalid stat count; cannot decrement." });
                            }
                        }
                    case "GO": 
                        {
                            var playerId = actionData.PlayerId;
                            var playerStat = await _context.PlayerStats.FirstOrDefaultAsync(ps => ps.PlayerID == actionData.PlayerId);
                            if (playerStat == null)
                            {
                                return Json(new { success = false, message = $"Player stat not found for playerId {playerId}." });
                            }

                            if (playerStat.GO > 0)
                            {
                                playerStat.GO -= 1;
                                _context.PlayerStats.Update(playerStat);
                                await _context.SaveChangesAsync();
                                await transaction.CommitAsync();
                                return Json(new { success = true });
                            }
                            else
                            {
                                return Json(new { success = false, message = "Invalid stat count; cannot decrement." });
                            }
                        }
                    case "FO":
                        {
                            var playerId = actionData.PlayerId;
                            var playerStat = await _context.PlayerStats.FirstOrDefaultAsync(ps => ps.PlayerID == actionData.PlayerId);
                            if (playerStat == null)
                            {
                                return Json(new { success = false, message = $"Player stat not found for playerId {playerId}." });
                            }

                            if (playerStat.FO > 0)
                            {
                                playerStat.FO -= 1;
                                _context.PlayerStats.Update(playerStat);
                                await _context.SaveChangesAsync();
                                await transaction.CommitAsync();
                                return Json(new { success = true });
                            }
                            else
                            {
                                return Json(new { success = false, message = "Invalid stat count; cannot decrement." });
                            }
                        }
                    case "PO":
                        {
                            var playerId = actionData.PlayerId;
                            var playerStat = await _context.PlayerStats.FirstOrDefaultAsync(ps => ps.PlayerID == actionData.PlayerId);
                            if (playerStat == null)
                            {
                                return Json(new { success = false, message = $"Player stat not found for playerId {playerId}." });
                            }

                            if (playerStat.PO > 0)
                            {
                                playerStat.PO -= 1;
                                _context.PlayerStats.Update(playerStat);
                                await _context.SaveChangesAsync();
                                await transaction.CommitAsync();
                                return Json(new { success = true });
                            }
                            else
                            {
                                return Json(new { success = false, message = "Invalid stat count; cannot decrement." });
                            }
                        }
                    case "RBI":
                        {
                            var playerId = actionData.PlayerId;
                            var playerStat = await _context.PlayerStats.FirstOrDefaultAsync(ps => ps.PlayerID == actionData.PlayerId);
                            if (playerStat == null)
                            {
                                return Json(new { success = false, message = $"Player stat not found for playerId {playerId}." });
                            }

                            if (playerStat.RBI > 0)
                            {
                                playerStat.RBI -= 1;
                                _context.PlayerStats.Update(playerStat);
                                await _context.SaveChangesAsync();
                                await transaction.CommitAsync();
                                return Json(new { success = true });
                            }
                            else
                            {
                                return Json(new { success = false, message = "Invalid stat count; cannot decrement." });
                            }
                        }
                    case "SAC":
                        {
                            var playerId = actionData.PlayerId;
                            var playerStat = await _context.PlayerStats.FirstOrDefaultAsync(ps => ps.PlayerID == actionData.PlayerId);
                            if (playerStat == null)
                            {
                                return Json(new { success = false, message = $"Player stat not found for playerId {playerId}." });
                            }

                            if (playerStat.SAC > 0)
                            {
                                playerStat.SAC -= 1;
                                _context.PlayerStats.Update(playerStat);
                                await _context.SaveChangesAsync();
                                await transaction.CommitAsync();
                                return Json(new { success = true });
                            }
                            else
                            {
                                return Json(new { success = false, message = "Invalid stat count; cannot decrement." });
                            }
                        }
                    case "SB":
                        {
                            var playerId = actionData.PlayerId;
                            var playerStat = await _context.PlayerStats.FirstOrDefaultAsync(ps => ps.PlayerID == actionData.PlayerId);
                            if (playerStat == null)
                            {
                                return Json(new { success = false, message = $"Player stat not found for playerId {playerId}." });
                            }

                            if (playerStat.SB > 0)
                            {
                                playerStat.SB -= 1;
                                _context.PlayerStats.Update(playerStat);
                                await _context.SaveChangesAsync();
                                await transaction.CommitAsync();
                                return Json(new { success = true });
                            }
                            else
                            {
                                return Json(new { success = false, message = "Invalid stat count; cannot decrement." });
                            }
                        }
                    case "BB" :
                        {
                            var playerId = actionData.PlayerId;
                            var playerStat = await _context.PlayerStats.FirstOrDefaultAsync(ps => ps.PlayerID == actionData.PlayerId);
                            if (playerStat == null)
                            {
                                return Json(new { success = false, message = $"Player stat not found for playerId {playerId}." });
                            }

                            if (playerStat.BB > 0)
                            {
                                playerStat.BB -= 1;
                                _context.PlayerStats.Update(playerStat);
                                await _context.SaveChangesAsync();
                                await transaction.CommitAsync();
                                return Json(new { success = true });
                            }
                            else
                            {
                                return Json(new { success = false, message = "Invalid stat count; cannot decrement." });
                            }
                        }
                    case "PlayerPlaced":
                        {
                            var playerId = actionData.PlayerId;
                            var playerStat = await _context.PlayerStats.FirstOrDefaultAsync(ps => ps.PlayerID == actionData.PlayerId);
                            if (playerStat == null)
                            {
                                return Json(new { success = false, message = $"Player stat not found for playerId {playerId}." });
                            }

                            if (playerStat.PA > 0)
                            {
                                playerStat.PA -= 1;
                                _context.PlayerStats.Update(playerStat);
                                await _context.SaveChangesAsync();
                                await transaction.CommitAsync();
                                return Json(new { success = true });
                            }
                            else
                            {
                                return Json(new { success = false, message = "Invalid stat count; cannot decrement." });
                            }
                        }
                    case "GamePlayed":
                        {
                            var playerId = actionData.PlayerId;
                            var playerStat = await _context.PlayerStats.FirstOrDefaultAsync(ps => ps.PlayerID == actionData.PlayerId);
                            if (playerStat == null)
                            {
                                return Json(new { success = false, message = $"Player stat not found for playerId {playerId}." });
                            }

                            if (playerStat.GP > 0)
                            {
                                playerStat.GP -= 1;
                                _context.PlayerStats.Update(playerStat);
                                await _context.SaveChangesAsync();
                                await transaction.CommitAsync();
                                return Json(new { success = true });
                            }
                            else
                            {
                                return Json(new { success = false, message = "Invalid stat count; cannot decrement." });
                            }
                        }

                }

                return Json(new { success = false, message = "Unhandled action type." });
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                return Json(new { success = false, message = ex.Message });
            }
        }



        [HttpPost]
        public async Task<IActionResult> UndoAllActionsForGame(int gameId)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                // Fetch all actions for the specified game ordered by Timestamp descending
                var actionLogs = await _context.ActionLogs
                                                .Where(a => a.GameID == gameId) // Assuming you added GameId to ActionLogs
                                                .OrderByDescending(a => a.Timestamp)
                                                .ToListAsync();

                if (!actionLogs.Any())
                {
                    return Json(new { success = false, message = "No actions found for this game." });
                }

                foreach (var actionLog in actionLogs)
                {
                    var actionData = JsonConvert.DeserializeObject<PlayerActionDto>(actionLog.Data);
                    if (actionData == null)
                    {
                        continue;
                    }

                    var playerStat = await _context.PlayerStats.FirstOrDefaultAsync(ps => ps.PlayerID == actionData.PlayerId);
                    if (playerStat == null)
                    {
                        continue; 
                    }

                    switch (actionLog.ActionType)
                    {
                        case "Run":
                            if (playerStat.Runs > 0) playerStat.Runs -= 1;
                            break;
                        case "Single":
                            if (playerStat.B1 > 0) playerStat.B1 -= 1;
                            if (playerStat.Hits > 0) playerStat.Hits -= 1;
                            break;
                        case "Double":
                            if (playerStat.B2 > 0) playerStat.B2 -= 1;
                            if (playerStat.Hits > 0) playerStat.Hits -= 1;
                            break;
                        case "Triple":
                            if (playerStat.B3 > 0) playerStat.B3 -= 1;
                            if (playerStat.Hits > 0) playerStat.Hits -= 1;
                            break;
                        case "HomeRun":
                            if (playerStat.HR > 0) playerStat.HR -= 1;
                            if (playerStat.RBI > 0) playerStat.RBI -= 1;
                            if (playerStat.Runs > 0) playerStat.Runs -= 1;
                            if (playerStat.Hits > 0) playerStat.Hits -= 1;
                            break;
                        case "GO":
                            if (playerStat.GO > 0) playerStat.GO -= 1;
                            break;
                        case "FO":
                            if (playerStat.FO > 0) playerStat.FO -= 1;
                            break;
                        case "PO":
                            if (playerStat.PO > 0) playerStat.PO -= 1;
                            break;
                        case "RBI":
                            if (playerStat.RBI > 0) playerStat.RBI -= 1;
                            break;
                        case "SAC":
                            if (playerStat.SAC > 0) playerStat.SAC -= 1;
                            break;
                        case "SB":
                            if (playerStat.SB > 0) playerStat.SB -= 1;
                            break;
                        case "BB":
                            if (playerStat.BB > 0) playerStat.BB -= 1;
                            break;
                        case "PlayerPlaced":
                            if (playerStat.PA > 0) playerStat.PA -= 1;
                            break;
                        case "GamePlayed":
                            if (playerStat.GP > 0) playerStat.GP -= 1;
                            break;

                    }

                    _context.ActionLogs.Remove(actionLog);
                }

                await _context.SaveChangesAsync();
                await transaction.CommitAsync();
                return Json(new { success = true, message = "All actions for the game have been successfully undone." });
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                return Json(new { success = false, message = $"An error occurred while undoing actions: {ex.Message}" });
            }
        }


    }

}




    