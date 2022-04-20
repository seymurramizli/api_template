using Common.Contract;

namespace Contract;

/// <summary>
/// Base class used by API responses
/// </summary>
public class ApiResponse<T> : ApiResponse
{
    public T Data { get; set; }
}

public class ApiResponse : BaseMessage
{
    public ResponseCode Code { get; set; }

    public string Message { get; set; }

    public DateTime TimeStamp { get; set; } = DateTime.Now;


    public ApiResponse(Guid correlationId) : base()
    {
        _correlationId = correlationId;
        Code = ResponseCode.Success;
        Message = "Success";
    }
    public ApiResponse() : base()
    {
        Code = ResponseCode.Success;
        Message = "Success";
    }
}