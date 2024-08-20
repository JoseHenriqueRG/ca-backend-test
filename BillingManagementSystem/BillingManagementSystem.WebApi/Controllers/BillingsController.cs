using BillingManagementSystem.Domain.Entities;
using BillingManagementSystem.Domain.Interfaces;
using BillingManagementSystem.Domain.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BillingManagementSystem.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BillingsController(IBillingApplication billingApplication) : ControllerBase
    {
        private readonly IBillingApplication _billingApplication = billingApplication;

        /// <summary>
        /// Insere uma nova fatura.
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> Insert(BillingViewModel viewModel)
        {
            var result = await _billingApplication.Insert(viewModel);

            if (result.Success)
                return Ok(result);

            return StatusCode(500, result.Message);
        }

        /// <summary>
        /// Lista todas as faturas.
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _billingApplication.GetAll();

            if (result.Success)
                return Ok(result);
            
            return StatusCode(500, result.Message);
        }
    }
}
