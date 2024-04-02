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
                            DivAge = "18U",
                            LeagueTypeID = 1
                        },
                        new Division
                        {
                            DivAge = "15U",
                            LeagueTypeID = 1
                        },
                        new Division
                        {
                            DivAge = "13U",
                            LeagueTypeID = 1
                        },
                        new Division
                        {
                            DivAge = "11U",
                            LeagueTypeID = 1
                        },
                        new Division
                        {
                            DivAge = "9U",
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
                    TeamName = "Dragons",
                    DivisionID = 1,
                },
                new Team
                {
                    TeamName = "Bisons",
                    DivisionID = 1,
                },
                new Team
                {
                    TeamName = "Bananas",
                    DivisionID = 2,
                },
                new Team
                {
                    TeamName = "Whitecaps",
                    DivisionID = 2,
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
                            GameDate = new DateTime(2024, 6, 1, 12, 30, 0),
                            GameTime = new DateTime(2024, 6, 1, 12, 30, 0),
                            GameLocation = "Memorial Park Diamond #2",
                            HomeTeamID = 2,
                            AwayTeamID = 3,
                            
                        },
                        new Game
                        {
                            GameDate = new DateTime(2024, 6, 1, 12, 30, 0),
                            GameTime = new DateTime(2024, 6, 1, 12, 30, 0),
                            GameLocation = "Burger Park Diamond",
                            HomeTeamID = 4,
                            AwayTeamID = 5,
                        },
                        new Game
                        {
                            GameDate = new DateTime(2024, 6, 8, 12, 30, 0),
                            GameTime = new DateTime(2024, 6, 8, 12, 30, 0),
                            GameLocation = "Memorial Park Diamond #3",
                            HomeTeamID = 3,
                            AwayTeamID = 2,
                        },
                        new Game
                        {
                            GameDate = new DateTime(2024, 6, 15, 12, 30, 0),
                            GameTime = new DateTime(2024, 6, 15, 12, 30, 0),
                            GameLocation = "Welland Jackfish Stadium",
                            HomeTeamID = 5,
                            AwayTeamID = 4,
                        },
                        new Game
                        {
                            GameDate = new DateTime(2024, 6, 16, 12, 30, 0),
                            GameTime = new DateTime(2024, 6, 16, 12, 30, 0),
                            GameLocation = "Memorial Park Diamond #2",
                            HomeTeamID = 2,
                            AwayTeamID = 3,

                        },
                        new Game
                        {
                            GameDate = new DateTime(2024, 6, 19, 12, 30, 0),
                            GameTime = new DateTime(2024, 6, 19, 12, 30, 0),
                            GameLocation = "Burger Park Diamond",
                            HomeTeamID = 4,
                            AwayTeamID = 5,
                        },
                        new Game
                        {
                            GameDate = new DateTime(2024, 6, 19, 12, 30, 0),
                            GameTime = new DateTime(2024, 6, 19, 12, 30, 0),
                            GameLocation = "Memorial Park Diamond #3",
                            HomeTeamID = 3,
                            AwayTeamID = 2,
                        },
                        new Game
                        {
                            GameDate = new DateTime(2024, 6, 19, 12, 30, 0),
                            GameTime = new DateTime(2024, 6, 19, 12, 30, 0),
                            GameLocation = "Welland Jackfish Stadium",
                            HomeTeamID = 5,
                            AwayTeamID = 4,
                        },
                        new Game
                        {
                            GameDate = new DateTime(2024, 6, 21, 12, 30, 0),
                            GameTime = new DateTime(2024, 6, 21, 12, 30, 0),
                            GameLocation = "Burger Park Diamond",
                            HomeTeamID = 4,
                            AwayTeamID = 5,
                        },
                        new Game
                        {
                            GameDate = new DateTime(2024, 6, 28, 12, 30, 0),
                            GameTime = new DateTime(2024, 6, 28, 12, 30, 0),
                            GameLocation = "Memorial Park Diamond #3",
                            HomeTeamID = 4,
                            AwayTeamID = 2,
                        },
                        new Game
                        {
                            GameDate = new DateTime(2023, 7, 1, 12, 30, 0),
                            GameTime = new DateTime(2023, 7, 1, 12, 30, 0),
                            GameLocation = "Memorial Park Diamond #2",
                            HomeTeamID = 2,
                            AwayTeamID = 3,

                        },
                        new Game
                        {
                            GameDate = new DateTime(2024, 7, 1, 12, 30, 0),
                            GameTime = new DateTime(2024, 7, 1, 12, 30, 0),
                            GameLocation = "Burger Park Diamond",
                            HomeTeamID = 4,
                            AwayTeamID = 5,
                        },
                        new Game
                        {
                            GameDate = new DateTime(2024, 7, 15, 12, 30, 0),
                            GameTime = new DateTime(2024, 7, 15, 12, 30, 0),
                            GameLocation = "Welland Jackfish Stadium",
                            HomeTeamID = 3,
                            AwayTeamID = 5,
                        },
                        new Game
                        {
                            GameDate = new DateTime(2024, 7, 15, 12, 30, 0),
                            GameTime = new DateTime(2024, 7, 15, 12, 30, 0),
                            GameLocation = "Welland Jackfish Stadium",
                            HomeTeamID = 3,
                            AwayTeamID = 5,
                        }
                        );
                    context.SaveChanges();


                    //Seed Rules
                    if (!context.Rules.Any())
                    {
                        context.Rules.AddRange(
                            new Rules
                            {
                                RuleName = "Ambidextrous Pitchers",
                                RuleDescription = "A pitcher must visually indicate with which hand he will use to pitch prior to the start of a plate appearance. This can be accomplished simply by wearing his glove on his non-throwing hand and placing his foot on the pitching rubber. Barring injury, he is not permitted to pitch with the other hand until the batter is retired, becomes a baserunner or is removed for a pinch-hitter.\r\n\r\nThe pitcher is allowed to pitch with the opposite hand mid-plate appearance should he incur an injury with the other, but he is then barred from pitching with the injured hand for the rest of the game.",
                            },
                            new Rules
                            {
                                RuleName = "Appeal Plays",
                                RuleDescription = "The defensive team can appeal certain plays to alert the umpires of infractions that would otherwise be allowed without the appeal. Appeal plays are not the same as a manager asking the umpire for an instant-replay review.\r\n\r\nAn appeal can be made when the offensive team bats out of turn. Baserunning instances that are subject to appeal include a runner failing to tag up correctly on a caught fly ball, a runner failing to touch the bases in order -- either when advancing or retreating -- a runner failing to return to first base promptly after overrunning or oversliding it, and a runner failing to touch home plate and making no attempt to return to it.\r\n\r\nNo runner may return to touch a missed base after a following runner has scored. And when the ball is dead, no runner may return to touch a missed base after he has touched a base beyond the missed base. That means a batter who hits a ground-rule double or a homer can be called out on appeal if he misses first base and doesn't correct his error before touching second.\r\n\r\nAppeals must be made before the next pitch or attempted play, or before the entire defensive team has left fair territory if the play in question resulted in the end of a half-inning. The appealing team must make clear their intention to appeal, either via verbal request or another act that unmistakably indicates its attempt to appeal."
                            },
                            new Rules
                            {
                                RuleName = "Balk (BK) & Disengagement Violation (2023 rule change)",
                                RuleDescription = "Definition\r\nA balk occurs when a pitcher makes an illegal motion on the mound that the umpire deems to be deceitful to the runner(s). As a result, any men on base are awarded the next base, and the pitch (if it was thrown in the first place) is waved off for a dead ball.\r\n\r\nIn September 2022, Major League Baseball announced three rule changes that were approved by the league’s Competition Committee.\r\n\r\nThe Pitch Time rule change stipulates the following around disengagement violations:\r\n\r\nPitchers are limited to two disengagements (pickoff attempts or step-offs) per plate appearance. However, this limit is reset if a runner or runners advance during the plate appearance.\r\nA third disengagement, or a disengagement violation, would result in a balk.\r\nFor example, if a third pickoff attempt is made, the runner automatically advances one base if the pickoff attempt is not successful.\r\nMound visits, injury timeouts and offensive team timeouts do not count as a disengagement.\r\nIf a team has used up all five of its allotted mound visits prior to the ninth inning, that team will receive an additional mound visit in the ninth inning. This effectively serves as an additional disengagement.\r\nExactly what constitutes a balk is summed up in section 8 of the MLB rules, which describes a legal pitching delivery.\r\n\r\nPitchers with high balk totals are also generally adept at picking runners off, this being because their moves to first base are typically so deceptive that they border on being illegal. Any umpire, if he notices an illegal movement by the pitcher, can call a balk.\r\n\r\nOrigin\r\nThe specific rules for balks were first introduced in 1898 to prevent pitchers from intentionally deceiving baserunners. Without balk rules, pitchers had any means of fooling baserunners, who had to act conservatively on the bases as a result.\r\n\r\nIn A Call\r\n\"free advancement\""
                            },
                            new Rules
                            {
                                RuleName = "Base Sizes (2023 rule change)",
                                RuleDescription = "Definition\r\nIn September 2022, Major League Baseball announced three rule changes that were approved by the league’s Competition committee. The rules were adopted after extensive testing at the Minor League level over a several-year period and with feedback from MLB player and umpire representatives..\r\n\r\nOne of the rule changes covers the size of the bases, which traditionally have been 15 inches square, but will now be 18 inches square. Home plate remains unchanged.\r\n\r\nWATCH: Theo Epstein, MLB consultant for On-Field Matters, discusses the new bases\r\nThough the base-size change may have a modest impact on stolen-base success rate, the modification’s primary goal is to give players more room to operate and to avoid collisions. This is especially key at first base, where fielders will have an extra 3-inch advantage to stay out of harm’s way from the baserunner while receiving throws.\r\n\r\nThe change will create a 4 1/2-inch reduction in the distance between first and second base and between second base and third, which will likely encourage more stolen-base attempts. The bigger bases could also have the effect of reducing over-sliding, whereby a player loses contact with the bag after sliding through it."
                            },
                            new Rules
                            {
                                RuleName = "Batter's Box",
                                RuleDescription = "A regulation baseball field has two batter's boxes -- one on the left side and one on the right side of home plate -- drawn using the same chalk as the baselines. From the pitcher's point of view, left-handed batters stand in the batter's box on the left side of the plate and right-handed batters stand in the batter's box on the right side of the plate.\r\n\r\nThe batter is expected to promptly take his place in the batter's box when his turn to bat comes up and is not permitted to exit the box once the pitcher begins his windup or comes to the set position. The batter can request to be granted time by the umpire if the pitcher has not begun his windup or come to the set position. If his request is granted, the batter is then able to exit the box -- but not the dirt area surrounding home plate. He may also exit the box in other select instances, such as after a swing or bunt attempt, a passed ball, a wild pitch or an inside pitch that forces him out of the box. Furthermore, he may exit the box if the pitcher or catcher vacates his respective position, a member of the defensive team requests time or attempts a play on a runner at any base, or an attempted check swing is appealed to a base umpire. Otherwise, the batter is expected to keep at least one foot inside the box throughout his time at bat.\r\n\r\nIf a batted ball that has not yet been touched by a fielder makes contact with a batter who still has two feet in the batter's box, the ball will be ruled foul. But if the batter has already exited the batter's box before being contacted by a fair ball that has not yet been touched by a fielder, the batter will be called out."
                            },
                            new Rules
                            {
                                RuleName = "Batting Out of Turn",
                                RuleDescription = "Definition\r\nIf a team bats out of turn, the onus is not on the umpires to notify either team of the transgression. The consequences of batting out of turn vary depending on the timing of the appeal.\r\n\r\nAppeal made during plate appearance\r\n\r\nIf the opposing team makes its appeal or the offensive team realizes its error before the incorrect batter's plate appearance has concluded, then the correct batter can take his place at bat while assuming the incorrect batter's count.\r\n\r\nAppeal made following plate appearance but before next pitch or attempted play\r\n\r\nIf the appeal occurs between the conclusion of the incorrect batter's plate appearance but before the next pitch or attempted play, the correct batter is called out. Furthermore, any score or advancement caused by the incorrect batter batting a ball or reaching first base is nullified.\r\n\r\nHowever, scores or advancements are counted if they occur as a result of a stolen base, balk, wild pitch or passed ball during the incorrect batter's plate appearance.\r\n\r\nAppeal made following plate appearance and after next pitch or attempted play\r\n\r\nIf no appeal is made before the next pitch or attempted play following the conclusion of the incorrect batter's plate appearance, the incorrect batter is now considered to have batted in turn and all scores or advancements made during or as a result of his plate appearance are counted. The offensive team continues batting in its designated order from that point and places the skipped batter back into his original lineup spot the next time around."
                            },
                            new Rules
                            {
                                RuleName = "Catcher Interference",
                                RuleDescription = "Definition\r\nThe batter is awarded first base if the catcher (or any other fielder) interferes with him at any point during a pitch.\r\n\r\nIf first base was occupied at the time of the pitch, the runner who held the base is permitted to move up one base. This also applies if first and second base were occupied or the bases were loaded at the time of the pitch.\r\n\r\nIf first base had been empty at the time of the pitch, no additional runners are permitted to advance.\r\n\r\nWhen catcher interference occurs, the umpire will allow the play to progress because the outcome of the play may be more desirable than the interference. In that case, the offensive manager can elect to accept the outcome of the play over the interference."
                            },
                            new Rules
                            {
                                RuleName = "Coach Interference",
                                RuleDescription = "Definition\r\nIf a base coach interferes with a thrown ball, the runner will be ruled out. But if a thrown ball accidentally touches the base coach, the ball is alive and in play. Coaches must respect the fielder's right of way to make a play on a batted or thrown ball.\r\n\r\nBase coaches can also be called for interference if the umpire determines that they physically assisted a runner -- by grabbing or holding said runner -- from leaving or returning to first or third base. Moreover, base coaches are not permitted to leave their boxes and act in any manner intended to draw a throw by a fielder."
                            },
                            new Rules
                            {
                                RuleName = "Collisions at Home Plate",
                                RuleDescription = "Definition\r\nThe baserunner is not allowed to deviate from his direct path to initiate contact with the catcher (or any player covering the plate). Runners are considered to be in violation of this rule if they collide with the catcher in cases where a slide could have been used to avoid the collision. If the umpire determines that the runner violated this rule, the runner shall be ruled out and the ball is dead. The other runners must return to the last base they had touched at the time of the collision.\r\n\r\nThe catcher is not permitted to block the runner's path to the plate unless he is in possession of the ball, though blocking the path of the runner in a legitimate attempt to receive a throw is not considered a violation. The runner can be ruled safe if the umpire determines the catcher violated this rule. But per a September 2014 memorandum to the rule, the runner may still be called out if he was clearly beaten by the throw. Backstops are not subject to this rule on force plays.\r\n\r\nWhen receiving a throw, catchers will often provide a sliding lane into home plate for the runner to lower the possibility that they will be called for violating the rule. Likewise, runners can lower their chances of being called for a violation by sliding in the given lane.\r\n\r\nHistory of the rule\r\nIn an attempt to place greater emphasis on player safety, the rule was adopted on an experimental basis for the 2014 season. The change was made partially in response to a May 2011 collision at home plate that saw star catcher Buster Posey suffer a season-ending ankle injury. MLB clarified the rule with the September 2014 memorandum."
                            },
                            new Rules
                            {
                                RuleName = "Dead Ball",
                                RuleDescription = "Definition\r\nA dead ball is a ball that is out of play. The ruling of a dead ball halts the game and no plays can legally occur until the umpire resumes the game, though baserunners can advance as the result of acts that occurred while the ball was live. Dead balls are frequent occurrences during a game, and the dead-ball period typically does not last long before the ball is put back into play.\r\n\r\nDead balls most frequently occur when a batted ball becomes a foul ball or a fair ball is hit out of the playing field. Other common instances in which the ball is ruled dead include a batter being hit by a pitch, a balk, an illegal collision at home plate, obstruction of a baserunner, interference with a fielder's right of way, spectator interference, a batter or runner being granted time out by the umpire and a fair batted ball striking an umpire or runner.\r\n\r\nIf a fair ball gets lodged in the outfield wall padding -- or the ivy, in the case of Wrigley Field -- it is a ground-rule double. On all ground-rule doubles, the ball is dead, the batter-runner goes to second and all additional runners are permitted to move up two bases from the one they occupied at the time of the pitch."
                            },
                            new Rules
                            {
                                RuleName = "Defensive Shift Limits (2023 rule change)",
                                RuleDescription = "Definition\r\nIn September 2022, Major League Baseball announced three rule changes that were approved by the league’s Competition committee. The rules were adopted after extensive testing at the Minor League level over a several-year period and with feedback from MLB player and umpire representatives.\r\n\r\nWith the new rule changes, defensive teams will be required to have a minimum of four players on the infield, with at least two infielders completely on either side of second base. These restrictions are intended to increase the batting average on balls in play, to allow infielders to better showcase their athleticism and to restore more traditional outcomes on batted balls. The league-wide batting average on balls in play of .290 in 2022 was six points lower than in 2012 and 10 points lower than in 2006.\r\n\r\nWATCH: Joe Martinez, MLB Vice President of On-Field Strategy, shows how new shift limits will work\r\nThe rule change stipulates the following:\r\n\r\nThe four infielders must be within the boundary of the infield when the pitcher is on the rubber.\r\n\r\nInfielders may not switch sides. In other words, a team cannot reposition its best defender on the side of the infield the batter is more likely to hit the ball.\r\n\r\nIf the infielders are not aligned properly at the time of the pitch, the offense can choose an automatic ball or the result of the play.\r\n\r\nThis rule does not preclude a team from positioning an outfielder in the infield or in the shallow outfield grass in certain situations. But it does prohibit four-outfielder alignments."
                            },
                            new Rules
                            {
                                RuleName = "Designated Hitter Rule",
                                RuleDescription = "Definition\r\nThe designated hitter rule allows teams to use another player to bat in place of the pitcher. Because the pitcher is still part of the team's nine defensive players, the designated hitter -- or \"DH\" -- does not take the field on defense.\r\n\r\nThe rule was adopted by the American League in 1973, while pitchers continued to hit in games played at National League parks. The designated hitter was adopted by the National League as part of the 2022-26 collective bargaining agreement.\r\n\r\nThe DH must be selected prior to the game, and that selected hitter must come to bat at least one time -- unless the opposing team changes pitchers prior to that point. A team that chooses not to select a DH prior to a game is barred from using a DH for the rest of that game. A player who enters the game in place of the DH -- either as a pinch-hitter or a pinch-runner -- becomes the DH in his team's lineup thereafter.\r\n\r\nIf a player serving as the DH is later used on defense, he continues to bat in his same lineup spot. But for the rest of the game, his team cannot use a DH to bat in place of the pitcher. A team is also barred from using a DH for the rest of the game if the pitcher moves from the mound to another defensive position, a player pinch-hits for any other player and then becomes the pitcher, or the current pitcher pinch-hits or pinch-runs for the DH.\r\n\r\nHistory of the rule\r\nThe designated hitter rule was adopted by the AL in 1973. Prior to 2022, pitchers were required to bat in all NL games and Interleague games in which the NL team was the designated home team. A universal DH was also in place as a one-season exception in 2020, as part of health and safety protocols instituted during the COVID-19 pandemic.\r\n\r\nThe DH was not used in the World Series from 1973-75, then was used by both World Series teams during even-numbered years from '76-85. The practice of playing each game by the rules of the designated home team's league began in the 1986 World Series."
                            },
                            new Rules
                            {
                                RuleName = "Automatic Runner",
                                RuleDescription = "Definition\r\nAll half-innings after the conclusion of the ninth inning begin with an automatic runner on second base. The rule applies only to regular-season games.\r\n\r\nThe runner placed on second base at the start of each half-inning in extras is the player in the batting order immediately preceding that half-inning’s leadoff hitter, or a pinch-runner. So, if the No. 7 hitter in the order is due to lead off, the No. 6 hitter (or a pinch-runner for the No. 6 hitter) would be placed on second base.\r\n\r\nIf the automatic runner comes around to score, an earned run is not charged to the pitcher.\r\n\r\nHistory of the rule\r\n\r\nThe automatic runner was initially instituted as part of MLB's health and safety protocols during the pandemic-shortened 2020 season. It was kept in place in 2021 and 2022, and the Joint Competition Committee voted to make it a permanent addition to the rulebook prior to the 2023 season."
                            },
                            new Rules
                            {
                                RuleName = "Doctoring the Baseball",
                                RuleDescription = "Definition\r\nNo player is permitted to intentionally damage, deface or discolor the baseball by rubbing it with any type of foreign item or substance, including dirt or saliva. Failure to follow this rule will result in an ejection and an automatic 10-game suspension.\r\n\r\nThe pitcher is allowed to rub the ball between his bare hands but cannot spit on the ball, his hands or his glove. Also, pitchers are not allowed to rub the ball on their clothes, glove or other body parts besides their hands, nor are they permitted to pitch with an attachment such as a bandage, tape or bracelet on either hand or wrist.\r\n\r\nWhile in contact with the pitching rubber, the pitcher is not allowed to touch his mouth or lips at all. He can touch his mouth or lips when in the 18-foot circle surrounding the pitching rubber, but he is not permitted to then touch the baseball or the pitching rubber without first wiping his pitching hand dry. The pitcher shall be issued a warning the first time he violates this rule and then the umpire shall call an automatic ball for each subsequent violation. The pitcher may be allowed to blow on his pitching hand in a game played in cold weather, provided both managers agree to that exception prior to the start of the game.\r\n\r\nHistory of the rule\r\nPitches utilizing foreign substances, such as the spitball, were outlawed in 1920, but teams were allowed to designate up to two pitchers who could legally use the spitball during the 1920 season. Following that campaign, Major League Baseball designated 17 pitchers as legal spitballers who were permitted to use the spitball for the remainder of their careers. The spitball hasn't been legally used since Burleigh Grimes retired in 1934."
                            },
                            new Rules
                            {
                                RuleName = "Fair Ball",
                                RuleDescription = "Definition\r\nThe foul lines and foul poles are used to demarcate fair territory and, thus, determine what constitutes a fair ball.\r\n\r\nAny batted ball that first contacts a fielder while the ball is in fair territory is considered fair. If not touched by a fielder, any batted ball that first contacts the field in fair territory beyond first or third base -- with the foul lines and foul poles counting as fair territory -- is considered fair. Batted balls that first contact the field between home plate and first or third base are considered fair if they subsequently bounce over or directly contact either base, or otherwise pass either base while in fair territory. They are also considered fair if they settle in fair territory between home plate and first or third base, including instances in which they bounce off home plate.\r\n\r\nBatted balls that directly strike either foul pole on the fly, or leave the park on a fly to the right of the left-field foul pole and to the left of the right-field foul pole are considered home runs."
                            },
                            new Rules
                            {
                                RuleName = "Field Dimensions",
                                RuleDescription = "Definition\r\nNo Major League ballparks are exactly alike, but certain aspects of the field of play must be uniform across baseball.\r\n\r\nThe infield must be a square that is 90 feet on each side, and the outfield is the area between the two foul lines formed by extending two sides of said square (though the dirt portion of the field that runs well past the 90-foot basepaths in all Major League parks is also commonly referred to as the infield). The field must be constructed so that the bases are the same level as home plate.\r\n\r\nThe rulebook states that parks constructed by professional teams after June 1, 1958, must have a minimum distance of 325 feet between home plate and the nearest fence, stand or other obstruction on the right- and left-field foul lines, and 400 feet between home plate and the nearest fence, stand or other obstruction in center field. However, some clubs have been permitted to construct parks after that date with dimensions shorter than those specified.\r\n\r\nThe pitcher's plate must be a 24-inch by 6-inch slab of whitened rubber that is 10 inches above the level of home plate and 60 feet, 6 inches away from the back point of home plate. It is placed 18 inches behind the center of the mound -- which is erected within an 18-foot diameter circle -- and surrounded by a level area that is 5 feet by 34 inches. The slope of the pitcher's mound begins 6 inches in front of the pitcher's plate and must gradually decrease by 1 inch every foot for 6 feet in the direction of home plate.\r\n\r\nHome plate is a 17-inch square of whitened rubber with two of the corners removed so that one edge is 17 inches long, two adjacent sides are 8 1/2 inches each and the remaining two sides are 12 inches each and set at an angle to make a point. The 17-inch side faces the pitcher's plate, and the two 12-inch edges coincide with the first- and third-base lines. The back tip of home plate must be 127 feet, 3 and 3/8 inches away from second base.\r\n\r\nThe other bases must be 15-inch squares that are between 3 and 5 inches thick, covered by white canvas or rubber and filled with soft material.\r\n\r\nHistory of the rule\r\nFrom the early 1900s through 1968, the pitcher's plate was permitted to be 15 inches above the level of home plate. That height was lowered to 10 inches starting with the 1969 season, in response to a 1968 campaign -- now known as the \"Year of the Pitcher\" -- in which the dominance of hurlers reached new heights.\r\n\r\nThe specification on minimum park dimensions was put into place due to the stadium controversy surrounding the Brooklyn Dodgers' move to Los Angeles in 1958. The Dodgers played at the Los Angeles Memorial Coliseum while Dodger Stadium was being built, but the Coliseum was not designed to hold baseball games. The Coliseum's left-field fence was roughly 250 feet away from home plate and the club had to erect a 40-foot-high screen to protect against short home runs. The specification is not strictly enforced, however, so long as teams do not build parks that egregiously violate the rule. For example, Petco Park opened in 2004 and is officially 396 feet in center field, and Oriole Park at Camden Yards opened in 1992 and is 318 feet down the right-field line.\r\n\r\n"
                            },
                            new Rules
                            {
                                RuleName = "Fielder Right of Way",
                                RuleDescription = "Definition\r\nFielders have a right to occupy any space needed to catch or field a batted ball and also must not be hindered while attempting to field a thrown ball.\r\n\r\nIf any member of the batting team (including the coaches) interferes with a fielder's right of way to field a batted ball, the batter shall be declared out. If any member of the batting team (including the coaches) interferes with a fielder's right of way to field a thrown ball, the runner on whom the play is being made shall be ruled out. In both cases, the ball will be declared dead and all runners must return to their last legally occupied base at the time of the interference. However, a runner is not obligated to vacate a base he is legally permitted to occupy to allow a defender the space to field a batted or thrown ball in the proximity of said base.\r\n\r\nInterference can also be called on the offensive team if a batter hinders the catcher after a third strike when the ball is not caught, a batter intentionally deflects any foul ball, and a baserunner hinders a following play being made on another runner after having scored or been put out. When running the last half of the way to first base while the ball is being fielded in the vicinity of first, a baserunner must stay within the three-foot runner's lane to the right of the foul line unless they are avoiding a player fielding a batted ball. If the umpire determines that the baserunner has interfered with the player taking the throw at first base by running to the left of the foul line or to the right of the runner's lane, the baserunner can be called for interference."
                            },
                            new Rules
                            {
                                RuleName = "Force Play",
                                RuleDescription = "Definition\r\nA force play occurs when a baserunner is no longer permitted to legally occupy a base and must attempt to advance to the next base. The defense can retire the runner by tagging the next base before he arrives, though not if the defensive team first forces out a trailing runner. In that instance, the force play is removed and the defense must tag the remaining runners to retire them.\r\n\r\nFirst base tends to have the most force plays, as batters are eligible to be forced out at first any time they put the ball into fair territory and it is not caught in the air."
                            },
                            new Rules
                            {
                                RuleName = "Foul Ball",
                                RuleDescription = "Definition\r\nThe foul lines and foul poles are used to demarcate fair territory and, thus, determine what constitutes a foul ball.\r\n\r\nAny batted ball that first contacts a fielder while the ball is in foul territory is considered foul. If not touched by a fielder in fair territory, any batted ball that first contacts the field in foul territory beyond first or third base -- with the foul lines and foul poles counting as fair territory -- is considered foul. Batted balls that first contact the field between home plate and first or third base are considered foul if they don't subsequently bounce over or directly contact either base, otherwise pass either base while in fair territory, or ultimately settle at some point in fair territory between home plate and either base.\r\n\r\nBatted balls that leave the park on a fly to the left of the left-field foul pole or to the right of the right-field foul pole -- without striking the pole in either case -- are considered foul."
                            },
                            new Rules
                            {
                                RuleName = "Foul Tip",
                                RuleDescription = "Definition\r\nA foul tip is a batted ball that goes sharply and directly to the catcher's hand or glove and is legally caught. A foul tip is considered equivalent to a ball in which the batter swings and misses, in that the baserunners are able to advance at their own risk (without needing to tag up). Should the batter produce a foul tip after previously accruing two strikes, the foul tip is considered strike three and the batter is out.\r\n\r\nThe term \"foul tip\" is commonly used to describe any pitch that is slightly struck into foul territory, but the ball must be caught by the catcher in order for it to be considered a foul tip by rule. If the ball is not caught by the catcher, it is considered a regular foul ball and the baserunners are not able to advance."
                            },
                            new Rules
                            {
                                RuleName = "Ground Rules",
                                RuleDescription = "Definition\r\nThe Commissioner's Office issues a list of universal ground rules that are to be used in every Major League ballpark each season. Individual parks then are able to institute their own special ground rules, covering instances in which the intricacies of said parks might influence the game. For example, Tropicana Field has a number of special ground rules regarding occurrences of a batted ball striking a catwalk, light or another suspended object.\r\n\r\nThe home team is the sole judge as to whether a game shall not begin due to unsuitable weather or playing-field conditions, except for the second game of a doubleheader. In the latter instance, the umpire-in-chief of the first game of the doubleheader shall make that call.\r\n\r\nFor ballparks with retractable roofs, the decision to begin the game with the roof open or closed rests with the home team during the regular season. The roof can be closed only for weather reasons if the game begins with the roof open. If the game begins with the roof closed, it can be reopened once if the home team determines the climatic environment has reached a level where fan comfort and enjoyment will be best served by opening the roof. The roof may be moved only once during a game, unless inclement weather indicates otherwise. During the postseason, the Commissioner or another designated official shall make all decisions regarding roof movement, in consultation with the home club and the umpire crew chief."
                            },
                            new Rules
                            {
                                RuleName = "Infield Fly",
                                RuleDescription = "Definition\r\nAn infield fly is any fair fly ball (not including a line drive or a bunt) which can be caught by an infielder with ordinary effort when first and second or first, second and third base are occupied, before two men are out. The rule is in place to protect against a team allowing a shallow fly ball to drop in with the intention of causing a force play at second and third or second, third and home. Otherwise, the team would be able to force out baserunners who had stayed put on a routine fly ball.\r\n\r\nIn these situations, the umpire will declare \"infield fly\" for the benefit of the baserunners as soon as it is apparent that the fly ball qualifies as an infield fly. The batter is out even if the ball is not caught, and the baserunners can advance at their own risk. If the ball is caught, the baserunners can attempt to advance as they would on a typical ball caught in the air."
                            },
                            new Rules
                            {
                                RuleName = "Manager Challenge",
                                RuleDescription = "Definition\r\nEach club receives two manager challenges to start each All-Star Game, postseason game and Divisional or Wild Card tiebreaker game, and one manager challenge to start every other game. All reviews are conducted at the Replay Command Center, which is located at Major League Baseball Advanced Media headquarters in New York, by replay officials -- full-time Major League umpires who work shifts at the Replay Command Center in addition to their on-field work. Replay officials review all calls subject to replay review and decide whether to change the call on the field, confirm the call on the field or let stand the call on the field due to the lack of clear and convincing evidence.\r\n\r\nA manager may challenge as many reviewable calls within a single play as he desires using one challenge. The club retains its manager challenge if the replay official overturns any challenged call (even if he upholds other challenged calls), and loses its manager challenge if no calls are overturned. Once a club has exhausted its available manager challenge(s), it will no longer have the ability to challenge any additional play or call in the game.\r\n\r\nA manager has a 20-second time limit (as of 2020; previously was 30 seconds) to inform the umpire (by verbal communication or hand signal) whether he wishes to use his manager challenge to invoke replay review, and the challenge may not be rescinded once it has been exercised.\r\n\r\nBoth managers may challenge different reviewable calls within the same reviewable play, and the replay official shall review the challenged calls in the order in which the calls occurred during the game. If the decision of the replay official on an earlier reviewable call renders moot a later reviewable call, the subsequent call shall not be reviewed and that club shall not be charged with the challenge.\r\n\r\nThe manager must ensure that the umpire knows the specific calls for which he is seeking replay review, but the manager need not state the reason for his belief that the call was incorrect. Moreover, the replay official shall have no authority to review any calls other than those included in a manager's challenge or those accepted for review by the crew chief.\r\n\r\nHistory of the rule\r\nMajor League Baseball instituted replay review -- to be used at the umpire's discretion -- on disputed home run calls (fair or foul, in or out of the ballpark, fan interference) on Aug. 28, 2008.\r\n\r\nReplay review was expanded starting in the 2014 season, giving managers one challenge to start the game and allowing them to challenge two times in total provided the first challenge resulted in an overturned call. In addition, a much wider range of calls were made subject to review.\r\n\r\nReplay review was modified again in 2015, permitting managers to retain their challenge after every overturned call; allowing them to signal for a challenge during an inning without approaching the umpire on the field; and providing two challenges for any All-Star Game, postseason game and Divisional or Wild Card tiebreaker game. The list of calls that were subject to review was also expanded again in 2015 and 2016.\r\n\r\nPrior to a 30-second time limit being implemented for the 2017 season, a manager challenge could be exercised up until the commencement of the next play or pitch. On-field personnel were not permitted to intentionally delay the game in order to provide their club with additional time to challenge a play. In the case of a play that resulted in a third-out call, a manager needed to immediately run onto the field to notify an umpire that the club was contemplating a challenge. After entering the field, a manager then had up to 30 seconds to invoke the challenge. The 30-second timeframe was shortened to 20 seconds prior to the 2020 season."
                            },
                            new Rules
                            {
                                RuleName = "Mound Visit",
                                RuleDescription = "Definition\r\nThe members of the coaching staff (including the manager) can make one mound visit per pitcher per inning without needing to remove the pitcher from the game. If the same pitcher is visited twice in one inning, the pitcher must be removed from the contest. These mound visits are limited to 30 seconds, starting when the manager or coach has exited the dugout and been granted time by the umpire. The mound visit is considered to be concluded once the manager or coach leaves the 18-foot circle surrounding the pitching rubber, though they are permitted to temporarily leave that area to notify the umpire of a substitution. In that case, the manager or coach can then return to the mound without it being counted as two mound visits.\r\n\r\nMound visits are limited to five per team per nine innings, with teams receiving an additional visit for every extra inning played. Any manager, coach or player visit to the mound counts as a mound visit under this rule, though visits to the mound to clean cleats in rainy weather, to check on a potential injury or after the announcement of an offensive substitution are excepted. Normal communication between a player and pitcher that doesn't require either to vacate his position on the field doesn't count as a visit. If a team is out of visits, the umpire will have discretion to grant a brief visit at the catcher's request if a cross-up has occurred between the pitcher and catcher.\r\n\r\nHistory of the rule\r\nMound visits had no time limit prior to the 2016 season, when Major League Baseball began limiting visits by managers and coaches to 30 seconds. The rule limiting each team to six mound visits per nine innings was instituted prior to the 2018 campaign and changed to five per nine innings prior to the 2019 season. Previously, the only restriction on the number of mound visits each team could make was the one requiring a pitcher to be removed if he was visited twice in one inning."
                            },
                            new Rules
                            {
                                RuleName = "Neighborhood Play",
                                RuleDescription = "Definition\r\nThe \"neighborhood play\" is a colloquial term used to describe the leeway granted to middle infielders with regards to touching second base while in the process of turning a ground-ball double play. Though it is not explicitly mentioned in the rulebook, middle infielders were long able to record an out on the double-play pivot simply by being in the proximity -- or neighborhood -- of the second-base bag. The maneuver had been permitted for safety purposes, as it allowed the pivot man to get out of the way of the oncoming baserunner as quickly as possible.\r\n\r\nBut via a rule change instituted before the 2016 season, the neighborhood play is now reviewable by instant replay. That means middle infielders must touch the second-base bag while in possession of the ball in order to ensure the out is made on a ground-ball double play. In order to protect the middle infielders, Major League Baseball also amended the sliding rules for baserunners.\r\n\r\nHistory of the rule\r\nThe amendments to the sliding rules were implemented after a 2015 season in which a number of middle infielders were injured by sliding baserunners while covering second base. In accordance with the rule change, MLB determined that questionable slides and the neighborhood play would both now be reviewable by instant replay."
                            },
                            new Rules
                            {
                                RuleName = "Obstruction",
                                RuleDescription = "Definition\r\nObstruction describes an act by a fielder, who is not in possession of the ball or in the process of fielding it, that impedes the baserunner's progress.\r\n\r\nIf a play is being made on the obstructed baserunner, the ball is ruled dead and the umpire can place all runners on the base he determines they would have reached without the obstruction. If no play is being made on the obstructed runner, the umpire will allow the play to progress until its natural conclusion and then impose any penalties he sees fit to nullify the act of obstruction."
                            },
                            new Rules
                            {
                                RuleName = "Official Scorer",
                                RuleDescription = "Definition\r\nThe official scorer is the person appointed to observe from the press box and record the outcome of everything that happens during a game, and to make judgment calls that affect the official record of said game. The official scorer files a report after each game for documentation purposes.\r\n\r\nThe most common judgment call an official scorer makes is whether a batter reached base on a hit or an error. Other rulings made by the official scorer include whether a pitch that goes past the catcher is a wild pitch or a passed ball, and which reliever is credited with a win when the starting pitcher does not go five innings but leaves with a lead that his team doesn't relinquish.\r\n\r\nThe official scorer is permitted to change a judgment call for up to 24 hours after a game concludes or is suspended. A player or team can request that the executive vice president of baseball operations review a call in which said player or team participated. This request must come within 72 hours after the conclusion or suspension of that game, or 72 hours after the official scorer's call in the event a postgame change is made."
                            },
                            new Rules
                            {
                                RuleName = "Ordinary Effort",
                                RuleDescription = "Definition\r\nOrdinary effort refers to the effort that a fielder of average skill at a specific position should exhibit on a play, with due consideration given to the conditions of the playing field and the weather. Umpires must use that standard when calling infield fly plays, and the official scorer uses it to judge what constitutes an error, a wild pitch, a passed ball and a sacrifice."
                            },
                            new Rules
                            {
                                RuleName = "Pace of Play",
                                RuleDescription = "Definition\r\nA number of changes have been implemented to improve the pace of play since the 2014 season.\r\n\r\nThe batter's box rule -- which requires hitters to keep one foot in the box during their time at bat unless one of a group of exceptions occurs -- has been more strictly enforced since the 2015 season.\r\n\r\nAlso in 2015, timers were installed in Major League stadiums to measure the break time between innings and pitching changes. MLB lowered the time between innings to 2 minutes for local broadcasts and nationally televised games in 2019. MLB had instituted times of 2 minutes, 5 seconds for local broadcasts and 2 minutes, 25 seconds for nationally televised games in 2016, decreasing these times by 20 seconds from where they were previously. Prior to the 2018 season, MLB established a separate time of 2 minutes, 55 seconds for tiebreaker and postseason games.\r\n\r\nAs of 2018, the umpire's signal for the final warmup pitch comes at the 25-second mark and the pitcher must throw it before the clock hits 20. The batter will be announced at the 20-second mark and the pitcher must begin his windup to throw the first pitch of the inning within the five seconds before the clock hits zero. The pitcher can take as many warmup pitches as he wants within these countdown parameters.\r\n\r\nFor between-innings breaks, the timer begins when the final out of the inning is recorded, with several exceptions. If the pitcher is on base, on deck or at bat when the inning ends, the timer begins when the pitcher leaves the dugout for the mound. If the catcher is on base, on deck or at bat when the inning ends, the timer begins when the catcher enters the dugout (another catcher can begin warming up the pitcher). If the final out of the inning is subject to replay, the timer begins when the umpire signals the out. And for any extended between-innings event previously approved by the Office of the Commissioner (such as the playing of \"God Bless America\"), the timer begins at the conclusion of the event.\r\n\r\nThe timing clock also applies to pitching changes and begins as soon as the relief pitcher crosses the warning track (or the foul line for on-field bullpens). Players can be excused from these time limits if a delay in normal warmup activities occurs due to no fault of the players, or the umpire believes a player would be at legitimate risk of injury without receiving additional time.\r\n\r\nIn 2016, MLB began limiting mound visits -- which previously had no limit -- to 30 seconds starting when the manager or coach has exited the dugout and been granted time by the umpire. As of 2019, clubs are limited to five mound visits per team per nine innings, with teams receiving an additional visit for every extra inning played. Any manager, coach or player visit to the mound counts as a mound visit, though visits to the mound to clean cleats in rainy weather, to check on a potential injury or after the announcement of an offensive substitution are excepted. Normal communication between a player and pitcher that doesn't require either to vacate his position on the field doesn't count as a visit. If a team is out of visits, the umpire will have discretion to grant a brief visit at the catcher's request if a cross-up has occurred between the pitcher and catcher. A rule limiting the number of mound visits to six per nine innings was first established in 2018 before being lowered to five in 2019."
                            },
                            new Rules
                            {
                                RuleName = "Pitch Timer (2023 rule change)",
                                RuleDescription = "Definition\r\nIn September 2022, Major League Baseball announced three rule changes that were approved by the league’s Competition Committee. The rules were adopted after extensive testing at the Minor League level over a several-year period and with feedback from MLB player and umpire representatives.\r\n\r\nWATCH: Pitch timer demo, 2022 MiLB vs. MLB\r\nIn an effort to create a quicker pace of play, a 30-second timer between batters will be implemented in 2023. Between pitches, a 15-second timer will be in place with the bases empty and a 20-second timer with runners on base.\r\n\r\nThe rule change stipulates the following:\r\n\r\nThe pitcher must begin his motion to deliver the pitch before the expiration of the pitch timer. Pitchers who violate the timer are charged with an automatic ball. Batters who violate the timer are charged with an automatic strike.\r\nBatters must be in the box and alert to the pitcher by the 8-second mark or else be charged with an automatic strike.\r\nWith runners on base, the timer resets if the pitcher attempts a pickoff or steps off the rubber.\r\nPitchers are limited to two disengagements (pickoff attempts or step-offs) per plate appearance. However, this limit is reset if a runner or runners advance during the plate appearance.\r\nIf a third pickoff attempt is made, the runner automatically advances one base if the pickoff attempt is not successful.\r\nMound visits, injury timeouts and offensive team timeouts do not count as a disengagement.\r\nIf a team has used up all five of its allotted mound visits prior to the ninth inning, that team will receive an additional mound visit in the ninth inning. This effectively serves as an additional disengagement.\r\nUmpires may provide extra time if warranted by special circumstances. (So if, as an example, a catcher were to be thrown out on the bases to end the previous half-inning and needed additional time to put on his catching gear, the umpire could allow it.)"
                            },
                            new Rules
                            {
                                RuleName = "Protested Game (2021 rule change)",
                                RuleDescription = "As of the 2021 season, Rule 7.04 in the Official Baseball Rules was amended to read:\r\n\r\n\'Protesting a game shall never be permitted, regardless of whether such complaint is based on judgment decisions by the umpire or an allegation that an umpire misapplied these rules or otherwise rendered a decision in violation of these rules.\'"
                            },
                            new Rules
                            {
                                RuleName = "Regulation Game",
                                RuleDescription = "Definition\r\nA game is considered a regulation game -- also known as an \"official game\" -- once the visiting team has made 15 outs (five innings) and the home team is leading, or once the home team has made 15 outs regardless of score.\r\n\r\nPrior to the 2020 season, if a game was terminated early due to weather before becoming official, the results up to the point of the termination did not count and the game was started over at a later date. But as part of MLB's health and safety protocols during the COVID-19 pandemic, all games cut short due to weather before becoming official were resumed at a later date, rather than started over from scratch, during the 2020 campaign.\r\n\r\nThe rules below remained in place.\r\n\r\nIf a regulation game is terminated early due to weather, the results are considered final if the home team is leading. If the home team is trailing, the results are considered final if the game is not in the midst of an inning when the visiting team has taken the lead.\r\n\r\nIf a regulation game is terminated early due to weather and the game is either tied or in the midst of an inning in which the visiting team has taken the lead, it becomes a suspended game that will be completed at a later date from the point of termination.\r\n\r\nIf not terminated early, regulation games last until the trailing team has had the chance to make 27 outs (nine innings). If the home team is leading after the visiting team has made three outs in the top of the ninth inning, the home team wins and does not have to come to bat in the bottom of the ninth.\r\n\r\nIf the game is tied after both teams have made 27 outs each, the game will go to extra innings. It will continue until the home team takes the lead at any point, or the visiting team takes the lead and the home team subsequently makes three outs without tying the game or going ahead."
                            },
                            new Rules
                            {
                                RuleName = "Replay Review",
                                RuleDescription = "Definition\r\nReplay review in Major League Baseball is designed to provide timely review of certain disputed calls and is initiated by a manager challenge or by the umpire crew chief.\r\n\r\nAll reviews are conducted at the Replay Command Center, which is located at Major League Baseball Advanced Media headquarters in New York, by replay officials -- full-time Major League umpires who work shifts at the Replay Command Center in addition to their on-field shifts. Replay officials review all calls subject to replay review and decide whether to change the call on the field, confirm the call on the field or let stand the call on the field due to the lack of clear and convincing evidence. Beginning in the 2017 season, replay officials are expected to render a decision on a replay review within a two-minute time period (some exceptions permitted).\r\n\r\nIf replay review results in a change to a call that had been made on the field, the replay official shall exercise his discretion to place both clubs in the position they would have been in had the call on the field been correct.\r\n\r\nThe decision of the replay official to either uphold or change one or more calls subject to replay review shall be final and is not subject to further review or revision. Once replay review is initiated, on-field personnel from either club who further argue the contested calls or the decision of the replay official shall be ejected. No protest shall ever be permitted on judgment decisions by the replay official.\r\n\r\nIf a call is overturned on replay review, any decision made by a manager after the play and influenced by the incorrect call shall be nullified. That manager shall be permitted to reaffirm or change his strategic decision based on the result of the play as determined by the replay official.\r\n\r\nAt any time during a game, a crew chief may initiate a replay review of a potential home run call without a manager challenge being exercised. Beginning in the eighth inning, a crew chief may conduct a replay review of all reviewable calls upon his own initiative or upon the request of a manager who has no remaining manager challenges.\r\n\r\nThe following calls are reviewable via replay:\r\n\r\nPotential home run calls: The umpires' decision to call or not call a home run may be reviewed if there is a question as to whether the ball left the playing field or struck an object; whether the ball struck the top of a fence, hit a railing or otherwise stayed within the field of play; whether the ball was interfered with by a fan reaching over the fence; or whether the ball was fair or foul.\r\nNon-home run boundary calls: Calls involving a decision regarding whether a live ball bounces out of the field of play, strikes the top of a fence or hits a railing or other obstacle in the ballpark, is interfered with by a fan reaching over the fence, is successfully caught by a fielder proximate to a stadium boundary, or leaves the field of play and becomes a dead ball.\r\nSpecified fair/foul ball calls: Calls involving a decision regarding whether a batted ball was foul are reviewable only on balls that first land at or beyond the set positions of the first- or third-base umpire.\r\nForce/tag play calls: Calls involving a defensive player's attempt to put out a runner by tagging him or touching a base, and/or whether or not the runner acquired the base.\r\nCatch plays in the outfield: An umpire's decision whether a fielder caught a fly ball or a line drive in flight in the outfield before it hit the ground is reviewable, but fly balls or line drives fielded by a defensive player in the infield is not eligible for review.\r\nSpecified baserunning calls: Calls involving whether a baserunner passes a preceding runner, whether a baserunner scored ahead of a third out, and whether a baserunner touched a base.\r\nHit by pitch: Calls involving whether a pitched ball may have hit a player, a piece of his clothing or his bat. Whether the ball was in the strike zone when it touched the batter and whether the batter made any attempt to avoid being touched by the ball is not be reviewable.\r\nCollisions at home plate: Calls involving a runner deviating from his path to initiate contact with the catcher, and a catcher blocking the path of the runner without being in possession of the ball.\r\nTag-ups: Whether a runner left the base early or properly touched a base following a catch on a fly ball is reviewable.\r\nPlacement of runners: An umpire's placement of a batter and/or runners following any boundary call is reviewable.\r\nInterference on double plays: Calls pertaining to whether a runner intentionally interfered with a fielder in an attempt to break up a double play.\r\nHistory of the rule\r\nMajor League Baseball instituted replay review -- to be used at the umpire's discretion -- on disputed home run calls (fair or foul, in or out of the ballpark, fan interference) on Aug. 28, 2008.\r\n\r\nReplay review was expanded starting in the 2014 season, giving managers one challenge to start the game and allowing them to challenge two times in total provided the first challenge resulted in an overturned call. In addition, a much wider range of calls were made subject to review. Clubs could challenge potential home run calls, non-home run boundary calls, tag and force plays (except on a fielder touching second base while turning a double play), fair and foul balls in the outfield, catch plays in the outfield, a potential hit by pitch, whether a runner scored before a third out, whether a runner touched a base, whether a following runner passed the runner ahead of him, and record-keeping situations (ball-strike counts, outs, score and substitutions).\r\n\r\nReplay review was modified again in the 2015 season, permitting managers to retain their challenge after every overturned call; allowing them to signal for a challenge during an inning without approaching the umpire on the field; and providing two challenges for any All-Star Game, postseason game and Divisional or Wild Card tiebreaker game. The list of calls that were subject to review was also expanded to include tag-up plays.\r\n\r\nThe slide rules on attempts to break up a double play were changed in the 2016 season, and calls pertaining to said rule were also made reviewable. Additionally, all force plays were made reviewable in the 2016 season, including a fielder touching second base while turning a double play."
                            },
                            new Rules
                            {
                                RuleName = "Rookie Eligibility",
                                RuleDescription = "Definition\r\nA player shall be considered a rookie unless he has exceeded any of the following thresholds in a previous season (or seasons):\r\n\r\n• 130 at-bats or 50 innings pitched in the Major Leagues.\r\n• 45 total days on an active Major League roster during the Championship Season (excluding time on the Injured List).\r\n\r\nA player must have rookie eligibility to be considered for any MLB rookie awards -- such as the American League or National League Rookie of the Year Award -- or appear on any MLB Pipeline prospect lists."
                            },
                            new Rules
                            {
                                RuleName = "Set Position",
                                RuleDescription = "Definition\r\nPitchers are permitted to use two legal pitching deliveries -- the windup position and the set position -- and either position may be used at any time.\r\n\r\nA pitcher is considered to be in the set position when he puts his pivot foot against the pitching rubber, has both shoulders facing first (for lefty pitchers) or third (for righty pitchers) base to some degree and holds the ball with both hands in front of his body. As a result of this configuration, a pitcher in the set position points his glove-hand shoulder toward the batter and his throwing shoulder toward center field. When delivering the ball from the set position, a pitcher simply lifts his free leg up, pushes off the rubber with his pivot foot, strides toward the batter and delivers the pitch to the catcher.\r\n\r\nThe set position is often used with runners on base, as the length of time needed to complete the delivery is much shorter than the windup position. Thus, pitchers are better able to prevent runners from stealing bases from the set position. To shorten their deliveries further when pitching from the set position, some pitchers use a \"slide step\" to stride toward the batter rather than first using a full leg kick. Because relievers often enter the game with runners already on base, many pitch from the set position exclusively -- even when the bases are empty."
                            },
                            new Rules
                            {
                                RuleName = "Slide Rule",
                                RuleDescription = "Definition\r\nWhen sliding into a base in an attempt to break up a double play, a runner has to make a \"bona fide slide.\" Such is defined as the runner making contact with the ground before reaching the base, being able to reach the base with a hand or foot, being able to remain on the base at the completion of the slide (except at home plate) and not changing his path for the purpose of initiating contact with a fielder. The slide rule prohibits runners from using a \"roll block\" or attempting to initiate contact with the fielder by elevating and kicking his leg above the fielder's knee, throwing his arm or his upper body or grabbing the fielder. When a violation of the slide rule occurs, the offending runner and the batter-runner will be called out.\r\n\r\nAccidental contact can occur in the course of a permissible slide, and a runner will not be called for interference if contact is caused by a fielder being in the runner's legal pathway to the base.\r\n\r\nHistory of the rule\r\nAmendments to the sliding rules were implemented after a 2015 season in which a number of middle infielders were injured by sliding baserunners while covering second base.\r\n\r\nPreviously, runners were given a good deal of leeway when sliding into a base in an attempt to break up a double play. Even on plays when they made contact with a fielder, runners were rarely called for interference provided they were in reach of the base at any point in the course of their slides."
                            },
                            new Rules
                            {
                                RuleName = "Spectator Interference",
                                RuleDescription = "Definition\r\nIn every case of spectator interference with a batted or thrown ball, the ball shall be declared dead and the baserunners can be placed where the umpire determines they would have been without the interference. When a spectator clearly prevents a fielder from catching a fly ball by reaching onto the field of play, the batter shall be ruled out. But no interference is called if a spectator comes in contact with a batted or thrown ball without reaching onto the field of play -- even if a fielder might have caught the ball had the spectator not been there."
                            },
                            new Rules
                            {
                                RuleName = "Strike Zone",
                                RuleDescription = "Definition\r\nThe official strike zone is the area over home plate from the midpoint between a batter's shoulders and the top of the uniform pants -- when the batter is in his stance and prepared to swing at a pitched ball -- and a point just below the kneecap. In order to get a strike call, part of the ball must cross over part of home plate while in the aforementioned area.\r\n\r\nStrikes and balls are called by the home-plate umpire after every pitch has passed the batter, unless the batter makes contact with the baseball (in which case the pitch is automatically a strike).\r\n\r\nHistory of the rule\r\nThe vertical specifications of the strike zone have been altered several times during the history of baseball, with the current version being implemented in 1996.\r\n\r\nPast strike zones\r\n\r\nFrom 1988-95, the strike zone went from the midpoint between the shoulders and the top of the uniform pants, to the top of the knees.\r\nFrom 1969-87, the strike zone went from the batter's armpits to the top of the knees. This strike zone was implemented, along with the lowering of the mound from 15 inches to 10 inches, in response to a 1968 season -- now known as the \"Year of the Pitcher\" -- in which the dominance of hurlers reached new heights.\r\nFrom 1963-68, the strike zone went from the top of the batter's shoulders to the knees.\r\nFrom 1950-62, the strike zone went from the batter's armpits to the top of the knees.\r\nThe version of the strike zone used from 1963-68 was also utilized prior to 1950, going back to the late 1800s.\r\nNote: The box outlined above delineates the borders of the Major League strike zone."
                            },
                            new Rules
                            {
                                RuleName = "Substitutions",
                                RuleDescription = "Definition\r\nTeams are permitted to substitute players any time the ball is dead. The manager must immediately notify the umpire of the switch and substitutes must bat in the replaced player's batting-order position. Once removed, players are not permitted to return to the game in any capacity. Types of substitutions include pinch-hitting, pinch-running, a pitching change and a defensive replacement.\r\n\r\nBarring injury or illness, the starting pitcher must pitch until at least one batter reaches base or is put out. Any substitute pitcher must pitch until at least one batter reaches base or is put out, or the offensive team is put out in some other manner.\r\n\r\nA double-switch refers to the act of substituting two players at once. The tactic is typically used in the National League to bring in a new pitcher when the pitcher's spot in the batting order is due up in the next half-inning. Rather than performing a straight one-for-one swap of pitcher for pitcher, a team subs the new pitcher into the batting-order spot of a non-pitcher and subs another non-pitcher into the removed pitcher's batting-order spot. That way, the team can avoid having the pitcher come to bat in the next half-inning without needing to use additional substitutions for a pinch-hitter and then another pitcher.\r\n\r\nImpending rule changes that will go into effect at the start of the 2020 season\r\n\r\nIn an effort to reduce the number of pitching changes and, in turn, cut down the average time per game, MLB will institute a rule change beginning in 2020 that requires pitchers to either face a minimum of three batters in an appearance or pitch to the end of a half-inning, with exceptions for injuries and illnesses."
                            },
                            new Rules
                            {
                                RuleName = "Suspended Game",
                                RuleDescription = "Definition\r\nA suspended game is a game that is stopped early and must be completed at a later date from the point of termination, though not all terminated games become suspended games.\r\n\r\nThe most frequent cause of a suspended game is when a regulation game is terminated due to weather, and the game is either tied or in the midst of an inning in which the visiting team has taken the lead. Other rarer causes for suspended games include a technical malfunction in the stadium that prevents the game from continuing.\r\n\r\nPrior to the 2020 season, if a regular season game was terminated early before becoming official, the results up to the point of the termination did not count and the game was started over at a later date. But as part of MLB's health and safety protocols during the COVID-19 pandemic, all games cut short before becoming official were resumed at a later date, rather than started over from scratch, during the 2020 campaign. This matched the rules that have been in place since 2008 for all postseason games and any tiebreaker games added to the end of the regular season."
                            },
                            new Rules
                            {
                                RuleName = "Three-batter Minimum",
                                RuleDescription = "Definition\r\nIn an effort to reduce the number of pitching changes and, in turn, cut down the average time per game, MLB instituted a rule change that requires pitchers to either face a minimum of three batters in an appearance or pitch to the end of a half-inning, with exceptions for injuries and illnesses. If a pitcher faces one batter to end an inning, he may be removed, but if he is brought back for a second inning, he must still face two more batters for a total of three.\r\n\r\nPreviously, Rule 5.10(f) stated that the starting pitcher must pitch to one batter until that batter was put out or reached base, and Rule 5.10(g) stated that any reliever must pitch to one batter until that batter was put out or reached base, or the offensive team was put out, with exceptions for injuries and illnesses. These rules were in effect through the end of the 2019 season."
                            },
                            new Rules
                            {
                                RuleName = "Two-way Players",
                                RuleDescription = "Definition\r\nMLB teams must designate every player on the active roster either as a pitcher or a position player and are limited to carrying 13 pitchers on the active roster (14 pitchers from Sept. 1 through the end of the regular season).\r\n\r\nThose designated as position players are unable to pitch unless it is extra innings or their team is ahead or trailing by more than six runs when they take the mound. Teams can also designate players as two-way players if they meet certain criteria. Two-way players are able to pitch in any situation but don't count toward the active roster's pitcher total.\r\n\r\nPlayers qualify for the two-way designation if they have met both of these conditions in either the current or previous MLB season:\r\n\r\n• Pitched at least 20 Major League innings\r\n• Played at least 20 Major League games as a position player or designated hitter, with at least three plate appearances in each game"
                            },
                            new Rules
                            {
                                RuleName = "Umpire",
                                RuleDescription = "Definition\r\nUmpires are responsible for enforcing on-field rules and rendering decisions on judgment calls such as: Whether a batter or baserunner is safe or out, and whether a pitched baseball is a strike or a ball.\r\n\r\nA regular-season contest will have four umpires: one behind home plate and one stationed near each of the other three bases. Each umpire makes \"out\" or \"safe\" decisions at his designated base, and the home-plate umpire is responsible for calling balls and strikes on each pitch that is thrown. The first-base umpire determines whether batted balls were hit into fair or foul territory down the right-field line past the first-base bag, with the third-base umpire doing the same on balls hit down the left-field line beyond the third-base bag. Umpires at first and third base can also assist the home-plate umpire in determining whether a batter offered at a pitch or \"checked his swing.\" If the latter is determined, a pitch outside the strike zone is recorded as a ball.\r\n\r\nUmpires also have the jurisdiction to eject players, coaches or managers who break certain rules or do not display proper on-field conduct.\r\n\r\nUnlike the umpires stationed in the field, the home-plate umpire stands behind the plate and wears a facemask and a heavy chest pad in order to protect against batted balls that are fouled directly backward.\r\n\r\nDuring postseason contests, Major League Baseball adds left-field and right-field umpires to help ensure accurate calls on baseballs hit down the fair/foul lines. Umpires stationed in the outfield can also assess if a batted ball hit in their vicinity was caught on the fly or trapped in a fielder's glove after bouncing on the grass.\r\n\r\nMajor League Baseball also employs replay officials -- full-time Major League umpires who work shifts in the Replay Command Center in New York. Replay officials review all calls subject to replay review and decide whether to change the call on the field, confirm the call on the field or let stand the call on the field due to the lack of clear and convincing evidence. Replay officials also work as on-field umpires during the season."
                            },
                            new Rules
                            {
                                RuleName = "Warmup Pitches",
                                RuleDescription = "Definition\r\nWhen taking their position at the beginning of an inning or when relieving another pitcher, pitchers are permitted to throw as many warmup pitches as they want within the countdown parameters set forth by Major League Baseball.\r\n\r\nThe time between innings and pitching changes is 2 minutes, 5 seconds for local broadcasts, 2 minutes, 25 seconds for nationally televised games and 2 minutes, 55 seconds for tiebreaker and postseason games. The umpire's signal for the final warmup pitch comes at the 25-second mark and the pitcher must throw it before the clock hits 20. The batter will be announced at the 20-second mark and the pitcher must begin his windup to throw the first pitch of the inning within the five seconds before the clock hits zero.\r\n\r\nThe timing clock also applies to pitching changes and begins as soon as the relief pitcher crosses the warning track (or the foul line for on-field bullpens). Players can be excused from these time limits if a delay in normal warmup activities occurs due to no fault of the players, or the umpire believes a player would be at legitimate risk of injury without receiving additional time.\r\n\r\nFor between-innings breaks, the timer begins when the final out of the inning is recorded, with several exceptions. If the pitcher is on base, on deck or at bat when the inning ends, the timer begins when the pitcher leaves the dugout for the mound. If the catcher is on base, on deck or at bat when the inning ends, the timer begins when the catcher enters the dugout (another catcher can begin warming up the pitcher). If the final out of the inning is subject to replay, the timer begins when the umpire signals the out. And for any extended between-innings event previously approved by the Office of the Commissioner (such as the playing of \"God Bless America\"), the timer begins at the conclusion of the event.\r\n\r\nShould a pitcher enter the game to replace one who was removed due to an injury or another emergency situation, he is granted as many warmup pitches as the umpire allows.\r\n\r\nHistory of the rule\r\nPrior to 2018, pitchers were granted up to eight warmup pitches under typical circumstances that didn't include an injury or another emergency situation."
                            },
                            new Rules
                            {
                                RuleName = "Windup Position",
                                RuleDescription = "Definition\r\nPitchers are permitted to use two legal pitching deliveries -- the windup position and the set position -- and either position may be used at any time.\r\n\r\nA pitcher is considered to be in the windup position when he puts his pivot foot on the pitching rubber and has both shoulders facing the batter to some degree. When starting his delivery from the windup position, a pitcher has the option to take a step back or to the side with his free foot. The pitcher then turns his pivot foot to be parallel to the pitching rubber, lifts his free leg up, pushes off the rubber with his pivot foot, strides toward the batter and delivers the pitch to the catcher.\r\n\r\nThe windup position is rarely used with runners on base, as the length of time needed to complete the windup makes pitchers more susceptible to stolen bases if using it with men on base."
                            }

                        );

                        context.SaveChanges();

                    }

					if (!context.PlayerStats.Any())
					{
                        context.PlayerStats.AddRange(
                            new PlayerStats
                            {
                                PlayerID = 1,
                                PA = 250,
                                Runs = 60,
                                Hits = 70,
                                B1 = 45,
                                B2 = 15,
                                B3 = 5,
                                HR = 5,
                                RBI = 55,
                                BB = 50,
                                K = 75,
                                SB = 20,
                                SAC = 3,
                                HBP = 4,
                                GP = 10,
                                GO = 3,
                                PO = 10,
                                FO = 3
                            },
                            new PlayerStats
                            {
                                PlayerID = 2,
                                PA = 200,
                                Runs = 40,
                                Hits = 50,
                                B1 = 35,
                                B2 = 10,
                                B3 = 3,
                                HR = 2,
                                RBI = 30,
                                BB = 25,
                                K = 50,
                                SB = 15,
                                SAC = 2,
                                HBP = 3,
                                GP = 3,
                                GO = 4,
                                PO = 3,
                                FO = 10

                            },
                            new PlayerStats
                            {
                                PlayerID = 3,
                                PA = 300,
                                Runs = 80,
                                Hits = 90,
                                B1 = 60,
                                B2 = 20,
                                B3 = 7,
                                HR = 3,
                                RBI = 70,
                                BB = 60,
                                K = 80,
                                SB = 25,
                                SAC = 4,
                                HBP = 5,
                                GP = 15,
                                GO = 8,
                                PO = 15,
                                FO = 10

                            },
                            new PlayerStats
                            {
                                PlayerID = 4,
                                PA = 250,
                                Runs = 60,
                                Hits = 70,
                                B1 = 45,
                                B2 = 15,
                                B3 = 5,
                                HR = 5,
                                RBI = 55,
                                BB = 50,
                                K = 75,
                                SB = 20,
                                SAC = 3,
                                HBP = 4,
                                GP = 4,
                                GO = 1,
                                PO = 7,
                                FO = 4
                            },
                            new PlayerStats
                            {
                                PlayerID = 5,
                                PA = 200,
                                Runs = 40,
                                Hits = 50,
                                B1 = 35,
                                B2 = 10,
                                B3 = 3,
                                HR = 2,
                                RBI = 30,
                                BB = 25,
                                K = 50,
                                SB = 15,
                                SAC = 2,
                                HBP = 3,
                                GP = 5,
                                GO = 9,
                                PO = 10,
                                FO = 6

                            }

                        ) ;
					    context.SaveChanges();
					}
                    


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

