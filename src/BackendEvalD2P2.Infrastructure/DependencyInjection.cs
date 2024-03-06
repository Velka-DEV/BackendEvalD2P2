using BackendEvalD2P2.Application.Common.Interfaces.Repositories;
using BackendEvalD2P2.Infrastructure.Data;
using BackendEvalD2P2.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BackendEvalD2P2.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<ApplicationDbContext>(options => options.UseSqlite(configuration.GetConnectionString("Sqlite")));

        services.AddScoped<IEventRepository, EventRepository>();
        
        return services;
    }   
}