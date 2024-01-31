using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using OfficeOpenXml.FormulaParsing.Excel.Functions.Math;
using WMBA_7_2_.Data;
using WMBA_7_2_.Models;

namespace WMBA_7_2_.Controllers
{
    public class PlayerModalController : Controller
    {

        private readonly WMBAContext _context;

        public PlayerModalController(WMBAContext context)
        {
            _context = context;
        }   

        public IActionResult Index()
        {

			ViewData["TeamID"] = new SelectList(_context.Teams, "ID", "TeamName");
			return View();
        }

        [HttpGet]
        public JsonResult GetPlayers()
        {
            var players = _context.Players
				.Include(p => p.Team).Select(p => new
            {
                id = p.ID,
                p.PlayerMemberID,
                p.PlayerFirstName,
                p.PlayerLastName,
                p.PlayerNumber,
                p.Team.TeamName

            })
            .ToList();
                
            return Json(players);
        }

        [HttpPost]
        public JsonResult Insert(Player model)
        {
            if (ModelState.IsValid)
            {
                _context.Players
                    .Add(model);
                _context.SaveChanges();


				return Json("Player saved successfully.");
            }
            return Json("Model validation failed.");
        }

		[HttpGet]
		public JsonResult Edit(int? id)
		{
			var player = _context.Players
								 .Include(p => p.Team)
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
				teamID = player.Team?.ID 
			};

			return Json(result);
		}

		[HttpPost]
		public JsonResult Update(Player model)
		{
            if (ModelState.IsValid)
            {
                _context.Players.Update(model);
                _context.SaveChanges();

                return Json("Player updated successfully."); 
            }
            return Json("Model validation failed.");
        }

        [HttpPost]
        public JsonResult Delete(int id)
        {
            var player = _context.Players.Find(id);

            if(player != null)
            {
                _context.Players.Remove(player);
                _context.SaveChanges();
                return Json("Player deleted successfully.");
			}
            return Json("Player not found.");
        }
	}
}
