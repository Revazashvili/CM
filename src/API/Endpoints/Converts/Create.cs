using System.Net.Mime;
using System.Threading;
using System.Threading.Tasks;
using API.Routes;
using Application.Commands.Converts;
using Application.Common.DTOs.Converts;
using Application.Common.Models;
using Ardalis.ApiEndpoints;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace API.Endpoints.Converts
{
    [Route(ConvertRoutes.Create)]
    public class Create : BaseAsyncEndpoint
        .WithRequest<ConvertDto>
        .WithResponse<IResponse<bool>>
    {
        private readonly IMediator _mediator;
        public Create(IMediator mediator) => _mediator = mediator;
        
        [HttpPost]
        [SwaggerOperation(Description = "Creates new convertation and returns true if successfully created,otherwise false.",
            Summary = "Create new convert.",
            OperationId = "Convert.Create",
            Tags = new []{ "Convert" })]
        [SwaggerResponse(StatusCodes.Status200OK,"New convertation created successfully.",typeof(IResponse<bool>))]
        [SwaggerResponse(StatusCodes.Status400BadRequest,"Error occurred while creating convertation.",typeof(IResponse<bool>))]
        [Produces(MediaTypeNames.Application.Json)]
        [Consumes(MediaTypeNames.Application.Json)]
        public override async Task<ActionResult<IResponse<bool>>> HandleAsync(
            [FromQuery,SwaggerParameter("Convert create payload")]ConvertDto request,
            CancellationToken cancellationToken = new())
        {
            var result = await _mediator.Send(new CreateConvertCommand(request), cancellationToken);
            return result.Succeeded ? Ok(result) : BadRequest(result);
        }
    }
}