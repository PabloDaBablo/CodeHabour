using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using OfficeOpenXml.FormulaParsing.Excel.Functions.Math;
using WMBA_7_2_.CustomControllers;
using WMBA_7_2_.Data;
using WMBA_7_2_.Models;
using WMBA_7_2_.ViewModels;

namespace WMBA_7_2_.Controllers
{
    public class PlayerModalController : CognizantController
    {

        private readonly WMBAContext _context;

        public PlayerModalController(WMBAContext context)
        {
            _context = context;
        }

		[Authorize(Roles = "Admin, Convenor, Coaches, Scorekeeper")]
        public IActionResult Index()
        {

			ViewData["DivisionID"] = new SelectList(_context.Divisions, "ID", "DivAge");
			ViewData["TeamID"] = new SelectList(_context.Teams, "ID", "TeamName");
			return View();
        }

		[HttpGet]
        [Authorize(Roles = "Admin, Convenor, Coaches, Scorekeeper")]
        public JsonResult GetPlayers(int page = 1, int pageSize = 10, string sortColumn = "divAge", string sortDirection = "asc",
							 int? divisionId = null, string firstNameSearch = "", string lastNameSearch = "",
							 string numberSearch = "", int? teamId = null)
		{
			try
			{
				var query = _context.Players
					.Include(p => p.Team)
					.Include(p => p.Division)
					.Include(p => p.GamePlayers)
					.Include(p => p.PlayerStats)
					.AsQueryable();

				if (divisionId.HasValue)
				{
					query = query.Where(p => p.DivisionID == divisionId.Value);
				}

				if (!string.IsNullOrEmpty(firstNameSearch))
				{
					query = query.Where(p => p.PlayerFirstName.ToUpper().Contains(firstNameSearch.ToUpper()));
				}

				if (!string.IsNullOrEmpty(lastNameSearch))
				{
					query = query.Where(p => p.PlayerLastName.ToUpper().Contains(lastNameSearch.ToUpper()));
				}

				//if (!string.IsNullOrEmpty(numberSearch) && int.TryParse(numberSearch, out int number))
				//{
				//	query = query.Where(p => p.PlayerNumber == number);
				//}

				if (teamId.HasValue)
				{
					query = query.Where(p => p.TeamID == teamId.Value);
				}

				if (sortColumn == "teamName")
				{
					query = sortDirection == "asc" ? query.OrderBy(p => p.Team.TeamName) : query.OrderByDescending(p => p.Team.TeamName);
				}
				else if (sortColumn == "divAge")
				{
					query = sortDirection == "asc" ? query.OrderBy(p => p.Division.DivAge) : query.OrderByDescending(p => p.Division.DivAge);
				}
				

				var totalRecords = query.Count();
				var totalPages = (int)Math.Ceiling((double)totalRecords / pageSize);

				var players = query
					.Skip((page - 1) * pageSize)
					.Take(pageSize)
					.Select(p => new
					{
						id = p.ID,
						p.PlayerMemberID,
						p.PlayerFirstName,
						p.PlayerLastName,
                        PlayerNumber = p.PlayerNumber ?? 0,
                        TeamName = p.Team.TeamName,
						p.DivisionID,
						DivAge = p.Division.DivAge,
						p.IsActive
					})
				.ToList();

				return Json(new { players, totalPages });
			}
			catch (Exception ex)
			{
				return Json(new { error = "An error occurred while processing your request." });
			}
		}

		[HttpPost]
        [Authorize(Roles = "Admin, Convenor, Coaches, Scorekeeper")]
        public JsonResult Insert(Player model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _context.Players
                        .Add(model);
                    _context.SaveChanges();


                    return Json("Player saved successfully.");
                }
                return Json("Error! Ensure all required fields are filled out!");
            }
			catch (DbUpdateException dex)
			{
				if(dex.InnerException.Message.Contains("UNIQUE"))
				{
					return Json(new { error = "Player Member ID already exists in the database." });
				}
				else
				{
					return Json(new { error = "An error occurred while processing your request." });
				}
			}
			catch (Exception ex)
            {
				return Json(new { error = "An error occurred while processing your request." });
			}
			
        }

		[HttpGet]
        [Authorize(Roles = "Admin, Convenor, Coaches, Scorekeeper")]
        public JsonResult Edit(int? id)
		{
            try
            {
                var player = _context.Players
                                     .Include(p => p.Team)
                                     .Include(p => p.Division)
                                     .FirstOrDefault(p => p.ID == id);

                if (player == null)
                {
                    return Json(new { error = "Player not found." });
                }

                var result = new
                {
                    id = player.ID,
                    playerMemberID = player.PlayerMemberID,
                    playerFirstName = player.PlayerFirstName,
                    playerLastName = player.PlayerLastName,
                    playerNumber = player.PlayerNumber,
                    teamID = player.Team?.ID,
                    divisionID = player.Division?.ID,
                    divAge = player.Division?.DivAge
                };

                return Json(result);
            }
            catch (Exception ex)
            {
            return Json(new { error = "An error occurred while processing your request. Please try again later or contact your system administrator" });
            }
		}

		[HttpPost]
        [Authorize(Roles = "Admin, Convenor, Coaches, Scorekeeper")]
        public JsonResult Update(Player model)
		{
            try
            {

                if (ModelState.IsValid)
                {
                    _context.Players.Update(model);
                    _context.SaveChanges();

                    return Json("Player updated successfully.");
                }
                return Json("Error! Ensure all required fields are filled out!");
            }
			catch (Exception ex)
            {
				return Json(new { error = "An error occurred while processing your request." });
			}
        }

        [HttpPost]
        [Authorize(Roles = "Admin, Convenor, Coaches, Scorekeeper")]
        public JsonResult Delete(int id)
        {
            try
            {
                var player = _context.Players.Find(id);

                if (player != null)
                {
                    _context.Players.Remove(player);
                    _context.SaveChanges();
                    return Json("Player deleted successfully.");
                }
                return Json("Player not found by ID. If this issue persists, please see your administrator.");
            }
			catch (Exception ex)
            {
				return Json(new { error = "An error occurred while processing your request." });
			}
        }

		public class PlayerStatusUpdateModel
		{
			public int Id { get; set; }
		}

		[HttpPost]
        [Authorize(Roles = "Admin, Convenor, Coaches, Scorekeeper")]
        public IActionResult ToggleStatus([FromBody] PlayerStatusUpdateModel model)
		{
			var player = _context.Players.FirstOrDefault(p => p.ID == model.Id);
			if (player != null)
			{
				player.IsActive = !player.IsActive;
				_context.SaveChanges();
				return Json(new { success = true, isActive = player.IsActive });
			}
			return Json(new { success = false });
		}

        [Authorize(Roles = "Admin, Convenor, Coaches, Scorekeeper")]
        public async Task<IActionResult> PlayerDetails(int playerId)
		{
			var player = await _context.Players
										.Include(p => p.PlayerStats)
										.FirstOrDefaultAsync(p => p.ID == playerId);

			if (player == null)
			{
				return NotFound();
			}

			return View(player);
		}


	}
}
