using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Text.Json.Serialization;

namespace ZXVentures.Domain.Model
{
    public class PdvDto
    {
        [Required]
        [JsonPropertyName("id")]
        public  string Id { get; set; }

      
 
        [JsonPropertyName("tradingName")]

        public string TradingName
        {
            get; set;
        }
        [JsonPropertyName("ownerName")]
      
        public string OwnerName { get; set; }

        [JsonPropertyName("document")]
  
        public string Document { get; set; }

       // [JsonPropertyName("coverageArea")]
         
        //public CoverageArea coverageArea { get; set; }


       // [JsonPropertyName("address")]
      //  public Address address { get; set; }
    }
}
