using ScientificLabManagementApp.API.Middlewares;
using ScientificLabManagementApp.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration.AddEnvironmentVariables();

builder.Services.AddApplicationDependencies(builder.Configuration)
                .AddInfrastructureExtensions(builder.Configuration);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();


builder.Configuration.AddJsonFile("Properties/launchSettings.json", optional: true);

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", config =>
    {
        config.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();

        config.WithOrigins(builder.Configuration["FrontendUrl"]).AllowCredentials().AllowAnyMethod().AllowAnyHeader();
    });
});

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    await new DbSeeder(scope.ServiceProvider, builder.Configuration).Run();
}

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<ErrorHandlingMiddleware>();

app.UseHttpsRedirection();
app.UseRouting();
app.UseCors("AllowAll");

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
