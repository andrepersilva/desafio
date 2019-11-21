using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using MongoDB.Bson.Serialization.Attributes;

namespace ZXVentures.Domain.Entities
{
    public class Address
    {
        [Required] [BsonElement("type")] public string type { get; set; }

        [BsonElement("coordinates")] public List<double> Coordinates { get; set; }
    }
}