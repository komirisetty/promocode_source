using Alphasource.Libs.Promocodes.Models;
using Alphasource.Libs.Promocodes.Repositories.Interfaces;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace Alphasource.Libs.Promocodes.Repositories
{
    public class DatabaseSettings : IDatabaseSettings
    {
        private readonly IMongoDatabase _db;

        public DatabaseSettings(IOptions<Settings> options, IMongoClient client)
        {
            _db = client.GetDatabase(options.Value.Database);
        }

        public IMongoCollection<FranchisePromocode> FranchisePromocode => _db.GetCollection<FranchisePromocode>("FranchisePromocode");//PromoCodeAllocationToFranchise

        public IMongoCollection<PromoCodeModel> Promocode => _db.GetCollection<PromoCodeModel>("GeneratePromoCode");

        public IMongoCollection<FranchiseConsumer> FranchiseConsumer => _db.GetCollection<FranchiseConsumer>("FranchiseConsumer");
    }
}
