namespace ScientificLabManagementApp.Application;

public interface IApplicationUserService
{
    Task<UserDto> FindOneAsync(Expression<Func<MappingApplicationUser, bool>> predicate, params Expression<Func<MappingApplicationUser, object>>[] includes);
    Task<IEnumerable<UserDto>> GetAllAsync(params Expression<Func<MappingApplicationUser, object>>[] includes);
    Task<IEnumerable<UserDto>> GetSupervisorsAsync(params Expression<Func<MappingApplicationUser, object>>[] includes);
    Task<IEnumerable<UserDto>> GetTechniciansAsync(params Expression<Func<MappingApplicationUser, object>>[] includes);
    Task<IEnumerable<UserDto>> GetResearchersAsync(params Expression<Func<MappingApplicationUser, object>>[] includes);
    Task<UserDto> GetDtoByIdAsync(string id, params Expression<Func<MappingApplicationUser, object>>[] includes);
    Task<MappingApplicationUser> GetEntityByIdAsync(string id, params Expression<Func<MappingApplicationUser, object>>[] includes);

}
