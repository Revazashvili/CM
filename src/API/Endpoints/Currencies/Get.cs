using System.Collections.Generic;
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
    [Route(CurrencyRoutes.Get)]
    public class Get : BaseAsyncEndpoint
        .WithoutRequest
        .WithResponse<IResponse<IReadOnlyList<CurrencyDto>>>
    {
        private readonly IMediator _mediator;
        public Get(IMediator mediator) => _mediator = mediator;

        [HttpGet]
        [SwaggerOperation(Description = "Returns all currencies",
            Summary = "All currency",
            OperationId = "Currency.Get",
            Tags = new []{ "Currency" })]
        [SwaggerResponse(StatusCodes.Status200OK,"All currency retrieved from database.",typeof(IResponse<IReadOnlyList<CurrencyDto>>))]
        [SwaggerResponse(StatusCodes.Status400BadRequest,"No currency were found",typeof(IResponse<IReadOnlyList<CurrencyDto>>))]
        [Produces(MediaTypeNames.Application.Json)]
        [Consumes(MediaTypeNames.Application.Json)]
        public override async Task<ActionResult<IResponse<IReadOnlyList<CurrencyDto>>>> HandleAsync(
            CancellationToken cancellationToken = new())
        {
            var result = await _mediator.Send(new GetAllCurrencyQuery(), cancellationToken);
            return result.Succeeded ? Ok(result) : BadRequest(result);
        }
    }
}