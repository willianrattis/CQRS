using System;
using MediatR;
using System.Threading.Tasks;
using System.Threading;
using Ecommerce.Domain.Commands.Requests;
using Ecommerce.Domain.Commands.Responses;

namespace Ecommerce.Domain.Handlers
{
    #region Trabalhando sem o MediatR
    //public class CreateCustomerHandler  : ICreateCustomerHandler
    //{
    //    public CreateCustomerResponse Handle(CreateCustomerRequest request)
    //    {
    //        // Verifica se o Cliente já esta cadastrado
    //        // Valida os dados
    //        //Insere o cliente
    //        // Enviar E-mail de boas vindas
    //
    //        return new CreateCustomerResponse
    //        {
    //            Id = Guid.NewGuid(),
    //            Nome = "Willian",
    //            Email = "willian.rattis@email.com",
    //            Date = DateTime.Now
    //        };
    //    }
    //}
    #endregion
    
    public class CriarClienteHandler  : IRequestHandler<CriarClienteRequest, CriarClienteResponse>
    {
        
        public async Task<CriarClienteResponse> Handle(CriarClienteRequest request, CancellationToken cancellationToken)
        {
           // Verifica se o Cliente já esta cadastrado
           // Valida os dados
           // Insere o cliente
           // Enviar E-mail de boas vindas

           var resultado = new CriarClienteResponse
           {
               Id = Guid.NewGuid(),
               Nome = request.Nome,
               Email = request.Email,
               Date = DateTime.Now
           };

           return await Task.FromResult(resultado);
        }
    }
}