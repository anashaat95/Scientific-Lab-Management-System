namespace ScientificLabManagementApp.Application;

public class ResponseBuilder
{
    private Response<T> CreateResponse<T>(HttpStatusCode statusCode, bool succeeded, string message = null, T data = default, object meta = null)
    {
        return new Response<T>()
        {
            StatusCode = statusCode,
            Succeeded = succeeded,
            Message = message,
            Data = data,
            Meta = meta
        };
    }


    public Response<T> Created<T>(T entity, object meta = null)
    {
        return CreateResponse(HttpStatusCode.Created, true, "Resource(s) created successfully", entity, meta);
    }

    public Response<T> Deleted<T>()
    {
        return CreateResponse<T>(HttpStatusCode.NoContent, true, "Resource deleted successfully");
    }

    public Response<T> Ok200<T>(string message = "Success")
    {
        return CreateResponse<T>(HttpStatusCode.OK, true, message);
    }

    public Response<T> Ok200<T>(T entity, string message = "Resource(s) fetched successfully", object meta = null)
    {
        return CreateResponse(HttpStatusCode.OK, true, message, entity, meta);
    }


    public Response<IEnumerable<T>> FetchedMultiple<T>(IEnumerable<T> entities, object meta = null)
    {
        return CreateResponse(HttpStatusCode.OK, true, "Resources fetched successfully", entities, meta);
    }

    public Response<T> Updated<T>(T entity)
    {
        return CreateResponse(HttpStatusCode.OK, true, "Resource updated successfully", entity);
    }

    public Response<T> Unauthorized<T>(string message = "Resourse is not allowed")
    {
        return CreateResponse<T>(HttpStatusCode.Unauthorized, false, message);
    }

    public Response<T> BadRequest<T>(string message = "Invalid request")
    {
        return CreateResponse<T>(HttpStatusCode.BadRequest, false, message);
    }

    public Response<T> UnprocessableEntity<T>(string message = "Unprocessable entity")
    {
        return CreateResponse<T>(HttpStatusCode.UnprocessableEntity, false, message);
    }

    public Response<T> NotFound<T>(string message = "Resource is not found")
    {
        return CreateResponse<T>(HttpStatusCode.NotFound, false, message);
    }
}