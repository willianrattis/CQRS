using Ecommerce.Domain.Commands.Responses;
using MediatR;

namespace Ecommerce.Domain.Commands.Requests
{
    #region Trabalhando sem o MediatR
    // public class CriarClienteRequest : ICreateCustomerHandler
    // {
    //     public string Nome { get; set; }
    //     public string Email { get; set; }
    // }
    #endregion
    
    public class CriarClienteRequest : IRequest<CriarClienteResponse>
    {
        public string Nome { get; set; }
        public string Email { get; set; }
    }
}