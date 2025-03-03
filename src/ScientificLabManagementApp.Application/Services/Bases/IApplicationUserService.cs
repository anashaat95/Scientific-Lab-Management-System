namespace ScientificLabManagementApp.Application;

public interface IApplicationUserService
{
    Task<UserDto> FindOneAsync(Expression<Func<MappingApplicationUser, bool>> predicate);
    Task<PagedList<UserDto>> GetAllUsersByRoleAsync(string? role = null);
    Task<IEnumerable<SelectOptionDto>> GetAllUsersSelectOptionsByRoleAsync(string? role = null);
    Task<UserDto> GetOneByIdAsync(string id);
    Task<MappingApplicationUser> GetEntityByIdAsync(string id);
    Task<UserDto> GetOneByEmailAsync(string email);
    Task<bool> ExistOneAsync(Expression<Func<MappingApplicationUser, bool>> predicate);
}
