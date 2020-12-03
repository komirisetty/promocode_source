using Alphasource.Libs.Promocodes.Models;
using Alphasource.Libs.Promocodes.Repositories;
using Alphasource.Libs.Promocodes.Repositories.Interface;
using System;
using System.Collections.Generic;
using System.Text;
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
        public async Task<FranchisePromocode> Create(FranchisePromocode allocatePromoCodeToFranchise)
        {
            return await _context.Create(allocatePromoCodeToFranchise);
        }

        public async Task<FranchisePromocode> Update(FranchisePromocode allocatePromoCodeToFranchise)
        {
            return await _context.Update(allocatePromoCodeToFranchise);
        }

        public void Delete(string id)
        {
            _context.Delete(id);
        }
    }
}
