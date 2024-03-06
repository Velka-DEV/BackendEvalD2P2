using System.Collections.Generic;
using System.Net;
using BackendEvalD2P2.Application.Events.Queries.ListEvents;
using MediatR;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;

namespace BackendEvalD2P2.AzureFunctions.Functions.Http.Events;

public class ListEventsHttpTrigger
{
    private readonly ILogger _logger;
    private readonly ISender _mediator;

    public ListEventsHttpTrigger(ILoggerFactory loggerFactory, ISender mediator)
    {
        _mediator = mediator;
        _logger = loggerFactory.CreateLogger<ListEventsHttpTrigger>();
    }

    [Function(nameof(ListEventsHttpTrigger))]
    public async Task<HttpResponseData> Run([HttpTrigger(AuthorizationLevel.Function, "get", Route = "events")] HttpRequestData req,
        FunctionContext executionContext)
    {
        try
        {
            var result = await _mediator.Send(new ListEventsQuery());

            var response = req.CreateResponse(HttpStatusCode.OK);

            await response.WriteAsJsonAsync(result);

            return response;
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Internal server error while getting events");

            return req.CreateResponse(HttpStatusCode.InternalServerError);
        }
    }
}