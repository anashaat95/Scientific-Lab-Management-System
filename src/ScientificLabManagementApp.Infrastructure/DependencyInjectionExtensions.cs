namespace ScientificLabManagementApp.Infrastructure;

public static class InfrastructureDependencyInjectionExtensions
{
    public static IServiceCollection AddInfrastructureExtensions(this IServiceCollection services, IConfiguration configurations)
    {
        services.AddAutoMapper(Assembly.GetExecutingAssembly());

        services.AddDbContext<ApplicationDbContext>(options =>
        {
            options.UseSqlServer(configurations["LabManagementSystemConnectionString"]);
        });

        services.AddIdentity<ApplicationUser, ApplicationRole>()
            .AddUserManager<UserManager<ApplicationUser>>()
            .AddRoleManager<RoleManager<ApplicationRole>>()
            .AddEntityFrameworkStores<ApplicationDbContext>()
            .AddDefaultTokenProviders();

        services.AddTransient(typeof(IGenericRepository<>), typeof(GenericRepository<>));
        services.AddTransient<IApplicationUserRepository, ApplicationUserRepository>();
        services.AddTransient<IUnitOfWork, UnitOfWork>();

        return services;
    }
}
