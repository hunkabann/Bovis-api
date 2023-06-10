namespace Bovis.Common;

public class Response<T>
{
    public T Data { get; set; }
    public bool Success { get; set; }
    public string? Message { get; set; }
    public string? TransactionId { get; set; }

    public Response()
    {
        Success = false;
        Message = default;
        TransactionId = default;
        Data = Activator.CreateInstance<T>();
    }

    public void AddError(Exception ex)
    {
        Success = false;
        Message = $"error:\"{ex.Message}\", stacktrace:\"{ex.StackTrace}\"";
    }
    public void AddError(string ex)
    {
        Success = false;
        Message = $"error:\"{ex}\", stacktrace:null";
    }

    public void AddError<X>(Response<X> ex) where X : class
    {
        Success = false;
        Message = ex.Message;
    }

}
