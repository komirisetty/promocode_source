﻿using Alphasource.Libs.Promocodes.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Alphasource.Libs.Promocodes.Repositories.Interfaces
{
   public interface IFranchisePromocodeRepository
    {

        Task<FranchisePromocode> Create(FranchisePromocode franchisePromoCodes);

        Task<FranchisePromocode> Update(FranchisePromocode franchisePromoCodes);

        Task<List<FranchisePromocode>> GetAllocatedFranchise(string campaignName);
        Task<List<FranchisePromocode>> GetAllocatedFranchiseById(string id);

        Task<PromoCodeModel> GetPromocode(string campaignName);

        void Delete(string id);
    }
}
