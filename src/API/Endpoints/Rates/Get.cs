using System.Collections.Generic;
using System.Net.Mime;
using System.Threading;
using System.Threading.Tasks;
using API.Routes;
using Application.Common.DTOs.Rates;
using Application.Common.Models;
using Application.Queries.Rates;
using Ardalis.ApiEndpoints;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace API.Endpoints.Rates
{
    [Route(RateRoutes.Get)]
    public class Get : BaseAsyncEndpoint
        .WithoutRequest
        .WithResponse<IResponse<IReadOnlyList<RateDto>>>
    {
        private readonly IMediator _mediator;
        public Get(IMediator mediator) => _mediator = mediator;

        [HttpGet]
        [SwaggerOperation(Description = "Returns all rates",
            Summary = "All rate",
            OperationId = "Rate.Get",
            Tags = new []{ "Rate" })]
        [SwaggerResponse(StatusCodes.Status200OK,"All rate retrieved from database.",typeof(IResponse<IReadOnlyList<RateDto>>))]
        [SwaggerResponse(StatusCodes.Status400BadRequest,"No rate were found.",typeof(IResponse<IReadOnlyList<RateDto>>))]
        [Produces(MediaTypeNames.Application.Json)]
        [Consumes(MediaTypeNames.Application.Json)]
        public override async Task<ActionResult<IResponse<IReadOnlyList<RateDto>>>> HandleAsync(
            CancellationToken cancellationToken = new())
        {
            var result = await _mediator.Send(new GetAllRateQuery(), cancellationToken);
            return result.Succeeded ? Ok(result) : BadRequest(result);
        }
    }
}