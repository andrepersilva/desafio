using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ZXVentures.Domain.Entities;


namespace ZXVentures.Domain.Interfaces
{
    public interface IService<T> where T : Pdv
    {
        Task Post (T obj);

        Task Put (T obj) ;

        Task Delete(int id);

        Task<T> GetById(int id);

        Task<T> GetByLocation(double longitude, double latitude);

        Task<IList<T>> GetAll();
    }
}
