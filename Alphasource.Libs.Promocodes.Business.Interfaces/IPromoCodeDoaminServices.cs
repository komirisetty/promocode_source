using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Alphasource.Libs.Promocodes.Models;

namespace Alphasource.Libs.Promocodes.Service.Interfaces
{
    public interface IPromoCodeDoaminServices
    {
        public PromoCodeModel AddNewCampaign(PromoCodeModel productsModel);

        Task<IEnumerable<PromoCodeModel>> GetPromoCodeByCampaign(string searchString);

        Task<IEnumerable<PromoCodeModel>> SearchCampaign(string name);

        IEnumerable<PromoCodeModel> viewCampagin();

        public PromoCodeModel UpdateCampaign(PromoCodeModel existingCampaign);
    }
}
