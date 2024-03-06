using System.Text.Json.Serialization;
using FluentValidation.Results;

namespace BackendEvalD2P2.AzureFunctions.Models;

public class ApiResponse
{
    public bool Success { get; set; }
    
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public string? Message { get; set; }
    
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public IEnumerable<ValidationFailure>? Errors { get; set; }
    
    public ApiResponse()
    {
        Success = true;
    }
    
    public ApiResponse(IEnumerable<ValidationFailure> errors)
    {
        Success = false;
        Message = "Validation error(s) occurred.";
        Errors = errors;
    }
    
    public ApiResponse(string message, bool success = false)
    {
        Success = success;
        Message = message;
    }
}

public class ApiResponse<TResult> : ApiResponse
{
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public TResult? Data { get; set; }
    
    public ApiResponse(TResult data)
    {
        Success = true;
        Data = data;
    }
    
    public ApiResponse(IEnumerable<ValidationFailure> errors) : base(errors)
    {
    }
    
    public ApiResponse(string message, bool success = false) : base(message, success)
    {
    }
}