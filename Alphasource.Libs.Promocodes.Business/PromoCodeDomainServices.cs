using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Alphasource.Libs.Promocodes.Repositories.Interfaces;
using Alphasource.Libs.Promocodes.Service.Interfaces;
using Alphasource.Libs.Promocodes.Models;

namespace Alphasource.Libs.Promocodes.Services
{
    public class PromoCodeDomainServices : IPromoCodeDoaminServices
    {
        private IPromoCodeRepository promoCodeRepository = default;

        public PromoCodeDomainServices(IPromoCodeRepository promoCodeRepository)
        {
            if (promoCodeRepository == default(IPromoCodeRepository))
                throw new ArgumentNullException(nameof(promoCodeRepository));

            this.promoCodeRepository = promoCodeRepository;
        }
        public PromoCodeModel AddNewCampaign(PromoCodeModel promoCodeModel)
        {
            promoCodeModel = promoCodeRepository.CreateNewCampaign(promoCodeModel);

            return promoCodeModel;

        }

        public async Task<IEnumerable<PromoCodeModel>> GetPromoCodeByCampaign(string searchString)
        {
            var campaignByName = await promoCodeRepository.GetPromoCodeByCampaign(searchString);

            return campaignByName;
        }

        public async Task<IEnumerable<PromoCodeModel>> SearchCampaign(string name)
        {
            var searchCampaign = await promoCodeRepository.GetPromoCodeByCampaign(name);

            return searchCampaign;
        }

        public PromoCodeModel UpdateCampaign(PromoCodeModel existingCampaign)
        {
            var updateCampaign = promoCodeRepository.UpdateCampaign(existingCampaign);
            return updateCampaign;
        }

        public IEnumerable<PromoCodeModel> viewCampagin()
        {
            var viewAllCampagin = promoCodeRepository.ViewCampaign();

            return viewAllCampagin;
        }
    }
}
