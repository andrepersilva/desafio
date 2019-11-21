using Xunit;
using ZXVentures.Aplication.Controllers;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Mongo2GoTests.Runner;
using MongoDB.Driver;
using MongoDB.Driver.Builders;
using ZXVentures.AplicationTests;
using ZXVentures.Domain.Entities;
using ZXVentures.Domain.Interfaces;
using ZXVentures.Infra.Data;
using ZXVentures.Service;
using Assert = Xunit.Assert;

namespace ZXVentures.Aplication.Controllers.Tests
{
   
    public class PdvControllerTest : MongoIntegrationTest
    {

        #region Configuration Repository


        public PdvControllerTest()
        {
            CreateConnection();

            ConfigureStruct();
        }
        private const string DataBaseName = "dbpdv";
        private const string PdvCollectionName = "pdv";
        private static bool adicionado = false;

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
        public async Task GetByIdTest()
        {
            IRepositoryPdv repository = RepositoryPdv();
            IServicePdv servicePdv = new ServicePdv(repository);
            PdvController controler = new PdvController (servicePdv);
            var idPdv = 50000;
            await AddTestPdv(repository);

            var retorno = controler.GetById(idPdv); 
            Assert.NotNull(retorno);
        }
         
    }
}