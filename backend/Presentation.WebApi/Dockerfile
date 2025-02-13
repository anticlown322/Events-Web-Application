FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Presentation.WebApi/Presentation.WebApi.csproj", "Presentation.WebApi/"]
COPY ["Infrastructure.Repository/Infrastructure.Repository.csproj", "Infrastructure.Repository/"]
COPY ["Domain.Contracts/Domain.Contracts.csproj", "Domain.Contracts/"]
COPY ["Domain.Entities/Domain.Entities.csproj", "Domain.Entities/"]
COPY ["Infrastructure.RequestFeatures/Infrastructure.RequestFeatures.csproj", "Infrastructure.RequestFeatures/"]
COPY ["Presentation.Core/Presentation.Core.csproj", "Presentation.Core/"]
COPY ["Application.DTO/Application.DTO.csproj", "Application.DTO/"]
COPY ["Application.Contracts/Application.Contracts.csproj", "Application.Contracts/"]
COPY ["Application.Validators/Application.Validators.csproj", "Application.Validators/"]
COPY ["Infrastructure.Logger/Infrastructure.Logger.csproj", "Infrastructure.Logger/"]
COPY ["Application.UseCases/Application.UseCases.csproj", "Application.UseCases/"]
COPY ["Application.Exceptions/Application.Exceptions.csproj", "Application.Exceptions/"]
RUN dotnet restore "Presentation.WebApi/Presentation.WebApi.csproj"
COPY . .
WORKDIR "/src/Presentation.WebApi"
RUN dotnet build "Presentation.WebApi.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "Presentation.WebApi.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Presentation.WebApi.dll"]
