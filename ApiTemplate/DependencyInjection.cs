using ApiTemplate.Services;
using ApiTemplate.Services.Interfaces;

namespace ApiTemplate
{
    public static class DependencyInjection
    {
        public static IServiceCollection InjectDependencies(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddTransient<IDepartmentService, DepartmentService>();

            return services;
        }
    }
}
