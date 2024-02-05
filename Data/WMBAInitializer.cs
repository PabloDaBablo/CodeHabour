using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Diagnostics;
using System.Numerics;
using WMBA_7_2_.Models;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

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
                         CoachMemberID = "7890987",
                         CoachName = "Dave Roberts",
                         CoachNumber = 99,
                         CoachPosition = "Head Coach"
                     },
                    new Coach
                    {
                        CoachMemberID = "5678765",
                        CoachName = "Alex Cora",
                        CoachNumber = 88,
                        CoachPosition = "Assistant Coach"
                    },
                    new Coach
                    {
                        CoachMemberID = "4984484",
                        CoachName = "Aaron Boone",
                        CoachNumber = 33,
                        CoachPosition = "Head Coach"
                    },

                    new Coach
                    {
                        CoachMemberID = "567876",
                        CoachName = "Terry Francona",
                        CoachNumber = 44,
                        CoachPosition = "Assistant Coach"
                    },
                     new Coach
                     {
                         CoachMemberID = "49810293",
                         CoachName = "Gabe Kapler",
                         CoachNumber = null,
                         CoachPosition = "Head Coach"
                     },

                        new Coach
                        {
                            CoachMemberID = "21983198",
                            CoachName = "Brian Snitker",
                            CoachNumber = null,
                            CoachPosition = "Assistant Coach"
                        },
                     new Coach
                     {
                         CoachMemberID = "49481283",
                         CoachName = "Dusty Baker",
                         CoachNumber = 77,
                         CoachPosition = "Assistant Coach"
                     },
                    new Coach
                    {
                        CoachMemberID = "3456543",
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
                    if (!context.Team_Coaches.Any())
                    {
                        context.Team_Coaches.AddRange(
                            new Team_Coach
                            {
                                TeamID = 1,
                                CoachID = 1
                            },
                            new Team_Coach
                            {
                                TeamID = 1,
                                CoachID = 2
                            },
                            new Team_Coach
                            {
                                TeamID = 2,
                                CoachID = 3
                            },
                            new Team_Coach
                            {
                                TeamID = 2,
                                CoachID = 4
                            },
                            new Team_Coach
                            {
                                TeamID = 3,
                                CoachID = 5
                            }
                            );
                        context.SaveChanges();

                    }
                    // Add 36 Players
                    if (!context.Players.Any())
                    {
                        context.Players.AddRange(
                        new Player
                        {
                            PlayerMemberID = "111",
                            PlayerFirstName = "Mike",
                            PlayerLastName = "Trout",
                            PlayerNumber = 1,
                            TeamID = 1,
                            DivisionID = 1,
                            IsActive = true
                        },
                        new Player
                        {
                            PlayerMemberID = "222",
                            PlayerFirstName = "Mookie",
                            PlayerLastName = "Betts",
                            PlayerNumber = 2,
                            TeamID = 1,
                            DivisionID = 1,
                            IsActive = true
                        },
                        new Player
                        {
                            PlayerMemberID = "333",
                            PlayerFirstName = "Fernando",
                            PlayerLastName = "Tatis",
                            PlayerNumber = 3,
                            TeamID = 1,
                            DivisionID = 1,
                            IsActive = true

                        },
                        new Player
                        {
                            PlayerMemberID = "444",
                            PlayerFirstName = "Jacob",
                            PlayerLastName = "deGrom",
                            PlayerNumber = 4,
                            TeamID = 1,
                            DivisionID = 1,
                            IsActive = true
                        },
                        new Player
                        {
                            PlayerMemberID = "555",
                            PlayerFirstName = "Shohei",
                            PlayerLastName = "Ohtani",
                            PlayerNumber = 5,
                            TeamID = 1,
                            DivisionID = 1,
                            IsActive = true
                        },
                        new Player
                        {
                            PlayerMemberID = "666",
                            PlayerFirstName = "Cole",
                            PlayerLastName = "Gerrit",
                            PlayerNumber = 6,
                            TeamID = 1,
                            DivisionID = 1,
                            IsActive = true
                        },
                        new Player
                        {
                            PlayerMemberID = "777",
                            PlayerFirstName = "Freddie",
                            PlayerLastName = "Freeman",
                            PlayerNumber = 7,
                            TeamID = 1,
                            DivisionID = 1,
                            IsActive = true
                        },
                        new Player
                        {
                            PlayerMemberID = "888",
                            PlayerFirstName = "José",
                            PlayerLastName = "Ramírez",
                            PlayerNumber = 8,
                            TeamID = 1,
                            DivisionID = 1,
                            IsActive = true
                        },
                        new Player
                        {
                            PlayerMemberID = "999",
                            PlayerFirstName = "Bryce",
                            PlayerLastName = "Harper",
                            PlayerNumber = 9,
                            TeamID = 1,
                            DivisionID = 1,
                            IsActive = true
                        },
                        new Player
                        {
                            PlayerMemberID = "121",
                            PlayerFirstName = "Juan",
                            PlayerLastName = "Soto",
                            PlayerNumber = 10,
                            TeamID = 2,
                            DivisionID = 2,
                            IsActive = true
                        },
                        new Player
                        {
                            PlayerMemberID = "131",
                            PlayerFirstName = "Ronald",
                            PlayerLastName = "Acuña Jr.",
                            PlayerNumber = 11,
                            TeamID = 2,
                            DivisionID = 2,
                            IsActive = true
                        },
                        new Player
                        {
                            PlayerMemberID = "141",
                            PlayerFirstName = "Nolan",
                            PlayerLastName = "Arenado",
                            PlayerNumber = 12,
                            TeamID = 2,
                            DivisionID = 2,
                            IsActive = true
                        },
                        new Player
                        {
                            PlayerMemberID = "151",
                            PlayerFirstName = "Giancarlo",
                            PlayerLastName = "Stanton",
                            PlayerNumber = 13,
                            TeamID = 2,
                            DivisionID = 2,
                            IsActive = true
                        },
                        new Player
                        {
                            PlayerMemberID = "161",
                            PlayerFirstName = "Clayton",
                            PlayerLastName = "Kershaw",
                            PlayerNumber = 14,
                            TeamID = 2,
                            DivisionID = 2,
                            IsActive = true
                        },
                        new Player
                        {
                            PlayerMemberID = "171",
                            PlayerFirstName = "Yadier",
                            PlayerLastName = "Molina",
                            PlayerNumber = 15,
                            TeamID = 2,
                            DivisionID = 2,
                            IsActive = true
                        },
                        new Player
                        {
                            PlayerMemberID = "181",
                            PlayerFirstName = "Xander",
                            PlayerLastName = "Bogaerts",
                            PlayerNumber = 16,
                            TeamID = 2,
                            DivisionID = 2,
                            IsActive = true
                        },
                        new Player
                        {
                            PlayerMemberID = "191",
                            PlayerFirstName = "Francisco",
                            PlayerLastName = "Ruth",
                            PlayerNumber = 17,
                            TeamID = 2,
                            DivisionID = 2,
                            IsActive = true
                        },
                        new Player
                        {
                            PlayerMemberID = "101",
                            PlayerFirstName = "Cody",
                            PlayerLastName = "Bellinger",
                            PlayerNumber = 18,
                            TeamID = 2,
                            DivisionID = 2,
                            IsActive = true
                        },
                        new Player
                        {
                            PlayerMemberID = "123",
                            PlayerFirstName = "Gerrit",
                            PlayerLastName = "Cole",
                            PlayerNumber = 19,
                            TeamID = 3,
                            DivisionID = 3,
                            IsActive = true
                        },
                        new Player
                        {
                            PlayerMemberID = "234",
                            PlayerFirstName = "Trea",
                            PlayerLastName = "Turner",
                            PlayerNumber = 20,
                            TeamID = 3,
                            DivisionID = 3,
                            IsActive = true
                        },
                        new Player
                        {
                            PlayerMemberID = "345",
                            PlayerFirstName = "Max",
                            PlayerLastName = "Scherzer",
                            PlayerNumber = 21,
                            TeamID = 3,
                            DivisionID = 3,
                            IsActive = true
                        },
                        new Player
                        {
                            PlayerMemberID = "456",
                            PlayerFirstName = "Jacob",
                            PlayerLastName = "Rendon",
                            PlayerNumber = 22,
                            TeamID = 3,
                            DivisionID = 3,
                            IsActive = true
                        },
                        new Player
                        {
                            PlayerMemberID = "567",
                            PlayerFirstName = "Shohei",
                            PlayerLastName = "Machado",
                            PlayerNumber = 23,
                            TeamID = 3,
                            DivisionID = 3,
                            IsActive = true
                        },
                        new Player
                        {
                            PlayerMemberID = "678",
                            PlayerFirstName = "Yu",
                            PlayerLastName = "Cole",
                            PlayerNumber = 24,
                            TeamID = 3,
                            DivisionID = 3,
                            IsActive = true
                        },
                        new Player
                        {
                            PlayerMemberID = "789",
                            PlayerFirstName = "Ozzie",
                            PlayerLastName = "Freeman",
                            PlayerNumber = 25,
                            TeamID = 3,
                            DivisionID = 3,
                            IsActive = true
                        },
                        new Player
                        {
                            PlayerMemberID = "890",
                            PlayerFirstName = "José",
                            PlayerLastName = "Bryant",
                            PlayerNumber = 26,
                            TeamID = 3,
                            DivisionID = 3,
                            IsActive = true
                        },
                        new Player
                        {
                            PlayerMemberID = "901",
                            PlayerFirstName = "Rafael",
                            PlayerLastName = "Harper",
                            PlayerNumber = 27,
                            TeamID = 3,
                            DivisionID = 3,
                            IsActive = true
                        },
                        new Player
                        {
                            PlayerMemberID = "109",
                            PlayerFirstName = "Trevor",
                            PlayerLastName = "Ruth",
                            PlayerNumber = 28,
                            TeamID = 4,
                            DivisionID = 4,
                            IsActive = true
                        },
                        new Player
                        {
                            PlayerMemberID = "998",
                            PlayerFirstName = "Ronald",
                            PlayerLastName = "Cruz",
                            PlayerNumber = 29,
                            TeamID = 4,
                            DivisionID = 4,
                            IsActive = true
                        },
                        new Player
                        {
                            PlayerMemberID = "987",
                            PlayerFirstName = "Nolan",
                            PlayerLastName = "Alonso",
                            PlayerNumber = 30,
                            TeamID = 4,
                            DivisionID = 4,
                            IsActive = true
                        },
                        new Player
                        {
                            PlayerMemberID = "876",
                            PlayerFirstName = "Giancarlo",
                            PlayerLastName = "Stanton",
                            PlayerNumber = 31,
                            TeamID = 4,
                            DivisionID = 4,
                            IsActive = true
                        },
                        new Player
                        {
                            PlayerMemberID = "765",
                            PlayerFirstName = "Clayton",
                            PlayerLastName = "Bogaerts",
                            PlayerNumber = 32,
                            TeamID = 4,
                            DivisionID = 4,
                            IsActive = true
                        },
                        new Player
                        {
                            PlayerMemberID = "654",
                            PlayerFirstName = "Xander",
                            PlayerLastName = "Molina",
                            PlayerNumber = 33,
                            TeamID = 4,
                            DivisionID = 4,
                            IsActive = true
                        },
                        new Player
                        {
                            PlayerMemberID = "543",
                            PlayerFirstName = "Xander",
                            PlayerLastName = "Bogaerts",
                            PlayerNumber = 34,
                            TeamID = 4,
                            DivisionID = 4,
                            IsActive = true
                        },
                        new Player
                        {
                            PlayerMemberID = "432",
                            PlayerFirstName = "Nelson",
                            PlayerLastName = "Martinez",
                            PlayerNumber = 35,
                            TeamID = 4,
                            DivisionID = 4,
                            IsActive = true
                        },
                        new Player
                        {
                            PlayerMemberID = "321",
                            PlayerFirstName = "J.F.",
                            PlayerLastName = "Martinez",
                            PlayerNumber = 36,
                            TeamID = 4,
                            DivisionID = 4,
                            IsActive = true
                        });

                        context.SaveChanges();
                    }
                }

            



                if (!context.Games.Any())
                {
                    context.Games.AddRange(
                        new Game
                        {
                            GameDate = DateTime.Now,
                            GameTime = DateTime.Now,
                            GameSeason = "2024",
                            GameLocation = "Memorial Park Diamond #2",
                            HomeTeam = "Bananas",
                            AwayTeam = "Dragons"

                        },
                        new Game
                        {
                            GameDate = DateTime.Now,
                            GameTime = DateTime.Now,
                            GameSeason = "2024",
                            GameLocation = "Burger Park Diamond",
                            HomeTeam = "Whitecaps",
                            AwayTeam = "Bisons"

                        },
                        new Game
                        {
                            GameDate = DateTime.Now,
                            GameTime = DateTime.Now,
                            GameSeason = "2024",
                            GameLocation = "Memorial Park Diamond #3",
                            HomeTeam = "Dragons",
                            AwayTeam = "Bananas"

                        },
                        new Game
                        {
                            GameDate = DateTime.Now,
                            GameTime = DateTime.Now,
                            GameSeason = "2024",
                            GameLocation = "Welland Jackfish Stadium",
                            HomeTeam = "Bisons",
                            AwayTeam = "Whitecaps"

                        }
                        );
                    context.SaveChanges();

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

