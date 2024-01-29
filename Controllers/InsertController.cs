using System;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using WMBA_7_2_.Models;
using Microsoft.EntityFrameworkCore;

namespace WMBA_7_2_.Controllers
{
//    public class InsertController : Controller
//    {
//        [HttpPost]
//        public async Task<IActionResult> InsertFromExcel(IFormFile theExcel)
//        {
//            //Note: This is a very basic example and has 
//            //no ERROR HANDLING.  It also assumes that
//            //duplicate values are allowed, both in the 
//            //uploaded data and the DbSet.
//            ExcelPackage excel;
//            using (var memoryStream = new MemoryStream())
//            {
//                await theExcel.CopyToAsync(memoryStream);
//                excel = new ExcelPackage(memoryStream);
//            }
//            var workSheet = excel.Workbook.Worksheets[0];
//            var start = workSheet.Dimension.Start;
//            var end = workSheet.Dimension.End;

//            //Start a new list to hold imported objects
//            List<AppointmentReason> appointmentReasons = new List<AppointmentReason>();

//            for (int row = start.Row; row <= end.Row; row++)
//            {
//                // Row by row...
//                AppointmentReason a = new AppointmentReason
//                {
//                    ReasonName = workSheet.Cells[row, 1].Text
//                };
//                appointmentReasons.Add(a);
//            }
//            _context.AppointmentReasons.AddRange(appointmentReasons);
//            _context.SaveChanges();
//            //Note that we are assuming that you are using the Preferred Approach to Lookup Values
//            //And the custom LookupsController
//            return Redirect(ViewData["returnURL"].ToString());
//        }
//    }
}

	
