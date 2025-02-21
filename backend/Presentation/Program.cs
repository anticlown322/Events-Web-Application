using System.Security.Claims;
using NLog;
using Application.Contracts;
using Infrastructure.Repository;
using Microsoft.EntityFrameworkCore;
using Presentation.Extensions;

var builder = WebApplication.CreateBuilder(args);
{
    var configFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "nlog.config");
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

    builder.Services.AddControllers();
    
    builder.Services.ConfigureApiBehaviorOptions();
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.ConfigureSwagger();
}

var app = builder.Build();
{
    AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
    using (var scope = app.Services.CreateScope())
    {
        var dbContext = scope.ServiceProvider.GetRequiredService<RepositoryContext>();
        dbContext.Database.Migrate();
    }
    
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