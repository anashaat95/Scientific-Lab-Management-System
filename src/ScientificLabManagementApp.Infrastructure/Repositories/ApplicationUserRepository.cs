using Microsoft.Data.SqlClient;

namespace ScientificLabManagementApp.Infrastructure;

public class ApplicationUserRepository : IApplicationUserRepository
{
    protected IGenericRepository<MappingApplicationUser> _repository;
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

    public ApplicationUserRepository(IGenericRepository<MappingApplicationUser> repository, ApplicationDbContext context, IMapper mapper, RoleManager<ApplicationRole> roleManager)
    {
        _repository = repository;
        _context = context;
        _mapper = mapper;
        _roleManager = roleManager;
    }

    public async Task<MappingApplicationUser> GetOneByIdAsync(string id, params Expression<Func<MappingApplicationUser, object>>[] includes)
    {
        var result = await FindOneAsync(e=>e.Id == id, includes);
        return result;
    }

    public async Task<IEnumerable<MappingApplicationUser>> GetAllUsersAsync(params Expression<Func<MappingApplicationUser, object>>[] includes)
    {
        var result = await _context.Database
                          .SqlQueryRaw<MappingApplicationUser>(RawSqlStatement)
                          .AsNoTracking()
                          .ToListAsync();
        return result;
    }
    public async Task<IEnumerable<MappingApplicationUser>> GetAllSupervisorsAsync( params Expression<Func<MappingApplicationUser, object>>[] includes)
    {
        var result = await _context.Database
                          .SqlQueryRaw<MappingApplicationUser>(RawSqlStatement 
                                    + " WHERE U.Id IN (SELECT UserId FROM AspNetUserRoles " +
                                      " WHERE RoleId = (SELECT Id FROM AspNetRoles WHERE Name = @roleName))",
                                      new SqlParameter("@roleName", enUserRoles.LabSupervisor.ToString()))
                          .AsNoTracking()
                          .ToListAsync();

        return result;
    }
    public async Task<IEnumerable<MappingApplicationUser>> GetAllTechniciansAsync(params Expression<Func<MappingApplicationUser, object>>[] includes)
    {
        var result = await _context.Database
                          .SqlQueryRaw<MappingApplicationUser>(RawSqlStatement
                                    + " WHERE U.Id IN (SELECT UserId FROM AspNetUserRoles " +
                                      " WHERE RoleId = (SELECT Id FROM AspNetRoles WHERE Name = @roleName))",
                                      new SqlParameter("@roleName", enUserRoles.Technician.ToString()))
                          .AsNoTracking()
                          .ToListAsync();

        return result;
    }
    public async Task<IEnumerable<MappingApplicationUser>> GetAllResearchersAsync(params Expression<Func<MappingApplicationUser, object>>[] includes)
    {
        var result = await _context.Database
                          .SqlQueryRaw<MappingApplicationUser>(RawSqlStatement
                                    + " WHERE U.Id IN (SELECT UserId FROM AspNetUserRoles " +
                                      " WHERE RoleId = (SELECT Id FROM AspNetRoles WHERE Name = @roleName))",
                                      new SqlParameter("@roleName", enUserRoles.Researcher.ToString()))
                          .AsNoTracking()
                          .ToListAsync();

        return result;
    }
 
    
    public async Task<MappingApplicationUser> FindOneAsync(Expression<Func<MappingApplicationUser, bool>> predicate, params Expression<Func<MappingApplicationUser, object>>[] includes)
    {
        var filtered = await _context.Database
                          .SqlQueryRaw<MappingApplicationUser>(RawSqlStatement)
                          .Where(predicate)
                          .AsNoTracking()
                          .FirstOrDefaultAsync();
        return filtered;
    }
    public async Task<IEnumerable<MappingApplicationUser>> FindAsync(Expression<Func<MappingApplicationUser, bool>> predicate, params Expression<Func<MappingApplicationUser, object>>[] includes)
    {
        var filtered = await _context.Database
                          .SqlQueryRaw<MappingApplicationUser>(RawSqlStatement)
                          .Where(predicate)
                          .AsNoTracking()
                          .ToListAsync();

        return filtered;
    }
}
