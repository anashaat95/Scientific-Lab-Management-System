using Microsoft.EntityFrameworkCore;

namespace ScientificLabManagementApp.Domain;
public interface IApplicationUserRepository<TDto> 
    where TDto : class
{
    Task<TDto> GetDtoByIdAsync(string id, params Expression<Func<MappingApplicationUser, object>>[] includes);
    Task<IEnumerable<TDto>> GetAllSupervisorsDtoByIdAsync(params Expression<Func<MappingApplicationUser, object>>[] includes);
    Task<IEnumerable<TDto>> GetAllTechniciansDtoByIdAsync(params Expression<Func<MappingApplicationUser, object>>[] includes);
    Task<MappingApplicationUser> GetEntityByIdAsync(string id, params Expression<Func<MappingApplicationUser, object>>[] includes);
    Task<IEnumerable<TDto>> GetAllAsync(params Expression<Func<MappingApplicationUser, object>>[] includes);
    Task<TDto> FindOneAsync(Expression<Func<MappingApplicationUser, bool>> predicate, params Expression<Func<MappingApplicationUser, object>>[] includes);
    Task<IEnumerable<TDto>> FindAsync(Expression<Func<MappingApplicationUser, bool>> predicate, params Expression<Func<MappingApplicationUser, object>>[] includes);

}
