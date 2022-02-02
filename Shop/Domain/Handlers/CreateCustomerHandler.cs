using System;
using Shop.Domain.Commands.Responses;
using Shop.Domain.Commands.Requests;
using MediatR;
using System.Threading.Tasks;
using System.Threading;

namespace Shop.Domain.Handlers
{
    //public class CreateCustomerHandler  : ICreateCustomerHandler
    public class CreateCustomerHandler  : IRequestHandler<CreateCustomerRequest, CreateCustomerResponse>
    {
        
    #region Trabalhando sem o MediatR

    //    public CreateCustomerResponse Handle(CreateCustomerRequest request)
    //    {
    //        // Verifica se o Cliente já esta cadastrado
    //        // Valida os dados
    //        //Insere o cliente
    //        // Enviar E-mail de boas vindas

    //        return new CreateCustomerResponse
    //        {
    //            Id = Guid.NewGuid(),
    //            Nome = "Willian",
    //            Email = "willian.rattis@email.com",
    //            Date = DateTime.Now
    //        };
    //    }
    #endregion
        Task<CreateCustomerResponse> IRequestHandler<CreateCustomerRequest, CreateCustomerResponse>.Handle(CreateCustomerRequest request, CancellationToken cancellationToken)
        {
           // Verifica se o Cliente já esta cadastrado
           // Valida os dados
           //Insere o cliente
           // Enviar E-mail de boas vindas

           var result = new CreateCustomerResponse
           {
               Id = Guid.NewGuid(),
               Nome = request.Nome,
               Email = request.Email,
               Date = DateTime.Now
           };

           return Task.FromResult(result);
        }
    }
}