using System.Collections.Generic;
using System.Net;
using BackendEvalD2P2.Application.Events.Queries.GetEvent;
using BackendEvalD2P2.AzureFunctions.Extensions;
using BackendEvalD2P2.AzureFunctions.Models;
using BackendEvalD2P2.Domain.Exceptions;
using MediatR;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;

namespace BackendEvalD2P2.AzureFunctions.Functions.Http.Events;

public class GetEventHttpTrigger
{
    private readonly ILogger _logger;
    private readonly ISender _mediator;

    public GetEventHttpTrigger(ILoggerFactory loggerFactory, ISender mediator)
    {
        _mediator = mediator;
        _logger = loggerFactory.CreateLogger<GetEventHttpTrigger>();
    }

    [Function(nameof(GetEventHttpTrigger))]
    public async Task<HttpResponseData> Run([HttpTrigger(AuthorizationLevel.Function, "get", Route = "events/{id:guid}")] HttpRequestData req,
        FunctionContext executionContext,
        Guid id)
    {
        try
        {
            var command = new GetEventQuery
            {
                Id = id
            };

            var result = await _mediator.Send(command);

            var response = req.CreateResponse(HttpStatusCode.OK);

            await response.WriteAsJsonAsync(result);

            return response;
        }
        catch (NotFoundException e)
        {
            _logger.LogDebug(e, "Cannot find event with id {id}", id);

            var apiResponse = new ApiResponse(e.Message);

            return await req.CreateErrorResponseAsync(apiResponse, HttpStatusCode.NotFound);
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Internal server error while getting event");

            return req.CreateResponse(HttpStatusCode.InternalServerError);
        }
        
    }
}