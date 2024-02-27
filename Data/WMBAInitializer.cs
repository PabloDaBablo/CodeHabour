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
                        }
                        );
                    
                    context.SaveChanges();
                }
                //    //create divisions
                if (!context.Divisions.Any())
                {
                    context.Divisions.AddRange(
                        new Division
                        {
                            DivAge = "15U",
                            LeagueTypeID = 1
                        },
                        new Division
                        {
                            DivAge = "13U",
                            LeagueTypeID = 1
                        }

                        );

                    context.SaveChanges();

                }
                if (!context.Positions.Any())
                {
                    context.Positions.AddRange(
                        new Position
                        {
                            PlayerPosName = "Catcher"
                        },
                        new Position
                        {
                            PlayerPosName = "First Base"
                        },
                        new Position
                        {
                            PlayerPosName = "Second Base"
                        },
                        new Position
                        {
                            PlayerPosName = "Third Base"
                        },  
                        new Position
                        {
                            PlayerPosName = "Pitcher"
                        },
                        new Position
                        {
                            PlayerPosName = "Short Stop"
                        },
                        new Position
                        {
                            PlayerPosName = "Left Field"
                        },
                        new Position
                        {
                            PlayerPosName = "Center Field"
                        },
                        new Position
                        {
                            PlayerPosName = "Right Field"
                        }

                        );

                    context.SaveChanges();

                }
                //    // Add 8 coaches.
                if (!context.Coaches.Any())
                {
                    context.Coaches.AddRange(

                     new Coach
                     {
                         CoachMemberID = "7890987",
                         CoachName = "Dave Roberts",
                         CoachNumber = 99
                     },
                    new Coach
                    {
                        CoachMemberID = "5678765",
                        CoachName = "Alex Cora",
                        CoachNumber = 88
                    },
                    new Coach
                    {
                        CoachMemberID = "4984484",
                        CoachName = "Aaron Boone",
                        CoachNumber = 33
                    },

                    new Coach
                    {
                        CoachMemberID = "567876",
                        CoachName = "Terry Francona",
                        CoachNumber = 44
                    },
                     new Coach
                     {
                         CoachMemberID = "49810293",
                         CoachName = "Gabe Kapler",
                         CoachNumber = null
                     },

                        new Coach
                        {
                            CoachMemberID = "21983198",
                            CoachName = "Brian Snitker",
                            CoachNumber = null
                        },
                     new Coach
                     {
                         CoachMemberID = "49481283",
                         CoachName = "Dusty Baker",
                         CoachNumber = 77
                     },
                    new Coach
                    {
                        CoachMemberID = "3456543",
                        CoachName = "Craig Counsell",
                        CoachNumber = null
                    });
                    context.SaveChanges();
                }
                if (!context.Teams.Any())
                {
                    context.Teams.AddRange(
                new Team
                {
                    TeamName = "No Assigned Team",

                },
                new Team
                {
                    TeamName = "Dragons"
                },
                new Team
                {
                    TeamName = "Bisons"
                },
                new Team
                {
                    TeamName = "Bananas"
                },
                new Team
                {
                    TeamName = "Whitecaps"
                }
                
                );
                }
                     context.SaveChanges();
                if (!context.Team_Coaches.Any())
                {
                    context.Team_Coaches.AddRange(
                        new Team_Coach
                        {
                            TeamID = 2,
                            CoachID = 1
                        },
                        new Team_Coach
                        {
                            TeamID = 2,
                            CoachID = 2
                        },
                        new Team_Coach
                        {
                            TeamID = 3,
                            CoachID = 3
                        },
                        new Team_Coach
                        {
                            TeamID = 3,
                            CoachID = 4
                        },
                        new Team_Coach
                        {
                            TeamID = 4,
                            CoachID = 5
                        }
                        );
                    context.SaveChanges();

                }
                //        // Add 36 Players
                if (!context.Players.Any())
                {
                    context.Players.AddRange(
                    new Player
                    {
                        PlayerMemberID = "111",
                        PlayerFirstName = "Mike",
                        PlayerLastName = "Trout",
                        PlayerNumber = 1,
                        TeamID = 2,
                        DivisionID = 1,
                        IsActive = true
                    },
                    new Player
                    {
                        PlayerMemberID = "222",
                        PlayerFirstName = "Mookie",
                        PlayerLastName = "Betts",
                        PlayerNumber = 2,
                        TeamID = 2,
                        DivisionID = 1,
                        IsActive = true
                    },
                    new Player
                    {
                        PlayerMemberID = "333",
                        PlayerFirstName = "Fernando",
                        PlayerLastName = "Tatis",
                        PlayerNumber = 3,
                        TeamID = 2,
                        DivisionID = 1,
                        IsActive = true

                    },
                    new Player
                    {
                        PlayerMemberID = "444",
                        PlayerFirstName = "Jacob",
                        PlayerLastName = "deGrom",
                        PlayerNumber = 4,
                        TeamID = 2,
                        DivisionID = 1,
                        IsActive = true
                    },
                    new Player
                    {
                        PlayerMemberID = "555",
                        PlayerFirstName = "Shohei",
                        PlayerLastName = "Ohtani",
                        PlayerNumber = 5,
                        TeamID = 2,
                        DivisionID = 1,
                        IsActive = true
                    },
                    new Player
                    {
                        PlayerMemberID = "666",
                        PlayerFirstName = "Cole",
                        PlayerLastName = "Gerrit",
                        PlayerNumber = 6,
                        TeamID = 2,
                        DivisionID = 1,
                        IsActive = true
                    },
                    new Player
                    {
                        PlayerMemberID = "777",
                        PlayerFirstName = "Freddie",
                        PlayerLastName = "Freeman",
                        PlayerNumber = 7,
                        TeamID = 2,
                        DivisionID = 1,
                        IsActive = true
                    },
                    new Player
                    {
                        PlayerMemberID = "888",
                        PlayerFirstName = "José",
                        PlayerLastName = "Ramírez",
                        PlayerNumber = 8,
                        TeamID = 2,
                        DivisionID = 1,
                        IsActive = true
                    },
                    new Player
                    {
                        PlayerMemberID = "999",
                        PlayerFirstName = "Bryce",
                        PlayerLastName = "Harper",
                        PlayerNumber = 9,
                        TeamID = 2,
                        DivisionID = 1,
                        IsActive = true
                    },
                    new Player
                    {
                        PlayerMemberID = "121",
                        PlayerFirstName = "Juan",
                        PlayerLastName = "Soto",
                        PlayerNumber = 1,
                        TeamID = 3,
                        DivisionID = 2,
                        IsActive = true
                    },
                    new Player
                    {
                        PlayerMemberID = "131",
                        PlayerFirstName = "Ronald",
                        PlayerLastName = "Acuña Jr.",
                        PlayerNumber = 11,
                        TeamID = 3,
                        DivisionID = 2,
                        IsActive = true
                    },
                    new Player
                    {
                        PlayerMemberID = "141",
                        PlayerFirstName = "Nolan",
                        PlayerLastName = "Arenado",
                        PlayerNumber = 12,
                        TeamID = 3,
                        DivisionID = 2,
                        IsActive = true
                    },
                    new Player
                    {
                        PlayerMemberID = "151",
                        PlayerFirstName = "Giancarlo",
                        PlayerLastName = "Stanton",
                        PlayerNumber = 13,
                        TeamID = 3,
                        DivisionID = 2,
                        IsActive = true
                    },
                    new Player
                    {
                        PlayerMemberID = "161",
                        PlayerFirstName = "Clayton",
                        PlayerLastName = "Kershaw",
                        PlayerNumber = 14,
                        TeamID = 3,
                        DivisionID = 2,
                        IsActive = true
                    },
                    new Player
                    {
                        PlayerMemberID = "171",
                        PlayerFirstName = "Yadier",
                        PlayerLastName = "Molina",
                        PlayerNumber = 15,
                        TeamID = 3,
                        DivisionID = 2,
                        IsActive = true
                    },
                    new Player
                    {
                        PlayerMemberID = "181",
                        PlayerFirstName = "Xander",
                        PlayerLastName = "Bogaerts",
                        PlayerNumber = 16,
                        TeamID = 3,
                        DivisionID = 2,
                        IsActive = true
                    },
                    new Player
                    {
                        PlayerMemberID = "191",
                        PlayerFirstName = "Francisco",
                        PlayerLastName = "Ruth",
                        PlayerNumber = 17,
                        TeamID = 3,
                        DivisionID = 2,
                        IsActive = true
                    },
                    new Player
                    {
                        PlayerMemberID = "101",
                        PlayerFirstName = "Cody",
                        PlayerLastName = "Bellinger",
                        PlayerNumber = 18,
                        TeamID = 3,
                        DivisionID = 2,
                        IsActive = true
                    },
                    new Player
                    {
                        PlayerMemberID = "123",
                        PlayerFirstName = "Gerrit",
                        PlayerLastName = "Cole",
                        PlayerNumber = 1,
                        TeamID = 4,
                        DivisionID = 2,
                        IsActive = true
                    },
                    new Player
                    {
                        PlayerMemberID = "234",
                        PlayerFirstName = "Trea",
                        PlayerLastName = "Turner",
                        PlayerNumber = 20,
                        TeamID = 4,
                        DivisionID = 2,
                        IsActive = true
                    },
                    new Player
                    {
                        PlayerMemberID = "345",
                        PlayerFirstName = "Max",
                        PlayerLastName = "Scherzer",
                        PlayerNumber = 21,
                        TeamID = 4,
                        DivisionID = 2,
                        IsActive = true
                    },
                    new Player
                    {
                        PlayerMemberID = "456",
                        PlayerFirstName = "Jacob",
                        PlayerLastName = "Rendon",
                        PlayerNumber = 22,
                        TeamID = 4,
                        DivisionID = 2,
                        IsActive = true
                    },
                    new Player
                    {
                        PlayerMemberID = "567",
                        PlayerFirstName = "Shohei",
                        PlayerLastName = "Machado",
                        PlayerNumber = 23,
                        TeamID = 4,
                        DivisionID = 2,
                        IsActive = true
                    },
                    new Player
                    {
                        PlayerMemberID = "678",
                        PlayerFirstName = "Yu",
                        PlayerLastName = "Cole",
                        PlayerNumber = 24,
                        TeamID = 4,
                        DivisionID = 2,
                        IsActive = true
                    },
                    new Player
                    {
                        PlayerMemberID = "789",
                        PlayerFirstName = "Ozzie",
                        PlayerLastName = "Freeman",
                        PlayerNumber = 25,
                        TeamID = 4,
                        DivisionID = 2,
                        IsActive = true
                    },
                    new Player
                    {
                        PlayerMemberID = "890",
                        PlayerFirstName = "José",
                        PlayerLastName = "Bryant",
                        PlayerNumber = 26,
                        TeamID = 4,
                        DivisionID = 2,
                        IsActive = true
                    },
                    new Player
                    {
                        PlayerMemberID = "901",
                        PlayerFirstName = "Rafael",
                        PlayerLastName = "Harper",
                        PlayerNumber = 27,
                        TeamID = 4,
                        DivisionID = 2,
                        IsActive = true
                    },
                    new Player
                    {
                        PlayerMemberID = "109",
                        PlayerFirstName = "Trevor",
                        PlayerLastName = "Ruth",
                        PlayerNumber = 28,
                        TeamID = 5,
                        DivisionID = 2,
                        IsActive = true
                    },
                    new Player
                    {
                        PlayerMemberID = "998",
                        PlayerFirstName = "Ronald",
                        PlayerLastName = "Cruz",
                        PlayerNumber = 29,
                        TeamID = 5,
                        DivisionID = 2,
                        IsActive = true
                    },
                    new Player
                    {
                        PlayerMemberID = "987",
                        PlayerFirstName = "Nolan",
                        PlayerLastName = "Alonso",
                        PlayerNumber = 30,
                        TeamID = 5,
                        DivisionID = 2,
                        IsActive = true
                    },
                    new Player
                    {
                        PlayerMemberID = "876",
                        PlayerFirstName = "Giancarlo",
                        PlayerLastName = "Stanton",
                        PlayerNumber = 31,
                        TeamID = 5,
                        DivisionID = 2,
                        IsActive = true
                    },
                    new Player
                    {
                        PlayerMemberID = "765",
                        PlayerFirstName = "Clayton",
                        PlayerLastName = "Bogaerts",
                        PlayerNumber = 32,
                        TeamID = 5,
                        DivisionID = 2,
                        IsActive = true
                    },
                    new Player
                    {
                        PlayerMemberID = "654",
                        PlayerFirstName = "Xander",
                        PlayerLastName = "Molina",
                        PlayerNumber = 33,
                        TeamID = 5,
                        DivisionID = 2,
                        IsActive = true
                    },
                    new Player
                    {
                        PlayerMemberID = "543",
                        PlayerFirstName = "Xander",
                        PlayerLastName = "Bogaerts",
                        PlayerNumber = 34,
                        TeamID = 5,
                        DivisionID = 2,
                        IsActive = true
                    },
                    new Player
                    {
                        PlayerMemberID = "432",
                        PlayerFirstName = "Nelson",
                        PlayerLastName = "Martinez",
                        PlayerNumber = 35,
                        TeamID = 5,
                        DivisionID = 2,
                        IsActive = true
                    },
                    new Player
                    {
                        PlayerMemberID = "321",
                        PlayerFirstName = "J.F.",
                        PlayerLastName = "Martinez",
                        PlayerNumber = 36,
                        TeamID = 5,
                        DivisionID = 2,
                        IsActive = true
                    });

                    context.SaveChanges();
                }

                if (!context.PlayerPositions.Any())
                {
                    context.PlayerPositions.AddRange(
                        new PlayerPosition
                        {
                            PlayerID = 1,
                            PositionID = 1
                        },
                        new PlayerPosition
                        {
                            PlayerID = 2,
                            PositionID = 2
                        },
                        new PlayerPosition
                        {
                            PlayerID = 3,
                            PositionID = 3
                        },
                        new PlayerPosition
                        {
                            PlayerID = 4,
                            PositionID = 4
                        },
                        new PlayerPosition
                        {
                            PlayerID = 5,
                            PositionID = 5
                        },
                        new PlayerPosition
                        {
                            PlayerID = 6,
                            PositionID = 6
                        },
                        new PlayerPosition
                        {
                            PlayerID = 7,
                            PositionID = 7
                        },
                        new PlayerPosition
                        {
                            PlayerID = 8,
                            PositionID = 8
                        },
                        new PlayerPosition
                        {
                            PlayerID = 9,
                            PositionID = 9
                        },
                        new PlayerPosition
                        {
                            PlayerID = 10,
                            PositionID = 1
                        },
                        new PlayerPosition
                        {
                            PlayerID = 11,
                            PositionID = 2
                        },
                        new PlayerPosition
                        {
                            PlayerID = 12,
                            PositionID = 3
                        },
                        new PlayerPosition
                        {
                            PlayerID = 13,
                            PositionID = 4
                        },
                        new PlayerPosition
                        {
                            PlayerID = 14,
                            PositionID = 5
                        },
                        new PlayerPosition
                        {
                            PlayerID = 15,
                            PositionID = 6
                        },
                        new PlayerPosition
                        {
                            PlayerID = 16,
                            PositionID = 7
                        },
                        new PlayerPosition
                        {
                            PlayerID = 17,
                            PositionID = 8
                        },
                        new PlayerPosition
                        {
                            PlayerID = 18,
                            PositionID = 9
                        },
                        new PlayerPosition
                        {
                            PlayerID = 19,
                            PositionID = 1
                        },
                        new PlayerPosition
                        {
                            PlayerID = 20,
                            PositionID = 2
                        },
                        new PlayerPosition
                        {
                            PlayerID = 21,
                            PositionID = 3
                        },
                        new PlayerPosition
                        {
                            PlayerID = 22,
                            PositionID = 4
                        },
                        new PlayerPosition
                        {
                            PlayerID = 23,
                            PositionID = 5
                        },
                        new PlayerPosition
                        {
                            PlayerID = 24,
                            PositionID = 6
                        },
                        new PlayerPosition
                        {
                            PlayerID = 25,
                            PositionID = 7
                        },
                        new PlayerPosition
                        {
                            PlayerID = 26,
                            PositionID = 8
                        },
                        new PlayerPosition
                        {
                            PlayerID = 27,
                            PositionID = 9
                        },
                        new PlayerPosition
                        {
                            PlayerID = 28,
                            PositionID = 1
                        },
                        new PlayerPosition
                        {
                            PlayerID = 29,
                            PositionID = 2
                        },
                        new PlayerPosition
                        {
                            PlayerID = 30,
                            PositionID = 3
                        },
                        new PlayerPosition
                        {
                            PlayerID = 31,
                            PositionID = 4
                        },
                        new PlayerPosition
                        {
                            PlayerID = 32,
                            PositionID = 5
                        },
                        new PlayerPosition
                        {
                            PlayerID = 33,
                            PositionID = 6
                        },
                        new PlayerPosition
                        {
                            PlayerID = 34,
                            PositionID = 7
                        },
                        new PlayerPosition
                        {
                            PlayerID = 35,
                            PositionID = 8
                        },
                        new PlayerPosition
                        {
                            PlayerID = 36,
                            PositionID = 9
                        }
                        
                        );
                    context.SaveChanges();

                }


                if (!context.Games.Any())
                {
                    context.Games.AddRange(
                        new Game
                        {
                            GameDate = DateTime.Now,
                            GameTime = DateTime.Now,
                            //GameSeason = "2024",
                            GameLocation = "Memorial Park Diamond #2",
                            HomeTeamID = 2,
                            AwayTeamID = 3,
                        },
                        new Game
                        {
                            GameDate = DateTime.Now,
                            GameTime = DateTime.Now,
                            //GameSeason = "2024",
                            GameLocation = "Burger Park Diamond",
                            HomeTeamID = 4,
                            AwayTeamID = 5,
                        },
                        new Game
                        {
                            GameDate = DateTime.Now,
                            GameTime = DateTime.Now,
                            // GameSeason = "2024",
                            GameLocation = "Memorial Park Diamond #3",
                            HomeTeamID = 4,
                            AwayTeamID = 2,
                        },
                        new Game
                        {
                            GameDate = DateTime.Now,
                            GameTime = DateTime.Now,
                            //GameSeason = "2024",
                            GameLocation = "Welland Jackfish Stadium",
                            HomeTeamID = 3,
                            AwayTeamID = 5,
                        }
                        );
                    context.SaveChanges();

                    foreach (Game game in context.Games)
                    {

                        //Add the players from the teams to each one
                        Team homeTeam = context.Teams.Include(t => t.Players).FirstOrDefault(t => t.ID == game.HomeTeamID);
                        foreach (Player p in homeTeam.Players)
                        {
                            game.GamePlayers.Add(new GamePlayer()
                            {
                                PlayerID = p.ID,
                                GameID = game.ID,
                                TeamLineup = TeamLineup.Home
                            });
                        }

                        Team awayTeam = context.Teams.Include(t => t.Players).FirstOrDefault(t => t.ID == game.AwayTeamID);
                        foreach (Player p in awayTeam.Players)
                        {
                            game.GamePlayers.Add(new GamePlayer()
                            {
                                PlayerID = p.ID,
                                GameID = game.ID,
                                TeamLineup = TeamLineup.Away
                            });
                        }
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

