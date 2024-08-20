using BillingManagementSystem.Domain.Entities;

namespace BillingManagementSystem.Domain.Interfaces
{
    public interface IBillingApi
    {
        Task<IList<Product>> GetProductsAsync();
        Task<IList<Customer>> GetCustomerAsync();
    }
}
