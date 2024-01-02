using System.Net;
using Newtonsoft.Json;

namespace Library.API.Responses;

public class ResponseDTO<T>
{
    public int StatusCode { get; set; }
    public bool IsSuccessful { get; set; }
    public string Message { get; set; } = string.Empty;
    public IEnumerable<string>? Errors { get; set; } = Array.Empty<string>();
    public T? Data { get; set; }

    public static ResponseDTO<T> Fail(IEnumerable<string> errors, int statusCode = (int)HttpStatusCode.InternalServerError)
    {
        return new ResponseDTO<T>
        {
            IsSuccessful = false,
            Errors = errors,
            StatusCode = statusCode
        };
    }
        
    public static ResponseDTO<T> Success(T data, string successMessage = "", int statusCode = (int)HttpStatusCode.OK)
    {
        return new ResponseDTO<T>
        {
            IsSuccessful = true,
            Message = successMessage,
            Data = data,
            StatusCode = statusCode
        };
    }
        
    public override string ToString() => JsonConvert.SerializeObject(this);
}