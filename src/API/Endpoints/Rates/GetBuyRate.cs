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
    [Route(RateRoutes.GetBuyRate)]
    public class GetBuyRate : BaseAsyncEndpoint
        .WithRequest<GetBuyRateDto>
        .WithResponse<double>
    {
        private readonly IMediator _mediator;
        public GetBuyRate(IMediator mediator) => _mediator = mediator;

        [HttpGet]
        [SwaggerOperation(Description = "Returns current day sell rate by give and receive currency",
            Summary = "Return sell rate",
            OperationId = "Rate.GetBuyRate",
            Tags = new []{ "Rate" })]
        [SwaggerResponse(StatusCodes.Status200OK,"Rate retrieved from database.",typeof(IResponse<double>))]
        [SwaggerResponse(StatusCodes.Status400BadRequest,"No rate were found.",typeof(IResponse<double>))]
        [Produces(MediaTypeNames.Application.Json)]
        [Consumes(MediaTypeNames.Application.Json)]
        public override async Task<ActionResult<double>> HandleAsync(
            [FromQuery,SwaggerParameter(Required = true,Description = "Get sell rate payload")]GetBuyRateDto request,
            CancellationToken cancellationToken = new())
        {
            var result =
                await _mediator.Send(new GetRateQuery(x => x.From.Code == request.From && x.To.Code == request.To),
                    cancellationToken);
            return result.Succeeded ? Ok(result.Data.Buy) : BadRequest(result.Data.Buy);
        }
    }
}