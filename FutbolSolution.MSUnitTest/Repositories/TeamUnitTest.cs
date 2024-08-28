using FutbolSolution.Core.Models;
using FutbolSolution.Repository.Repositories;
using FutbolSolution.Repository;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FutbolSolution.MSUnitTest.Repositories
{
    [TestClass]
    public class TeamRepositoryTests
    {
        private const string ConnectionString = @"Data Source=localhost:1521/xe;User Id=football;Password=118911;";
        private TeamRepository _repository;
        private AppDbContext _dbContext;

        [TestInitialize]
        public void Setup()
        {
            _dbContext = new AppDbContext(ConnectionString);
            _repository = new TeamRepository(_dbContext);
   
        }

        [TestCleanup]
        public void TearDown()
        {
   
            _dbContext.Dispose();
        }

     

        [TestMethod]
        public async Task AddAsync_Should_Add_Entity()
        {
            // Arrange
            var team = new Team
            {
                Name = "Team A",
                Stadium = "Stadium A",
                Coach = "Coach A",
                FoundedYear = 1990,
                City = "City A"
            };

            // Act
            await _repository.AddAsync(team);

            // Assert
            var result = await _repository.GetByIdAsync(team.TeamId);
            Assert.IsNotNull(result);
            Assert.AreEqual(team.Name, result.Name);
            Assert.AreEqual(team.Stadium, result.Stadium);
        }

        [TestMethod]
        public async Task AddRangeAsync_Should_Add_Entities()
        {
            // Arrange
            var teams = new List<Team>
            {
                new Team { Name = "Team A", Stadium = "Stadium A", Coach = "Coach A", FoundedYear = 1990, City = "City A" },
                new Team { Name = "Team B", Stadium = "Stadium B", Coach = "Coach B", FoundedYear = 1995, City = "City B" }
            };

            // Act
            await _repository.AddRangeAsync(teams);

            // Assert
            var result = await _repository.GetAllAsync();
            Assert.AreEqual(2, result.Count());
            Assert.IsTrue(result.Any(t => t.Name == "Team A"));
            Assert.IsTrue(result.Any(t => t.Name == "Team B"));
        }

        [TestMethod]
        public async Task GetAllAsync_Should_Return_All_Entities()
        {
            // Arrange
            var team = new Team
            {
                Name = "Team A",
                Stadium = "Stadium A",
                Coach = "Coach A",
                FoundedYear = 1990,
                City = "City A"
            };
            await _repository.AddAsync(team);

            // Act
            var result = await _repository.GetAllAsync();

            // Assert
            Assert.IsTrue(result.Any(t => t.Name == "Team A"));
        }

        [TestMethod]
        public async Task GetByIdAsync_Should_Return_Entity()
        {
            // Arrange
            var team = new Team
            {
                Name = "Team A",
                Stadium = "Stadium A",
                Coach = "Coach A",
                FoundedYear = 1990,
                City = "City A"
            };
            await _repository.AddAsync(team);

            // Act
            var result = await _repository.GetByIdAsync(team.TeamId);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(team.Name, result.Name);
        }

        [TestMethod]
        public async Task RemoveAsync_Should_Remove_Entity()
        {
            // Arrange
            var team = new Team
            {
                Name = "Team A",
                Stadium = "Stadium A",
                Coach = "Coach A",
                FoundedYear = 1990,
                City = "City A"
            };
            await _repository.AddAsync(team);

            // Act
            await _repository.RemoveAsync(team);

            // Assert
            var result = await _repository.GetByIdAsync(team.TeamId);
            Assert.IsNull(result);
        }

        [TestMethod]
        public async Task UpdateAsync_Should_Update_Entity()
        {
            // Arrange
            var team = new Team
            {
                Name = "Team A",
                Stadium = "Stadium A",
                Coach = "Coach A",
                FoundedYear = 1990,
                City = "City A"
            };
            await _repository.AddAsync(team);

            // Act
            team.Name = "Updated Team";
            team.Stadium = "Updated Stadium";
            await _repository.UpdateAsync(team);

            // Assert
            var result = await _repository.GetByIdAsync(team.TeamId);
            Assert.IsNotNull(result);
            Assert.AreEqual("Updated Team", result.Name);
            Assert.AreEqual("Updated Stadium", result.Stadium);
        }
    }

}
