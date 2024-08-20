using LibraryApi.Application.Interfaces.Services;
using LibraryApi.Application.Interfaces.UnitOfWork;
using LibraryApi.Application.Services;
using LibraryApi.Infrastructure.Authorization.Context;
using LibraryApi.Infrastructure.Implementations.Contexts;
using LibraryApi.Infrastructure.Implementations.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace LibraryApi.Infrastructure
{
    public static class DependecyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, string connectionString)
        {
            services.AddLogging();

            services.AddDbContext<ReposContext>(options => options.UseSqlServer(connectionString));

            services.AddDbContext<AuthContext>(opt => opt.UseSqlServer(connectionString));

            services.AddScoped<IUnitOfWork, UnitOfWork>();

            services.AddTransient<IAuthService, AuthService>();

            return services;
        }
    }
}
