﻿using Microsoft.Data.SqlClient;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace ScientificLabManagementApp.Infrastructure;

public class ApplicationUserRepository : IApplicationUserRepository
{
    protected IGenericRepository<MappingApplicationUser> _repository;
    protected readonly ApplicationDbContext _context;
    protected readonly IMapper _mapper;
    protected readonly RoleManager<ApplicationRole> _roleManager;


    protected static string RawSqlStatement =
    @"SELECT 
        U.*, 
        C.[Name] AS CompanyName, 
        D.[Name] AS DepartmentName, 
        L.[Name] AS LabName, 
        R.Roles AS Roles
    FROM AspNetUsers AS U
    LEFT JOIN Companies AS C ON U.[CompanyId] = C.[Id]
    LEFT JOIN Departments AS D ON U.[DepartmentId] = D.[Id]
    LEFT JOIN Labs AS L ON U.[LabId] = L.[Id]
    LEFT JOIN (
        SELECT 
            UR.UserId, 
            STRING_AGG(R.Name, ',') AS Roles 
        FROM AspNetUserRoles UR
        LEFT JOIN AspNetRoles R ON UR.RoleId = R.Id
        GROUP BY UR.UserId
    ) AS R ON R.UserId = U.Id ";

    protected static string RawSqlStatementForSelectOptions =
    @"SELECT 
        U.Id, U.FirstName, U.LastName, R.Roles
    FROM AspNetUsers AS U
    LEFT JOIN (
        SELECT 
            UR.UserId, 
            STRING_AGG(R.Name, ',') AS Roles 
        FROM AspNetUserRoles UR
        LEFT JOIN AspNetRoles R ON UR.RoleId = R.Id
        GROUP BY UR.UserId
    ) AS R ON R.UserId = U.Id ";

    public ApplicationUserRepository(IGenericRepository<MappingApplicationUser> repository, ApplicationDbContext context, IMapper mapper, RoleManager<ApplicationRole> roleManager)
    {
        _repository = repository;
        _context = context;
        _mapper = mapper;
        _roleManager = roleManager;
    }

    public async Task<MappingApplicationUser> GetOneByIdAsync(string id)
    {
        var result = await FindOneAsync(e => e.Id == id);
        return result;
    }

    public async Task<PagedList<MappingApplicationUser>> GetAllUsersByRoleAsync(string? role, AllResourceParameters parameters)
    {
        var roleStatement = role != null ? " WHERE R.Roles LIKE @roleName" : "";

        var query =  _context.Database
                          .SqlQueryRaw<MappingApplicationUser>(RawSqlStatement + roleStatement, new SqlParameter("@roleName", $"%{role}%"))
                          .AsNoTracking().AsQueryable();

        query = _repository.ApplyFilteringSortingAndPagination(query, parameters);

        return await PagedList<MappingApplicationUser>.CreateAsync(query, parameters.PageNumber, parameters.PageSize);
    }

    public async Task<IEnumerable<MappingApplicationUserSelectOption>> GetAllUsersSelectOptionsByRoleAsync(string? role = null)
    {
        var roleStatement = role != null ? " WHERE R.Roles LIKE @roleName" : "";

        var result = await _context.Database
                          .SqlQueryRaw<MappingApplicationUserSelectOption>(RawSqlStatementForSelectOptions + roleStatement, new SqlParameter("@roleName", $"%{role}%"))
                          .AsNoTracking()
                          .ToListAsync();
        return result;
    }

    public async Task<MappingApplicationUser> FindOneAsync(Expression<Func<MappingApplicationUser, bool>> predicate)
    {
        var filtered = await _context.Database
                          .SqlQueryRaw<MappingApplicationUser>(RawSqlStatement)
                          .Where(predicate)
                          .AsNoTracking()
                          .FirstOrDefaultAsync();
        return filtered;
    }
    public async Task<IEnumerable<MappingApplicationUser>> FindAsync(Expression<Func<MappingApplicationUser, bool>> predicate)
    {
        var filtered = await _context.Database
                          .SqlQueryRaw<MappingApplicationUser>(RawSqlStatement)
                          .Where(predicate)
                          .AsNoTracking()
                          .ToListAsync();

        return filtered;
    }

    public async Task<bool> ExistOneAsync(Expression<Func<MappingApplicationUser, bool>> predicate)
    {
        return await _context.Users.AnyAsync(predicate);
    }
}
