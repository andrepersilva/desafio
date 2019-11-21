using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json;

namespace ZXVentures.Domain.Entities
{
    [BsonIgnoreExtraElements]
    public class Pdv
    {
        [JsonProperty("id")]
        [BsonElement("partnerId")]
        public string partnerId { get; set; }

        [BsonRequired]
        [BsonElement("tradingName")]
        public string TradingName { get; set; }

        [BsonRequired]
        [BsonElement("ownerName")]
        public string OwnerName { get; set; }

        [BsonRequired]
        [BsonElement("document")]
        public string Document { get; set; }

        [BsonRequired]
        [BsonElement("coverageArea")]
        public CoverageArea coverageArea { get; set; }

        [BsonRequired]
        [BsonElement("address")]
        public Address address { get; set; }
    }
}