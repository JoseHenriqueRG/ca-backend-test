using BillingFlowManager.Domain.Entities;

namespace BillingFlowManager.Domain.Interfaces
{
    public interface IBillingApi
    {
        Task<IList<Product>> GetProductsAsync();
        Task<IList<Customer>> GetCustomerAsync();
    }
}
