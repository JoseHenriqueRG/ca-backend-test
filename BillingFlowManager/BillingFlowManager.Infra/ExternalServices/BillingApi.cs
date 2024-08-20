using BillingFlowManager.Domain.Entities;
using BillingFlowManager.Domain.Interfaces;
using System.Text.Json;

namespace BillingFlowManager.Infra.ExternalServices
{
    public class BillingApi : IBillingApi
    {
        private readonly HttpClient _httpClient;

        public BillingApi() {
            _httpClient = new HttpClient()
            {
                BaseAddress = new Uri("https://65c3b12439055e7482c16bca.mockapi.io/api/v1/")
            };
        }

        public async Task<IList<Customer>> GetCustomerAsync()
        {
            try
            {
                var response = await _httpClient.GetAsync("billing");

                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadAsStringAsync();

                    var billings = JsonSerializer.Deserialize<Billing[]>(result);

                    if (billings is not null)
                        return billings.Select(i => i.Customer).Distinct(new CustomerComparer()).ToList();
                }
            }
            catch (Exception) 
            { 
                // SaveLog();
            }

            return Array.Empty<Customer>();
        }

        public async Task<IList<Product>> GetProductsAsync()
        {
            try
            {
                var response = await _httpClient.GetAsync("billing");

                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadAsStringAsync();

                    var billings = JsonSerializer.Deserialize<Billing[]>(result);

                    if(billings is not null)
                        return billings
                            .SelectMany(b => b.BillingLines)
                            .Select(bl => new Product() { ProductId = bl.ProductId, Description = bl.Description})
                            .Distinct(new ProductComparer()).ToList();
                }
            }
            catch (Exception)
            {
                // SaveLog();
            }

            return Array.Empty<Product>();
        }
    }
}
