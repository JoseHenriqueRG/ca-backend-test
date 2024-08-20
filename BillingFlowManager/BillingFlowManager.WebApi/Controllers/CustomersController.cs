using BillingFlowManager.Domain.Entities;
using BillingFlowManager.Domain.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BillingFlowManager.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController(IRepository<Customer> repository) : ControllerBase
    {
        private readonly IRepository<Customer> _customersRepository = repository;

        /// <summary>
        /// Obtém um cliente.
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpGet("{Id}")]
        public async Task<IActionResult> Get(string Id)
        {
            var result = await _customersRepository.Get(Id);

            if (result.Id is null)
                return NotFound("Cliente não encontrado.");

            return Ok(result);
        }

        /// <summary>
        /// Lista todos os clientes.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _customersRepository.GetAll();

            return Ok(result);
        }

        /// <summary>
        /// Exclui um cliente.
        /// </summary>
        [HttpDelete]
        public async Task<IActionResult> Delete(Customer customer)
        {
            if (string.IsNullOrWhiteSpace(customer.Id.ToString()))
                return BadRequest("Id inválido.");

            if (!await _customersRepository.Exist(customer.Id.ToString()))
                return NotFound("Cliente não encontrado.");

            var result = await _customersRepository.Delete(customer);

            if (!result)
                return StatusCode(500, "Erro ao excluir o cliente.");

            return Ok(result);
        }

        /// <summary>
        /// Adiciona um cliente.
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> Insert(Customer customer)
        {
            if (await _customersRepository.Insert(customer))
                return Ok(customer);

            return StatusCode(500, "Erro ao cadastrar o cliente.");
        }

        /// <summary>
        /// Edita um cliente.
        /// </summary>
        [HttpPut]
        public async Task<IActionResult> Update(Customer customer)
        {
            if (string.IsNullOrWhiteSpace(customer.Id.ToString()))
                return BadRequest("Id inválido.");

            if (!await _customersRepository.Exist(customer.Id.ToString()))
                return NotFound("Cliente não encontrado.");

            if (await _customersRepository.Update(customer))
                return Ok(customer);

            return StatusCode(500, "Erro ao atualizar o cliente.");
        }
    }
}
