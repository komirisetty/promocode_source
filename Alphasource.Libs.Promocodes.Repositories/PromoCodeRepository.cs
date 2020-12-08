using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Alphasource.Libs.Promocodes.Repositories.Interface;
using Alphasource.Libs.Promocodes.Models;


namespace Alphasource.Libs.Promocodes.Repositories
{
    public class PromoCodeRepository : IPromoCodeRepository
    {
        private readonly IDatabaseSettings promoCodeContext = default(IDatabaseSettings);
        public PromoCodeRepository(IDatabaseSettings promoCodeContxt)
        {
            if (promoCodeContxt == default(IDatabaseSettings))

                throw new ArgumentNullException(nameof(promoCodeContxt));

            this.promoCodeContext = promoCodeContxt;
        }

        public async Task<IEnumerable<PromoCodeModel>> GetPromoCodeByCampaign(string campaignName)
        {
            List<PromoCodeModel> promocodes = new List<PromoCodeModel>();
            var getPromocodes = await this.promoCodeContext.Promocode.FindAsync<PromoCodeModel>(f => f.CampaignName.ToLower().Contains(campaignName.ToLower()));           

            var campResults = await getPromocodes.ToListAsync();

            return campResults;
        }
    }
}
