﻿using System;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using WMBA_7_2_.Models;
using Microsoft.EntityFrameworkCore;
using OfficeOpenXml;
using WMBA_7_2_.Data;

namespace WMBA_7_2_.Controllers
{
    public class InsertController : Controller
    {
        private readonly WMBAContext _context;

        // GET: Excel Data
        public async Task<IActionResult> Index()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> InsertFromExcel(IFormFile theExcel)
        {
             
        //Note: This is a very basic example and has 
        //no ERROR HANDLING.  It also assumes that
        //duplicate values are allowed, both in the 
        //uploaded data and the DbSet.
        ExcelPackage excel;
            using (var memoryStream = new MemoryStream())
            {
                await theExcel.CopyToAsync(memoryStream);
                excel = new ExcelPackage(memoryStream);
            }
            var workSheet = excel.Workbook.Worksheets[0];
            var start = workSheet.Dimension.Start;
            var end = workSheet.Dimension.End;

            //Start a new list to hold imported objects
            List<Team> teams = new List<Team>();

            for (int row = start.Row; row <= end.Row; row++)
            {
                  // Row by row...
                Team a = new Team
                {
                    TeamName = workSheet.Cells[row, 7].Text
                };
                teams.Add(a);
            }
            _context.Teams.AddRange(teams);
            _context.SaveChanges();
            //Note that we are assuming that you are using the Preferred Approach to Lookup Values
            //And the custom LookupsController
            return Redirect(ViewData["returnURL"].ToString());
        }
    }
}

	
