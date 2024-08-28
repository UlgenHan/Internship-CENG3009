using FutbolSolution.Core.Models;
using FutbolSolution.Core.Repositories;
using FutbolSolution.Repository;
using FutbolSolution.Repository.Repositories;
using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Remoting.Contexts;
using System.Threading.Tasks;

namespace FutbolSolution.CustomTest
{
    public class Program
    {
        static async Task Main(string[] args)
        {
            //string connectionString = @"Data Source=localhost:1521/xe;User Id=football;Password=118911;";
            //AppDbContext appDbContext = new AppDbContext(connectionString);
            //IGenericRepository<Player> playerRepository = new PlayerRepository(appDbContext);

            //Player player = new Player()
            //{ Name="ali",Surname="veli",Age="32", DateOfBirth="04-02-1999",Nationality="TRöl",Position="Forvet",CurrentClub="Fenerbahçe",Height="1.43",Weight="50",PreferredFoot ="lEFT" };


            //var result = playerRepository.AddAsync(player);
            //Console.ReadLine();


            //string connectionString = @"Data Source=localhost:1521/xe;User Id=football;Password=118911;";
            //AppDbContext appDbContext = new AppDbContext(connectionString);
            //var teamRepository = new TeamRepository(appDbContext);

            //// Test AddAsync
            //var newTeam = new Team
            //{
            //    Name = "Test Team",
            //    Stadium = "Test Stadium",
            //    Coach = "Test Coach",
            //    FoundedYear = 2024,
            //    City = "Test City"
            //};

            //Console.WriteLine("Adding a new team...");
            // await teamRepository.AddAsync(newTeam);

            //var item = await teamRepository.GetByIdAsync(63);

            //Console.WriteLine($"{item.TeamId} , {item.Name} , {item.FoundedYear} , {item.Coach} , {item.City}");

            //item.Name = "Updated Team";
            //await teamRepository.UpdateAsync(item);
            //Console.WriteLine("Team updated.");

            //var teams = await teamRepository.GetAllAsync();
            //foreach (var team in teams)
            //{
            //    Console.WriteLine($"Team: {team.Name}, {team.Stadium}, {team.Coach}");
            //}

            //// Test RemoveAsync
            //Console.WriteLine("Removing the team...");
            //await teamRepository.RemoveAsync(item);
            //Console.WriteLine("Team removed.");
            //Console.ReadLine();


            string connectionString = @"Data Source=localhost:1521/xe;User Id=football;Password=118911;";
            //AppDbContext appDbContext = new AppDbContext(connectionString);

            //var teamStatsRepository = new TeamStatisticsRepository(appDbContext);
            //// Test AddAsync
            //var newTeamStats = new TeamStatistics
            //{
            //    TeamId = 1, // Adjust as needed
            //    SeasonYear = "2024",
            //    GoalsScored = 50,
            //    GoalsConceded = 30,
            //    Wins = 15,
            //    Draws = 5,
            //    Losses = 10,
            //    HomeWins = 10,
            //    AwayWins = 5,
            //    RecentForm = "W-D-W-L-W"
            //};

            //Console.WriteLine("Adding a new team statistics entry...");
            //await teamStatsRepository.AddAsync(newTeamStats);
            //Console.WriteLine($"Added with ID: {newTeamStats.TeamStatsId}");

            // Test GetByIdAsync
            //Console.WriteLine("Retrieving team statistics by ID...");
            //var retrievedStats = await teamStatsRepository.GetByIdAsync(newTeamStats.TeamStatsId);
            //if (retrievedStats != null)
            //{
            //    Console.WriteLine($"Retrieved: {retrievedStats.TeamId}, {retrievedStats.SeasonYear}, {retrievedStats.GoalsScored}");
            //}
            //else
            //{
            //    Console.WriteLine("Team statistics not found.");
            //}

            // Test GetAllAsync
            //Console.WriteLine("Retrieving all team statistics...");
            //var allStats = await teamStatsRepository.GetAllAsync();
            //foreach (var stats in allStats)
            //{
            //    Console.WriteLine($"Team Stats: ID={stats.TeamStatsId}, TeamId={stats.TeamId}, SeasonYear={stats.SeasonYear}");
            //}
            //Console.ReadLine();

            //// Test UpdateAsync
            //Console.WriteLine("Updating team statistics...");
            //retrievedStats.GoalsScored = 55; // Update a value for testing
            //await teamStatsRepository.UpdateAsync(retrievedStats);
            //Console.WriteLine("Updated successfully.");

            //// Retrieve and display updated stats
            //var updatedStats = await teamStatsRepository.GetByIdAsync(retrievedStats.TeamStatsId);
            //if (updatedStats != null)
            //{
            //    Console.WriteLine($"Updated: {updatedStats.TeamId}, {updatedStats.SeasonYear}, {updatedStats.GoalsScored}");
            //}

            //// Test RemoveAsync
            //Console.WriteLine("Removing the team statistics...");
            //await teamStatsRepository.RemoveAsync(retrievedStats);
            //Console.WriteLine("Removed successfully.");

            //// Verify removal
            //var removedStats = await teamStatsRepository.GetByIdAsync(retrievedStats.TeamStatsId);
            //if (removedStats == null)
            //{
            //    Console.WriteLine("Team statistics successfully removed.");
            //}
            //else
            //{
            //    Console.WriteLine("Failed to remove team statistics.");
            //}


            //var dbContext = new AppDbContext(connectionString);
            //var refereeRepository = new RefereeRepository(dbContext);

            //// Test AddAsync
            //var newReferee = new Referee
            //{
            //    Name = "John",
            //    Surname = "Doe",
            //    Nationality = "American",
            //    ExperienceYears = 5,
            //    Bias = 1
            //};

            //Console.WriteLine("Adding a new referee entry...");
            //await refereeRepository.AddAsync(newReferee);
            //Console.WriteLine($"Added with ID: {newReferee.RefereeId}");

            //// Test GetByIdAsync
            //Console.WriteLine("Retrieving referee by ID...");
            //var retrievedReferee = await refereeRepository.GetByIdAsync(newReferee.RefereeId);
            //if (retrievedReferee != null)
            //{
            //    Console.WriteLine($"Retrieved: {retrievedReferee.Name}, {retrievedReferee.Surname}, {retrievedReferee.Nationality}");
            //}
            //else
            //{
            //    Console.WriteLine("Referee not found.");
            //}

            //// Test GetAllAsync
            //Console.WriteLine("Retrieving all referees...");
            //var allReferees = await refereeRepository.GetAllAsync();
            //foreach (var referee in allReferees)
            //{
            //    Console.WriteLine($"Referee: ID={referee.RefereeId}, Name={referee.Name}, Surname={referee.Surname} , {referee.ExperienceYears}");
            //}

            // Test UpdateAsync
            //Console.WriteLine("Updating referee...");
            //retrievedReferee.ExperienceYears = 6; // Update a value for testing
            //await refereeRepository.UpdateAsync(retrievedReferee);
            //Console.WriteLine("Updated successfully.");

            //// Retrieve and display updated referee
            //var updatedReferee = await refereeRepository.GetByIdAsync(retrievedReferee.RefereeId);
            //if (updatedReferee != null)
            //{
            //    Console.WriteLine($"Updated: {updatedReferee.Name}, {updatedReferee.Surname}, {updatedReferee.ExperienceYears}");
            //}

            //// Test RemoveAsync
            //Console.WriteLine("Removing the referee...");
            //await refereeRepository.RemoveAsync(retrievedReferee);
            //Console.WriteLine("Removed successfully.");

            //// Verify removal
            //var removedReferee = await refereeRepository.GetByIdAsync(retrievedReferee.RefereeId);
            //if (removedReferee == null)
            //{
            //    Console.WriteLine("Referee successfully removed.");
            //}
            //else
            //{
            //    Console.WriteLine("Failed to remove referee.");
            //}

            // Initialize the DbContext and Repository
            //var dbContext = new AppDbContext(connectionString);
            //var playerRepository = new PlayerRepository(dbContext);


            //// Test AddAsync
            //var newPlayer = new Player
            //{
            //    Name = "Lionel",
            //    Surname = "Messi",
            //    Age = 34,
            //    DateOfBirth = new DateTime(1987, 6, 24),
            //    Nationality = "Argentinian",
            //    Position = "Forward",
            //    CurrentClub = "Paris Saint-Germain",
            //    Height = 1.69m,
            //    Weight = 67m,
            //    PreferredFoot = "Left"
            //};

            //Console.WriteLine("Adding a new player entry...");
            //await playerRepository.AddAsync(newPlayer);
            //Console.WriteLine($"Added with ID: {newPlayer.Id}");

            //// Test GetByIdAsync
            //Console.WriteLine("Retrieving player by ID...");
            //var retrievedPlayer = await playerRepository.GetByIdAsync(newPlayer.Id);
            //if (retrievedPlayer != null)
            //{
            //    Console.WriteLine($"Retrieved: {retrievedPlayer.Name} {retrievedPlayer.Surname}, {retrievedPlayer.Nationality}, {retrievedPlayer.Position}");
            //}
            //else
            //{
            //    Console.WriteLine("Player not found.");
            //}

            //// Test GetAllAsync
            //Console.WriteLine("Retrieving all players...");
            //var allPlayers = await playerRepository.GetAllAsync();
            //foreach (var player in allPlayers)
            //{
            //    Console.WriteLine($"Player: ID={player.Id}, Name={player.Name}, Surname={player.Surname}");
            //}

            //// Test UpdateAsync
            //Console.WriteLine("Updating player...");
            //retrievedPlayer.Age = 49; // Update a value for testing
            //await playerRepository.UpdateAsync(retrievedPlayer);
            //Console.WriteLine("Updated successfully.");

            //// Retrieve and display updated player
            //var updatedPlayer = await playerRepository.GetByIdAsync(retrievedPlayer.Id);
            //if (updatedPlayer != null)
            //{
            //    Console.WriteLine($"Updated: {updatedPlayer.Name} {updatedPlayer.Surname}, {updatedPlayer.Age}");
            //}

            ////// Test RemoveAsync
            ////Console.WriteLine("Removing the player...");
            ////await playerRepository.RemoveAsync(retrievedPlayer);
            ////Console.WriteLine("Removed successfully.");

            //// Verify removal
            //var removedPlayer = await playerRepository.GetByIdAsync(retrievedPlayer.Id);
            //if (removedPlayer == null)
            //{
            //    Console.WriteLine("Player successfully removed.");
            //}
            //else
            //{
            //    Console.WriteLine("Failed to remove player.");
            //}


            //var dbContext = new AppDbContext(connectionString); // Initialize your DbContext here
            //var repository = new PlayerStatsRepository(dbContext);

            //Console.WriteLine("Starting PlayerStatsRepository tests...");

            //// Add a new player stat
            //var newPlayerStats = new PlayerStats
            //{
            //    Goals = 10,
            //    Assists = 5,
            //    TotalMinutesIn = 900,
            //    PassAccuracy = 85.5m,
            //    Tackles = 50,
            //    Interceptions = 30,
            //    Clearances = 40,
            //    Shots = 20,
            //    ShotsOnTarget = 10,
            //    DribblesCompleted = 15,
            //    AerialDuelsWon = 8,
            //    YellowCards = 2,
            //    RedCards = 1,
            //    FoulsCommitted = 10,
            //    FoulsSuffered = 12,
            //    Offsides = 3,
            //    Saves = 0,
            //    CleanSheets = 0
            //};

            //await repository.AddAsync(newPlayerStats);
            //Console.WriteLine("Added new player stats with ID: " + newPlayerStats.PlayerStatsId);

            //// Get all player stats
            //var allPlayerStats = await repository.GetAllAsync();
            //Console.WriteLine("All player stats:");
            //foreach (var stats in allPlayerStats)
            //{
            //    Console.WriteLine($"ID: {stats.PlayerStatsId}, Goals: {stats.Goals}, Assists: {stats.Assists}");
            //}

            //// Update the player stats
            //newPlayerStats.Goals = 12;
            //await repository.UpdateAsync(newPlayerStats);
            //Console.WriteLine("Updated player stats.");

            //// Get the updated player stats by ID
            //var updatedPlayerStats = await repository.GetByIdAsync(newPlayerStats.PlayerStatsId);
            //Console.WriteLine($"Updated PlayerStats - ID: {updatedPlayerStats.PlayerStatsId}, Goals: {updatedPlayerStats.Goals}, Assists: {updatedPlayerStats.Assists}");

            //// Remove the player stats
            //await repository.RemoveAsync(newPlayerStats);
            //Console.WriteLine("Removed player stats.");

            //Console.WriteLine("PlayerStatsReposit/ory tests completed.");

            //var dbContext = new AppDbContext(connectionString); // Initialize your DbContext here
            //var repository = new PlayerTeamLinkRepository(dbContext);

            //Console.WriteLine("Starting PlayerTeamLinkRepository tests...");

            //// Add a new player-team link
            //var newPlayerTeamLink = new PlayerTeamLink
            //{
            //    PlayerId = 8,
            //    TeamId = 1,
            //    StartDate = DateTime.Now,
            //    EndDate = DateTime.Now.AddYears(1),
            //    IsLoan = "No"
            //};

            //await repository.AddAsync(newPlayerTeamLink);
            //Console.WriteLine("Added new player-team link with ID: " + newPlayerTeamLink.PlayerTeamLinkId);

            //// Get all player-team links
            //var allPlayerTeamLinks = await repository.GetAllAsync();
            //Console.WriteLine("All player-team links:");
            //foreach (var link in allPlayerTeamLinks)
            //{
            //    Console.WriteLine($"ID: {link.PlayerTeamLinkId}, PlayerId: {link.PlayerId}, TeamId: {link.TeamId}");
            //}

            //// Update the player-team link
            //newPlayerTeamLink.IsLoan = "Yes";
            //await repository.UpdateAsync(newPlayerTeamLink);
            //Console.WriteLine("Updated player-team link.");

            //// Get the updated player-team link by ID
            //var updatedPlayerTeamLink = await repository.GetByIdAsync(newPlayerTeamLink.PlayerTeamLinkId);
            //Console.WriteLine($"Updated PlayerTeamLink - ID: {updatedPlayerTeamLink.PlayerTeamLinkId}, PlayerId: {updatedPlayerTeamLink.PlayerId}, TeamId: {updatedPlayerTeamLink.TeamId}, IsLoan: {updatedPlayerTeamLink.IsLoan}");

            //// Remove the player-team link
            //await repository.RemoveAsync(newPlayerTeamLink);
            //Console.WriteLine("Removed player-team link.");

            //Console.WriteLine("PlayerTeamLinkRepository tests completed.");


            // Create a new match entity
            var dbContext = new AppDbContext(connectionString);
            //var matchRepository = new MatchRepository(dbContext);
            //var newMatch = new Match
            //{
            //    HomeTeamId = 1,
            //    AwayTeamId = 64,
            //    MatchDate = DateTime.Now,
            //    Stadium = "Stadium Name",
            //    RefereeId = 3,
            //    WeatherConditions = "Sunny",
            //    Importance = "High"
            //};

            //// Add the new match
            //await matchRepository.AddAsync(newMatch);
            //Console.WriteLine($"Added match with ID: {newMatch.MatchId}");

            //// Retrieve all matches
            //var matches = await matchRepository.GetAllAsync();
            //Console.WriteLine("All Matches:");
            //foreach (var match in matches)
            //{
            //    Console.WriteLine($"Match ID: {match.MatchId}, Home Team ID: {match.HomeTeamId}, Away Team ID: {match.AwayTeamId}, Date: {match.MatchDate}");
            //}

            //// Retrieve a match by ID
            //var retrievedMatch = await matchRepository.GetByIdAsync(newMatch.MatchId);
            //Console.WriteLine($"Retrieved match with ID: {retrievedMatch.MatchId}, Home Team ID: {retrievedMatch.HomeTeamId}, Away Team ID: {retrievedMatch.AwayTeamId}, Date: {retrievedMatch.MatchDate}");

            //// Update the match
            //retrievedMatch.Stadium = "Updated Stadium Name";
            //await matchRepository.UpdateAsync(retrievedMatch);
            //Console.WriteLine($"Updated match with ID: {retrievedMatch.MatchId}");

            //// Retrieve the updated match
            //var updatedMatch = await matchRepository.GetByIdAsync(retrievedMatch.MatchId);
            //Console.WriteLine($"Updated match with ID: {updatedMatch.MatchId}, Stadium: {updatedMatch.Stadium}");

            //// Remove the match
            //await matchRepository.RemoveAsync(updatedMatch);
            //Console.WriteLine($"Removed match with ID: {updatedMatch.MatchId}");

            ////// Ensure the match is removed
            ////var removedMatch = await matchRepository.GetByIdAsync(updatedMatch.MatchId);
            ////if (removedMatch == null)
            ////{
            ////    Console.WriteLine("Match successfully removed.");
            ////}
            ////else
            ////{
            ////    Console.WriteLine("Failed to remove match.");
            ////}
            //Console.ReadLine();

            //var matchPerformanceRepository = new MatchPerformanceRepository(dbContext);

            //// Test AddAsync
            //var newMatchPerformance = new MatchPerformance
            //{
            //    PlayerId = 8,
            //    MatchId =1,
            //    Goals = 2,
            //    Assists = 1,
            //    MinutesPlayed = 90,
            //    PassAccuracy = 85.5m,
            //    Tackles = 3,
            //    Interceptions = 2,
            //    Clearances = 5,
            //    Shots = 4,
            //    ShotsOnTarget = 2,
            //    DribblesCompleted = 3,
            //    AerialDuelsWon = 1,
            //    YellowCards = 0,
            //    RedCards = 0,
            //    FoulsCommitted = 1,
            //    FoulsSuffered = 2,
            //    Offsides = 1,
            //    Saves = 0,
            //    CleanSheets = 0
            //};

            ////await matchPerformanceRepository.AddAsync(newMatchPerformance);
            ////Console.WriteLine($"Added MatchPerformance with ID: {newMatchPerformance.MatchPerformanceId}");

            ////// Test GetAllAsync
            ////var allMatchPerformances = await matchPerformanceRepository.GetAllAsync();
            //////Console.WriteLine($"Total MatchPerformances: {allMatchPerformances.Count()}");

            ////// Test GetByIdAsync
            ////var fetchedMatchPerformance = await matchPerformanceRepository.GetByIdAsync(newMatchPerformance.MatchPerformanceId);
            ////Console.WriteLine($"Fetched MatchPerformance with ID: {fetchedMatchPerformance.MatchPerformanceId}");

            ////// Test UpdateAsync
            ////fetchedMatchPerformance.Goals = 3;
            ////await matchPerformanceRepository.UpdateAsync(fetchedMatchPerformance);
            //var updatedMatchPerformance = await matchPerformanceRepository.GetByIdAsync(19);
            ////// Test RemoveAsync
            ////await matchPerformanceRepository.RemoveAsync(fetchedMatchPerformance);
            ////var remainingMatchPerformances = await matchPerformanceRepository.GetAllAsync();
            ////Console.WriteLine($"Remaining MatchPerformances: {remainingMatchPerformances.Count()}");

            //// Test AddRangeAsync and RemoveRangeAsync
            //await matchPerformanceRepository.RemoveAsync(updatedMatchPerformance);

            //Console.WriteLine("Console test completed.");
            //Console.ReadLine();

            //var repository = new MatchStatisticsRepository(dbContext);
            //var newStats = new MatchStatistics
            //{
            //    MatchId = 1,
            //    HomeGoals = 2,
            //    AwayGoals = 1,
            //    HomePossession = 55.5m,
            //    AwayPossession = 44.5m,
            //    HomeShots = 10,
            //    AwayShots = 8,
            //    HomeShotsOnTarget = 5,
            //    AwayShotsOnTarget = 3,
            //    HomeFouls = 15,
            //    AwayFouls = 12,
            //    HomeYellowCards = 2,
            //    AwayYellowCards = 3,
            //    HomeRedCards = 0,
            //    AwayRedCards = 1
            //};

            //await repository.AddAsync(newStats);
            //Console.WriteLine($"Added MatchStatistics with ID: {newStats.MatchStatsId}");

            //// Test GetByIdAsync
            //var statsById = await repository.GetByIdAsync(newStats.MatchStatsId);
            //Console.WriteLine($"Retrieved MatchStatistics with ID: {statsById.MatchStatsId}, HomeGoals: {statsById.HomeGoals}, AwayGoals: {statsById.AwayGoals}");

            //// Test UpdateAsync
            //statsById.HomeGoals = 3;
            //await repository.UpdateAsync(statsById);
            //var updatedStats = await repository.GetByIdAsync(statsById.MatchStatsId);
            //Console.WriteLine($"Updated MatchStatistics with ID: {updatedStats.MatchStatsId}, HomeGoals: {updatedStats.HomeGoals}");

            //// Test GetAllAsync
            //var allStats = await repository.GetAllAsync();
            //foreach (var stats in allStats)
            //{
            //    Console.WriteLine($"MatchStatsId: {stats.MatchStatsId}, HomeGoals: {stats.HomeGoals}, AwayGoals: {stats.AwayGoals}");
            //}

            //// Test RemoveAsync
            //await repository.RemoveAsync(updatedStats);
            //Console.WriteLine($"Removed MatchStatistics with ID: {updatedStats.MatchStatsId}");

            //// Test AddRangeAsync and RemoveRangeAsync
            //var statsList = new List<MatchStatistics>
            //{
            //    new MatchStatistics { MatchId = 2, HomeGoals = 1, AwayGoals = 1, HomePossession = 50.0m, AwayPossession = 50.0m, HomeShots = 12, AwayShots = 10, HomeShotsOnTarget = 4, AwayShotsOnTarget = 4, HomeFouls = 10, AwayFouls = 10, HomeYellowCards = 1, AwayYellowCards = 1, HomeRedCards = 0, AwayRedCards = 0 },
            //    new MatchStatistics { MatchId = 3, HomeGoals = 0, AwayGoals = 2, HomePossession = 40.0m, AwayPossession = 60.0m, HomeShots = 5, AwayShots = 15, HomeShotsOnTarget = 2, AwayShotsOnTarget = 8, HomeFouls = 8, AwayFouls = 14, HomeYellowCards = 2, AwayYellowCards = 2, HomeRedCards = 1, AwayRedCards = 0 }
            //};

            //await repository.AddRangeAsync(statsList);
            //Console.WriteLine("Added multiple MatchStatistics.");

            //foreach (var stats in statsList)
            //{
            //    Console.WriteLine($"Added MatchStatistics with ID: {stats.MatchStatsId}");
            //}

            //await repository.RemoveRangeAsync(statsList);
            //Console.WriteLine("Removed multiple MatchStatistics.");


            //Console.ReadLine();

            //var repository = new InjuriesSuspensionsRepository(dbContext);

            //// Create a new InjuriesSuspensions entity
            //var newInjurySuspension = new InjuriesSuspensions
            //{
            //    PlayerId = 8,
            //    Type = "Injury",
            //    Description = "Test injury",
            //    StartDate = DateTime.Now,
            //    EndDate = DateTime.Now.AddDays(7)
            //};

            //// Add the entity
            //await repository.AddAsync(newInjurySuspension);
            //Console.WriteLine($"Added: {newInjurySuspension.InjurySuspensionId}");

            //// Get the entity by id
            //var fetchedInjurySuspension = await repository.GetByIdAsync(newInjurySuspension.InjurySuspensionId);
            //Console.WriteLine($"Fetched: {fetchedInjurySuspension.Description}");

            //// Update the entity
            //fetchedInjurySuspension.Description = "Updated test injury";
            //await repository.UpdateAsync(fetchedInjurySuspension);
            //Console.WriteLine("Updated");

            //// Get all entities
            //var allInjuriesSuspensions = await repository.GetAllAsync();
            ////Console.WriteLine($"Total injuries/suspensions: {allInjuriesSuspensions.Count()}");

            //// Remove the entity
            //await repository.RemoveAsync(fetchedInjurySuspension);
            //Console.WriteLine("Removed");

            //Console.WriteLine("Tests completed.");
            //var playerRepo = new PlayerRepository(dbContext);

            //var player = new Player
            //{
            //    Name = "John",
            //    Surname = "Doe",
            //    Age = 25,
            //    DateOfBirth = new DateTime(1999, 5, 15),
            //    Nationality = "USA",
            //    Position = "Forward",
            //    CurrentClub = "Club A",
            //    Height = 180,
            //    Weight = 75,
            //    PreferredFoot = "Right"
            //};

            //await playerRepo.AddAsync(player);

            //// Create and Add a Player Image
            //var imagePath = @"D:\ziraatTeknoloji\OracleDbConnection\Futbol\FutbolSolution\FutbolSolution.CustomTest\gsforma.png";
            //var imageData = File.ReadAllBytes(imagePath);

            //var playerImage = new PlayerImage
            //{
            //    PlayerId = player.Id,
            //    ImageData = imageData
            //};

            //await playerRepo.AddImageAsync(playerImage);
            //Console.WriteLine("Image added successfully.");

            //// Update Player Image
            //var updatedImagePath = "path_to_your_updated_image_file.jpg";
            //var updatedImageData = File.ReadAllBytes(updatedImagePath);

            //var updatedPlayerImage = new PlayerImage
            //{
            //    PlayerId = player.Id,
            //    ImageData = updatedImageData
            //};

            //await playerRepo.UpdateImageAsync(updatedPlayerImage);
            //Console.WriteLine("Image updated successfully.");

            //// Retrieve and Display the Image
            //var retrievedImage = await playerRepo.GetImageAsync(39);
            //Console.WriteLine($"Retrieved image size: {retrievedImage.ImageData.Length} bytes");

            //// Remove Player Image
            //await playerRepo.RemoveImageAsync(39);
            //Console.WriteLine("Image removed successfully.");

            ////// Clean up player record (assume this method exists)
            ////await playerRepo.RemoveAsync(player);

            //var teamRepository = new TeamRepository(dbContext);

            //var team = new Team()
            //{
            //    City = "İstanbul",
            //    Name = "gs",
            //    FoundedYear = 1999,
            //    Stadium = "ıfnromat",
            //    Coach = "fatih terim",
            //};

            // await teamRepository.AddAsync(team);

            //var imagePath = @"D:\ziraatTeknoloji\OracleDbConnection\Futbol\FutbolSolution\FutbolSolution.CustomTest\gsforma.png";
            //var imageData = File.ReadAllBytes(imagePath);

            //var teamImage = new TeamImage() { ImageData = imageData, TeamId = team.TeamId };

            //await teamRepository.AddImageAsync(teamImage);



            //Console.ReadLine();
        }
    }
}
