namespace Library.Core.Interfaces;

public interface IResult
{
    public bool IsSuccessful { get; set; }
    public IEnumerable<string> Errors { get; set; }
}