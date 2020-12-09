using Alphasource.Libs.Promocodes.Models;
using Alphasource.Libs.Promocodes.Repositories.Interfaces;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;


namespace Alphasource.Libs.Promocodes.Repositories
{
    public class FranchisePromocodeRepository : IFranchisePromocodeRepository
    {
        private readonly IDatabaseSettings _context;


        public FranchisePromocodeRepository(IDatabaseSettings context)
        {
            _context = context;
            //webhostenvt and serviceconfig commented
        }

        public async Task<FranchisePromocode> Create(FranchisePromocode franchisePromoCodes)
        {

            franchisePromoCodes.IsActive = true;
            //var promodetaiils = await _context
            //        .ChefPromoCodes
            //        .Find(m => m.FranchiseId == chefPromoCodes.FranchiseId && m.PromoCodeType == chefPromoCodes.PromoCodeType && m.IsDeleted == false)
            //        .FirstOrDefaultAsync();
            //if (promodetaiils == null)
            //{
            await _context.FranchisePromocode.InsertOneAsync(franchisePromoCodes);
            return franchisePromoCodes;
            //}


            //return chefPromoCodes;
        }

        public Task<FranchisePromocode> Update(FranchisePromocode franchisePromoCodes)
        {
            FilterDefinition<FranchisePromocode> filter = Builders<FranchisePromocode>.Filter.Eq(m => m.Id, franchisePromoCodes.Id);

            var updateAllocation = Builders<FranchisePromocode>.Update.Set("CampaignName", franchisePromoCodes.CampaignName).
                Set("FranchiseName", franchisePromoCodes.FranchiseName).
                Set("AllocatedPromoCode", franchisePromoCodes.AllocatedPromoCode).
                Set("AvailablePromoCode", franchisePromoCodes.AvailablePromoCode).
                Set("AllocatedDate", franchisePromoCodes.AllocatedDate).
                Set("AllocatedUser", franchisePromoCodes.AllocatedUser).
                Set("CancelledUser", franchisePromoCodes.CancelledUser).
                Set("CancelledDate", franchisePromoCodes.CancelledDate).
                Set("CancellationReason", franchisePromoCodes.CancellationReason);

            var result = _context.FranchisePromocode.FindOneAndUpdateAsync(filter, updateAllocation);

            return result;
        }

        public async Task<List<FranchisePromocode>> GetAllocatedFranchise(string campaignName)
        {
            return await _context
                                 .FranchisePromocode
                                 .Find(m => m.CampaignName == campaignName && m.IsActive == true)
                                 .ToListAsync();
        }
        public async Task<List<FranchisePromocode>> GetAllocatedFranchiseById(string id)
        {
            ObjectId idMongo = new ObjectId(id);

            return await _context
                                 .FranchisePromocode
                                 .Find(m => m.Id == idMongo && m.IsActive == true)
                                 .ToListAsync();
        }

        public void Delete(string id)
        {
            ObjectId idMongo = new ObjectId(id);
            FilterDefinition<FranchisePromocode> filter = Builders<FranchisePromocode>.Filter.Eq(m => m.Id, idMongo);

            var update = Builders<FranchisePromocode>.Update.Set("IsActive", false);
            _context.FranchisePromocode.FindOneAndUpdate(filter, update);
        }

        public async Task<PromoCodeModel> GetPromocode(string campaignName)
        {
            return await _context
                                  .Promocode
                                  .Find(m => m.CampaignName == campaignName && m.CampaignStatus == "active")
                                  .FirstOrDefaultAsync();
        }

        public void Validate(FranchisePromocode franchisePromoCodes)
        {
            var result = _context
                                 .Promocode
                                 .Find(m => m.CampaignName == franchisePromoCodes.CampaignName && m.CampaignStatus == "active")
                                 .ToList();

            if (result.Count ==0)
            {
                throw new Exception("Invalid CampaignName.");
            }

            List<PromoCodeModel> franchiseDetails = _context
                                 .Promocode
                                 .Find(m => m.CampaignName == franchisePromoCodes.CampaignName && m.CampaignStatus == "active")
                                 .ToList();
            if (franchisePromoCodes.AllocatedPromoCode > franchiseDetails[0].NoOfPromoCodes)
            {
                throw new Exception("Allocated Promocode cannot exceed total promocode.");
            }
        }

    }
}
