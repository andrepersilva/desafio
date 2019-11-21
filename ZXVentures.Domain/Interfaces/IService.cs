using System.Threading.Tasks;
using ZXVentures.Domain.Entities;

namespace ZXVentures.Domain.Interfaces
{
    public interface IService<T> where T : Pdv
    {
        Task Post(T obj);

        Task<T> GetById(int id);

        Task<T> GetByLocation(double longitude, double latitude);
    }
}