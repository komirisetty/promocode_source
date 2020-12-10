using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Alphasource.Libs.Promocodes.Models
{
        public class FranchisePromocode
    {
            [BsonId]
            [BsonRepresentation(BsonType.ObjectId)]
            public string Id { get; set; }

            public string CampaignName { get; set; }
            public string Location { get; set; }
            public string FranchiseName { get; set; }
            public int TotalPromoCode { get; set; }
            public int AllocatedPromoCode { get; set; }
            public int AvailablePromoCode { get; set; }
            public DateTime AllocatedDate { get; set; }
            public string AllocatedUser { get; set; }
            public bool IsActive { get; set; }
            public string CancelledUser { get; set; }
            public DateTime CancelledDate { get; set; }
            public string CancellationReason { get; set; }
        }
}
