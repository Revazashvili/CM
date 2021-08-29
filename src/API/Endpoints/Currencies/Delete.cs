using System.Net.Mime;
using System.Threading;
using System.Threading.Tasks;
using API.Routes;
using Application.Commands.Currencies;
using Application.Common.Models;
using Ardalis.ApiEndpoints;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace API.Endpoints.Currencies
{
    [Route(CurrencyRoutes.Delete)]
    public class Delete : BaseAsyncEndpoint
        .WithRequest<string>
        .WithResponse<IResponse<bool>>
    {
        private readonly IMediator _mediator;
        public Delete(IMediator mediator) => _mediator = mediator;
        
        [HttpDelete]
        [SwaggerOperation(Description = "Deletes currency and returns true if successfully deleted, otherwise false.",
            Summary = "Deletes currency",
            OperationId = "Currency.Delete",
            Tags = new []{ "Currency" })]
        [SwaggerResponse(StatusCodes.Status200OK,"Currency deleted successfully.",typeof(IResponse<bool>))]
        [SwaggerResponse(StatusCodes.Status400BadRequest,"Error occurred while deleting weather forecast.",typeof(IResponse<bool>))]
        [Produces(MediaTypeNames.Application.Json)]
        [Consumes(MediaTypeNames.Application.Json)]
        public override async Task<ActionResult<IResponse<bool>>> HandleAsync(
            [FromQuery,SwaggerParameter("Currency code",Required = true)]string code,
            CancellationToken cancellationToken = new())
        {
            var result = await _mediator.Send(new DeleteCurrencyCommand(code), cancellationToken);
            return result.Succeeded ? Ok(result) : BadRequest(result);
        }
    }
}