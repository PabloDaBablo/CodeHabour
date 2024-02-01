using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Diagnostics;
using System.Numerics;
using WMBA_7_2_.Models;
using Microsoft.EntityFrameworkCore;

namespace WMBA_7_2_.Data
{
    public class WMBAInitializer
    {
        public static void Seed(IApplicationBuilder applicationBuilder)
        {
            WMBAContext context = applicationBuilder.ApplicationServices.CreateScope()
                 .ServiceProvider.GetRequiredService<WMBAContext>();
            
            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();
            //context.Database.Migrate();
            try
            {
                // Add 4 coaches.
                if (!context.Coaches.Any())
                {
                    context.Coaches.AddRange(

                     new Coach
                     {
                         CoachMemberID = 7890987,
                         CoachName = "Coach Whitecap",
                         CoachNumber = 99,
                         CoachPosition = "Head Coach"
                         //TeamID = context.Teams.FirstOrDefault(t => t.TeamName == "Whitecaps").ID
                     },

                        new Coach
                        {
                            CoachMemberID = 5678765,
                            CoachName = "A/Coach Whitecaps",
                            CoachNumber = 88,
                            CoachPosition = "Assistant Coach"
                            // TeamID = context.Teams.FirstOrDefault(t => t.TeamName == "Whitecaps").ID
                        },
                     new Coach
                     {
                         CoachMemberID = 4567654,
                         CoachName = "Coach Banana",
                         CoachNumber = 77,
                         CoachPosition = "Assistant Coach"
                         // TeamID = context.Teams.FirstOrDefault(t => t.TeamName == "Bananas").ID
                     },
                    new Coach
                    {
                        CoachMemberID = 3456543,
                        CoachName = "A/Coach Banana",
                        CoachNumber = null,
                        CoachPosition = "Head Coach"
                        // TeamID = context.Teams.FirstOrDefault(t => t.TeamName == "Bananas").ID
                    });
                    context.SaveChanges();
                }
                //Add two Teams
                if (!context.Teams.Any())
                {
                    context.Teams.AddRange(
                new Team
                {
                    TeamName = "Bananas",
                    CoachID = 3
                },
                new Team
                {
                    TeamName = "Whitecaps",
                    CoachID = 1
                });

                    context.SaveChanges();
                    // Add Players
                    if (!context.Players.Any())
                {
                    context.Players.AddRange(
                    new Player
                    {
                        PlayerMemberID = 111,
                        PlayerFirstName = "Mike",
                        PlayerLastName = "Trout",
                        PlayerNumber = 1,
                        TeamID = 1
                    },
                    new Player
                    {
                        PlayerMemberID = 222,
                        PlayerFirstName = "Mookie",
                        PlayerLastName = "Betts",
                        PlayerNumber = 2,
                        TeamID = 1
                    },
                    new Player
                    {
                        PlayerMemberID = 333,
                        PlayerFirstName = "Fernando",
                        PlayerLastName = "Tatis",
                        PlayerNumber = 3,
                        TeamID = 1
                    },
                    new Player
                    {
                        PlayerMemberID = 444,
                        PlayerFirstName = "Jacob",
                        PlayerLastName = "deGrom",
                        PlayerNumber = 4,
                        TeamID = 1
                    },
                    new Player
                    {
                        PlayerMemberID = 555,
                        PlayerFirstName = "Shohei",
                        PlayerLastName = "Ohtani",
                        PlayerNumber = 5,
                        TeamID = 1
                    },
                    new Player
                    {
                        PlayerMemberID = 666,
                        PlayerFirstName = "Gerrit",
                        PlayerLastName = "Cole",
                        PlayerNumber = 6,
                        TeamID = 1
                    },
                    new Player
                    {
                        PlayerMemberID = 777,
                        PlayerFirstName = "Freddie",
                        PlayerLastName = "Freeman",
                        PlayerNumber = 7,
                        TeamID = 1
                    },
                    new Player
                    {
                        PlayerMemberID = 888,
                        PlayerFirstName = "José",
                        PlayerLastName = "Ramírez",
                        PlayerNumber = 8,
                        TeamID = 1
                    },
                    new Player
                    {
                        PlayerMemberID = 999,
                        PlayerFirstName = "Bryce",
                        PlayerLastName = "Harper",
                        PlayerNumber = 9,
                        TeamID = 1
                    },
                    new Player
                    {
                        PlayerMemberID = 121,
                        PlayerFirstName = "Babe",
                        PlayerLastName = "Ruth",
                        PlayerNumber = 1,
                        TeamID = 2
                    },
                    new Player
                    {
                        PlayerMemberID = 131,
                        PlayerFirstName = "Ronald",
                        PlayerLastName = "Acuña Jr.",
                        PlayerNumber = 2,
                        TeamID = 2
                    },
                    new Player
                    {
                        PlayerMemberID = 141,
                        PlayerFirstName = "Nolan",
                        PlayerLastName = "Arenado",
                        PlayerNumber = 3,
                        TeamID = 2
                    },
                    new Player
                    {
                        PlayerMemberID = 151,
                        PlayerFirstName = "Giancarlo",
                        PlayerLastName = "Stanton",
                        PlayerNumber = 4,
                        TeamID = 2
                    },
                    new Player
                    {
                        PlayerMemberID = 161,
                        PlayerFirstName = "Clayton",
                        PlayerLastName = "Kershaw",
                        PlayerNumber = 5,
                        TeamID = 2
                    },
                    new Player
                    {
                        PlayerMemberID = 171,
                        PlayerFirstName = "Yadier",
                        PlayerLastName = "Molina",
                        PlayerNumber = 6,
                        TeamID = 2
                    },
                    new Player
                    {
                        PlayerMemberID = 181,
                        PlayerFirstName = "Xander",
                        PlayerLastName = "Bogaerts",
                        PlayerNumber = 7,
                        TeamID = 2
                    },
                    new Player
                    {
                        PlayerMemberID = 191,
                        PlayerFirstName = "Francisco",
                        PlayerLastName = "Ruth",
                        PlayerNumber = 8,
                        TeamID = 2
                    },
                    new Player
                    {
                        PlayerMemberID = 101,
                        PlayerFirstName = "J.D.",
                        PlayerLastName = "Martinez",
                        PlayerNumber = 9,
                        TeamID = 2
                    });

                        context.SaveChanges();
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.GetBaseException().Message);
            }
        }
    }
}

