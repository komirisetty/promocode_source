using Alphasource.Libs.Promocodes.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Alphasource.Libs.Promocodes.Service.Interfaces
{
    public interface IFranchisePromocodeService
    {
        
        Task<FranchisePromocode> Create(FranchisePromocode franchise);

        Task<FranchisePromocode> Update(FranchisePromocode franchise);

        Task<List<FranchisePromocode>> GetAllocatedFranchise(string campaignName);

        Task<List<FranchisePromocode>> GetAllocatedFranchiseById(string id);

        void Delete(string id);

        Task<PromoCodeModel> GetPromocode(string campaignName);

    }
}
