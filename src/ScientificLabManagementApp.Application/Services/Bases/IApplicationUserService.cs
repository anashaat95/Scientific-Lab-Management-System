namespace ScientificLabManagementApp.Application;

public interface IApplicationUserService
{
    Task<UserDto> FindOneAsync(Expression<Func<MappingApplicationUser, bool>> predicate, params Expression<Func<MappingApplicationUser, object>>[] includes);
    Task<IEnumerable<UserDto>> GetAllUsersByRoleAsync(string? role = null, params Expression<Func<MappingApplicationUser, object>>[] includes);
    Task<UserDto> GetOneByIdAsync(string id, params Expression<Func<MappingApplicationUser, object>>[] includes);
    Task<MappingApplicationUser> GetEntityByIdAsync(string id, params Expression<Func<MappingApplicationUser, object>>[] includes);

}
