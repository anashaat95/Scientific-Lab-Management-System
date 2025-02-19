
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

    public async Task<UserDto> FindOneAsync(Expression<Func<MappingApplicationUser, bool>> predicate)
    {
        var result = await _userRepository.FindOneAsync(predicate);
        return _mapper.Map<UserDto>(result);
    }

    public async Task<IEnumerable<UserDto>> GetAllUsersByRoleAsync(string? role = null)
    {
        var result = await _userRepository.GetAllUsersByRoleAsync(role);
        return _mapper.Map<IEnumerable<UserDto>>(result);
    }

    public async Task<IEnumerable<SelectOptionDto>> GetAllUsersSelectOptionsByRoleAsync(string? role = null)
    {
        var result = await _userRepository.GetAllUsersSelectOptionsByRoleAsync(role);
        return _mapper.Map<IEnumerable<SelectOptionDto>>(result);
    }

    public Task<UserDto> GetOneByIdAsync(string id)
    {
        return FindOneAsync(e => e.Id == id);
    }

    public Task<MappingApplicationUser> GetEntityByIdAsync(string id)
    {
        return _userRepository.GetOneByIdAsync(id);
    }

}
