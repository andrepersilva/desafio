using ZXVentures.Domain.Interfaces;

namespace ZXVentures.Domain.Entities
{
    public class PdvDataSettings : IPdvDataSettings
    {
        public string PdvCollectionName { get; set; }
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
    }
}