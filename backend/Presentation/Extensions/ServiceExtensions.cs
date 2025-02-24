using System.Security.Claims;
using System.Text;
using Application.Contracts;
using Application.Contracts.UseCaseContracts.Authentication;
using Application.Contracts.UseCaseContracts.Event;
using Application.Contracts.UseCaseContracts.Image;
using Application.Contracts.UseCaseContracts.Participant;
using Application.DTO.Events;
using Application.DTO.MappingProfiles;
using Application.DTO.MappingProfiles.Event;
using Application.DTO.MappingProfiles.Participant;
using Application.DTO.MappingProfiles.User;
using Application.DTO.Participants;
using Application.DTO.User;
using Application.UseCases.Authentication;
using Application.UseCases.Event;
using Application.UseCases.Image;
using Application.UseCases.Participant;
using Application.Validation.EventValidators;
using Application.Validation.ParticipantValidators;
using Application.Validation.UserValidators;
using Domain.Models;
using Domain.RepositoryContracts;
using FluentValidation;
using Infrastructure;
using Infrastructure.Logs;
using Infrastructure.Repository;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

namespace Presentation.Extensions;

public static class ServiceExtensions
{
    public static void ConfigureLoggerService(this IServiceCollection services) =>
        services.AddSingleton<ILoggerManager, LoggerManager>();

    public static void ConfigureRepositoryManager(this IServiceCollection services) =>
        services.AddScoped<IRepositoryManager, RepositoryManager>();

    public static void ConfigureAuthenticationManager(this IServiceCollection services) =>
        services.AddScoped<IAuthenticationManager, AuthenticationManager>();

    public static void ConfigureImageService(this IServiceCollection services) =>
        services.AddScoped<IImageService, ImageService>();

    public static void ConfigureUseCases(this IServiceCollection services)
    {
        #region Authentication use cases

        services.AddScoped<IRegisterUserUseCase, RegisterUserUseCase>();
        services.AddScoped<ICreateTokenForAuthUseCase, CreateTokenForAuthUseCase>();
        services.AddScoped<IRefreshTokenForAuthUseCase, RefreshTokenForAuthUseCase>();

        #endregion

        #region Event use cases

        services.AddScoped<ICreateEventUseCase, CreateEventUseCase>();
        services.AddScoped<IDeleteEventUseCase, DeleteEventUseCase>();
        services.AddScoped<IGetEventByIdUseCase, GetEventByIdUseCase>();
        services.AddScoped<IGetEventByNameUseCase, GetEventByNameUseCase>();
        services.AddScoped<IGetEventCollectionByIdsUseCase, GetEventCollectionByIdsUseCase>();
        services.AddScoped<IGetEventsUseCase, GetEventsUseCase>();
        services.AddScoped<IUpdateEventUseCase, UpdateEventUseCase>();

        #endregion

        #region Participant use cases

        services.AddScoped<ICreateParticipantUseCase, CreateParticipantUseCase>();
        services.AddScoped<IDeleteParticipantUseCase, DeleteParticipantUseCase>();
        services.AddScoped<IGetParticipantByIdUseCase, GetParticipantByIdUseCase>();
        services.AddScoped<IGetParticipantsUseCase, GetParticipantsUseCase>();

        #endregion

        #region Image use cases

        services.AddScoped<IGetImageUseCase, GetImageUseCase>();

        #endregion
    }

    public static void ConfigureSqlContext(this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddDbContext<RepositoryContext>(opts =>
            opts.UseNpgsql(configuration.GetConnectionString("sqlConnection")));
    }

    public static void ConfigureAutoMapper(this IServiceCollection services)
    {
        services.AddAutoMapper(cfg =>
        {
            cfg.AddProfile<CreateEventMappingProfile>();
            cfg.AddProfile<GetEventMappingProfile>();
            cfg.AddProfile<UpdateEventMappingProfile>();
            
            cfg.AddProfile<CreateParticipantMappingProfile>();
            cfg.AddProfile<GetParticipantMappingProfile>();
            
            cfg.AddProfile<RegisterUserMappingProfile>();
        }, AppDomain.CurrentDomain.GetAssemblies());
    }

    public static void ConfigureApiBehaviorOptions(this IServiceCollection services) =>
        services.Configure<ApiBehaviorOptions>(opt => { opt.SuppressModelStateInvalidFilter = true; });

    public static void ConfigureCors(this IServiceCollection services) =>
        services.AddCors(options =>
        {
            options.AddPolicy("CorsPolicy", builder =>
                builder.WithExposedHeaders("X-Pagination"));
        });

    public static void ConfigureIdentity(this IServiceCollection services)
    {
        var builder = services.AddIdentity<User, IdentityRole>(o =>
            {
                o.Password.RequireDigit = true;
                o.Password.RequireLowercase = false;
                o.Password.RequireUppercase = false;
                o.Password.RequireNonAlphanumeric = false;
                o.Password.RequiredLength = 10;
                o.User.RequireUniqueEmail = true;
            })
            .AddEntityFrameworkStores<RepositoryContext>()
            .AddDefaultTokenProviders();
    }

    public static void ConfigureJwt(this IServiceCollection services, IConfiguration
        configuration)
    {
        var jwtSettings = configuration.GetSection("JwtSettings");
        var secretKey = jwtSettings.GetSection("validIssuer").Value;

        services.AddAuthentication(opt =>
            {
                opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = jwtSettings.GetSection("validIssuer").Value,
                    ValidAudience = jwtSettings.GetSection("validAudience").Value,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey!))
                };
            });
    }

    public static void AddAuthorizationPolicies(this IServiceCollection services) =>
        services.AddAuthorization(options =>
        {
            options.AddPolicy("AdminOnly", policy =>
                policy.RequireRole("Administrator"));

            options.AddPolicy("ParticipantOnly", policy =>
                policy.RequireRole("Participant"));

            options.AddPolicy("AdminOrParticipant", policy =>
                policy.RequireRole("Administrator", "Participant"));
        });

    public static void AddValidators(this IServiceCollection services)
    {
        services.AddTransient<IValidator<Event>, EventValidator>();
        services.AddTransient<IValidator<EventForCreationDto>, EventCreationDtoValidator>();
        services.AddTransient<IValidator<EventForUpdateDto>, EventUpdateDtoValidator>();
        
        services.AddTransient<IValidator<Participant>, ParticipantValidator>();
        services.AddTransient<IValidator<ParticipantForCreationDto>, ParticipantCreationDtoValidator>();
        
        services.AddTransient<IValidator<User>, UserValidator>();
        services.AddTransient<IValidator<UserForRegistrationDto>, UserRegistrationDtoValidator>();
        services.AddTransient<IValidator<UserForAuthenticationDto>, UserAuthenticationDtoValidator>();
    }

    public static void ConfigureSwagger(this IServiceCollection services)
    {
        services.AddSwaggerGen(s =>
        {
            s.SwaggerDoc("v1", new OpenApiInfo
            {
                Title = "Event Web App API",
                Version = "v1"
            });
            
            s.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                In = ParameterLocation.Header,
                Description = "Please enter JWT with Bearer into field",
                Name = "Authorization",
                Type = SecuritySchemeType.Http, 
                Scheme = "Bearer", 
                BearerFormat = "JWT" 
            });
            
            s.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        },
                        Scheme = "oauth2",
                        Name = "Bearer",
                        In = ParameterLocation.Header,
                    },
                    new List<string>()
                }
            });
        });
    }
}