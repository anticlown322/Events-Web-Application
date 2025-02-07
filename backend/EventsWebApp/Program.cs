using EventsWebApp.Extensions;

var builder = WebApplication.CreateBuilder(args);

// config services
{
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

// config app
{
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    app.UseHttpsRedirection();
    app.UseAuthorization();
    app.MapControllers();
}

app.Run();