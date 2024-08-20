using BillingManagementSystem.Domain.Entities;
using BillingManagementSystem.Domain.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace BillingManagementSystem.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController(ICrudApplication<Product> crudApplication) : ControllerBase
    {
        private readonly ICrudApplication<Product> _crudApplication = crudApplication;

        /// <summary>
        /// Obtém um produto.
        /// </summary>
        [HttpGet("{productId}")]
        public async Task<IActionResult> Get(Guid productId)
        {
            var result = await _crudApplication.Get(productId.ToString());

            if (!result.Success)
                return BadRequest(result.Message);

            return Ok(result);
        }

        /// <summary>
        /// Lista todos os produtos.
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _crudApplication.GetAll();

            if (!result.Success)
                return BadRequest(result.Message);

            return Ok(result);
        }

        /// <summary>
        /// Exclui um produto.
        /// </summary>
        [HttpDelete]
        public async Task<IActionResult> Delete(Product product)
        {
            if (string.IsNullOrWhiteSpace(product.ProductId.ToString()))
                return BadRequest("ProductId inválido.");

            var result = await _crudApplication.Delete(product);

            if (!result.Success)
                return BadRequest(result.Message);

            return Ok(result);
        }

        /// <summary>
        /// Adiciona um produto.
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> Insert(Product product)
        {
            if (string.IsNullOrWhiteSpace(product.Description))
                return BadRequest("O campo descrição não pode ser vazio nem nulo.");

            var result = await _crudApplication.Insert(product);

            if (!result.Success)
                return BadRequest(result.Message);

            return Ok(result);
        }

        /// <summary>
        /// Altera um produto.
        /// </summary>
        [HttpPut]
        public async Task<IActionResult> Update(Product product)
        {
            if (string.IsNullOrWhiteSpace(product.ProductId.ToString()))
                return BadRequest("ProductId inválido.");

            if (string.IsNullOrWhiteSpace(product.Description))
                return BadRequest("O campo descrição não pode ser vazio nem nulo.");

            var result = await _crudApplication.Update(product);

            if (!result.Success)
                return BadRequest(result.Message);

            return Ok(result);
        }
    }
}
