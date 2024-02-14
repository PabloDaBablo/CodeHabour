using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using WMBA_7_2_.CustomControllers;
using WMBA_7_2_.Data;

namespace WMBA_7_2_.Controllers
{
	public class ManageLookupController : CognizantController
	{

		private readonly WMBAContext _context;

		public ManageLookupController(WMBAContext context)
		{
			_context = context;
		}

		public IActionResult Index(string tab = "Division-Tab")
		{

			ViewData["Tab"] = tab;
			return View();
		}

		public PartialViewResult Division()
		{
			ViewData["DivisionID"] = new
				SelectList(_context.Divisions
				.OrderBy(a => a.DivisionTeams), "ID", "DivisionTeams");
			return PartialView("_Division");
		}
	}
}
