
using ZXVentures.Infra.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mongo2GoTests.Runner;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Builders;
using Xunit;
using ZXVentures.AplicationTests;
using ZXVentures.Domain.Entities;
using ZXVentures.Domain.Interfaces;
using ZXVentures.Domain.Model;

namespace ZXVentures.Aplication.Tests.Repository 
{
 
    public class RepositoryPdvTests : MongoIntegrationTest
    {
        private const string DataBaseName = "dbpdv";
        private const string PdvCollectionName = "pdv";
        
        public RepositoryPdvTests( )
        {
            CreateConnection();

            ConfigureStruct();
        }
 
        [Fact]

     
        public async System.Threading.Tasks.Task DeveAdiconarPdvComSucessoTestAsync()
        {
            var repository = RepositoryPdv();

            var pdv = Newtonsoft.Json.JsonConvert.DeserializeObject<Pdv>(TestConstants.JsonInserir);
            await  repository.Add(pdv)  ;

            var pesquisa =  repository.GetById(Convert.ToInt32(pdv.partnerId)).Result;

            Assert.Equal( pdv.partnerId , pesquisa.partnerId );

        }

        public  IRepositoryPdv RepositoryPdv()
        {



            IPdvDataSettings pdvDataSettings = new PdvDataSettings();
            pdvDataSettings.ConnectionString = _runner.ConnectionString;
            pdvDataSettings.DatabaseName = DataBaseName;
            pdvDataSettings.PdvCollectionName = PdvCollectionName;
            IRepositoryPdv repository = new RepositoryPdv(pdvDataSettings);
            return repository;
        }

        internal void ConfigureStruct()
        {
        

            MongoServerSettings serverSettings = new MongoServerSettings();
            serverSettings.Server =
                new MongoServerAddress(RetornarIp(_runner.ConnectionString), RetornarPorta((_runner.ConnectionString)));
            ;
            var server = new MongoServer(serverSettings);
            var db = server.GetDatabase(DataBaseName);

            var pdv = db.GetCollection<Pdv>("pdv");

            if (pdv.Count() > 0 ) 
                pdv.RemoveAll();

            pdv.CreateIndex(new IndexKeysBuilder()
                .Ascending("document"), IndexOptions.SetUnique(true));
     

        }

        internal int RetornarPorta(string connectionString)
        {
            var porta = connectionString.Split('/')[2].Split(':')[1];
            return Convert.ToInt32(porta) ;
        }

        internal string RetornarIp(string connectionString)
        {
            var ip = connectionString.Split('/')[2].Split(':')[0];
            return ip; 
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
        [Fact]
        public async System.Threading.Tasks.Task DeveDarErroAdiconarPdvTestAsync()
        {
            var repository = RepositoryPdv();
 
            await AddTestPdv(repository);

            var pdv = Newtonsoft.Json.JsonConvert.DeserializeObject<Pdv>(TestConstants.JsonInserir);
        

            var ex = await Record.ExceptionAsync(async () => await repository.Add(pdv));

            Assert.NotNull(ex);
            Assert.IsType<MongoWriteException>(ex);

        }

        [Fact]
        public async System.Threading.Tasks.Task NaoDeveEncontrarPDVImportadoAsync()
        {
            var IdPDv = 10000;
            var repository = RepositoryPdv();
            var pesquisa = repository.GetById( IdPDv ).Result;
            Assert.Null(pesquisa);
        }


        [Fact]
        public async System.Threading.Tasks.Task DeveEncontrarPDVPorLocalizacaoImportadoAsync()
        {

            var repository = RepositoryPdv();

            await AddTestPdv(repository);

            FilterPdvLocation filter = new FilterPdvLocation(TestConstants.LongitudeDeveEncontrar, TestConstants.LatitudeDeveEncontrar);
 

            var pesquisa = repository.GetByLocation(longitude:filter.Longitude, latitude:filter.Latitude) .Result;
            Assert.NotNull(pesquisa);
        }
        
        [Fact]
        public async System.Threading.Tasks.Task NaoDeveEncontrarPDVPorLocalizacaoImportadoAsync()
        {

            var repository = RepositoryPdv();

            await AddTestPdv(repository);

            var pesquisa = repository.GetByLocation(longitude: TestConstants.LongitudeNaoDeveEncontrar, latitude: TestConstants.LatitudeNaoDeveEncontrar).Result;
            Assert.Null(pesquisa);
        }


    }
}