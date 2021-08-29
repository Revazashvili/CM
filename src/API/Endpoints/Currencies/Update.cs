using System.Net.Mime;
using System.Threading;
using System.Threading.Tasks;
using API.Routes;
using Application.Commands.Currencies;
using Application.Common.DTOs.Currencies;
using Application.Common.Models;
using Ardalis.ApiEndpoints;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace API.Endpoints.Currencies
{
    [Route(CurrencyRoutes.Update)]
    public class Update : BaseAsyncEndpoint
        .WithRequest<CurrencyDto>
        .WithResponse<IResponse<string>>
    {
        private readonly IMediator _mediator;
        public Update(IMediator mediator) => _mediator = mediator;
        [HttpPut]
        [SwaggerOperation(Description = "Updates currency and returns currency code.",
            Summary = "Update currency",
            OperationId = "Currency.Update",
            Tags = new []{ "Currency" })]
        [SwaggerResponse(StatusCodes.Status200OK,"Currency updated successfully.",typeof(IResponse<string>))]
        [SwaggerResponse(StatusCodes.Status400BadRequest,"Error occurred while updating currency.",typeof(IResponse<string>))]
        [Produces(MediaTypeNames.Application.Json)]
        [Consumes(MediaTypeNames.Application.Json)]
        public override async Task<ActionResult<IResponse<string>>> HandleAsync(
            [FromBody,SwaggerRequestBody("Update currency payload")]CurrencyDto currencyDto,
            CancellationToken cancellationToken = new())
        {
            var result = await _mediator.Send(new UpdateCurrencyCommand(currencyDto), cancellationToken);
            return result.Succeeded ? Ok(result) : BadRequest(result);
        }
    }
}