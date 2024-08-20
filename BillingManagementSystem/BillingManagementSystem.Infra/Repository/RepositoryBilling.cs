using BillingManagementSystem.Domain.Entities;
using BillingManagementSystem.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BillingManagementSystem.Infra.Repository
{
    public class RepositoryBilling : IRepositoryBilling
    {
        private readonly RepositoryDBContext _db = new();

        public async Task<IList<Billing>> GetAll()
        {
            return await _db.Billings
                .Include(b => b.BillingLines)
                .Include(b => b.Customer)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<bool> Insert(Billing billing)
        {
            try
            {
                var existingCustomer = await _db.Customers.FindAsync(billing.Customer.Id);

                if (existingCustomer != null)
                {
                    billing.Customer = existingCustomer;
                }
                else
                {
                    _db.Customers.Add(billing.Customer);
                }

                _db.Billings.Add(billing);
                await _db.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                // saveLog(ex);
                return false;
            }
        }
    }
}
