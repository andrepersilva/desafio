using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ZXVentures.Domain.Model
{
    public class FilterPdvLocation
    {
        [Required]
        public double longitude { get; set; }
        [Required]
        public double latitude { get; set; }
    }
}
