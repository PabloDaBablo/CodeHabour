using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WMBA_7_2_.Data;
using WMBA_7_2_.Models;
using WMBA_7_2_.ViewModels;

namespace WMBA_7_2_.Controllers
{
	public class CoachModalController : Controller
	{

		private readonly WMBAContext _context;

		public CoachModalController(WMBAContext context)
		{
			_context = context;
		}

		public IActionResult Index()
		{
			var teams = _context.Teams.ToList(); 
			ViewData["TeamCoaches"] = new SelectList(teams, "ID", "TeamName");
			return View();
		}


		[HttpGet]
		public JsonResult GetCoaches()
		{
			var coaches = _context.Coaches
				.Include(c => c.TeamCoach)
					.ThenInclude(tc => tc.Team)
				.Select(c => new
				{
					id = c.ID,
					CoachMemberID = c.CoachMemberID,
					CoachName = c.CoachName,
					CoachNumber = c.CoachNumber,
					CoachPosition = c.CoachPosition,
					Teams = c.TeamCoach.Select(tc => new { tc.TeamID, tc.Team.TeamName }).ToList()
				})
				.ToList();

			return Json(coaches);
		}

		[HttpPost]
		public JsonResult Insert([FromBody]CoachViewModel viewModel)
		{
			if (ModelState.IsValid)
			{
				var coach = new Coach
				{
					ID = viewModel.ID,
					CoachMemberID = viewModel.CoachMemberID,
					CoachName = viewModel.CoachName,
					CoachNumber = viewModel.CoachNumber,
					CoachPosition = viewModel.CoachPosition,
				};

				if (viewModel.ID > 0)
				{
					var existingAssociations = _context.Team_Coaches.Where(tc => tc.CoachID == viewModel.ID);
					_context.Team_Coaches.RemoveRange(existingAssociations);
				}

				foreach (var teamId in viewModel.SelectedTeamIds)
				{
					coach.TeamCoach.Add(new Team_Coach { CoachID = coach.ID, TeamID = teamId });
				}

				if (viewModel.ID == 0)
				{
					_context.Coaches.Add(coach);
				}
				else
				{
					_context.Entry(coach).State = EntityState.Modified;
				}

				_context.SaveChanges();

				return Json("Coach saved successfully.");
			}

			return Json("Model validation failed.");
		}


		[HttpGet]
		public JsonResult Edit(int? id)
		{
			var coaches = _context.Coaches
								 .Include(p => p.TeamCoach)
								 .FirstOrDefault(p => p.ID == id);

			if (coaches == null)
			{
				return Json(new { error = "Coach not found." });
			}

			var result = new
			{
				id = coaches.ID,
				coachMemberID = coaches.CoachMemberID,
				coachName = coaches.CoachName,
				coachPosition = coaches.CoachPosition

			};

			return Json(result);
		}

	}
}
