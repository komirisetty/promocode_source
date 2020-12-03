﻿using Alphasource.Libs.Promocodes.Models;
using MongoDB.Driver;

namespace Alphasource.Libs.Promocodes.Repositories.Interface
{
    public interface IDatabaseSettings
    {

        IMongoCollection<FranchisePromocode> AllocatePromoCodeToFranchise { get; }

    }
}