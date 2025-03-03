
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

    public async Task<PaginationResult<MappingApplicationUser, UserDto>> GetAllUsersByRoleAsync(string? role, AllResourceParameters parameters)
    {
        var result = await _userRepository.GetAllUsersByRoleAsync(role, parameters);
        return new PaginationResult<MappingApplicationUser, UserDto>(result, parameters);
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

    public Task<UserDto> GetOneByEmailAsync(string email)
    {
        return FindOneAsync(e => e.NormalizedEmail == email.Trim().ToUpper());
    }

    public Task<bool> ExistOneAsync(Expression<Func<MappingApplicationUser, bool>> predicate)
    {
        return _userRepository.ExistOneAsync(predicate);
    }

    public Task<MappingApplicationUser> GetEntityByIdAsync(string id)
    {
        return _userRepository.GetOneByIdAsync(id);
    }

}
