using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WMBA_7_2_.Data;
using WMBA_7_2_.Models;

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

			ViewData["CoachID"] = new SelectList(_context.Coaches, "ID", "CoachName");

			return View();
        }

		public JsonResult GetTeams()
		{
			var teams = _context.Teams
				.Include(t => t.Players)
				.Include(t => t.TeamCoaches)
					.ThenInclude(tc => tc.Coach)
				.Select(t => new
				{
					id = t.ID,
					t.TeamName,
					Players = t.Players.Select(p => p.PlayerFullName),
					Coaches = t.TeamCoaches.Select(tc => new { tc.CoachID, tc.Coach.CoachName })
				})
				.ToList();

			return Json(teams);
		}

		public JsonResult Insert(Team model)
		{
			if (ModelState.IsValid)
			{
				_context.Teams
					.Add(model);
				_context.SaveChanges();

				return Json("Team saved successfully.");
			}
			return Json("Model validation failed.");
		}

		[HttpGet]
		public JsonResult Edit(int? id)
		{
			var teams = _context.Teams
								 .Include(p => p.Players)
								 .Include(p => p.TeamCoaches)
								 .FirstOrDefault(p => p.ID == id);

			if (teams == null)
			{
				return Json(new { error = "Team not found." });
			}

			var result = new
			{
				id = teams.ID,
				teamName = teams.TeamName
			};

			return Json(result);
		}

		[HttpPost]
		public JsonResult Update(Team model)
		{
			if (ModelState.IsValid)
			{
				_context.Teams.Update(model);
				_context.SaveChanges();

				return Json("Team updated successfully.");
			}
			return Json("Model validation failed.");
		}

		[HttpPost]
		public JsonResult Delete(int id)
		{
			var team = _context.Teams.Find(id);

			if (team != null)
			{
				_context.Teams.Remove(team);
				_context.SaveChanges();
				return Json("Team deleted successfully.");
			}
			return Json("Team not found.");
		}

		[HttpGet]
		public async Task<IActionResult> Details(int id)
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

	}
}
