using System.Collections.Generic;
using System.Threading.Tasks;
using ZXVentures.Domain.Entities;

namespace ZXVentures.Domain.Interfaces
{
    public interface IAsyncRepository<T> where T : Pdv
    {
        Task Add(T obj);

        Task Update(T obj);

        Task Remove(int id);

        Task<T> GetById(int id);

        Task<T> GetByLocation(double longitude,
            double latitude);

        Task<IEnumerable<T>> GetAll();
    }
}