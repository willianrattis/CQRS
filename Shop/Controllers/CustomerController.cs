using Microsoft.AspNetCore.Mvc;
using Shop.Domain.Commands.Requests;
using Shop.Domain.Commands.Responses;
// using Shop.Domain.Handlers;
using MediatR;
using System.Threading.Tasks;
using System.Net;
using Shop.Domain.Handlers;

namespace Shop.Controllers
{
    [ApiController]
    [Route("v1/customers")]
    public class CustomerController : ControllerBase
    {

        #region Trabalhando sem o MediatR
        // [HttpPost("")]
        // public CreateCustomerResponse Create([FromServices] ICreateCustomerHandler handler, 
        //                                      [FromBody]  CreateCustomerRequest command)
        // {
        //     return handler.Handle(command);
        // }

        // Utilizando o MediatR
        #endregion

        [HttpPost()]
        [Route("sem-from-body")]
        public Task<CreateCustomerResponse> Create([FromServices] IMediator mediatr,
                                      CreateCustomerRequest command)
        {
            return mediatr.Send(command);
        }

        [HttpPost()]
        [Route("com-from-body")]
        public Task<CreateCustomerResponse> CreateFromBody([FromServices] IMediator mediatr, 
                                             [FromBody]  CreateCustomerRequest command)
        {
            return mediatr.Send(command);
        }

        [HttpPost()]
        [Route("com-from-query")]
        public Task<CreateCustomerResponse> CreateQueryString([FromServices] IMediator mediatr,
                                             [FromQuery] CreateCustomerRequest command)
        {
            return mediatr.Send(command);
        }

        [ProducesResponseType(typeof(CreateCustomerResponse), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ApiError), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(ApiError), (int)HttpStatusCode.NotFound)]
        [HttpPost()]
        [Route("com-responsetype")]
        public Task<CreateCustomerResponse> CreateResponseType([FromServices] IMediator mediatr,
                                             CreateCustomerRequest command)
        {
            return mediatr.Send(command);
        }
    }
}