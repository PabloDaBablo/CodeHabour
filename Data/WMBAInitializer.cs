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
                //Create House Leage
                if (!context.Leagues.Any())
                {
                    context.Leagues.AddRange(
                        new League
                        {
                            LeagueType = "House League"
                        },
                    new League
                        {
                            LeagueType = "Select"
                        });

                    context.SaveChanges();
                }
                //Create Divisions
                if (!context.Divisions.Any())
                {
                    context.Divisions.AddRange(
                        new Division
                        {
                            DivAge = "U15",
                            DivisionTeams = "U15 Bananas",
                            LeagueTypeID = 1
                        },
                        new Division
                        {
                            DivAge = "U15",
                            DivisionTeams = "U15 Dragons",
                            LeagueTypeID = 1
                        },
                        new Division
                        {
                            DivAge = "U13",
                            DivisionTeams = "U13 Bisons",
                            LeagueTypeID = 1
                        },
                        new Division
                        {
                            DivAge = "U13",
                            DivisionTeams = "U13 Whitecaps",
                            LeagueTypeID = 1
                        });
                   
                    context.SaveChanges();

                }
                // Add 8 coaches.
                if (!context.Coaches.Any())
                {
                    context.Coaches.AddRange(

                     new Coach
                     {
                         CoachMemberID = 7890987,
                         CoachName = "Dave Roberts",
                         CoachNumber = 99,
                         CoachPosition = "Head Coach"
                     },

                        new Coach
                        {
                            CoachMemberID = 5678765,
                            CoachName = "Alex Cora",
                            CoachNumber = 88,
                            CoachPosition = "Assistant Coach"
                        },
                        new Coach
                        {
                            CoachMemberID = 7890987,
                            CoachName = "Aaron Boone",
                            CoachNumber = 33,
                            CoachPosition = "Head Coach"
                        },

                        new Coach
                        {
                            CoachMemberID = 5678765,
                            CoachName = "Terry Francona",
                            CoachNumber = 44,
                            CoachPosition = "Assistant Coach"
                        },
                     new Coach
                     {
                         CoachMemberID = 7890987,
                         CoachName = "Gabe Kapler",
                         CoachNumber = null,
                         CoachPosition = "Head Coach"
                     },

                        new Coach
                        {
                            CoachMemberID = 5678765,
                            CoachName = "Brian Snitker",
                            CoachNumber = null,
                            CoachPosition = "Assistant Coach"
                        },
                     new Coach
                     {
                         CoachMemberID = 4567654,
                         CoachName = "Dusty Baker",
                         CoachNumber = 77,
                         CoachPosition = "Assistant Coach"
                     },
                    new Coach
                    {
                        CoachMemberID = 3456543,
                        CoachName = "Craig Counsell",
                        CoachNumber = null,
                        CoachPosition = "Head Coach"
                    });
                    context.SaveChanges();
                }
                //Add 4 Teams
                if (!context.Teams.Any())
                {
                    context.Teams.AddRange(
                new Team
                {
                    TeamName = "Bananas",
                    CoachID = 1
                },
                new Team
                {
                    TeamName = "Dragons",
                    CoachID = 3
                },
                new Team
                {
                    TeamName = "Bisons",
                    CoachID = 5
                },
                new Team
                {
                    TeamName = "Whitecaps",
                    CoachID = 7
                });

                context.SaveChanges();
                    // Add 36 Players
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
                        PlayerFirstName = "Cole",
                        PlayerLastName = "Gerrit",
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
                        PlayerFirstName = "Juan",
                        PlayerLastName = "Soto",
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
                        PlayerFirstName = "Cody",
                        PlayerLastName = "Bellinger",
                        PlayerNumber = 9,
                        TeamID = 2
                     },
                    new Player
                    {
                        PlayerMemberID = 123,
                        PlayerFirstName = "Gerrit",
                        PlayerLastName = "Cole",
                        PlayerNumber = 1,
                        TeamID = 3
                    },
                    new Player
                    {
                        PlayerMemberID = 234,
                        PlayerFirstName = "Trea",
                        PlayerLastName = "Turner",
                        PlayerNumber = 2,
                        TeamID = 3
                    },
                    new Player
                    {
                        PlayerMemberID = 345,
                        PlayerFirstName = "Max",
                        PlayerLastName = "Scherzer",
                        PlayerNumber = 3,
                        TeamID = 3
                    },
                    new Player
                    {
                        PlayerMemberID = 456,
                        PlayerFirstName = "Jacob",
                        PlayerLastName = "Rendon",
                        PlayerNumber = 4,
                        TeamID = 3
                    },
                    new Player
                    {
                        PlayerMemberID = 567,
                        PlayerFirstName = "Shohei",
                        PlayerLastName = "Machado",
                        PlayerNumber = 5,
                        TeamID = 3
                    },
                    new Player
                    {
                        PlayerMemberID = 678,
                        PlayerFirstName = "Yu",
                        PlayerLastName = "Cole",
                        PlayerNumber = 6,
                        TeamID = 3
                    },
                    new Player
                    {
                        PlayerMemberID = 789,
                        PlayerFirstName = "Ozzie",
                        PlayerLastName = "Freeman",
                        PlayerNumber = 7,
                        TeamID = 3
                    },
                    new Player
                    {
                        PlayerMemberID = 890,
                        PlayerFirstName = "José",
                        PlayerLastName = "Bryant",
                        PlayerNumber = 8,
                        TeamID = 3
                    },
                    new Player
                    {
                        PlayerMemberID = 901,
                        PlayerFirstName = "Rafael",
                        PlayerLastName = "Harper",
                        PlayerNumber = 9,
                        TeamID = 3
                    },
                    new Player
                    {
                        PlayerMemberID = 109,
                        PlayerFirstName = "Trevor",
                        PlayerLastName = "Ruth",
                        PlayerNumber = 1,
                        TeamID = 4
                    },
                    new Player
                    {
                        PlayerMemberID = 998,
                        PlayerFirstName = "Ronald",
                        PlayerLastName = "Cruz",
                        PlayerNumber = 2,
                        TeamID = 4
                    },
                    new Player
                    {
                        PlayerMemberID = 987,
                        PlayerFirstName = "Nolan",
                        PlayerLastName = "Alonso",
                        PlayerNumber = 3,
                        TeamID = 4
                    },
                    new Player
                    {
                        PlayerMemberID = 876,
                        PlayerFirstName = "Giancarlo",
                        PlayerLastName = "Stanton",
                        PlayerNumber = 4,
                        TeamID = 4
                    },
                    new Player
                    {
                        PlayerMemberID = 765,
                        PlayerFirstName = "Clayton",
                        PlayerLastName = "Bogaerts",
                        PlayerNumber = 5,
                        TeamID = 4
                    },
                    new Player
                    {
                        PlayerMemberID = 654,
                        PlayerFirstName = "Xander",
                        PlayerLastName = "Molina",
                        PlayerNumber = 6,
                        TeamID = 4
                    },
                    new Player
                    {
                        PlayerMemberID = 543,
                        PlayerFirstName = "Xander",
                        PlayerLastName = "Bogaerts",
                        PlayerNumber = 7,
                        TeamID = 4
                    },
                    new Player
                    {
                        PlayerMemberID = 432,
                        PlayerFirstName = "Nelson",
                        PlayerLastName = "Martinez",
                        PlayerNumber = 8,
                        TeamID = 4
                    },
                    new Player
                    {
                        PlayerMemberID = 321,
                        PlayerFirstName = "J.F.",
                        PlayerLastName = "Martinez",
                        PlayerNumber = 9,
                        TeamID = 4
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

        //Fields for each division

        // 9u         : Chippawa Park Diamond, Maple Park Diamond #1, Maple Park Diamond #2
        // 11u and 13u: Memorial Park Diamond #2, Memorial Park Diamond #3
        // 15u and 18u: Burger Park Diamond, Welland Jackfish Stadium
    }
}

