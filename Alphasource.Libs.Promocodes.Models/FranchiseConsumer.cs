using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Alphasource.Libs.Promocodes.Models
{
    public class FranchiseConsumer
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public ObjectId Id { get; set; }

        public string CampaignName { get; set; }
        public string FranchiseName { get; set; }
        public string PromocodeId { get; set; }
        public string ConsumerName { get; set; }
        public int ConsumerMobileNo { get; set; }
        public string ConsumerMail { get; set; }
        public DateTime PromocodeReceivedDate { get; set; }
        public string AllocatedUser { get; set; }
        public DateTime AllocatedDate { get; set; }
        public int TotalPromoCode { get; set; }
        public int AllocatedPromoCode { get; set; }
        public int AvailablePromoCode { get; set; }
        public int CancelledPromoCode { get; set; }
        public bool IsActive { get; set; }
        public string CancelledUser { get; set; }
        public DateTime CancelledDate { get; set; }
        public string CancellationReason { get; set; }
    }
}
