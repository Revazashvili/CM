using System;
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
    [Route(CurrencyRoutes.Create)]
    public class Create : BaseAsyncEndpoint
        .WithRequest<CurrencyDto>
        .WithResponse<IResponse<string>>
    {
        private readonly IMediator _mediator;
        public Create(IMediator mediator) => _mediator = mediator;
        
        [HttpPost]
        [SwaggerOperation(Description = "Creates new currency and returns created entity code and location where resource is created.",
            Summary = "Create new currency.",
            OperationId = "Currency.Create",
            Tags = new []{ "Currency" })]
        [SwaggerResponse(StatusCodes.Status201Created,"New currency created successfully.",typeof(IResponse<string>))]
        [SwaggerResponse(StatusCodes.Status400BadRequest,"Error occurred while creating currency.",typeof(IResponse<string>))]
        [Produces(MediaTypeNames.Application.Json)]
        [Consumes(MediaTypeNames.Application.Json)]
        public override async Task<ActionResult<IResponse<string>>> HandleAsync(
            [FromBody,SwaggerRequestBody(Required = true,Description = "Create currency payload")]CurrencyDto currencyDto,
            CancellationToken cancellationToken = new())
        {
            var result = await _mediator.Send(new CreateCurrencyCommand(currencyDto), cancellationToken);
            return result.Succeeded
                ? Created(
                    new Uri(
                        $"{Request.Scheme}://{Request.Host.ToString()}/{CurrencyRoutes.GetByCode}?code={result.Data}"),
                    result)
                : BadRequest(result);
        }
    }
}