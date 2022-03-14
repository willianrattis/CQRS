using System.Net;
using System.Threading.Tasks;
using Ecommerce.Domain.Commands.Requests;
using Ecommerce.Domain.Commands.Responses;
using Ecommerce.Domain.Handlers;
using MediatR;
using Microsoft.AspNetCore.Mvc;

// using Ecommerce.Domain.Handlers;

namespace Ecommerce.Controllers
{
    #region Trabalhando sem o MediatR
    // [ApiController]
    // [Route("clientes")]
    // public class ClienteController : ControllerBase
    // {
    //     [HttpPost("cadastro")]
    //     public CriarClienteResponse Create([FromServices] ICriarClienteHandler handler,
    //                                        [FromBody] CriarClienteRequest command)
    //     {
    //         return handler.Handle(command);
    //     }
    // }
    #endregion
    
    [ApiController]
    [Route("v1/clientes")]
    public class ClienteController : ControllerBase
    {

        [HttpPost("cadastrar")]
        [ProducesResponseType(typeof(CriarClienteResponse), (int)HttpStatusCode.OK)]
        public Task<CriarClienteResponse> CreateFromBody([FromServices] IMediator mediatr, 
            [FromBody]  CriarClienteRequest command)
        {
            return mediatr.Send(command);
        }
    }
}