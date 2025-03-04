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

        services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
        services.AddScoped<IApplicationUserRepository, ApplicationUserRepository>();
        services.AddScoped<IUnitOfWork, UnitOfWork>();

        services.AddTransient(typeof(IPropertyMappingService<,>), typeof(PropertyMappingService<,>));

        return services;
    }
}
