using BillingFlowManager.Domain.Entities;
using BillingFlowManager.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BillingFlowManager.Infra.Repository
{
    public class RepositoryProduct : IRepository<Product>
    {
        private readonly RepositoryDBContext _db = new();

        public async Task<bool> Delete(Product entity)
        {
            try
            {
                _db.Products.Remove(entity);
                await _db.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                // saveLog(ex)
                return false;
            }
        }

        public async Task<bool> Exist(string id)
        {
            return await _db.Products.AsNoTracking().FirstOrDefaultAsync(p => p.ProductId.Equals(new Guid(id))) is not null;
        }

        public async Task<Product> Get(string id)
        {
            return await _db.Products.FirstOrDefaultAsync(p => p.ProductId.Equals(new Guid(id))) ?? new();
        }

        public async Task<IList<Product>> GetAll()
        {
            return await _db.Products.AsNoTracking().ToListAsync() ?? [];
        }

        public async Task<bool> Insert(Product entity)
        {
            try
            {
                await _db.Products.AddAsync(entity);
                await _db.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                // saveLog(ex)
                return false;
            }
        }

        public async Task<bool> Update(Product entity)
        {
            try
            {
                var existingEntity = _db.ChangeTracker.Entries<Product>()
                               .FirstOrDefault(e => e.Entity.ProductId == entity.ProductId);

                if (existingEntity is not null)
                {
                    _db.Products.Entry(existingEntity.Entity).State = EntityState.Detached;
                }

                _db.Products.Update(entity);
                await _db.SaveChangesAsync();
                return true;
            }
            catch (Exception ex) 
            {
                // saveLog(ex)
                return false;
            }
        }
    }
}
