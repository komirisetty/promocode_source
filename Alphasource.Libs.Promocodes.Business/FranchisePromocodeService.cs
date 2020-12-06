using Alphasource.Libs.Promocodes.Models;
using Alphasource.Libs.Promocodes.Repositories.Interface;
using System.Collections.Generic;
using System.Threading.Tasks;


namespace Alphasource.Libs.Promocodes.Service
{
    public class FranchisePromocodeService : IFranchisePromocodeService
    {
        private readonly IFranchisePromocodeRepository _context;
        public FranchisePromocodeService(IFranchisePromocodeRepository context)
        {
            _context = context;
        }
        public async Task<List<FranchisePromocode>> GetAllocatedFranchise(string campaignName)
        {
            return await _context.GetAllocatedFranchise(campaignName);
        }

        public async Task<List<FranchisePromocode>> GetAllocatedFranchiseById(string id)
        {
            return await _context.GetAllocatedFranchiseById(id);
        }

        public async Task<FranchisePromocode> Create(FranchisePromocode allocatePromoCodeToFranchise)
        {
            return await _context.Create(allocatePromoCodeToFranchise);
        }

        public async Task<FranchisePromocode> Update(FranchisePromocode allocatePromoCodeToFranchise)
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
