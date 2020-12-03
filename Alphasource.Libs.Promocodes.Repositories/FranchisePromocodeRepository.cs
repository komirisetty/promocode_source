﻿using Alphasource.Libs.Promocodes.Models;
using Alphasource.Libs.Promocodes.Repositories.Interface;
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
            await _context.AllocatePromoCodeToFranchise.InsertOneAsync(franchisePromoCodes);
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
                Set("AllocatedDate", franchisePromoCodes.AllocatedDate).
                Set("AllocatedUser", franchisePromoCodes.AllocatedUser).
                Set("CancelledUser", franchisePromoCodes.CancelledUser).
                Set("CancelledDate", franchisePromoCodes.CancelledDate).
                Set("CancellationReason", franchisePromoCodes.CancellationReason);
                
            var result = _context.AllocatePromoCodeToFranchise.FindOneAndUpdateAsync(filter, updateAllocation);

            return result;
        }

        public async Task<List<FranchisePromocode>> GetAllocatedFranchise(string campaignName)
        {
            return await _context
                                 .AllocatePromoCodeToFranchise
                                 .Find(m => m.CampaignName == campaignName && m.IsActive == true)
                                 .ToListAsync();
        }

        public void Delete(string id)
        {
            ObjectId idMongo = new ObjectId(id);
            FilterDefinition<FranchisePromocode> filter = Builders<FranchisePromocode>.Filter.Eq(m => m.Id, idMongo);

            var update = Builders<FranchisePromocode>.Update.Set("IsActive", false);
            _context.AllocatePromoCodeToFranchise.FindOneAndUpdate(filter, update);
        }
    }
}
