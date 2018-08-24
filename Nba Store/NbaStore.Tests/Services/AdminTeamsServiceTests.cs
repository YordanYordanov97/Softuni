using AutoMapper;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NbaStore.Data;
using NbaStore.Models;
using NbaStore.Services.Admin;
using NbaStore.Tests.Mocks;
using System.Threading.Tasks;
using System.Linq;
using NbaStore.Common.ViewModels.Admin.Teams;
using NbaStore.Common.BindingModels.Admin.Teams;
using System;
using System.Collections.Generic;
using NbaStore.Common.Constants;

namespace NbaStore.Services.Tests
{
    [TestClass]
    public class AdminTeamsServiceTests
    {
        private NbaStoreDbContext dbContext;
        private IMapper mapper;
        private AdminTeamsService service;

        [TestInitialize]
        public void InitializeTests()
        {
            this.dbContext = MockDbContext.GetContext();
            this.mapper = MockAutoMapper.GetAutoMapper();
            this.service = new AdminTeamsService(this.dbContext, this.mapper);

            this.dbContext.Teams.Add(new Team() { Id = 1, Name = string.Format(TestsConstants.Team, 1),
                LogoUrl=string.Format(TestsConstants.Logo,1)
            });
            this.dbContext.SaveChanges();
        }

        [TestMethod]
        public void GetTeams_WithFewTeams_ShouldReturnAll()
        {
            this.dbContext.Teams.Add(new Team() { Id = 2, Name = string.Format(TestsConstants.Team, 2) });
            this.dbContext.Teams.Add(new Team() { Id = 3, Name = string.Format(TestsConstants.Team, 3) });
            this.dbContext.SaveChanges();

            var teams = this.service.GetTeams().ToList();

            Assert.IsNotNull(teams);
            Assert.AreEqual(3, teams.Count);
            CollectionAssert.AreEqual(new[] { string.Format(TestsConstants.Team, 1),
                string.Format(TestsConstants.Team, 2) ,
                string.Format(TestsConstants.Team, 3)  },
                teams.Select(t => t.Name).ToArray());
        }

        [TestMethod]
        public void GetTeams_WithFewTeams_ShouldReturnTypeofIEnumerableTeamIndexViewModel()
        {
            this.dbContext.Teams.Add(new Team() { Id = 2, Name = string.Format(TestsConstants.Team, 2) });

            this.dbContext.SaveChanges();

            var teams = this.service.GetTeams();

            Assert.IsInstanceOfType(teams, typeof(IEnumerable<TeamIndexViewModel>));
        }

        [TestMethod]
        public void GetTeams_WithZeroTeams_ShouldReturnZero()
        {
            var teamToRemove = this.dbContext.Teams.SingleOrDefault(x => x.Id == 1);
            this.dbContext.Teams.Remove(teamToRemove);
            this.dbContext.SaveChanges();

            var teams = this.service.GetTeams().ToList();

            Assert.IsNotNull(teams);
            Assert.AreEqual(0, teams.Count);
        }

        [TestMethod]
        public async Task GetDetailsAsync_WithId_ShouldReturnTeamDetailsViewModel()
        {
            this.dbContext.Products.Add(new Product() { Title = string.Format(TestsConstants.Product,1),
                TeamId = 1 });
            this.dbContext.Products.Add(new Product() { Title = string.Format(TestsConstants.Product, 2),
                TeamId = 1 });
            this.dbContext.SaveChanges();

            var teamFromService = await this.service.GetDetails(1);

            var teamDetailsViewModel = new TeamDetailsViewModel()
            {
                Name = string.Format(TestsConstants.Team, 1),
                LogoUrl = string.Format(TestsConstants.Logo, 1)
            };

            Assert.AreEqual(teamDetailsViewModel.LogoUrl, teamFromService.LogoUrl);
            Assert.AreEqual(teamDetailsViewModel.Name, teamFromService.Name);
        }

        [TestMethod]
        public async Task GetDetailsAsync_WithId_ShouldReturnTypeofTeamDetailsViewModel()
        {
            var teamFromService = await this.service.GetDetails(1);

            var teamDetailsViewModel = new TeamDetailsViewModel()
            {
                Name = string.Format(TestsConstants.Test, 1),
                LogoUrl = string.Format(TestsConstants.Logo, 1)
            };

            Assert.IsInstanceOfType(teamFromService, typeof(TeamDetailsViewModel));

        }

        [TestMethod]
        public async Task GetTeamAsync_WithId_ShouldReturnThisTeam()
        {
            var teamFromService = await this.service.GetTeam(1);

            var bindingModelTest = new TeamBindingModel()
            {
                Name = string.Format(TestsConstants.Team, 1),
                LogoUrl = string.Format(TestsConstants.Logo, 1)
            };

            Assert.AreEqual(bindingModelTest.LogoUrl, teamFromService.LogoUrl);
            Assert.AreEqual(bindingModelTest.Name, teamFromService.Name);
        }

        [TestMethod]
        public async Task GetTeamAsync_WithId_ShouldReturnTypeofTeamBindingModel()
        {
            var teamFromService = await this.service.GetTeam(1);

            var bindingModelTest = new TeamBindingModel()
            {
                Name = string.Format(TestsConstants.Team, 1),
                LogoUrl = string.Format(TestsConstants.Logo, 1)
            };
            
            Assert.IsInstanceOfType(teamFromService, typeof(TeamBindingModel));
        }

        [TestMethod]
        public async Task GetTeamAsync_WithNoValidId_ShouldThrowArgumentNullException()
        {
            await Assert.ThrowsExceptionAsync<ArgumentNullException>(() =>
             this.service.GetTeam(2));
        }

        [TestMethod]
        public async Task SaveTeamAsync_WithProperTeam_ShouldAddCorrectly()
        {
            var teamToRemove = this.dbContext.Teams.SingleOrDefault(x => x.Id == 1);
            this.dbContext.Teams.Remove(teamToRemove);
            this.dbContext.SaveChanges();

            var teamModel = new TeamBindingModel
            {
                Name = string.Format(TestsConstants.Team, 1),
                LogoUrl = string.Format(TestsConstants.Logo, 1),
            };

            await this.service.SaveTeam(teamModel);

            Assert.AreEqual(1, this.dbContext.Teams.Count());

            var team = this.dbContext.Teams.Last();

            Assert.AreEqual(string.Format(TestsConstants.Team, 1), team.Name);
            Assert.AreEqual(string.Format(TestsConstants.Logo, 1), team.LogoUrl);
        }

        [TestMethod]
        public async Task DeleteTeamAsync_WithId_ShouldDeleteCorrectly()
        {
            await service.DeleteTeam(1);

            Assert.AreEqual(0, this.dbContext.Teams.Count());
        }

        [TestMethod]
        public async Task DeleteTeamAsync_WithNoValidId_ShouldThrowArgumentNullException()
        {
            await Assert.ThrowsExceptionAsync<ArgumentNullException>(() =>
            this.service.DeleteTeam(2));
        }

        [TestMethod]
        public async Task EditTeamAsync_WithId_ShouldEditCorrectly()
        {
            var bindingModel = new TeamBindingModel
            {
                Name = string.Format(TestsConstants.EditTeam, 1),
                LogoUrl = string.Format(TestsConstants.EditLogo, 1)
            };

            await service.EditTeam(1, bindingModel);

            var team = this.dbContext.Teams.SingleOrDefault(x => x.Id == 1);

            Assert.AreEqual(1, team.Id);
            Assert.AreEqual(string.Format(TestsConstants.EditTeam, 1), team.Name);
            Assert.AreEqual(string.Format(TestsConstants.EditLogo, 1), team.LogoUrl);
        }

        [TestMethod]
        public async Task EditTeamAsync_WithNoValidId_ShouldThrowArgumentNullException()
        {
            var bindingModel = new TeamBindingModel
            {
                Name = string.Format(TestsConstants.EditTeam, 1),
                LogoUrl = string.Format(TestsConstants.EditLogo, 1)
            };

            await Assert.ThrowsExceptionAsync<ArgumentNullException>(() =>
            this.service.EditTeam(2, bindingModel));
        }
    }
}
