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
    public class ProductApplication(IRepository<Product> repository) : ICrudApplication<Product>
    {
        private readonly IRepository<Product> _productsRepository = repository;

        public async Task<ActionResult<Product>> Delete(Product product)
        {
            if (!await _productsRepository.Exist(product.ProductId.ToString()))
                return new ActionResult<Product>() { Success = false, Message = "Produto não encontrado." };

            var result = await _productsRepository.Delete(product);

            if (!result)
                return new ActionResult<Product>() { Success = false, Message = "Erro ao excluir o produto. Por favor, tente novamente mais tarde." };

            return new ActionResult<Product>() { Success = true, Message = "Produto excluído com sucesso." };
        }

        public async Task<ActionResult<Product>> Get(string id)
        {
            try
            {
                if (!await _productsRepository.Exist(id))
                    return new ActionResult<Product>() { Success = false, Message = "Produto não encontrado." };

                var product = await _productsRepository.Get(id);

                return new ActionResult<Product>() { Success = true, Message = "Dados do produto carregado com sucesso.", Value = product };
            }
            catch 
            {
                return new ActionResult<Product>() { Success = false, Message = "Não foi possível obter os dados do produto. Por favor, tente novamente mais tarde." };
            }
        }

        public async Task<ActionResult<IList<Product>>> GetAll()
        {
            try
            {
                var products = await _productsRepository.GetAll();

                return new ActionResult<IList<Product>>() { Success = true, Message = "Lista de produtos carregada com sucesso.", Value = products };
            }
            catch (Exception)
            {
                return new ActionResult<IList<Product>>() { Success = false, Message = "Não foi possível recuperar a lista de produtos. Por favor, tente novamente mais tarde." };
            }
        }

        public async Task<ActionResult<Product>> Insert(Product product)
        {
            product.ProductId = null;

            if (await _productsRepository.Insert(product))
                return new ActionResult<Product>() { Success = true, Message = "Produto cadastrado com sucesso.", Value = product };

            return new ActionResult<Product>() { Success = false, Message = "Não foi possível cadastrar o produto. Por favor, tente novamente mais tarde." };
        }

        public async Task<ActionResult<Product>> Update(Product product)
        {
            if (!await _productsRepository.Exist(product.ProductId.ToString()))
                return new ActionResult<Product>() { Success = false, Message = "Produto não encontrado." };

            if (await _productsRepository.Update(product))
                return new ActionResult<Product>() { Success = true, Message = "Produto atualizado com sucesso.", Value = product };

            return new ActionResult<Product>() { Success = false, Message = "Não foi possível atualizar o produto. Por favor, tente novamente mais tarde." };
        }
    }
}
