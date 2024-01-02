using Library.Core.Interfaces;

namespace Library.Core.Result;

public class Result<T> : IResult
{
    public bool IsSuccessful { get; set; }
    public IEnumerable<string> Errors { get; set; } = Array.Empty<string>();
    public T? Data { get; set; }
    
    public static Result<T> Fail(IEnumerable<string> errors )
    {
        return new Result<T>
        {
            IsSuccessful = false,
            Errors = errors,
        };
    }
        
    public static Result<T> Success(T data)
    {
        return new Result<T>
        {
            IsSuccessful = true,
            Data = data,
        };
    }
}