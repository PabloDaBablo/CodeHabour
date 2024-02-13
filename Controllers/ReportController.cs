using System;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using WMBA_7_2_.Models;
using Microsoft.EntityFrameworkCore;
using OfficeOpenXml;
using WMBA_7_2_.Data;
using OfficeOpenXml.Style;
using System.Drawing;
using WMBA_7_2_.ViewModels;
using OfficeOpenXml.Table;

namespace WMBA_7_2_.Controllers
{
    public class ReportController : Controller
    {
        private readonly WMBAContext _context;

        // GET: Excel Data
        public async Task<IActionResult> Index()
        {
            return View();
        }

        public IActionResult DownloadGames()
        {
            //Get the appointments
            var appts = from tg in _context.Team_Games
                        .Include(tg => tg.Game)
                        select new
                        {

                        };
            //How many rows?
            int numRows = appts.Count();

            if (numRows > 0) //We have data
            {
                //Create a new spreadsheet from scratch.
                using (ExcelPackage excel = new ExcelPackage())
                {

                    //Note: you can also pull a spreadsheet out of the database if you
                    //have saved it in the normal way we do, as a Byte Array in a Model
                    //such as the UploadedFile class.
                    //
                    // Suppose...
                    //
                    // var theSpreadsheet = _context.UploadedFiles.Include(f => f.FileContent).Where(f => f.ID == id).SingleOrDefault();
                    //
                    //    //Pass the Byte[] FileContent to a MemoryStream
                    //
                    // using (MemoryStream memStream = new MemoryStream(theSpreadsheet.FileContent.Content))
                    // {
                    //     ExcelPackage package = new ExcelPackage(memStream);
                    // }

                    var workSheet = excel.Workbook.Worksheets.Add("Games");

                    //Note: Cells[row, column]
                    workSheet.Cells[3, 1].LoadFromCollection(appts, true);

                    //Style first column for dates
                    workSheet.Column(1).Style.Numberformat.Format = "yyyy-mm-dd";


                    //Note: You can define a BLOCK of cells: Cells[startRow, startColumn, endRow, endColumn]
                    //Make Date and Patient Bold
                    workSheet.Cells[4, 1, numRows + 3, 2].Style.Font.Bold = true;


                    //Set Style and backgound colour of headings
                    using (ExcelRange headings = workSheet.Cells[3, 1, 3, 7])
                    {
                        headings.Style.Font.Bold = true;
                        var fill = headings.Style.Fill;
                        fill.PatternType = ExcelFillStyle.Solid;
                        fill.BackgroundColor.SetColor(Color.LightBlue);
                    }


                    //Autofit columns
                    workSheet.Cells.AutoFitColumns();
                    //Note: You can manually set width of columns as well
                    //workSheet.Column(7).Width = 10;

                    //Add a title and timestamp at the top of the report
                    workSheet.Cells[1, 1].Value = "WMBA Games";
                    using (ExcelRange Rng = workSheet.Cells[1, 1, 1, 6])
                    {
                        Rng.Merge = true; //Merge columns start and end range
                        Rng.Style.Font.Bold = true; //Font should be bold
                        Rng.Style.Font.Size = 18;
                        Rng.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    }
                    //Since the time zone where the server is running can be different, adjust to 
                    //Local for us.
                    DateTime utcDate = DateTime.UtcNow;
                    TimeZoneInfo esTimeZone = TimeZoneInfo.FindSystemTimeZoneById("Eastern Standard Time");
                    DateTime localDate = TimeZoneInfo.ConvertTimeFromUtc(utcDate, esTimeZone);
                    using (ExcelRange Rng = workSheet.Cells[2, 6])
                    {
                        Rng.Value = "Created: " + localDate.ToShortTimeString() + " on " +
                            localDate.ToShortDateString();
                        Rng.Style.Font.Bold = true; //Font should be bold
                        Rng.Style.Font.Size = 12;
                        Rng.Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                    }

                    //Ok, time to download the Excel

                    try
                    {
                        Byte[] theData = excel.GetAsByteArray();
                        string filename = "WMBA_Games.xlsx";
                        string mimeType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                        return File(theData, mimeType, filename);
                    }
                    catch (Exception)
                    {
                        return BadRequest("Could not build and download the file.");
                    }
                }
            }
            return NotFound("No data.");
        }
        [HttpPost]
        public async Task<IActionResult> ImportFromExcel(IFormFile theExcel)
        {
            string feedBack = string.Empty;
            if (theExcel != null)
            {
                string mimeType = theExcel.ContentType;
                long fileLength = theExcel.Length;
                if (!(mimeType == "" || fileLength == 0))//Looks like we have a file!!!
                {
                    if (mimeType.Contains("excel") || mimeType.Contains("spreadsheet"))
                    {
                        ExcelPackage excel;
                        using (var memoryStream = new MemoryStream())
                        {
                            await theExcel.CopyToAsync(memoryStream);
                            excel = new ExcelPackage(memoryStream);
                        }
                        var workSheet = excel.Workbook.Worksheets[0];

                        await ReadImportedData(workSheet, feedBack);
                    }
                    else if (mimeType.Contains("text/csv"))
                    {
                        var format = new ExcelTextFormat();
                        format.Delimiter = ',';
                        bool firstRowIsHeader = true;

                        using var reader = new System.IO.StreamReader(theExcel.OpenReadStream());

                        using ExcelPackage package = new ExcelPackage();
                        var result = reader.ReadToEnd();
                        ExcelWorksheet workSheet = package.Workbook.Worksheets.Add("Imported Report Data");

                        workSheet.Cells["A1"].LoadFromText(result, format, TableStyles.None, firstRowIsHeader);

                        await ReadImportedData(workSheet, feedBack);
                    }
                    else
                    {
                        feedBack = "Error: That file is not an Excel spreadsheet.";
                    }
                }
                else
                {
                    feedBack = "Error:  file appears to be empty";
                }
            }
            else
            {
                feedBack = "Error: No file uploaded";
            }

            TempData["Feedback"] = feedBack + "<br /><br />";

            //Note that we are assuming that you are using the Preferred Approach to Lookup Values
            //And the custom LookupsController
            return Redirect(ViewData["returnURL"].ToString());
        }


        private async Task ReadImportedData(ExcelWorksheet workSheet, string feedBack)
        {
            //Prepare the colleciton of imported data
            List<ImportReport> imported = new();

            var start = workSheet.Dimension.Start;
            var end = workSheet.Dimension.End;

            //Test some of the heading cells to help confirm this is the right kind of file.
            if (workSheet.Cells[1, 2].Text == "First Name" &&
                workSheet.Cells[1, 3].Text == "Last Name" &&
                workSheet.Cells[1, 4].Text == "Member ID" &&
                workSheet.Cells[1, 6].Text == "Division" &&
                workSheet.Cells[1, 8].Text == "Team"
                )
            {
                for (int row = start.Row + 1; row <= end.Row; row++)
                {
                    ImportReport ir = new ImportReport
                    {
                        ID = workSheet.Cells[row, 1].Text,
                        First_Name = workSheet.Cells[row, 2].Text,
                        Last_Name = workSheet.Cells[row, 3].Text,
                        Member_ID = workSheet.Cells[row, 4].Text,
                        Season = workSheet.Cells[row, 5].Text,
                        Division = workSheet.Cells[row, 6].Text,
                        Club = workSheet.Cells[row, 7].Text,
                        Team = workSheet.Cells[row, 8].Text
                    };
                    imported.Add(ir);
                }
                //Great! you have read in the data so now do domething with it.
                //You probably want to check that the team exists or add it if it does not.
                //For the player you would again check if they are in the system, add them if required
                //and assign them to the team.
                //Find out from your client ifyou need to remove old team assignments.
                //
                //You will get a warning about using an await for this to be async but that
                //will come when you start saving data to the database.

                }
                else
                {
                    feedBack = "Error: You may have selected the wrong file to upload.";
                }
                return;
            }
        }

        
    


	
