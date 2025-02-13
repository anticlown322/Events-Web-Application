FROM mcr.microsoft.com/dotnet/sdk:8.0-alpine AS build

WORKDIR /app
COPY . .

WORKDIR /app/Presentation.WebApi
RUN dotnet publish -c Release -o /app/publish

# state w/o build files
FROM mcr.microsoft.com/dotnet/aspnet:8.0-alpine AS final
EXPOSE 50500
EXPOSE 50501

WORKDIR /app
COPY --from=build /app/publish .

ENTRYPOINT ["dotnet", "Presentation.WebApi.dll"]