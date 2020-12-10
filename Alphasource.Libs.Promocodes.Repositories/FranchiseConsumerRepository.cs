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
    public class FranchiseConsumerRepository : IFranchiseConsumerRepository
    {
        private readonly IDatabaseSettings _context;


        public FranchiseConsumerRepository(IDatabaseSettings context)
        {
            _context = context;
        }

        public async Task<FranchiseConsumer> Create(FranchiseConsumer franchisePromoCodes)
        {
            franchisePromoCodes.IsActive = true;
            await _context.FranchiseConsumer.InsertOneAsync(franchisePromoCodes);
            return franchisePromoCodes;
        }

        public Task<FranchiseConsumer> Update(FranchiseConsumer franchisePromoCodes)
        {
            FilterDefinition<FranchiseConsumer> filter = Builders<FranchiseConsumer>.Filter.Eq(m => m.Id, franchisePromoCodes.Id);

            var updateAllocation = Builders<FranchiseConsumer>.Update.Set("CampaignName", franchisePromoCodes.CampaignName).
                Set("FranchiseName", franchisePromoCodes.FranchiseName).
                Set("AllocatedPromoCode", franchisePromoCodes.AllocatedPromocodes).
                Set("AvailablePromoCode", franchisePromoCodes.AvailablePromocodes).
                Set("AvailableActivePromocodes", franchisePromoCodes.AvailableActivePromocodes).
                Set("AllocatedDate", franchisePromoCodes.AllocatedDate).
                Set("AllocatedUser", franchisePromoCodes.AllocatedUser).
                Set("CancelledUser", franchisePromoCodes.CancelledUser).
                Set("CancelledDate", franchisePromoCodes.CancelledDate).
                Set("CancellationReason", franchisePromoCodes.CancellationReason);

            var result = _context.FranchiseConsumer.FindOneAndUpdateAsync(filter, updateAllocation);

            return result;
        }

        public async Task<List<FranchiseConsumer>> GetAllocatedFranchise(string campaignName)
        {
            return await _context
                                 .FranchiseConsumer
                                 .Find(m => m.CampaignName == campaignName && m.IsActive == true)
                                 .ToListAsync();
        }
        public async Task<List<FranchiseConsumer>> GetAllocatedFranchiseById(string id)
        {
            ObjectId idMongo = new ObjectId(id);

            return await _context
                                 .FranchiseConsumer
                                 .Find(m => m.Id == idMongo && m.IsActive == true)
                                 .ToListAsync();
        }

        public async Task<List<FranchiseConsumer>> GetAllocatedConsumer(string franchiseName)
        {
            return await _context
                                 .FranchiseConsumer
                                 .Find(m => m.CampaignName == franchiseName && m.IsActive == true)
                                 .ToListAsync();
        }

        public void Delete(string id)
        {
            ObjectId idMongo = new ObjectId(id);
            FilterDefinition<FranchiseConsumer> filter = Builders<FranchiseConsumer>.Filter.Eq(m => m.Id, idMongo);

            var update = Builders<FranchiseConsumer>.Update.Set("IsActive", false);
            _context.FranchiseConsumer.FindOneAndUpdate(filter, update);
        }

        public async Task<PromoCodeModel> GetPromocode(string campaignName)
        {
            return await _context
                                  .Promocode
                                  .Find(m => m.CampaignName == campaignName && m.CampaignStatus == "active")
                                  .FirstOrDefaultAsync();
        }
    }
}
