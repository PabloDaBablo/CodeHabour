using WMBA_7_2_.CustomControllers;
using WMBA_7_2_.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Reflection;

namespace WMBA_7_2_.Controllers
{
    
    public class LookupController : CognizantController
    {
        private readonly WMBAContext _context;

        public LookupController(WMBAContext context)
        {
            _context = context;
        }

        [Authorize(Roles = "Admin")]
        public IActionResult Index(string Tab = "Specialty-Tab")
        {
            
            ViewData["Tab"] = Tab;
            return View();
        }


        [Authorize(Roles = "Admin")]
        public PartialViewResult Upload()
        {
            ViewData["Excel/CSV Upload"] = new
                SelectList(_context.Reports);
            return PartialView("_Upload");
        }
       

    }
}
