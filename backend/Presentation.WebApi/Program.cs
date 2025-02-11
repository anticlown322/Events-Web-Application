using Domain.Contracts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.Extensions.Options;
using Presentation.WebApi.Extensions;
using NLog;
using Shared.DTO.Events;

var builder = WebApplication.CreateBuilder(args);
{
    LogManager.Setup().LoadConfigurationFromFile(string.Concat(Directory.GetCurrentDirectory(), "/Logs/nlog.config"));
    builder.Services.ConfigureLoggerService();

    builder.Services.ConfigureCors();
    builder.Services.ConfigureRepositoryManager();
    builder.Services.ConfigureServiceManager();
    builder.Services.ConfigureSqlContext(builder.Configuration); 
    builder.Services.ConfigureAutoMapper();
    
    builder.Services.AddAuthentication();
    builder.Services.ConfigureIdentity();
    builder.Services.ConfigureJwt(builder.Configuration);
    builder.Services.AddAuthorizationPolicies();
    
    builder.Services.AddControllers()
        .AddApplicationPart(typeof(Presentation.Core.AssemblyReference).Assembly);
    
    builder.Services.ConfigureApiBehaviorOptions();
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();
}

var app = builder.Build();
{
    var logger = app.Services.GetRequiredService<ILoggerManager>();
    app.ConfigureExceptionHandler(logger);
    
    app.UseCors("CorsPolicy");
    
    app.UseAuthentication();
    app.UseAuthorization();
    
    app.UseHttpsRedirection();
    app.MapControllers();
    
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.Run();