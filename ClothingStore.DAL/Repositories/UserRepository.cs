using ClothingStore.DAL.Interfaces;
using ClothingStore.Domain.Entities;

namespace ClothingStore.DAL.Repositories
{
    public class UserRepository : IBaseRepository<User>
    {
        private readonly StoreAppContext _storeAppContext;

        public UserRepository(StoreAppContext storeAppContext)
        {
            _storeAppContext = storeAppContext;
        }

        public async Task Create(User entity)
        {
            await _storeAppContext.Users.AddAsync(entity);
            await _storeAppContext.SaveChangesAsync();
        }

        public async Task Delete(User entity)
        {
            _storeAppContext.Users.Remove(entity);
            await _storeAppContext.SaveChangesAsync();
        }

        public async Task<User> Update(User entity)
        {
            _storeAppContext.Users.Update(entity);
            await _storeAppContext.SaveChangesAsync();
            return entity;
        }

        public IQueryable<User> GetAll()
        {
            return _storeAppContext.Users;
        }
    }
}
