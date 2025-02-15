using Microsoft.Data.SqlClient;

namespace ScientificLabManagementApp.Infrastructure;

public class ApplicationUserRepository<TDto> : IApplicationUserRepository<TDto>
    where TDto : class
{
    protected IGenericRepository<MappingApplicationUser, TDto> _repository;
    protected readonly ApplicationDbContext _context;
    protected readonly IMapper _mapper;
    protected readonly RoleManager<ApplicationRole> _roleManager;


    protected static string RawSqlStatement =
    @"SELECT U.*, C.[Name] AS CompanyName, D.[Name] AS DepartmentName, L.[Name] AS LabName
    FROM AspNetUsers AS U
    LEFT JOIN Companies AS C ON U.[CompanyId] = C.[Id]
    LEFT JOIN Departments AS D ON U.[DepartmentId] = D.[Id]
    LEFT JOIN Labs AS L ON U.[LabId] = L.[Id]
    ";

    public ApplicationUserRepository(IGenericRepository<MappingApplicationUser, TDto> repository, ApplicationDbContext context, IMapper mapper, RoleManager<ApplicationRole> roleManager)
    {
        _repository = repository;
        _context = context;
        _mapper = mapper;
        _roleManager = roleManager;
    }

    public async Task<TDto> GetDtoByIdAsync(string id, params Expression<Func<MappingApplicationUser, object>>[] includes)
    {
        var result = await _context.Database
                                  .SqlQueryRaw<MappingApplicationUser>(RawSqlStatement + " WHERE U.id = @id", new SqlParameter("@id", id))
                                  .ProjectTo<TDto>(_mapper.ConfigurationProvider)
                                  .AsNoTracking()
                                  .FirstOrDefaultAsync();

        return result;
    }
    public async Task<IEnumerable<TDto>> GetAllSupervisorsDtoByIdAsync( params Expression<Func<MappingApplicationUser, object>>[] includes)
    {
        var result = await _context.Database
                          .SqlQueryRaw<MappingApplicationUser>(RawSqlStatement 
                                    + " WHERE U.Id IN (SELECT UserId FROM AspNetUserRoles " +
                                      " WHERE RoleId = (SELECT Id FROM AspNetRoles WHERE Name = @roleName))",
                                      new SqlParameter("@roleName", enUserRoles.LabSupervisor.ToString()))
                          .ProjectTo<TDto>(_mapper.ConfigurationProvider)
                          .AsNoTracking()
                          .ToListAsync();

        return result;
    }
    public async Task<IEnumerable<TDto>> GetAllTechniciansDtoByIdAsync(params Expression<Func<MappingApplicationUser, object>>[] includes)
    {
        var result = await _context.Database
                          .SqlQueryRaw<MappingApplicationUser>(RawSqlStatement
                                    + " WHERE U.Id IN (SELECT UserId FROM AspNetUserRoles " +
                                      " WHERE RoleId = (SELECT Id FROM AspNetRoles WHERE Name = @roleName))",
                                      new SqlParameter("@roleName", enUserRoles.Technician.ToString()))
                          .ProjectTo<TDto>(_mapper.ConfigurationProvider)
                          .AsNoTracking()
                          .ToListAsync();

        return result;
    }
    public async Task<MappingApplicationUser> GetEntityByIdAsync(string id, params Expression<Func<MappingApplicationUser, object>>[] includes)
    {
        var result = await _context.Database
                  .SqlQueryRaw<MappingApplicationUser>(RawSqlStatement + " WHERE U.id = @id", new SqlParameter("@id", id))
                  .AsNoTracking()
                  .FirstOrDefaultAsync();

        return result;
    }
    public async Task<IEnumerable<TDto>> GetAllAsync(params Expression<Func<MappingApplicationUser, object>>[] includes)
    {
        var result = await _context.Database
                          .SqlQueryRaw<MappingApplicationUser>(RawSqlStatement)
                          .ProjectTo<TDto>(_mapper.ConfigurationProvider)
                          .AsNoTracking()
                          .ToListAsync();


        return result;
    }
    public async Task<TDto> FindOneAsync(Expression<Func<MappingApplicationUser, bool>> predicate, params Expression<Func<MappingApplicationUser, object>>[] includes)
    {
        var filtered = await _context.Database
                          .SqlQueryRaw<MappingApplicationUser>(RawSqlStatement)
                          .Where(predicate)
                          .ProjectTo<TDto>(_mapper.ConfigurationProvider)
                          .AsNoTracking()
                          .FirstOrDefaultAsync();
        return filtered;
    }
    public async Task<IEnumerable<TDto>> FindAsync(Expression<Func<MappingApplicationUser, bool>> predicate, params Expression<Func<MappingApplicationUser, object>>[] includes)
    {
        var filtered = await _context.Database
                          .SqlQueryRaw<MappingApplicationUser>(RawSqlStatement)
                          .Where(predicate)
                          .ProjectTo<TDto>(_mapper.ConfigurationProvider)
                          .AsNoTracking()
                          .ToListAsync();

        return filtered;
    }
}
