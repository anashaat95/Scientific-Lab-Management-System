using Microsoft.EntityFrameworkCore;

namespace ScientificLabManagementApp.Domain;
public interface IApplicationUserRepository
{
    Task<MappingApplicationUser> GetOneByIdAsync(string id);
    Task<IEnumerable<MappingApplicationUser>> GetAllUsersByRoleAsync(string? role);
    Task<MappingApplicationUser> FindOneAsync(Expression<Func<MappingApplicationUser, bool>> predicate);
    Task<IEnumerable<MappingApplicationUser>> FindAsync(Expression<Func<MappingApplicationUser, bool>> predicate);
    Task<IEnumerable<MappingApplicationUserSelectOption>> GetAllUsersSelectOptionsByRoleAsync(string? role = null);
    Task<bool> ExistOneAsync(Expression<Func<MappingApplicationUser, bool>> predicate);
}
