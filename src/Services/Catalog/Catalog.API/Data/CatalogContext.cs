using Catalog.API.Entities;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;

namespace Catalog.API.Data
{
    public class CatalogContext : ICatalogContext
    {
        public CatalogContext(IConfiguration configuration)
        {
            var client = new MongoClient(configuration.GetValue<string>("DatabaseSetting:ConnectionString"));
            var database = client.GetDatabase(configuration.GetValue<string>("DatabaseSetting:DatabaseName"));
            Products = database.GetCollection<Entities.Product>(configuration.GetValue<string>("DatabaseSetting:CollectionName"));

            CatalogContextSeed.SeedData(Products);
        }
        public IMongoCollection<Entities.Product> Products { get; }
    }
}
