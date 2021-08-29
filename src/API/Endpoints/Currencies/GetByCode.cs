using System.Net.Mime;
using System.Threading;
using System.Threading.Tasks;
using API.Routes;
using Application.Common.DTOs.Currencies;
using Application.Common.Models;
using Application.Queries.Currencies;
using Ardalis.ApiEndpoints;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace API.Endpoints.Currencies
{
    [Route(CurrencyRoutes.GetByCode)]
    public class GetByCode : BaseAsyncEndpoint
        .WithRequest<string>
        .WithResponse<IResponse<CurrencyDto>>
    {
        private readonly IMediator _mediator;

        public GetByCode(IMediator mediator) => _mediator = mediator;

        [HttpGet]
        [SwaggerOperation(Description = "Returns currency by id",
            Summary = "Retrieve single currency",
            OperationId = "Currency.GetByCode",
            Tags = new []{ "Currency" })]
        [SwaggerResponse(StatusCodes.Status200OK,"Currency retrieved from database.",typeof(IResponse<CurrencyDto>))]
        [SwaggerResponse(StatusCodes.Status400BadRequest,"No currency were found",typeof(IResponse<CurrencyDto>))]
        [Produces(MediaTypeNames.Application.Json)]
        [Consumes(MediaTypeNames.Application.Json)]
        public override async Task<ActionResult<IResponse<CurrencyDto>>> HandleAsync(
            [FromQuery,SwaggerParameter("Currency code")]string code,
            CancellationToken cancellationToken = new())
        {
            if (string.IsNullOrEmpty(code)) return BadRequest();
            var result = await _mediator.Send(new GetCurrencyQuery(x => x.Code == code), cancellationToken);
            return result.Succeeded ? Ok(result) : BadRequest(result);
        }
    }
}