
namespace ScientificLabManagementApp.Application;

public static class ApplicationDependencyInjectionExtensions
{
    public static IServiceCollection AddApplicationDependencies(this IServiceCollection services, IConfiguration configManager)
    {
        services.AddHttpContextAccessor();

        services.AddTransient(typeof(IBaseService<,>), typeof(BaseService<,>));

        services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly()));

        services.AddAutoMapper(Assembly.GetExecutingAssembly());

        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));


        services.AddScoped<ITokenService, TokenService>();
        services.AddScoped<IEmailService, EmailService>();
        services.Configure<SmtpSettings>(configManager.GetSection(nameof(SmtpSettings)));
        services.AddScoped<ICurrentUserService, CurrentUserService>();

        services.AddSingleton<IUrlService, UrlService>();
        services.AddSingleton<EmailCreator>();
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(AuthorizationBehaviour<,>));

        services.AddAuthenticationDependencies(configManager);

        services.Configure<DataProtectionTokenProviderOptions>(options =>
        {
            options.TokenLifespan = TimeSpan.FromHours(6); 
        });

        return services;
    }
}


public static class AuthenticationInjectionExtensions
{
    public static IServiceCollection AddAuthenticationDependencies(this IServiceCollection services, IConfiguration configManager)
    {
        services.Configure<JwtSettings>(configManager.GetSection(nameof(JwtSettings)));

        var jwtSettings = new JwtSettings();
        configManager.GetSection(nameof(JwtSettings)).Bind(jwtSettings);

        services
            .AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.RequireHttpsMetadata = false;
                options.SaveToken = false;

                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = jwtSettings.ValidateIssuer,
                    ValidIssuers = new[] { jwtSettings.Issuer },

                    ValidateAudience = jwtSettings.ValidateAudience,
                    ValidAudience = jwtSettings.Audience,

                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configManager["SECURITY_KEY"])),

                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.Zero
                };
            });

        services.AddSwaggerGen(options =>
        {
            options.SwaggerDoc("v1", new OpenApiInfo { Title = "Scientific Lab Management", Version = "v1" });

            options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                Description = "JWT Authorization header using the Bearer schema (e.g. Bearer 212555dfef)",
                Name = "Authorization",
                In = ParameterLocation.Header,
                Type = SecuritySchemeType.ApiKey,
                Scheme = JwtBearerDefaults.AuthenticationScheme,
                BearerFormat = "JWT",
            });

            options.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = JwtBearerDefaults.AuthenticationScheme
                        },
                        Name = JwtBearerDefaults.AuthenticationScheme,
                        In = ParameterLocation.Header
                    },
                    new string[] {}
                }
            });
        });


        return services;
    }
}