using NbaStore.Common.BindingModels.Admin.Teams;
using NbaStore.Common.ViewModels.Admin.Teams;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace NbaStore.Services.Admin.Interfaces
{
    public interface IAdminTeamsService
    {
        IEnumerable<TeamIndexViewModel> GetTeams();

        Task<TeamBindingModel> GetTeam(int id);

        Task<TeamDetailsViewModel> GetDetails(int id);

        Task SaveTeam(TeamBindingModel model);

        Task EditTeam(int id, TeamBindingModel model);

        Task DeleteTeam(int id);
    }
}
