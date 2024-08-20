using BillingFlowManager.Domain.Entities;
using BillingFlowManager.Domain.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BillingFlowManager.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BillingsController(
        IRepositoryBilling repositoryBilling, 
        IRepository<Customer> repositoryCustomer, 
        IRepository<Product> repositoryProduct) : ControllerBase
    {
        private readonly IRepositoryBilling _billingRepository = repositoryBilling;
        private readonly IRepository<Customer> _customerRepository = repositoryCustomer;
        private readonly IRepository<Product> _productRepository = repositoryProduct;

        /// <summary>
        /// Insere uma nova fatura.
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> Insert(Billing billing)
        {
            if (billing.Customer is null || billing.Customer.Id is null)
            {
                return BadRequest("Cliente inválido.");
            }
            else if(!await _customerRepository.Exist(billing.Customer.Id.ToString()))
            {
                return NotFound("Cliente não encontrado.");
            }
            else
            {
                billing.Customer = await _customerRepository.Get(billing.Customer.Id.ToString());
            }

            if (billing.BillingLines is null)
                return NotFound("Nenhum produto não encontrado.");

            foreach(var line in billing.BillingLines)
            {
                if (!await _productRepository.Exist(line.ProductId.ToString()))
                {
                    return NotFound($"Produto {line.Description} não encontrado.");
                }
                else
                {
                    var product = await _productRepository.Get(line.ProductId.ToString());

                    // Product description in the database
                    line.Description = product.Description;

                    if(line.Subtotal <= 0)
                        line.CalculateSubtotal();
                }
            }

            if(billing.TotalAmount <= 0)
                billing.CalculateTotalAmount();

            if (await _billingRepository.Insert(billing))
                return Ok();

            return StatusCode(500, "Erro ao cadastrar o faturamento.");
        }

        /// <summary>
        /// Lista todas as faturas.
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _billingRepository.GetAll();

            return Ok(result);
        }
    }
}
