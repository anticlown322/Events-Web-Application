using Domain.Contracts;
using Presentation.WebApi.Extensions;
using NLog;

var builder = WebApplication.CreateBuilder(args);

{
    LogManager.Setup().LoadConfigurationFromFile(string.Concat(Directory.GetCurrentDirectory(), "/Logs/nlog.config"));
    builder.Services.ConfigureLoggerService();

    builder.Services.ConfigureRepositoryManager();
    builder.Services.ConfigureServiceManager();
    builder.Services.ConfigureSqlContext(builder.Configuration);
    
    builder.Services.AddAutoMapper(typeof(Program));

    // to find controllers in Presentation.Core assembly
    builder.Services.AddControllers()
        .AddApplicationPart(typeof(Presentation.Core.AssemblyReference).Assembly);
    
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();
}

var app = builder.Build();

{
    var logger = app.Services.GetRequiredService<ILoggerManager>();
    app.ConfigureExceptionHandler(logger);
    
    app.UseSwagger();
    app.UseSwaggerUI();
    
    app.UseHttpsRedirection();
    app.MapControllers();
    
    app.UseAuthorization();
}

app.Run();