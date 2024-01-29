using System;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using WMBA_7_2_.Models;

namespace WMBA_7_2_.Controllers
{
    public class InsertController : Controller
    {
        private readonly ILogger<InsertController> Logger;

        public InsertController(ILogger<InsertController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }


    }
}

	
