using System.Collections.Generic;
using System.Net;
using BackendEvalD2P2.Application.Common.Models.DTOs;
using BackendEvalD2P2.Application.Events.Commands.EditEvent;
using BackendEvalD2P2.AzureFunctions.Extensions;
using BackendEvalD2P2.AzureFunctions.Models;
using BackendEvalD2P2.Domain.Exceptions;
using FluentValidation;
using MediatR;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;

namespace BackendEvalD2P2.AzureFunctions.Functions.Http.Events;

public class EditEventHttpTrigger
{
    private readonly ILogger _logger;
    private readonly ISender _mediator;

    public EditEventHttpTrigger(ILoggerFactory loggerFactory, ISender mediator)
    {
        _mediator = mediator;
        _logger = loggerFactory.CreateLogger<EditEventHttpTrigger>();
    }

    [Function(nameof(EditEventHttpTrigger))]
    public async Task<HttpResponseData> Run([HttpTrigger(AuthorizationLevel.Function, "put", Route = "events/{id:guid}")] HttpRequestData req,
        FunctionContext executionContext,
        Guid id)
    {
        try
        {
            var command = await req.ReadFromJsonAsync<EditEventCommand>();

            if (command is null)
            {
                _logger.LogDebug("Invalid request body");

                return req.CreateResponse(HttpStatusCode.BadRequest);
            }
            
            command.Id = id;

            var result = await _mediator.Send(command);

            var response = req.CreateResponse(HttpStatusCode.OK);

            await response.WriteAsJsonAsync(result);

            return response;
        }
        catch (ValidationException e)
        {
            _logger.LogDebug(e, "Validation error while editing event");

            var apiResponse = new ApiResponse<EventDto>(e.Errors);
            
            return await req.CreateErrorResponseAsync(apiResponse, HttpStatusCode.BadRequest);
        }
        catch (NotFoundException e)
        {
            _logger.LogDebug(e, "Cannot find event with id {id}", id);

            var apiResponse = new ApiResponse(e.Message);

            return await req.CreateErrorResponseAsync(apiResponse, HttpStatusCode.NotFound);
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Internal server error while editing event");

            return req.CreateResponse(HttpStatusCode.InternalServerError);
        } 
    }
}