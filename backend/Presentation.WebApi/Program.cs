using System.Reflection;
using Domain.Contracts;
using Presentation.WebApi.Extensions;
using NLog;
using Application.Contracts;

var builder = WebApplication.CreateBuilder(args);
{
    var assemblyDirectory = Path.GetDirectoryName(
        Assembly.GetAssembly(typeof(Infrastructure.Logger.AssemblyReference)).Location);
    var configFilePath = Path.Combine(assemblyDirectory, "Logs", "nlog.config");
    LogManager.Setup().LoadConfigurationFromFile(configFilePath);
    builder.Services.ConfigureLoggerService();

    builder.Services.ConfigureCors();
    builder.Services.ConfigureRepositoryManager();
    builder.Services.ConfigureSqlContext(builder.Configuration); 
    builder.Services.ConfigureAutoMapper();
    
    builder.Services.AddAuthentication();
    builder.Services.ConfigureIdentity();
    builder.Services.ConfigureJwt(builder.Configuration);
    builder.Services.AddAuthorizationPolicies();
    
    builder.Services.ConfigureAuthenticationManager();
    builder.Services.ConfigureImageService();
    builder.Services.ConfigureUseCases();
    builder.Services.AddValidators();
    
    builder.Services.AddControllers()
        .AddApplicationPart(typeof(Presentation.Core.AssemblyReference).Assembly);
    
    builder.Services.ConfigureApiBehaviorOptions();
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.ConfigureSwagger();
}

var app = builder.Build();
{
    var logger = app.Services.GetRequiredService<ILoggerManager>();
    app.ConfigureExceptionHandler(logger);
    
    app.UseCors("CorsPolicy");
    app.UseStaticFiles();
    
    app.UseAuthentication();
    app.UseAuthorization();
    
    app.UseHttpsRedirection();
    app.MapControllers();
    
    app.UseSwagger();
    app.UseSwaggerUI(s =>
    {
        s.SwaggerEndpoint("/swagger/v1/swagger.json", "Event Web App API");
    });
}

app.Run();