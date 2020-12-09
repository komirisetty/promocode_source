using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Alphasource.Libs.Promocodes.Models;

namespace Alphasource.Libs.Promocodes.Repositories.Interfaces
{
    public interface IPromoCodeRepository
    {
        Task<IEnumerable<PromoCodeModel>> GetPromoCodeByCampaign(string campaignName);

        Task<IEnumerable<PromoCodeModel>> SearchCampaign(string campaign);

        IEnumerable<PromoCodeModel> ViewCampaign();

        public PromoCodeModel CreateNewCampaign(PromoCodeModel addNewCampaign);

        public PromoCodeModel UpdateCampaign(PromoCodeModel exixstingCampaign);
    }
}
