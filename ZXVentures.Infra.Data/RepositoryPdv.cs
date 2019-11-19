using System;
using System.Collections.Generic;
using System.Drawing;
using ZXVentures.Domain.Entities;
using ZXVentures.Domain.Interfaces;
using MongoDB.Driver;
using System.Threading.Tasks;
using System.Linq;
using System.Net;
using MongoDB.Driver.GeoJsonObjectModel;

namespace ZXVentures.Infra.Data
{

       public class RepositoryPdv  : IRepositoryPdv
    {
        private readonly IMongoCollection<Domain.Entities.Pdv> _collection;
        public RepositoryPdv(IPdvDataSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);
 
            _collection = database.GetCollection<Domain.Entities.Pdv>(settings.PdvCollectionName);
        }

        public async Task Add(Pdv obj)
        {
           
                 await _collection.InsertOneAsync(obj);
           
        }
        public async Task<Pdv> GetByLocation(double longitude,
                              double latitude)
        {
            var point = GeoJson.Point(GeoJson.Geographic(longitude, latitude));
            var filter = Builders<Pdv>.Filter.GeoIntersects(p=>p.coverageArea, point );
            var pdv = await _collection.FindAsync(filter);
            return pdv.FirstOrDefault();
        }
        public Task<IEnumerable<Pdv>> GetAll()
        {
            throw new NotImplementedException();
        }

        public async Task<Pdv> GetById(int id)
        {

            var filter = Builders<Pdv>.Filter.Eq("partnerId", id.ToString()); 
            
           var pdv=  await _collection.FindAsync(filter );
            return pdv.FirstOrDefault<Pdv>();
        }

        public Task Remove(int id)
        {
            throw new NotImplementedException();
        }

        public Task Update(Pdv obj)
        {
            throw new NotImplementedException();
        }
    }
}
