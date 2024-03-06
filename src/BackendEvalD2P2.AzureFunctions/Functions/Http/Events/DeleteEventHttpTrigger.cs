using System.Collections.Generic;
using System.Net;
using BackendEvalD2P2.Application.Events.Commands.DeleteEvent;
using BackendEvalD2P2.AzureFunctions.Extensions;
using BackendEvalD2P2.AzureFunctions.Models;
using BackendEvalD2P2.Domain.Exceptions;
using MediatR;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;

namespace BackendEvalD2P2.AzureFunctions.Functions.Http.Events;

public class DeleteEventHttpTrigger
{
    private readonly ILogger _logger;
    private readonly ISender _mediator;

    public DeleteEventHttpTrigger(ILoggerFactory loggerFactory, ISender mediator)
    {
        _mediator = mediator;
        _logger = loggerFactory.CreateLogger<DeleteEventHttpTrigger>();
    }

    [Function(nameof(DeleteEventHttpTrigger))]
    public async Task<HttpResponseData> Run([HttpTrigger(AuthorizationLevel.Function, "delete", Route = "events/{id:guid}")] HttpRequestData req,
        FunctionContext executionContext,
        Guid id)
    {
        try
        {
            var command = new DeleteEventCommand
            {
                Id = id
            };

            await _mediator.Send(command);

            var response = req.CreateResponse(HttpStatusCode.NoContent);
            await response.WriteAsJsonAsync(new ApiResponse("Successfully deleted event", true));
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
            _logger.LogError(e, "Internal server error while deleting event");

            return req.CreateResponse(HttpStatusCode.InternalServerError);
        }
    }
}