using System.Net.Mime;
using System.Threading;
using System.Threading.Tasks;
using API.Routes;
using Application.Commands.Rates;
using Application.Common.DTOs.Rates;
using Application.Common.Models;
using Ardalis.ApiEndpoints;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace API.Endpoints.Rates
{
    [Route(RateRoutes.Update)]
    public class Update : BaseAsyncEndpoint
        .WithRequest<UpdateRateDto>
        .WithResponse<IResponse<int>>
    {
        private readonly IMediator _mediator;
        public Update(IMediator mediator) => _mediator = mediator;
        [HttpPut]
        [SwaggerOperation(Description = "Updates rate and returns id.",
            Summary = "Update rate",
            OperationId = "Rate.Update",
            Tags = new []{ "Rate" })]
        [SwaggerResponse(StatusCodes.Status200OK,"Rate updated successfully.",typeof(IResponse<int>))]
        [SwaggerResponse(StatusCodes.Status400BadRequest,"Error occurred while updating rate.",typeof(IResponse<int>))]
        [Produces(MediaTypeNames.Application.Json)]
        [Consumes(MediaTypeNames.Application.Json)]
        public override async Task<ActionResult<IResponse<int>>> HandleAsync(
            [FromBody,SwaggerRequestBody("Update rate payload")]UpdateRateDto updateRateDto,
            CancellationToken cancellationToken = new())
        {
            var result = await _mediator.Send(new UpdateRateCommand(updateRateDto), cancellationToken);
            return result.Succeeded ? Ok(result) : BadRequest(result);
        }
    }
}