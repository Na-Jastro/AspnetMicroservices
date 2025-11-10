using Catalog.API.Data;
using Catalog.API.Entities;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Catalog.API.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly ICatalogContext _catalogContext;

        public ProductRepository(ICatalogContext catalogContext)
        {
            _catalogContext = catalogContext;
        }
        public async Task CreateProduct(Entities.Product product)
        {
            await _catalogContext.Products.InsertOneAsync(product);
        }

        public async Task<bool> DeleteProduct(string id)
        {
            FilterDefinition<Entities.Product> filter = Builders<Entities.Product>.Filter.Eq(p => p.Id, id);
            var deleteResult = await _catalogContext
                                    .Products
                                    .DeleteOneAsync(filter);
            return deleteResult.IsAcknowledged && deleteResult.DeletedCount > 0;
        }

        public async Task<Entities.Product> GetProduct(string id)
        {
            return await _catalogContext.Products.Find(p => p.Id == id).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Entities.Product>> GetProductByCategory(string categoryName)
        {
            FilterDefinition<Entities.Product> filter = Builders<Entities.Product>.Filter.Eq(p => p.Category, categoryName);
            return await _catalogContext.Products.Find(filter).ToListAsync();
        }

        public async Task<IEnumerable<Entities.Product>> GetProductByName(string name)
        {
            FilterDefinition<Entities.Product> filter = Builders<Entities.Product>.Filter.Eq(p => p.Name, name);
            return await _catalogContext.Products.Find(filter).ToListAsync();
        }

        public async Task<IEnumerable<Entities.Product>> GetProducts()
        {
            return await _catalogContext.Products.Find(p => true)
                         .ToListAsync();
        }

        public async Task<bool> UpdateProduct(Entities.Product product)
        {
            var updateResult = await _catalogContext
                                    .Products
                                    .ReplaceOneAsync(filter: g => g.Id == product.Id, replacement: product);

            return updateResult.IsAcknowledged && updateResult.ModifiedCount > 0;
        }
    }
}
