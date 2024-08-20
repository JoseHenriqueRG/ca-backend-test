using BillingManagementSystem.Domain.Entities;
using BillingManagementSystem.Domain.Interfaces;
using BillingManagementSystem.Domain.ViewModels;
using BillingManagementSystem.Infra.Repository;

namespace BillingManagementSystem.Application
{
    public class BillingApplication(
        IRepositoryBilling repositoryBilling,
        IRepository<Customer> repositoryCustomer,
        IRepository<Product> repositoryProduct) : IBillingApplication
    {
        private readonly IRepositoryBilling _billingRepository = repositoryBilling;
        private readonly IRepository<Customer> _customerRepository = repositoryCustomer;
        private readonly IRepository<Product> _productRepository = repositoryProduct;

        public async Task<ActionResult<IList<BillingViewModel>>> GetAll()
        {
            try
            {
                var result = await _billingRepository.GetAll();

                var listBillingViewModel = result.SelectMany(r => new List<BillingViewModel>()
                {
                    new()
                    {
                        InvoiceNumber = r.InvoiceNumber,
                        Customer = r.Customer,
                        DueDate = r.DueDate,
                        Currency = r.Currency,
                        TotalAmount = (decimal)r.TotalAmount,
                        BillingLines = r.BillingLines.SelectMany(b => new List<BillingLineViewModel>()
                        {
                            new()
                            {
                                Product = new()
                                {
                                    ProductId = b.ProductId,
                                    Description = b.Description,
                                },
                                Quantity = (int)b.Quantity,
                                Subtotal = (int)b.Subtotal,
                                UnitPrice = (int)b.UnitPrice
                            }
                        }).ToList(),
                    }
                }).ToList();

                return new ActionResult<IList<BillingViewModel>>() { Success = true, Message = "Lista carregada com sucesso.", Value = listBillingViewModel };
            }
            catch (Exception)
            {
                return new ActionResult<IList<BillingViewModel>>() { Success = false, Message = $"Falha ao carregar a lista. Por favor, tente novamente mais tarde." };
            }
        }

        public async Task<ActionResult<BillingViewModel>> Insert(BillingViewModel billingViewModel)
        {          
            Billing billing = new()
            {
                InvoiceNumber = billingViewModel.InvoiceNumber,
                Currency = billingViewModel.Currency,
                Date = DateTime.Now,
                DueDate = billingViewModel.DueDate,
                BillingLines = []
            };

            if (billingViewModel.Customer is null || billingViewModel.Customer.Id is null)
            {
                return new ActionResult<BillingViewModel>() { Success = false, Message = "Cliente inválido." };
            }
            else if (!await _customerRepository.Exist(billingViewModel.Customer.Id.ToString()))
            {
                return new ActionResult<BillingViewModel>() { Success = false, Message = "Cliente não encontrado." };
            }
            else
            {
                billing.Customer = await _customerRepository.Get(billingViewModel.Customer.Id.ToString());
            }

            if (billingViewModel.BillingLines is null)
                return new ActionResult<BillingViewModel>() { Success = false, Message = "Nenhum produto não encontrado." };

            foreach (var line in billingViewModel.BillingLines)
            {
                if (!await _productRepository.Exist(line.Product.ProductId.ToString()))
                {
                    return new ActionResult<BillingViewModel>() { Success = false, Message = $"Produto {line.Product.Description} não encontrado." };
                }
                    
                line.Product = await _productRepository.Get(line.Product.ProductId.ToString());
                line.CalculateSubtotal();

                billing.BillingLines.Add(new()
                {
                    ProductId = new Guid(line.Product.ProductId.ToString()),
                    Description = line.Product.Description,
                    Quantity = line.Quantity,
                    UnitPrice = line.UnitPrice,
                    Subtotal = line.Subtotal,
                });
            }

            billing.TotalAmount = billingViewModel.CalculateTotalAmount();

            if (await _billingRepository.Insert(billing))
                return new ActionResult<BillingViewModel>() { Success = true, Message = "Item adicionado com sucesso.", Value = billingViewModel };

            return new ActionResult<BillingViewModel>() { Success = false, Message = "Ocorreu um erro ao adicionar o item. Por favor, tente mais tarde." };
        }

    }
}
