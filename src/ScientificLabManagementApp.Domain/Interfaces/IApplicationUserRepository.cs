namespace ScientificLabManagementApp.Domain;
public interface IApplicationUserRepository<TDto> : IGenericRepository<MappingApplicationUser, TDto>
    where TDto : class
{
   
}
