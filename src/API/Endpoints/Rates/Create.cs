using System;
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
    [Route(RateRoutes.Create)]
    public class Create : BaseAsyncEndpoint
        .WithRequest<CreateRateDto>
        .WithResponse<IResponse<int>>
    {
        private readonly IMediator _mediator;
        public Create(IMediator mediator) => _mediator = mediator;
        
        [HttpPost]
        [SwaggerOperation(Description = "Creates new rate and returns created entity id and location where resource is created.",
            Summary = "Create new rate.",
            OperationId = "Rate.Create",
            Tags = new []{ "Rate" })]
        [SwaggerResponse(StatusCodes.Status201Created,"New currency created successfully.",typeof(IResponse<int>))]
        [SwaggerResponse(StatusCodes.Status400BadRequest,"Error occurred while creating rate.",typeof(IResponse<int>))]
        [Produces(MediaTypeNames.Application.Json)]
        [Consumes(MediaTypeNames.Application.Json)]
        public override async Task<ActionResult<IResponse<int>>> HandleAsync(
            [FromBody,SwaggerRequestBody(Required = true,Description = "Create rate payload")]CreateRateDto rateDto,
            CancellationToken cancellationToken = new())
        {
            var result = await _mediator.Send(new CreateRateCommand(rateDto), cancellationToken);
            return result.Succeeded
                ? Created(
                    new Uri(
                        $"{Request.Scheme}://{Request.Host.ToString()}/{RateRoutes.GetById}?id={result.Data}"),
                    result)
                : BadRequest(result);
        }
    }
}