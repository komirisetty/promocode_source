using Alphasource.Libs.Promocodes.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Alphasource.Libs.Promocodes.Service
{
    public interface IFranchisePromocodeService
    {
        
        Task<FranchisePromocode> Create(FranchisePromocode Franchise);
        Task<FranchisePromocode> Update(FranchisePromocode franchisePromoCodes);

        Task<List<FranchisePromocode>> GetAllocatedFranchise(string campaignName);

        void Delete(string id);

        //void Delete(string id);
        //void Update(string id, AllocatePromoCodeToFranchise Franchise);

    }
}
