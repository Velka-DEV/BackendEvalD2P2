using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace BackendEvalD2P2.WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public abstract class ApiControllerBase<TController> : ControllerBase
    where TController : ApiControllerBase<TController>
{
    protected ApiControllerBase(ISender sender, ILogger<TController> logger)
    {
        Sender = sender;
        Logger = logger;
    }

    protected ILogger<TController> Logger { get; }
    
    protected ISender Sender { get; }
}