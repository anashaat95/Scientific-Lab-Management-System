using Microsoft.AspNetCore.Mvc.Infrastructure;
using Newtonsoft.Json.Serialization;
using ScientificLabManagementApp.API.Middlewares;
using ScientificLabManagementApp.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration.AddEnvironmentVariables();

builder.Services.AddApplicationDependencies(builder.Configuration)
                .AddInfrastructureExtensions(builder.Configuration);

builder.Services.AddControllers((configure) =>
{
    configure.ReturnHttpNotAcceptable = true;
})

    .AddNewtonsoftJson(setupAction =>
    {
        setupAction.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
    })
    .AddNewtonsoftJson()
    .AddXmlDataContractSerializerFormatters()
    .ConfigureApiBehaviorOptions(setupAction =>
    {
        setupAction.InvalidModelStateResponseFactory = context =>
        {
            var problemDetailsFactory = context.HttpContext.RequestServices.GetRequiredService<ProblemDetailsFactory>();
            var validationDetailsProblem = problemDetailsFactory
                        .CreateValidationProblemDetails(context.HttpContext, context.ModelState);

            validationDetailsProblem.Detail = "See the errors field for details.";
            validationDetailsProblem.Instance = context.HttpContext.Request.Path;
            validationDetailsProblem.Status = StatusCodes.Status422UnprocessableEntity;
            validationDetailsProblem.Title = "One or more errors occurred";

            return new UnprocessableEntityObjectResult(validationDetailsProblem)
            {
                ContentTypes = { "application/problem+json" }
            };
        };
    })
    ;

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
    //app.UseDeveloperExceptionPage();
}
else
{
    app.UseExceptionHandler(appBuilder =>
    {
        appBuilder.Run(async context =>
        {
            context.Response.StatusCode = 500;
            await context.Response.WriteAsJsonAsync(new { message = "An error occurred. Please try again later." });
        });
    });
    app.UseMiddleware<ErrorHandlingMiddleware>();
}
app.UseHttpsRedirection();
app.UseRouting();
app.UseCors("AllowAll");

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
