 
using ZXVentures.Service;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Mongo2GoTests.Runner;
using MongoDB.Driver;
using MongoDB.Driver.Builders;
using Xunit;
using ZXVentures.Domain.Entities;
using ZXVentures.Domain.Interfaces;
using ZXVentures.Infra.Data;
using ZXVentures.Aplication.Tests.Repository;
using ZXVentures.AplicationTests;
using ZXVentures.Domain.Model;

namespace ZXVentures.Service.Tests
{ 
    public class ServicePdvTest : MongoIntegrationTest
    {
        #region Configuration Repository


        public ServicePdvTest()
        {
            CreateConnection();

            ConfigureStruct();
        }
        private const string DataBaseName = "dbpdv";
        private const string PdvCollectionName = "pdv";
        private static bool adicionado=false;

        internal void ConfigureStruct()
        {


            MongoServerSettings serverSettings = new MongoServerSettings();
            serverSettings.Server =
                new MongoServerAddress(RetornarIp(_runner.ConnectionString), RetornarPorta((_runner.ConnectionString)));
            ;
            var server = new MongoServer(serverSettings);
            var db = server.GetDatabase(DataBaseName);

            var pdv = db.GetCollection<Pdv>("pdv");

            if (pdv.Count() > 0)
                pdv.RemoveAll();
             
            pdv.CreateIndex(new IndexKeysBuilder()
                .Ascending("document"), IndexOptions.SetUnique(true));


        }
        private static async Task AddTestPdv(IRepositoryPdv repository)
        {
            try

            {
                var pdv = Newtonsoft.Json.JsonConvert.DeserializeObject<Pdv>(TestConstants.JsonInserir);
                await repository.Add(pdv);
            }
            catch
            {

            }



        }
        internal int RetornarPorta(string connectionString)
        {
            var porta = connectionString.Split('/')[2].Split(':')[1];
            return Convert.ToInt32(porta);
        }

        internal string RetornarIp(string connectionString)
        {
            var ip = connectionString.Split('/')[2].Split(':')[0];
            return ip;
        }
        public IRepositoryPdv RepositoryPdv()
        {
 
            IPdvDataSettings pdvDataSettings = new PdvDataSettings();
            pdvDataSettings.ConnectionString = _runner.ConnectionString;
            pdvDataSettings.DatabaseName = DataBaseName;
            pdvDataSettings.PdvCollectionName = PdvCollectionName;
            IRepositoryPdv repository = new RepositoryPdv(pdvDataSettings);
            return repository;
        }

        #endregion


        [Fact]
        public async System.Threading.Tasks.Task DeveRetornarGetByIdTestAsync()
        {
           
         
            var idPdv = 50000;
            IRepositoryPdv repository =  RepositoryPdv();

            await AddTestPdv(repository);

            IServicePdv servePdv = new ServicePdv(repository);
        
            var  retorno = servePdv.GetById(idPdv).Result; 

           Assert.Equal(idPdv.ToString(), retorno.partnerId.ToString()  );

        }


        [Fact]
        public async System.Threading.Tasks.Task NaoDeveRetornarGetByIdTestAsync()
        {


            var idPdv = 0;
            IRepositoryPdv repository = RepositoryPdv();

            await AddTestPdv(repository);

            IServicePdv servePdv = new ServicePdv(repository);

            var retorno = servePdv.GetById(idPdv).Result;

            Assert.Null(  retorno );

        }

        [Fact]
        public async System.Threading.Tasks.Task  DeveRetornarGetByLocationTestAsync()
        {


           
            IRepositoryPdv repository = RepositoryPdv();

            await AddTestPdv(repository);
            IServicePdv servePdv = new ServicePdv(repository);

            var retorno = servePdv.GetByLocation(TestConstants.LongitudeDeveEncontrar,
                TestConstants.LatitudeDeveEncontrar) .Result;

            Assert.NotNull(retorno);

        }
        [Fact]
        public async System.Threading.Tasks.Task NaoDeveRetornarGetByLocationTestAsync()
        {



            IRepositoryPdv repository = RepositoryPdv();
            await AddTestPdv(repository);
            IServicePdv servePdv = new ServicePdv(repository);
            FilterPdvLocation filter = new FilterPdvLocation(TestConstants.LongitudeNaoDeveEncontrar, TestConstants.LatitudeNaoDeveEncontrar);
            var retorno = servePdv.GetByLocation(filter.Longitude ,
                filter.Latitude).Result;

            Assert.Null(retorno);

        }
        
        [Fact]
        public async System.Threading.Tasks.Task DeveAdiconarNovaPdvTestAsync()
        {

            IRepositoryPdv repository = RepositoryPdv();
      
            IServicePdv servePdv = new ServicePdv(repository);
            var pdv = Newtonsoft.Json.JsonConvert.DeserializeObject<Pdv>(TestConstants.JsonInserirService);
            await  servePdv.Post(pdv);
            var retorno = servePdv.GetById(90000).Result;
            Assert.NotNull(retorno);

        }


        [Fact]
        public async System.Threading.Tasks.Task NaoDeveAdiconarNovaPdvTestAsync()
        {

            IRepositoryPdv repository = RepositoryPdv();

            IServicePdv servePdv = new ServicePdv(repository);
            var pdv = Newtonsoft.Json.JsonConvert.DeserializeObject<Pdv>(TestConstants.JsonInserirService);
            await servePdv.Post(pdv);
             

            var ex = await Record.ExceptionAsync(async () =>   await servePdv.Post(pdv));

            Assert.NotNull(ex);
            Assert.IsType<ArgumentException>(ex);

        }

    }
}