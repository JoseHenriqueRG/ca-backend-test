using BillingManagementSystem.Domain.Entities;
using BillingManagementSystem.Domain.Interfaces;
using BillingManagementSystem.Domain.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace BillingManagementSystem.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController(ICrudApplication<Customer> crudApplication) : ControllerBase
    {
        private readonly ICrudApplication<Customer> _crudApplication = crudApplication;
        /// <summary>
        /// Obtém um cliente.
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpGet("{Id}")]
        public async Task<IActionResult> Get(Guid Id)
        {
            var result = await _crudApplication.Get(Id.ToString());

            if (!result.Success)
                return BadRequest(result.Message);

            return Ok(result);
        }

        /// <summary>
        /// Lista todos os clientes.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _crudApplication.GetAll();

            if (!result.Success) 
                return StatusCode(500, result.Message);

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

            var result = await _crudApplication.Delete(customer);

            if (!result.Success)
                return StatusCode(500, result.Message);

            return Ok(result);
        }

        /// <summary>
        /// Adiciona um cliente.
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> Insert(Customer customer)
        {
            var result = await _crudApplication.Insert(customer);

            if (!result.Success)
                return StatusCode(500, result.Message);

            return Ok(result);
        }

        /// <summary>
        /// Edita um cliente.
        /// </summary>
        [HttpPut]
        public async Task<IActionResult> Update(Customer customer)
        {
            if (string.IsNullOrWhiteSpace(customer.Id.ToString()))
                return BadRequest("Id inválido.");

            var result = await _crudApplication.Update(customer);

            if (!result.Success)
                return StatusCode(500, result.Message);

            return Ok(result);
        }
    }
}
