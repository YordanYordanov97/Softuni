using AutoMapper;
using Microsoft.EntityFrameworkCore;
using NbaStore.Common.BindingModels.Admin.Teams;
using NbaStore.Common.ViewModels.Admin.Teams;
using NbaStore.Data;
using NbaStore.Models;
using NbaStore.Services.Admin.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NbaStore.Services.Admin
{
    public class AdminTeamsService : BaseEFService, IAdminTeamsService
    {
        public AdminTeamsService(NbaStoreDbContext dbContext, IMapper mapper) 
            : base(dbContext, mapper)
        {
        }
        
        public IEnumerable<TeamIndexViewModel> GetTeams()
        {
            var teams = this.DbContext.Teams
                .Include(p=>p.Products)
                .ToList();

            var model= this.Mapper.Map<IEnumerable<TeamIndexViewModel>>(teams);

            return model;
        }

        public async Task<TeamDetailsViewModel> GetDetails(int id)
        {
            var team = await this.DbContext.Teams
                .Include(p => p.Products)
                .SingleOrDefaultAsync(x => x.Id == id);
            this.CheckIfTeamExist(team);

            var model= this.Mapper.Map<TeamDetailsViewModel>(team);

            return model;
        }

        public async Task SaveTeam(TeamBindingModel model)
        {
            var team = this.Mapper.Map<Team>(model);
            await this.DbContext.Teams.AddAsync(team);
            await this.DbContext.SaveChangesAsync();
        }

        public async Task<TeamBindingModel> GetTeam(int id)
        {
            var team = await this.DbContext.Teams.FindAsync(id);
            this.CheckIfTeamExist(team);

            return this.Mapper.Map<TeamBindingModel>(team);
        }

        public async Task DeleteTeam(int id)
        {
            var team = await this.DbContext.Teams.FindAsync(id);
            this.CheckIfTeamExist(team);

            this.DbContext.Remove(team);
            await this.DbContext.SaveChangesAsync();
        }

        public async Task EditTeam(int id,TeamBindingModel model)
        {
            var team = await this.DbContext.Teams.FindAsync(id);
            this.CheckIfTeamExist(team);

            team.Name = model.Name;
            team.LogoUrl = model.LogoUrl;

            await this.DbContext.SaveChangesAsync();
        }

        private void CheckIfTeamExist(Team team)
        {
            if (team == null)
            {
                throw new ArgumentNullException();
            }
        }
    }
}
