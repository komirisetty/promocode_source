﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Alphasource.Libs.Promocodes.Models;

namespace Alphasource.Libs.Promocodes.Repositories.Interface
{
    public interface IPromoCodeRepository
    {
        Task<IEnumerable<PromoCodeModel>> GetPromoCodeByCampaign(string campaignName);
    }
}
