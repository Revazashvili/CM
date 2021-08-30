using System.Net.Mime;
using System.Threading;
using System.Threading.Tasks;
using API.Routes;
using Application.Common.DTOs.Rates;
using Application.Common.DTOs.Users;
using Application.Common.Models;
using Application.Queries.Users;
using Ardalis.ApiEndpoints;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace API.Endpoints.Users
{
    [Route(UserRoutes.GetByPin)]
    public class GetByPin : BaseAsyncEndpoint
        .WithRequest<string>
        .WithResponse<UserDto>
    {
        private readonly IMediator _mediator;
        public GetByPin(IMediator mediator) => _mediator = mediator;

        [HttpGet]
        [SwaggerOperation(Description = "Returns user by pin",
            Summary = "Retrieve single user",
            OperationId = "User.GetByPin",
            Tags = new []{ "User" })]
        [SwaggerResponse(StatusCodes.Status200OK,"User retrieved from database.",typeof(IResponse<RateDto>))]
        [SwaggerResponse(StatusCodes.Status400BadRequest,"No user were found.",typeof(IResponse<RateDto>))]
        [Produces(MediaTypeNames.Application.Json)]
        [Consumes(MediaTypeNames.Application.Json)]
        public override async Task<ActionResult<UserDto>> HandleAsync(
            [FromQuery,SwaggerParameter(Required = true,Description = "Get user payload")]string pin, 
            CancellationToken cancellationToken = new())
        {
            var result = await _mediator.Send(new GetUserQuery(x => x.Pin == pin), cancellationToken);
            return result.Succeeded ? Ok(result) : BadRequest(result);
        }
    }
}