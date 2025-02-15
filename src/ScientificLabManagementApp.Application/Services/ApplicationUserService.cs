
namespace ScientificLabManagementApp.Application;

public class ApplicationUserService : IApplicationUserService
{
    protected readonly IApplicationUserRepository<UserDto> _userRepository;

    public ApplicationUserService(IApplicationUserRepository<UserDto> userRepository)
    {
        _userRepository = userRepository;
    }

    public Task<UserDto> FindOneAsync(Expression<Func<MappingApplicationUser, bool>> predicate, params Expression<Func<MappingApplicationUser, object>>[] includes)
    {
        return _userRepository.FindOneAsync(predicate, includes);   
    }

    public Task<IEnumerable<UserDto>> GetAllAsync(params Expression<Func<MappingApplicationUser, object>>[] includes)
    {
        return _userRepository.GetAllAsync(includes);
    }

    public Task<IEnumerable<UserDto>> GetSupervisorsAsync(params Expression<Func<MappingApplicationUser, object>>[] includes)
    {
        return _userRepository.GetAllSupervisorsDtoByIdAsync(includes);
    }

    public Task<IEnumerable<UserDto>> GetTechniciansAsync(params Expression<Func<MappingApplicationUser, object>>[] includes)
    {
        return _userRepository.GetAllTechniciansDtoByIdAsync(includes);
    }

    public Task<IEnumerable<UserDto>> GetResearchersAsync(params Expression<Func<MappingApplicationUser, object>>[] includes)
    {
        return _userRepository.GetAllResearchersDtoByIdAsync(includes);
    }

    public Task<UserDto> GetDtoByIdAsync(string id, params Expression<Func<MappingApplicationUser, object>>[] includes)
    {
        return _userRepository.GetDtoByIdAsync(id, includes);
    }

    public Task<MappingApplicationUser> GetEntityByIdAsync(string id, params Expression<Func<MappingApplicationUser, object>>[] includes)
    {
        return _userRepository.GetEntityByIdAsync(id, includes);
    }
}
