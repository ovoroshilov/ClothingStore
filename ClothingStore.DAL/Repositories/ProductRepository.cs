using ClothingStore.DAL.Interfaces;
using ClothingStore.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace ClothingStore.DAL.Repositories
{
    public class ProductRepository : IBaseRepository<Product>
    {
        private readonly StoreAppContext _storeAppContext;

        public ProductRepository(StoreAppContext storeAppContext)
        {
            _storeAppContext = storeAppContext;
        }

        public async Task Create(Product entity)
        {
            await _storeAppContext.Products.AddAsync(entity);
            await _storeAppContext.SaveChangesAsync();
        }

        public async Task Delete(Product entity)
        {
            _storeAppContext.Products.Remove(entity);
            await _storeAppContext.SaveChangesAsync();
        }

        public async Task<Product?> Get(int id)
        {
            return await _storeAppContext.Products.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Product> Update(Product entity)
        {
            _storeAppContext.Products.Update(entity);
            await _storeAppContext.SaveChangesAsync();
            return entity;
        }

         public IQueryable<Product> GetAll()
        {
            return _storeAppContext.Products;
        }
    }
}
