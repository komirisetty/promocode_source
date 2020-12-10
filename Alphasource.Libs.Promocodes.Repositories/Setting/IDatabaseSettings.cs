using Alphasource.Libs.Promocodes.Models;
using MongoDB.Driver;

namespace Alphasource.Libs.Promocodes.Repositories.Interfaces
{
    public interface IDatabaseSettings
    {

        IMongoCollection<FranchisePromocode> FranchisePromocode { get; }

        IMongoCollection<PromoCodeModel> Promocode { get; }

        IMongoCollection<FranchiseConsumer> FranchiseConsumer { get; }

    }
}