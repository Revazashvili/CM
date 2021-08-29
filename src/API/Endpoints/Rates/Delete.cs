using System.Net.Mime;
using System.Threading;
using System.Threading.Tasks;
using API.Routes;
using Application.Commands.Rates;
using Application.Common.Models;
using Ardalis.ApiEndpoints;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace API.Endpoints.Rates
{
    [Route(RateRoutes.Delete)]
    public class Delete : BaseAsyncEndpoint
        .WithRequest<int>
        .WithResponse<IResponse<bool>>
    {
        private readonly IMediator _mediator;
        public Delete(IMediator mediator) => _mediator = mediator;
        
        [HttpDelete]
        [SwaggerOperation(Description = "Deletes rate and returns true if successfully deleted,otherwise false.",
            Summary = "Deletes rate",
            OperationId = "Rate.Delete",
            Tags = new []{ "Rate" })]
        [SwaggerResponse(StatusCodes.Status200OK,"Rate deleted successfully.",typeof(IResponse<bool>))]
        [SwaggerResponse(StatusCodes.Status400BadRequest,"Error occurred while deleting rate.",typeof(IResponse<bool>))]
        [Produces(MediaTypeNames.Application.Json)]
        [Consumes(MediaTypeNames.Application.Json)]
        public override async Task<ActionResult<IResponse<bool>>> HandleAsync(
            [FromQuery,SwaggerParameter("Rate id",Required = true)]int id,
            CancellationToken cancellationToken = new())
        {
            var result = await _mediator.Send(new DeleteRateCommand(id), cancellationToken);
            return result.Succeeded ? Ok(result) : BadRequest(result);
        }
    }
}