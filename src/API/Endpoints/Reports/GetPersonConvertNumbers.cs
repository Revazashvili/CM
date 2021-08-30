using System.Collections.Generic;
using System.Net.Mime;
using System.Threading;
using System.Threading.Tasks;
using API.Routes;
using Application.Common.DTOs.Reports;
using Application.Queries.Reports;
using Ardalis.ApiEndpoints;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace API.Endpoints.Reports
{
    [Route(ReportRoutes.GetPersonConvertNumbers)]
    public class GetPersonConvertNumbers : BaseAsyncEndpoint
        .WithRequest<GetPersonConvertNumbersDto>
        .WithResponse<IReadOnlyList<PersonConvertNumbersDto>>
    {
        private readonly IMediator _mediator;
        public GetPersonConvertNumbers(IMediator mediator) => _mediator = mediator;

        [HttpGet]
        [SwaggerOperation(Summary = "Retrieve convert numbers",
            OperationId = "Report.GetPersonConvertNumbers",
            Tags = new []{ "Report" })]
        [Produces(MediaTypeNames.Application.Json)]
        [Consumes(MediaTypeNames.Application.Json)]
        public override async Task<ActionResult<IReadOnlyList<PersonConvertNumbersDto>>> HandleAsync(
            [FromQuery]GetPersonConvertNumbersDto request, CancellationToken cancellationToken = new())
        {
            var result = await _mediator.Send(new GetPersonConvertNumbersQuery(request.From, request.To),
                cancellationToken);
            return result.Succeeded ? Ok(result) : BadRequest(result);
        }
    }
}