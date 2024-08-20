using BillingFlowManager.Domain.Entities;
using BillingFlowManager.Domain.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BillingFlowManager.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController(IRepository<Product> repository) : ControllerBase
    {
        private readonly IRepository<Product> _productsRepository = repository;

        /// <summary>
        /// Obtém um produto.
        /// </summary>
        [HttpGet("{productId}")]
        public async Task<IActionResult> Get(string productId)
        {
            var result = await _productsRepository.Get(productId);

            if(result.ProductId is null)
                return NotFound("Produto não encontrado.");

            return Ok(result);
        }

        /// <summary>
        /// Lista todos os produtos.
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _productsRepository.GetAll();

            return Ok(result);
        }

        /// <summary>
        /// Exclui um produto.
        /// </summary>
        [HttpDelete]
        public async Task<IActionResult> Delete(Product product)
        {
            if(string.IsNullOrWhiteSpace(product.ProductId.ToString())) 
                return BadRequest("ProductId inválido.");

            if (!await _productsRepository.Exist(product.ProductId.ToString())) 
                return NotFound("Produto não encontrado.");

            var result = await _productsRepository.Delete(product);

            if(!result) 
                return StatusCode(500, "Erro ao excluir o produto.");

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

            if (await _productsRepository.Insert(product))
                return Ok(product);

            return StatusCode(500, "Erro ao cadastrar o produto.");
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

            if (!await _productsRepository.Exist(product.ProductId.ToString()))
                return NotFound("Produto não encontrado.");

            if (await _productsRepository.Update(product))
                return Ok(product);

            return StatusCode(500, "Erro ao atualizar o produto.");
        }
    }
}
