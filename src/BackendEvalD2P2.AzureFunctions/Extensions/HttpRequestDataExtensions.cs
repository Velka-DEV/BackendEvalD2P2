using System.Net;
using BackendEvalD2P2.AzureFunctions.Models;
using Microsoft.Azure.Functions.Worker.Http;

namespace BackendEvalD2P2.AzureFunctions.Extensions;

public static class HttpRequestDataExtensions
{
    public static async Task<HttpResponseData> CreateErrorResponseAsync(this HttpRequestData req, ApiResponse apiResponse, HttpStatusCode code)
    {
        var response = req.CreateResponse(code);

        await response.WriteAsJsonAsync(apiResponse);

        response.StatusCode = code;

        return response;
    }
}