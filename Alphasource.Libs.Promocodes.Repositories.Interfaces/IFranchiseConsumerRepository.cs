using Alphasource.Libs.Promocodes.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Alphasource.Libs.Promocodes.Repositories.Interfaces
{
   public interface IFranchiseConsumerRepository
    {

        Task<FranchiseConsumer> Create(FranchiseConsumer franchiseConsumer);

        Task<FranchiseConsumer> Update(FranchiseConsumer franchiseConsumer);

        Task<List<FranchiseConsumer>> GetAllocatedFranchise(string campaignName);
        Task<List<FranchiseConsumer>> GetAllocatedFranchiseById(string id);

        Task<List<FranchiseConsumer>> GetAllocatedConsumer(string franchiseName);

        Task<PromoCodeModel> GetPromocode(string campaignName);

        void Delete(string id);
    }
}
