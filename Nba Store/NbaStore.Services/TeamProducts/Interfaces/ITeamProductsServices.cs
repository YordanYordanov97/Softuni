using NbaStore.Common.ViewModels.Admin.Teams;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace NbaStore.Services.TeamProducts.Interfaces
{
    public interface ITeamProductsServices
    {
        //Most Sellable - TODO
        TeamDetailsViewModel GetTeamWithProducts(int id,string gender);

        TeamDetailsViewModel GetTeamWithProductsOrderByPriceAscending(int id, string gender);

        TeamDetailsViewModel GetTeamWithProductsOrderByPriceDescending(int id, string gender);

        TeamDetailsViewModel GetTeamWithProductsOrderByDiscountAscending(int id, string gender);

        TeamDetailsViewModel GetTeamWithProductsOrderByDiscountDescending(int id, string gender);
    }
}
