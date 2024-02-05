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
                            HomeTeam = tg.HomeTeam,
                            AwayTeam = tg.AwayTeam
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
        public async Task<IActionResult> InsertFromExcel(IFormFile theExcel)
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
                        var start = workSheet.Dimension.Start;
                        var end = workSheet.Dimension.End;
                        int successCount = 0;
                        int errorCount = 0;
                        if (workSheet.Cells[1, 2].Text == "First Name" &&
                            workSheet.Cells[1, 3].Text == "Last Name" &&
                            workSheet.Cells[1, 4].Text == "Member ID" &&
                            workSheet.Cells[1, 6].Text == "Division" &&
                            workSheet.Cells[1, 8].Text == "Team")

                        {
                            for (int row = start.Row + 1; row <= end.Row; row++)
                            {

                                ImportReportVM r = new ImportReportVM();
                                r.FirstName = workSheet.Cells[row, 2].Text;
                                r.LastName = workSheet.Cells[row, 3].Text;
                                r.MemberID = workSheet.Cells[row, 4].Text;
                                r.Division = workSheet.Cells[row, 6].Text;
                                r.Team = workSheet.Cells[row, 8].Text;
                                reports.Add(r);
                            }
                        }
                                //                    Player p = new Player();
                                //                    try
                                //                    {
                                //                        // Row by row...
                                //                        p.PlayerFirstName = workSheet.Cells[row, 2].Text;
                                //                        p.PlayerLastName = workSheet.Cells[row, 3].Text;
                                //                        p.PlayerMemberID = workSheet.Cells[row, 4].Text;
                                //                        p.PlayerNumber = null;
                                //                        p.Division.DivAge = workSheet.Cells[row, 6].Text;
                                //                        p.Division.DivisionTeams = workSheet.Cells[row, 8].Text;
                                //                        _context.Players.Add(p);
                                //                        _context.SaveChanges();
                                //                        successCount++;
                                //                    }
                                //                    catch (DbUpdateException dex)
                                //                    {
                                //                        errorCount++;
                                //                        if (dex.GetBaseException().Message.Contains("UNIQUE constraint failed"))
                                //                        {
                                //                            feedBack += "Error: Record " + p.PlayerFullName + " was rejected as a duplicate."
                                //                                    + "<br />";
                                //                        }
                                //                        else
                                //                        {
                                //                            feedBack += "Error: Record " + p.PlayerFullName + " caused an error."
                                //                                    + "<br />";
                                //                        }
                                //                        //Here is the trick to using SaveChanges in a loop.  You must remove the 
                                //                        //offending object from the cue or it will keep raising the same error.
                                //                        _context.Remove(p);
                                //                    }
                                //                    catch (Exception ex)
                                //                    {
                                //                        errorCount++;
                                //                        if (ex.GetBaseException().Message.Contains("correct format"))
                                //                        {
                                //                            feedBack += "Error: Record " + p.PlayerFullName + " was rejected becuase the standard charge was not in the correct format."
                                //                                    + "<br />";
                                //                        }
                                //                        else
                                //                        {
                                //                            feedBack += "Error: Record " + p.PlayerFullName + " caused and error."
                                //                                    + "<br />";
                                //                        }
                                //                    }
                                //                }
                                //                feedBack += "Finished Importing " + (successCount + errorCount).ToString() +
                                //                    " Records with " + successCount.ToString() + " inserted and " +
                                //                    errorCount.ToString() + " rejected";
                                //            }
                                //            else
                                //            {
                                //                feedBack = "Error: You may have selected the wrong file to upload.<br /> Remember, you must have the headings 'Name' and 'Standard Charge' in the first two cells of the first row.";
                                //            }
                                //        }
                                //        else
                                //        {
                                //            feedBack = "Error: That file is not an Excel spreadsheet.";
                                //        }
                                //    }
                                //    else
                                //    {
                                //        feedBack = "Error:  file appears to be empty";
                                //    }
                                //}
                                //else
                                //{
                                //    feedBack = "Error: No file uploaded";
                                //}

                            }
                        }
                    }
                }
            }
        }
    


	
