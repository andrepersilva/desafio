using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Driver.GeoJsonObjectModel;
namespace ZXVentures.Domain.Entities
{
    public class CoverageArea
    {
        [Required]
        [BsonElement("type")]
        public string Type { get; set; }
        [BsonElement("coordinates")]
        [Required]
        public List<List<List<List<double>>>> Coordinates { get; set; }

    }

    
}
