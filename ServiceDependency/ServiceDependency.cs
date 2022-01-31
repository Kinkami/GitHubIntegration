using Core.Application.Services;
using Core.Domain.Interfaces.Repositories;
using Core.Domain.Interfaces.Services;
using Infrastructure.GitHub.Repository;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Service
{
    public static class ServiceDependency
    {
        public static void AddServices(this IServiceCollection services)
        {
            services.AddScoped<IUserGitHubReposService, UserGitHubReposService>();
            services.AddScoped<IUserGitHubReposRepository, UserGitHubReposRepository>();

         

        }
    }
}
