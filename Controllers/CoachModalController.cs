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
			var teams = _context.Teams
				.ToList(); 
			

			ViewData["TeamCoaches"] = new SelectList(teams, "ID", "TeamName");

			ViewData["DivisionID"] = new SelectList(_context.Divisions, "ID", "DivAge");

			return View();
		}


		[HttpGet]
		public JsonResult GetCoaches()
		{
			var coaches = _context.Coaches
				.Include(c => c.TeamCoach)
					.ThenInclude(tc => tc.Team)
				.Include(c => c.Division)
				.Select(c => new
				{
					id = c.ID,
					CoachMemberID = c.CoachMemberID,
					CoachName = c.CoachName,
					CoachNumber = c.CoachNumber,
					Teams = c.TeamCoach.Select(tc => new { tc.TeamID, tc.Team.TeamName }).ToList(),
					division = c.Division.DivAge
				})
				.ToList();

			return Json(coaches);
		}

		[HttpPost]
		public JsonResult Insert([FromBody]CoachViewModel viewModel)
		{
			try
			{
				if (ModelState.IsValid)
				{
					var coach = new Coach
					{
						ID = viewModel.ID,
						CoachMemberID = viewModel.CoachMemberID,
						CoachName = viewModel.CoachName,
						CoachNumber = viewModel.CoachNumber,
						DivisionID = viewModel.DivisionID
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

				return Json("Model validation failed. Ensure all required fields are filled out first!");
			}
			catch (Exception ex)
			{
				return Json(new { error = "An error occurred while processing your request." });
			}
		}


		[HttpGet]
		public JsonResult Edit(int? id)
		{
			var coaches = _context.Coaches
								 .Include(p => p.TeamCoach)
									.ThenInclude(tc => tc.Team) 
									.Include(p => p.Division)
								 .FirstOrDefault(p => p.ID == id);

			if (coaches == null)
			{
				return Json(new { error = "Coach not found." });
			}

			
			var teams = coaches.TeamCoach.Select(tc => new { tc.TeamID, tc.Team.TeamName }).ToList();

			var result = new
			{
				id = coaches.ID,
				coachMemberID = coaches.CoachMemberID,
				coachName = coaches.CoachName,
				coachNumber = coaches.CoachNumber,
				teams = teams, 
				division = coaches.DivisionID
			};

			return Json(result);
		}

		[HttpPost]
		public JsonResult Update([FromBody] CoachViewModel viewModel)
		{
			if (ModelState.IsValid)
			{
				var coachToUpdate = _context.Coaches
					.Include(c => c.TeamCoach)
					.Include(c => c.Division)
					.FirstOrDefault(c => c.ID == viewModel.ID);

				if (coachToUpdate == null)
				{
					return Json("Coach not found.");
				}

				coachToUpdate.CoachMemberID = viewModel.CoachMemberID;
				coachToUpdate.CoachName = viewModel.CoachName;
				coachToUpdate.CoachNumber = viewModel.CoachNumber;
				coachToUpdate.DivisionID = viewModel.DivisionID;


				var existingTeamIds = coachToUpdate.TeamCoach.Select(tc => tc.TeamID).ToList();
				var newTeamIds = viewModel.SelectedTeamIds.Except(existingTeamIds).ToList();
				var removedTeamIds = existingTeamIds.Except(viewModel.SelectedTeamIds).ToList();

				foreach (var newTeamId in newTeamIds)
				{
					coachToUpdate.TeamCoach.Add(new Team_Coach { TeamID = newTeamId });
				}

				foreach (var removedTeamId in removedTeamIds)
				{
					var toRemove = coachToUpdate.TeamCoach.FirstOrDefault(tc => tc.TeamID == removedTeamId);
					if (toRemove != null)
					{
						coachToUpdate.TeamCoach.Remove(toRemove);
					}
				}

				_context.SaveChanges();
				return Json("Coach updated successfully.");
			}

			return Json("Model validation failed. Ensure all required fields are filled out!");
		}
		/*public JsonResult Delete(int id)
		{
			var coaches = _context.Coaches.Find(id);

			if (coaches != null)
			{
				_context.Coaches.Remove(coaches);
				_context.SaveChanges();
				return Json("Coach deleted successfully.");
			}
			return Json("Coach not found.");
		}

		*/ //bugged at the moment, not sure if teachers
		   // want a delete option for coaches since they dont want one for players

		

	}
}
