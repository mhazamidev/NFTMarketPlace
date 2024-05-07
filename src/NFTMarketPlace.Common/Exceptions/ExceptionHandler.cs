namespace NFTMarketPlace.Common.Exceptions;



public interface IExceptionHandler
{
    ExceptionHandler HandleException(Exception ex);
    Exception GetException(ExceptionHandler ex);
}
public class ExceptionHandler : IExceptionHandler
{
    public string Message { get; set; }
    public string? StackTrace { get; set; }
    public string? Source { get; set; }
    public ExceptionHandler? InnerException { get; set; }
    public ExceptionHandler HandleException(Exception ex)
    {
        ExceptionHandler exceptionBase = new() { Message = ex.Message, StackTrace = ex.StackTrace, Source = ex.Source };
        if (ex.InnerException != null)
            HandleException(ex.InnerException);
        return exceptionBase;
    }
    public Exception GetException(ExceptionHandler ex)
    {
        var handleException = ex.InnerException != null ? GetException(ex.InnerException) : null;
        var exceptionBase = new Exception(ex.Message, handleException);
        return exceptionBase;
    }
}
