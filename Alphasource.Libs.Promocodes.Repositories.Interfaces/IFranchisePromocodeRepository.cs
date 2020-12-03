using Alphasource.Libs.Promocodes.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Alphasource.Libs.Promocodes.Repositories.Interface
{
   public interface IFranchisePromocodeRepository
    {

        Task<FranchisePromocode> Create(FranchisePromocode franchisePromoCodes);

        Task<FranchisePromocode> Update(FranchisePromocode franchisePromoCodes);

        Task<List<FranchisePromocode>> GetAllocatedFranchise(string campaignName);

        void Delete(string id);
    }
}
