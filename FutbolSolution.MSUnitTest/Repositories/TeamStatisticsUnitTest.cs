using FutbolSolution.Core.Models;
using FutbolSolution.Repository;
using FutbolSolution.Repository.Repositories;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FutbolSolution.MSUnitTest.Repositories
{
    [TestClass]
    public class TeamStatisticsRepositoryTests
    {
        private const string ConnectionString = @"Data Source=localhost:1521/xe;User Id=football;Password=118911;";
        private TeamStatisticsRepository _repository;
        private AppDbContext _dbContext;

        [TestInitialize]
        public void Setup()
        {
            _dbContext = new AppDbContext(ConnectionString);
            _repository = new TeamStatisticsRepository(_dbContext);
        }

        [TestCleanup]
        public async Task TearDown()
        {
            if (_dbContext != null)
            {
                // Clean up the database to ensure a known state for the next test
                //await _dbContext.Database.ExecuteSqlCommandAsync("DELETE FROM TeamStatistics");
                _dbContext.Dispose();
            }
        }

        [TestMethod]
        public async Task AddAsync_Should_Add_Entity()
        {
            // Arrange
            var teamStatistics = new TeamStatistics
            {
                TeamId = 1,
                SeasonYear = "2023",
                GoalsScored = 30,
                GoalsConceded = 15,
                Wins = 10,
                Draws = 5,
                Losses = 5,
                HomeWins = 6,
                AwayWins = 4,
                RecentForm = "WWLDW"
            };

            // Act
            await _repository.AddAsync(teamStatistics);

            // Assert
            var result = await _repository.GetByIdAsync(teamStatistics.TeamStatsId);
            Assert.IsNotNull(result);
            Assert.AreEqual(teamStatistics.TeamId, result.TeamId);
        }

        [TestMethod]
        public async Task GetByIdAsync_Should_Return_Entity()
        {
            // Arrange
            var teamStatistics = new TeamStatistics
            {
                TeamId = 1,
                SeasonYear = "2023",
                GoalsScored = 30,
                GoalsConceded = 15,
                Wins = 10,
                Draws = 5,
                Losses = 5,
                HomeWins = 6,
                AwayWins = 4,
                RecentForm = "WWLDW"
            };
            await _repository.AddAsync(teamStatistics);

            // Act
            var result = await _repository.GetByIdAsync(teamStatistics.TeamStatsId);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(teamStatistics.TeamStatsId, result.TeamStatsId);
        }

        [TestMethod]
        public async Task GetAllAsync_Should_Return_All_Entities()
        {
            // Arrange
            var teamStatistics1 = new TeamStatistics
            {
                TeamId = 1,
                SeasonYear = "2023",
                GoalsScored = 30,
                GoalsConceded = 15,
                Wins = 10,
                Draws = 5,
                Losses = 5,
                HomeWins = 6,
                AwayWins = 4,
                RecentForm = "WWLDW"
            };
            var teamStatistics2 = new TeamStatistics
            {
                TeamId = 2,
                SeasonYear = "2023",
                GoalsScored = 25,
                GoalsConceded = 20,
                Wins = 8,
                Draws = 6,
                Losses = 6,
                HomeWins = 5,
                AwayWins = 3,
                RecentForm = "LWWDL"
            };
            await _repository.AddAsync(teamStatistics1);
            await _repository.AddAsync(teamStatistics2);

            // Act
            var result = await _repository.GetAllAsync();

            // Assert
            Assert.AreEqual(2, result.Count());
        }

        [TestMethod]
        public async Task RemoveAsync_Should_Remove_Entity()
        {
            // Arrange
            var teamStatistics = new TeamStatistics
            {
                TeamId = 1,
                SeasonYear = "2023",
                GoalsScored = 30,
                GoalsConceded = 15,
                Wins = 10,
                Draws = 5,
                Losses = 5,
                HomeWins = 6,
                AwayWins = 4,
                RecentForm = "WWLDW"
            };
            await _repository.AddAsync(teamStatistics);

            // Act
            await _repository.RemoveAsync(teamStatistics);

            // Assert
            var result = await _repository.GetByIdAsync(teamStatistics.TeamStatsId);
            Assert.IsNull(result);
        }

        [TestMethod]
        public async Task UpdateAsync_Should_Update_Entity()
        {
            // Arrange
            var teamStatistics = new TeamStatistics
            {
                TeamId = 1,
                SeasonYear = "2023",
                GoalsScored = 30,
                GoalsConceded = 15,
                Wins = 10,
                Draws = 5,
                Losses = 5,
                HomeWins = 6,
                AwayWins = 4,
                RecentForm = "WWLDW"
            };
            await _repository.AddAsync(teamStatistics);

            // Act
            teamStatistics.GoalsScored = 35;
            await _repository.UpdateAsync(teamStatistics);

            // Assert
            var result = await _repository.GetByIdAsync(teamStatistics.TeamStatsId);
            Assert.IsNotNull(result);
            Assert.AreEqual(35, result.GoalsScored);
        }
    }
}
