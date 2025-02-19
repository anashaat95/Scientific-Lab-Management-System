namespace ScientificLabManagementApp.Application;

public interface IApplicationUserService
{
    Task<UserDto> FindOneAsync(Expression<Func<MappingApplicationUser, bool>> predicate);
    Task<IEnumerable<UserDto>> GetAllUsersByRoleAsync(string? role = null);
    Task<IEnumerable<SelectOptionDto>> GetAllUsersSelectOptionsByRoleAsync(string? role = null);
    Task<UserDto> GetOneByIdAsync(string id);
    Task<MappingApplicationUser> GetEntityByIdAsync(string id);

}
