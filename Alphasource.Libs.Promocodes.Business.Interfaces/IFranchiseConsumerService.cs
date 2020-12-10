using Alphasource.Libs.Promocodes.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Alphasource.Libs.Promocodes.Service.Interfaces
{
    public interface IFranchiseConsumerService
    {
        
        Task<FranchiseConsumer> Create(FranchiseConsumer franchise);

        Task<FranchiseConsumer> Update(FranchiseConsumer franchise);

        Task<List<FranchiseConsumer>> GetAllocatedFranchise(string campaignName);
        Task<List<FranchiseConsumer>> GetAllocatedConsumer(string franchiseName);

        Task<List<FranchiseConsumer>> GetAllocatedFranchiseById(string id);

        void Delete(string id);

        Task<PromoCodeModel> GetPromocode(string campaignName);

    }
}
