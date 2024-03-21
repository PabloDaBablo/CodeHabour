using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using WMBA_7_2_.Data;
using WMBA_7_2_.Models;
using WMBA_7_2_.ViewModels;
using static WMBA_7_2_.ViewModels.TeamVM;

namespace WMBA_7_2_.Controllers
{
    public class TeamModalController : Controller
    {

        private readonly WMBAContext _context;

        public TeamModalController(WMBAContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {

			ViewData["Coaches"] = new SelectList(_context.Coaches, "ID", "CoachName");
			ViewData["DivisionID"] = new SelectList(_context.Divisions, "ID", "DivAge");
			ViewData["Players"] = new SelectList(_context.Players, "ID", "PlayerFullName");

			return View();
        }

		public JsonResult GetTeams()
		{
			try
			{
				var teams = _context.Teams
					.Include(t => t.Players)
					.Include(t => t.TeamCoaches)
						.ThenInclude(tc => tc.Coach)
					.Include(t => t.Division)
					.Select(t => new
					{
						id = t.ID,
						t.TeamName,
						Division = t.Division.DivAge,
						Players = t.Players.Select(p => p.PlayerFullName),
						Coaches = t.TeamCoaches.Select(tc => new { tc.CoachID, tc.Coach.CoachName })
					})
					.ToList();

				return Json(teams);
			}
			catch (Exception ex)
			{
				return Json(new { error = "An error occurred while processing your request. Please try again later." });
			}
		}

		[HttpPost]
		public async Task<IActionResult> Insert([FromBody] TeamVM model)
		{
			if (!ModelState.IsValid)
			{
				return Json("Error! Ensure all required fields are filled out!");
			}

			var team = new Team
			{
				TeamName = model.TeamName,
				DivisionID = model.DivisionID
			};

			
			_context.Teams.Add(team);
			await _context.SaveChangesAsync();

			
			foreach (var playerId in model.Players)
			{
				var player = await _context.Players.FindAsync(playerId);
				if (player != null)
				{
					player.TeamID = team.ID; 
				}
			}

			
			foreach (var coachId in model.Coaches)
			{
				var teamCoach = new Team_Coach { CoachID = coachId, TeamID = team.ID };
				_context.Team_Coaches.Add(teamCoach);
			}

			await _context.SaveChangesAsync();

			return Json("Team saved successfully.");
		}


		[HttpGet]
		public JsonResult Edit(int? id)
		{
			try
			{
				var team = _context.Teams
					.Include(t => t.Players)
					.Include(t => t.TeamCoaches)
					.ThenInclude(tc => tc.Coach)
					.Include(t => t.Division)
					.FirstOrDefault(t => t.ID == id);

				if (team == null)
				{
					return Json(new { error = "Team not found by given ID. If this issue persists, please see your system administrator." });
				}

				var result = new
				{
					id = team.ID,
					teamName = team.TeamName,
					divisionID = team.DivisionID,
					players = team.Players.Select(p => p.ID).ToList(),
					coaches = team.TeamCoaches.Select(tc => tc.CoachID).ToList()
				};

				return Json(result);
			}
			catch (Exception ex)
			{
				return Json(new { error = "An error occurred while processing your request. If this issue persists, please see your system administrator" });
			}
		}

		[HttpPost]
		public async Task<IActionResult> Update([FromBody] TeamVM model)
		{
			if (!ModelState.IsValid)
			{
				return Json("Error! Ensure all required fields are filled out!");
			}

			var team = await _context.Teams
				.Include(t => t.TeamCoaches)
				.Include(t => t.Players)
				.FirstOrDefaultAsync(t => t.ID == model.ID);

			team.TeamName = model.TeamName;
			team.DivisionID = model.DivisionID;

			
			foreach (var player in team.Players.ToList())
			{
				if (!model.Players.Contains(player.ID))
				{
					player.TeamID = 1; 
				}
			}
			foreach (var playerId in model.Players)
			{
				var player = await _context.Players.FindAsync(playerId);
				if (player != null && player.TeamID != team.ID)
				{
					player.TeamID = team.ID;
				}
			}

			
			_context.Team_Coaches.RemoveRange(team.TeamCoaches);
			foreach (var coachId in model.Coaches)
			{
				var teamCoach = new Team_Coach { CoachID = coachId, TeamID = team.ID };
				_context.Team_Coaches.Add(teamCoach);
			}


			try
			{
				await _context.SaveChangesAsync();
				return Json("Team updated successfully.");
			}
			catch (Exception ex)
			{
				
				return Json(new { error = "An error occurred while updating the team. If this issue persists, please see your system administrator" });
			}
		}

		[HttpPost]
		public JsonResult Delete(int id)
		{
			try
			{
				var team = _context.Teams.Find(id);

				if (team != null)
				{
					_context.Teams.Remove(team);
					_context.SaveChanges();
					return Json("Team deleted successfully.");
				}
				return Json("Team not found by ID.");
			}
			catch (Exception ex)
			{
				return Json(new { error = "An error occurred while processing your request." });
			}
		}

		[HttpGet]
		public async Task<IActionResult> Details(int id)
		{
			try
			{
				var team = await _context.Teams
										 .Include(t => t.Players)
										 .Include(t => t.TeamCoaches)
											 .ThenInclude(tc => tc.Coach)
										 .FirstOrDefaultAsync(t => t.ID == id);

				if (team == null)
				{
					return NotFound();
				}

				var result = new
				{
					teamName = team.TeamName,
					players = team.Players.Select(p => p.PlayerFirstName + " " + p.PlayerLastName),
					coaches = team.TeamCoaches.Select(tc => tc.Coach.CoachName)
				};

				return Json(result);
			}
			catch (Exception ex)
			{
				return Json(new { error = "An error occurred while processing your request." });
			}
		}

	}
}
