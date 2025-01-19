namespace ScientificLabManagementApp.Application;

public interface ICurrentUserService
{
    ClaimsPrincipal User { get; }
    string UserId { get; }
    IEnumerable<string> UserRoles { get; }
}
