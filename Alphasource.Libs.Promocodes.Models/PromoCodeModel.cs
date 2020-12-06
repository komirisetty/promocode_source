using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;

namespace Alphasource.Libs.Promocodes.Models
{
    public class PromoCodeModel
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string _id { get; set; }
       
        [BsonElement("campaignName")]
        public string CampaignName { get; set; }
       
        [BsonElement("noOfPromoCodes")]
        public int NoOfPromoCodes { get; set; }
       
        [BsonElement("promocodeCost")]
        public int PromocodeCost { get; set; }

        [BsonElement("startDate")]
        public DateTime StartDate { get; set; }

        [BsonElement("endDate")]
        public DateTime EndDate { get; set; }

        [BsonElement("prefix")]
        public string Prefix { get; set; }

        [BsonElement("promocodeGenerated")]
        public string PromocodeGenerated { get; set; }
        //public List<string> PromocodeGenerated { get; set; }
        //public Array PromocodeGenerated { get; set; }

        [BsonElement("remarks")]
        public string Remarks { get; set; }

        [BsonElement("campaignCreatedDate")]
        public DateTime CampaignCreatedDate { get; set; }

        [BsonElement("createdUserName")]
        public string CreatedUserName { get; set; }

        [BsonElement("campaignStatus")]
        public string CampaignStatus { get; set; }

        [BsonElement("cancellaionResaon")]
        public string CancellaionResaon { get; set; }

        [BsonElement("cancelledDate")]
        public DateTime CancelledDate { get; set; }

        [BsonElement("cancelledUser")]
        public string CancelledUser { get; set; }

        [BsonElement("portalPercentage")]
        public int PortalPercentage { get; set; }

        [BsonElement("franschisePercentage")]
        public int FranschisePercentage { get; set; }

        [BsonElement("chefPercentage")]
        public int ChefPercentage { get; set; }
    }
}
