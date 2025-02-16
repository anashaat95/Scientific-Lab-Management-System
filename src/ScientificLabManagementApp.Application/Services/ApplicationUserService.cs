
namespace ScientificLabManagementApp.Application;

public class ApplicationUserService : IApplicationUserService
{
    protected readonly IApplicationUserRepository _userRepository;
    protected readonly IMapper _mapper;

    public ApplicationUserService(IApplicationUserRepository userRepository, IMapper mapper)
    {
        _userRepository = userRepository;
        _mapper = mapper;
    }

    public async Task<UserDto> FindOneAsync(Expression<Func<MappingApplicationUser, bool>> predicate, params Expression<Func<MappingApplicationUser, object>>[] includes)
    {
        var result = await _userRepository.FindOneAsync(predicate, includes);
        return _mapper.Map<UserDto>(result);
    }

    public async Task<IEnumerable<UserDto>> GetAllUsersAsync(params Expression<Func<MappingApplicationUser, object>>[] includes)
    {
        var result = await _userRepository.GetAllUsersAsync(includes);
        return _mapper.Map<IEnumerable<UserDto>>(result);
    }

    public async Task<IEnumerable<UserDto>> GetSupervisorsAsync(params Expression<Func<MappingApplicationUser, object>>[] includes)
    {
        var result = await _userRepository.GetAllSupervisorsAsync(includes);
        return _mapper.Map<IEnumerable<UserDto>>(result);
    }

    public async Task<IEnumerable<UserDto>> GetTechniciansAsync(params Expression<Func<MappingApplicationUser, object>>[] includes)
    {
        var result = await _userRepository.GetAllTechniciansAsync(includes);
        return _mapper.Map<IEnumerable<UserDto>>(result);
    }

    public async Task<IEnumerable<UserDto>> GetResearchersAsync(params Expression<Func<MappingApplicationUser, object>>[] includes)
    {
        var result = await _userRepository.GetAllResearchersAsync(includes);
        return _mapper.Map<IEnumerable<UserDto>>(result);
    }

    public Task<UserDto> GetOneByIdAsync(string id, params Expression<Func<MappingApplicationUser, object>>[] includes)
    {
        return FindOneAsync(e => e.Id == id);
    }

    public Task<MappingApplicationUser> GetEntityByIdAsync(string id, params Expression<Func<MappingApplicationUser, object>>[] includes)
    {
        return _userRepository.GetOneByIdAsync(id, includes);
    }
}
