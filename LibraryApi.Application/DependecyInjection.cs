using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace LibraryApi.Application
{
    public static class DependecyInjection
    {
        public static IServiceCollection AddAplictaion(this IServiceCollection services)
        {
            var assembly = typeof(DependecyInjection).Assembly;

            services.AddValidatorsFromAssembly(assembly);

            return services;
        }
    }
}
