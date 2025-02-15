
namespace ScientificLabManagementApp.Application;

public class ApplicationUserService : IApplicationUserService
{
    protected readonly IApplicationUserRepository<UserDto> _userRepository;

    public ApplicationUserService(IApplicationUserRepository<UserDto> userRepository)
    {
        _userRepository = userRepository;
    }

    public Task<UserDto> AddAsync(MappingApplicationUser entityToAdd)
    {
        return _userRepository.AddAsync(entityToAdd);
    }

    public Task DeleteAsync(MappingApplicationUser entity)
    {
        return _userRepository.DeleteAsync(entity);
    }

    public Task<bool> ExistsAsync(string id)
    {
        return _userRepository.ExistsAsync(id);
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

    public Task<UserDto> GetDtoByIdAsync(string id, params Expression<Func<MappingApplicationUser, object>>[] includes)
    {
        return _userRepository.GetDtoByIdAsync(id, includes);
    }

    public Task<MappingApplicationUser> GetEntityByIdAsync(string id, params Expression<Func<MappingApplicationUser, object>>[] includes)
    {
        return _userRepository.GetEntityByIdAsync(id, includes);
    }

    public Task<UserDto> UpdateAsync(MappingApplicationUser entityToUpdate)
    {
        return _userRepository.UpdateAsync(entityToUpdate);
    }

    public Task<RelatedEntity> FindRelatedEntityByIdAsync<RelatedEntity>(Expression<Func<RelatedEntity, bool>> predicate, params Expression<Func<RelatedEntity, object>>[] includes) where RelatedEntity : class, IEntityBase
    {
          return _userRepository.FindRelatedEntityByIdAsync(predicate, includes);
    }

    public Task<bool> RelatedExistsAsync<RelatedEntity>(string id) where RelatedEntity : class, IEntityBase
    {
          return _userRepository.RelatedExistsAsync<RelatedEntity>(id);
    }
}
