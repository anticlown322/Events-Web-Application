using System.Reflection;
using Infrastructure.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace Infrastructure;

public class RepositoryContextFactory : IDesignTimeDbContextFactory<RepositoryContext>
{
    public RepositoryContext CreateDbContext(string[] args)
    {
        var presentationAssemblyPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
        
        var presentationPath = Path.Combine(presentationAssemblyPath, @"..\..\..\..\", "Presentation");

        var configuration = new ConfigurationBuilder()
            .SetBasePath(presentationPath) 
            .AddJsonFile("appsettings.json") 
            .Build();

        var optionsBuilder = new DbContextOptionsBuilder<RepositoryContext>();
        optionsBuilder.UseNpgsql(configuration.GetConnectionString("sqlConnection"));

        return new RepositoryContext(optionsBuilder.Options);
    }
}