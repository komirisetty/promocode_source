using Alphasource.Libs.Promocodes.Models;
using Alphasource.Libs.Promocodes.Repositories.Interfaces;
using Alphasource.Libs.Promocodes.Service.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;


namespace Alphasource.Libs.Promocodes.Services
{
    public class FranchiseConsumerService : IFranchiseConsumerService
    {
        private readonly IFranchiseConsumerRepository _context;
        public FranchiseConsumerService(IFranchiseConsumerRepository context)
        {
            _context = context;
        }
        public async Task<List<FranchiseConsumer>> GetAllocatedFranchise(string campaignName)
        {
            return await _context.GetAllocatedFranchise(campaignName);
        }

        public async Task<List<FranchiseConsumer>> GetAllocatedConsumer(string franchiseName)
        {
            return await _context.GetAllocatedFranchise(franchiseName);
        }

        public async Task<List<FranchiseConsumer>> GetAllocatedFranchiseById(string id)
        {
            return await _context.GetAllocatedFranchiseById(id);
        }

        public async Task<FranchiseConsumer> Create(FranchiseConsumer allocatePromoCodeToFranchise)
        {
            return await _context.Create(allocatePromoCodeToFranchise);
        }

        public async Task<FranchiseConsumer> Update(FranchiseConsumer allocatePromoCodeToFranchise)
        {
            return await _context.Update(allocatePromoCodeToFranchise);
        }

        public Task<PromoCodeModel> GetPromocode(string campaignName)
        {
            return  _context.GetPromocode(campaignName);
        }

        public void Delete(string id)
        {
            _context.Delete(id);
        }
    }
}
