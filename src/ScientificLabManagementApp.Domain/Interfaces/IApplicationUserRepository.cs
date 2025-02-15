namespace ScientificLabManagementApp.Domain;
public interface IApplicationUserRepository<TDto> : IGenericRepository<MappingApplicationUser, TDto>
    where TDto : class
{
    Task<IEnumerable<TDto>> GetAllSupervisorsDtoByIdAsync(params Expression<Func<MappingApplicationUser, object>>[] includes);
    Task<IEnumerable<TDto>> GetAllTechniciansDtoByIdAsync(params Expression<Func<MappingApplicationUser, object>>[] includes);
}
