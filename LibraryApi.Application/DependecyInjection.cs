using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace LibraryApi.Application
{
    public static class DependecyInjection
    {
        public static IServiceCollection AddApplictaion(this IServiceCollection services)
        {

            var assembly = typeof(DependecyInjection).Assembly;
            services.AddAutoMapper(assembly);
            services.AddValidatorsFromAssembly(assembly);

            return services;
        }
    }
}
