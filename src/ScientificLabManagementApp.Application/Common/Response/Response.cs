namespace ScientificLabManagementApp.Application;

public class Response<T>
{
    public Response()
    {
        Errors = new List<string>();
    }

    public Response(T data, string message = null)
    {
        Succeeded = true;
        Message = message;
        Data = data;
        Errors = new List<string>();
    }

    public Response(string message)
    {
        Succeeded = false;
        Message = message;
        Errors = new List<string>();
    }

    public Response(string message, bool succeeded, HttpStatusCode statusCode)
    {
        Succeeded = succeeded;
        Message = message;
        Errors = new List<string>();
        StatusCode = statusCode;
    }

    public HttpStatusCode StatusCode { get; set; } = HttpStatusCode.OK;
    public bool Succeeded { get; set; }
    public string Message { get; set; }
    public List<string> Errors { get; set; }
    public T Data { get; set; }
    public object Meta { get; set; }


    public static Response<T> Fail(string message, HttpStatusCode statusCode) => new Response<T>(message, false, statusCode);
    public static Response<T> Success() => new Response<T>(string.Empty, true, HttpStatusCode.OK);
}