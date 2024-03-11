using System;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using WMBA_7_2_.Models;
using Microsoft.EntityFrameworkCore;
using OfficeOpenXml;
using WMBA_7_2_.Data;
using OfficeOpenXml.Style;
using System.Drawing;
using OfficeOpenXml.Table;
using WMBA_7_2_.CustomControllers;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using WMBA_7_2_.ViewModels;
using System.Numerics;

namespace WMBA_7_2_.Controllers
{
    public class ReportController : LookupsController
    {
        private readonly WMBAContext _context;

        public ReportController(WMBAContext context)
        {
            _context = context;
        }

        // GET: Excel Data
        public async Task<IActionResult> Index()
        {
            return View();
        }
        public IActionResult Reports()
        {
            var report = _context.Reports.AsNoTracking();

            return View(report);
        }
        public IActionResult DownloadSampleExcel()
        {
            //Create a new spreadsheet from scratch.
            using (ExcelPackage excel = new ExcelPackage())
            {
                var workSheet = excel.Workbook.Worksheets.Add("Sample");

                //Set Style and backgound colour of headings
                using (ExcelRange headings = workSheet.Cells[1, 1, 1, 8])
                {
                    headings.Style.Font.Bold = true;
                    var fill = headings.Style.Fill;
                    fill.PatternType = ExcelFillStyle.Solid;
                    fill.BackgroundColor.SetColor(Color.LightBlue);
                }

                //Manually set width of columns
                //workSheet.Column(1).Width = 15;
                workSheet.Column(1).Width = 15;
                workSheet.Column(2).Width = 15;
                workSheet.Column(3).Width = 15;
                workSheet.Column(4).Width = 10;
                workSheet.Column(5).Width = 10;
                workSheet.Column(6).Width = 35;
                workSheet.Column(7).Width = 20;

                //Add headings and a title
               // workSheet.Cells[1, 1].Value = "ID";
                workSheet.Cells[1, 1].Value = "First Name";
                workSheet.Cells[1, 2].Value = "Last Name";
                workSheet.Cells[1, 3].Value = "Member ID";
                workSheet.Cells[1, 4].Value = "Season";
                workSheet.Cells[1, 5].Value = "Division";
                workSheet.Cells[1, 6].Value = "Club";
                workSheet.Cells[1, 7].Value = "Team";
                workSheet.Cells[1, 10].Value = "Welland Minor Baseball Association";

                using (ExcelRange Rng = workSheet.Cells[1, 10, 1, 18])
                {
                    Rng.Merge = true;
                    Rng.Style.Font.Bold = true;
                    Rng.Style.Font.Size = 18;
                    Rng.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                }

                try
                {
                    Byte[] theData = excel.GetAsByteArray();
                    string filename = "Sample_WMBA_Players.xlsx";
                    string mimeType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                    return File(theData, mimeType, filename);
                }
                catch (Exception)
                {
                    return BadRequest("Could not build and download the file.");
                }
            }
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
                        feedBack = "Error: File is not a CSV file or an Excel spreadsheet.";
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

            TempData["Feedback"] = feedBack;

            //Note that we are assuming that you are using the Preferred Approach to Lookup Values
            //And the custom LookupsController
            //return Redirect(ViewData["returnURL"].ToString());
            return RedirectToAction("Index", "Report");
        }


        private async Task ReadImportedData(ExcelWorksheet workSheet, string success)
        {
            //Prepare the colleciton of imported data
            List<ImportReport> imported = new();

            var start = workSheet.Dimension.Start;
            var end = workSheet.Dimension.End;
            //Test some of the heading cells to help confirm this is the right kind of file.
            if (workSheet.Cells[1, 1].Text == "ID" &&
                workSheet.Cells[1, 2].Text == "First Name" &&
                workSheet.Cells[1, 3].Text == "Last Name" &&
                workSheet.Cells[1, 4].Text == "Member ID" &&
                workSheet.Cells[1, 5].Text == "Season" &&
                workSheet.Cells[1, 6].Text == "Division" &&
                workSheet.Cells[1, 7].Text == "Club" &&
                workSheet.Cells[1, 8].Text == "Team")
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
                    //Add if not blank, at least check a couple of properties
                    if (!(ir.Team == "" || ir.Member_ID == ""))
                    {
                        imported.Add(ir);
                    }

                }
                //Great! you have read in the data so now do domething with it.
                //You probably want to check that the team exists or add it if it does not.
                //For the player you would again check if they are in the system, add them if required
                //and assign them to the team.
                //Find out from your client ifyou need to remove old team assignments.
                //
                //You will get a warning about using an await for this to be async but that
                //will come when you start saving data to the database.

                //Get lists of the unique values
                var uniqueDivisions = imported.GroupBy(d => d.Division).Select(d => d.First()).Select(d => d.Division);
                var uniqueDivisionTeams = imported.GroupBy(d => d.Team).Select(i => i.First()).Select(d => d.Team);
                var uniqueSeasons = imported.GroupBy(d => d.Season).Select(i => i.First()).Select(d => d.Season);
                var uniqueClubs = imported.GroupBy(d => d.Club).Select(i => i.First()).Select(d => d.Club);
                //For Team, we only want the name of the Team so we split the string to remove the Division
                //var uniqueTeams = uniqueDivisionTeams.Select(sub => sub.Split(' ')[1]).ToList();
                var uniqueTeams = uniqueDivisionTeams.Select(sub => sub.Substring(3).TrimStart()).ToList();
                //Now you can go on to check each of these to see if they are in the databse already and add 
                //them if they are not.  Then you should be ready to add the players and assign them to their
                //lookup values.

                foreach (var div in uniqueDivisions)
                {
                    // Check if division exists in the database
                    bool divisionExists = _context.Divisions.Any(d => d.DivAge == div);

                    if (!divisionExists)
                    {
                        var newDivision = new Division() { DivAge = div };
                        _context.Divisions.Add(newDivision);
                        _context.SaveChanges();
                    }
                }

                //foreach (var tn in uniqueDivisionTeams)
                //{
                //    // Check if team exists in the database
                //    bool teamExists = _context.Divisions.Any(d => d.DivisionTeams == tn);
                //    if (!teamExists)
                //    {
                //        var newTeam = new Division() { DivisionTeams = tn.Substring(3).TrimStart() , DivAge = tn.Remove(3).TrimEnd(), LeagueTypeID = 1 };
                //        _context.Divisions.Add(newTeam);
                //        _context.SaveChanges();
                //    }
                //}

                foreach (var team in uniqueTeams)
                {
                    bool teamExists = _context.Teams.Any(d => d.TeamName == team);
                    if (!teamExists)
                    {

                        var newTeam = new Team() { TeamName = team };
                        _context.Teams.Add(newTeam);
                        _context.SaveChanges();

                    }
                }

                foreach (var cl in uniqueClubs)
                {
                    bool teamExists = _context.Clubs.Any(t => t.ClubName == cl);

                    if (!teamExists)
                    {
                        var newClub = new Club() { ClubName = cl };
                        _context.Clubs.Add(newClub);
                        _context.SaveChanges();
                    }
                }

                foreach (var season in uniqueSeasons)
                {
                    bool seasonExists = _context.Clubs.Any(s => s.ClubName == season);

                    if (!seasonExists)
                    {
                        var newSeason = new Season() { Year = season };
                        _context.Seasons.Add(newSeason);
                        _context.SaveChanges();
                    }
                }

                //Add the Players
                for (int i = 0; i < imported.Count(); i++)
                {
                    Player player = new Player();
                    player.PlayerFirstName = imported[i].First_Name;
                    player.PlayerLastName = imported[i].Last_Name;
                    player.PlayerMemberID = imported[i].Member_ID;
                    player.TeamID = _context.Teams.FirstOrDefault(p => p.TeamName == imported[i].Team.Substring(3).TrimStart()).ID;
                    player.DivisionID = _context.Divisions.FirstOrDefault(p => p.DivAge == imported[i].Division).ID;
                    player.IsActive = true;
                    _context.Players.Add(player);
                }
                await _context.SaveChangesAsync();
                success = $"{imported.Count()} Players uploaded";
            }

            else
            {
                success = "Error: You may have selected the wrong file to upload.";
            }

            TempData["Success"] = success;
            //return RedirectToAction("Index", "Report");
        }
    }
} 






