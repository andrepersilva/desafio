using System.ComponentModel.DataAnnotations;

namespace ZXVentures.Domain.Model
{
    public class FilterPdvLocation
    {
        private double longitudeNaoDeveEncontrar;
        private object latitudeNaoDeveEncontra;

        public FilterPdvLocation(double longitudeNaoDeveEncontrar, double latitudeNaoDeveEncontra)
        {
            this.Longitude = longitudeNaoDeveEncontrar;
            this.Latitude = latitudeNaoDeveEncontra;
        }

        [Required] public double Longitude { get; set; }

        [Required] public double Latitude { get; set; }
    }
}