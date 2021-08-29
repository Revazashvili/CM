using System.Net.Mime;
using System.Threading;
using System.Threading.Tasks;
using API.Routes;
using Application.Common.DTOs.Rates;
using Application.Common.Models;
using Ardalis.ApiEndpoints;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace API.Endpoints.Rates
{
    [Route(RateRoutes.AmountToGive)]
    public class AmountToBePaid : BaseAsyncEndpoint
        .WithRequest<CalculateAmountToGive>
        .WithResponse<double>
    {
        private readonly IMediator _mediator;
        public AmountToBePaid(IMediator mediator) => _mediator = mediator;

        [HttpGet]
        [SwaggerOperation(Description = "Calculates amount to be paid based on current day sell rate.",
            Summary = "Return amount to be paid",
            OperationId = "Rate.AmountToGive",
            Tags = new []{ "Rate" })]
        [SwaggerResponse(StatusCodes.Status200OK,"Amount calculated successfully.",typeof(IResponse<double>))]
        [SwaggerResponse(StatusCodes.Status400BadRequest,"No rate were found or error occured while calculating.",typeof(IResponse<double>))]
        [Produces(MediaTypeNames.Application.Json)]
        [Consumes(MediaTypeNames.Application.Json)]
        public override Task<ActionResult<double>> HandleAsync(
            [FromQuery,SwaggerParameter(Required = true,Description = "Calculate amount payload")]CalculateAmountToGive request,
            CancellationToken cancellationToken = new())
        {
            throw new System.NotImplementedException();
        }
    }
}