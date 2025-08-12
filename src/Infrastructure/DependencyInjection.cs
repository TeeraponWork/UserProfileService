using Application.Abstractions;
using Infrastructure.Persistence;
using Infrastructure.Repositories;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration config)
        {
            services.AddDbContext<UserProfileDbContext>(opt =>
            {
                var cs = config.GetConnectionString("SqlServer");
                opt.UseSqlServer(cs);
            });

            services.AddScoped<IUserProfileRepository, UserProfileRepository>();
            return services;
        }
    }
}
