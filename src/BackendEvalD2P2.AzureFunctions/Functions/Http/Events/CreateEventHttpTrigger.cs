using BackendEvalD2P2.Application.Common.Models.DTOs;
using BackendEvalD2P2.Application.Events.Commands.CreateEvent;
using BackendEvalD2P2.AzureFunctions.Extensions;
using BackendEvalD2P2.AzureFunctions.Models;
using FluentValidation;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;
using System.Net;
using MediatR;

namespace BackendEvalD2P2.AzureFunctions.Functions.Http.Events;

public class CreateEventHttpTrigger
{
    private readonly ILogger _logger;
    private readonly ISender _mediator;

    public CreateEventHttpTrigger(ILoggerFactory loggerFactory, ISender mediator)
    {
        _mediator = mediator;
        _logger = loggerFactory.CreateLogger<CreateEventHttpTrigger>();
    }

    [Function(nameof(CreateEventHttpTrigger))]
    public async Task<HttpResponseData> Run([HttpTrigger(AuthorizationLevel.Function, "post", Route = "events")] HttpRequestData req,
        FunctionContext executionContext)
    {
        try 
        {
            var command = await req.ReadFromJsonAsync<CreateEventCommand>();

            if (command is null)
            {
                _logger.LogDebug("Invalid request body");

                return req.CreateResponse(HttpStatusCode.BadRequest);
            }

            var result = await _mediator.Send(command);

            var response = req.CreateResponse(HttpStatusCode.OK);

            await response.WriteAsJsonAsync(result);

            return response;
        }
        catch (ValidationException e)
        {
            _logger.LogDebug(e, "Validation error while creating event");

            var apiResponse = new ApiResponse<EventDto>(e.Errors);
            
            return await req.CreateErrorResponseAsync(apiResponse, HttpStatusCode.BadRequest);
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Internal server error while creating event");

            return req.CreateResponse(HttpStatusCode.InternalServerError);
        } 
    }
}