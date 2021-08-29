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
    [Route(RateRoutes.GetById)]
    public class GetById : BaseAsyncEndpoint
        .WithRequest<int>
        .WithResponse<IResponse<RateDto>>
    {
        private readonly IMediator _mediator;

        public GetById(IMediator mediator) => _mediator = mediator;

        [HttpGet]
        [SwaggerOperation(Description = "Returns rate by id",
            Summary = "Retrieve single rate",
            OperationId = "Rate.GetById",
            Tags = new []{ "Rate" })]
        [SwaggerResponse(StatusCodes.Status200OK,"Rate retrieved from database.",typeof(IResponse<RateDto>))]
        [SwaggerResponse(StatusCodes.Status400BadRequest,"No rate were found.",typeof(IResponse<RateDto>))]
        [Produces(MediaTypeNames.Application.Json)]
        [Consumes(MediaTypeNames.Application.Json)]
        public override async Task<ActionResult<IResponse<RateDto>>> HandleAsync(
            [FromQuery,SwaggerParameter("Currency code")]int id,
            CancellationToken cancellationToken = new())
        {
            if (id <= 0) return BadRequest();
            var result = await _mediator.Send(new GetRateQuery(x => x.Id == id), cancellationToken);
            return result.Succeeded ? Ok(result) : BadRequest(result);
        }
    }
}