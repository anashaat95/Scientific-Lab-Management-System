using System.Linq.Expressions;

namespace ScientificLabManagementApp.Application;

public interface IApplicationUserService : IBaseService<MappingApplicationUser, UserDto>
{
    Task<IEnumerable<UserDto>> GetSupervisorsAsync(params Expression<Func<MappingApplicationUser, object>>[] includes);
    Task<IEnumerable<UserDto>> GetTechniciansAsync(params Expression<Func<MappingApplicationUser, object>>[] includes);
}
