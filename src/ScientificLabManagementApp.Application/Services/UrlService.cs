namespace ScientificLabManagementApp.Application;

public class UrlService : IUrlService
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public UrlService(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public string GetBaseUrl()
    {
        var request = _httpContextAccessor.HttpContext?.Request;
        if (request == null)
        {
            throw new InvalidOperationException("Request is not available.");
        }

        return $"{request.Scheme}://{request.Host.Value}{request.PathBase.Value}";
    }
}
