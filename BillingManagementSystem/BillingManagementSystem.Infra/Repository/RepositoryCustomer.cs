using BillingManagementSystem.Domain.Entities;
using BillingManagementSystem.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BillingManagementSystem.Infra.Repository
{
    public class RepositoryCustomer : IRepository<Customer>
    {
        private readonly RepositoryDBContext _db = new();

        public async Task<bool> Delete(Customer entity)
        {
            try
            {
                _db.Customers.Remove(entity);
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
            return await _db.Customers.AsNoTracking().FirstOrDefaultAsync(x => x.Id.Equals(new Guid(id))) is not null;
        }

        public async Task<Customer> Get(string id)
        {
            return await _db.Customers.FirstOrDefaultAsync(x => x.Id.Equals(new Guid(id))) ?? new();
        }

        public async Task<IList<Customer>> GetAll()
        {
            return await _db.Customers.AsNoTracking().ToListAsync() ?? [];
        }

        public async Task<bool> Insert(Customer entity)
        {
            try
            {
                await _db.Customers.AddAsync(entity);
                await _db.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                // saveLog(ex)
                return false;
            }
        }

        public async Task<bool> Update(Customer entity)
        {
            try
            {
                var existingEntity = _db.ChangeTracker.Entries<Customer>().FirstOrDefault(e => e.Entity.Id == entity.Id);

                if (existingEntity is not null)
                {
                    _db.Customers.Entry(existingEntity.Entity).State = EntityState.Detached;
                }

                _db.Customers.Update(entity);
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
