using Microsoft.EntityFrameworkCore;

namespace ScientificLabManagementApp.Domain;
public interface IApplicationUserRepository
{
    Task<MappingApplicationUser> GetOneByIdAsync(string id, params Expression<Func<MappingApplicationUser, object>>[] includes);
    Task<IEnumerable<MappingApplicationUser>> GetAllUsersByRoleAsync(string? role, params Expression<Func<MappingApplicationUser, object>>[] includes);
    Task<MappingApplicationUser> FindOneAsync(Expression<Func<MappingApplicationUser, bool>> predicate, params Expression<Func<MappingApplicationUser, object>>[] includes);
    Task<IEnumerable<MappingApplicationUser>> FindAsync(Expression<Func<MappingApplicationUser, bool>> predicate, params Expression<Func<MappingApplicationUser, object>>[] includes);

}
