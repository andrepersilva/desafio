namespace ZXVentures.Domain.Interfaces
{
    public interface IPdvDataSettings
    {
        string PdvCollectionName { get; set; }
        string ConnectionString { get; set; }
        string DatabaseName { get; set; }
    }
}