using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ZXVentures.Domain.Entities;
using ZXVentures.Domain.Interfaces;

namespace ZXVentures.Service
{
    public class ServicePdv : IServicePdv
    {
        private readonly IRepositoryPdv _pdv;

        public ServicePdv(IRepositoryPdv pdv)

        {
            _pdv = pdv;
        }

 

        public async Task<Pdv> GetById(int id)
        {
            return await _pdv.GetById(id);
        }

        public async Task<Pdv> GetByLocation(double longitude, double latitude)
        {
            return await _pdv.GetByLocation(longitude, latitude);
        }


        public async Task Post(Pdv obj)
        {
            try
            {
                if (GetById(Convert.ToInt32(obj.partnerId)).Result == null)
                    await _pdv.Add(obj);
                else
                    throw new ArgumentException("Document partner ja existe ");
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw new ArgumentException(e.Message);
            }
        }


 
    }
}