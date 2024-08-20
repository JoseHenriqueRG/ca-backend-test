using BillingManagementSystem.Domain.Entities;
using BillingManagementSystem.Domain.Interfaces;
using BillingManagementSystem.Domain.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BillingManagementSystem.Application
{
    public class CustomerApplication(IRepository<Customer> repository) : ICrudApplication<Customer>
    {
        private readonly IRepository<Customer> _customersRepository = repository;

        public async Task<ActionResult<Customer>> Delete(Customer customer)
        {
            if (!await _customersRepository.Exist(customer.Id.ToString()))
                return new ActionResult<Customer>() { Success = false, Message = "Cliente não encontrado." };

            var result = await _customersRepository.Delete(customer);

            if (!result) 
                return new ActionResult<Customer>() { Success = false, Message = "Erro ao excluir o cliente." };

            return new ActionResult<Customer>() { Success = true, Message = "Cliente excluído com sucesso.", Value = customer };
        }

        public async Task<ActionResult<Customer>> Get(string id)
        {
            try
            {
                if(!await _customersRepository.Exist(id))
                    return new ActionResult<Customer>() { Success = false, Message = "Cliente não encontrado." };
                
                var customer = await _customersRepository.Get(id);                   

                return new ActionResult<Customer>() { Success = true, Message = "Dados do cliente carregado sucesso.", Value = customer };
            }
            catch (Exception) 
            {
                return new ActionResult<Customer>() { Success = false, Message = "Não foi possível obter os dados do cliente. Por favor, tente novamente mais tarde." };
            }
        }

        public async Task<ActionResult<IList<Customer>>> GetAll()
        {
            try
            {
                var customers = await _customersRepository.GetAll();

                return new ActionResult<IList<Customer>>() { Success = true, Message = "Lista de clientes carregada com sucesso.", Value = customers };
            }
            catch (Exception)
            {
                return new ActionResult<IList<Customer>>() { Success = false, Message = "Não foi possível recuperar a lista de clientes. Por favor, tente novamente mais tarde." };
            }
        }

        public async Task<ActionResult<Customer>> Insert(Customer customer)
        {
            customer.Id = null;

            if (await _customersRepository.Insert(customer))
                return new ActionResult<Customer>() { Success = true, Message = "Cliente cadastrado com sucesso.", Value = customer };

            return new ActionResult<Customer>() { Success = false, Message = "Não foi possível cadastrar o cliente. Por favor, tente novamente mais tarde." };
        }

        public async Task<ActionResult<Customer>> Update(Customer customer)
        {
            if (!await _customersRepository.Exist(customer.Id.ToString()))
                return new ActionResult<Customer>() { Success = false, Message = "Cliente não encontrado." };

            if (await _customersRepository.Update(customer))
                return new ActionResult<Customer>() { Success = true, Message = "Cliente atualizado com sucesso.", Value = customer };

            return new ActionResult<Customer>() { Success = false, Message = "Não foi possível atualizar o cliente. Por favor, tente novamente mais tarde." };
        }
    }
}
