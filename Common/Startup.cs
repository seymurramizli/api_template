using Common.Converters;
using Common.Filters;
using Common.Logging;
using Common.Mappings;
using Microsoft.OpenApi.Models;
using System.Text.Json;

namespace Common;

public static class Startup
{
    public static IServiceCollection AddCommon(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddAutoMapper(cfg =>
        {
            cfg.AddProfile(new MappingProfile());
            AppDomain.CurrentDomain.GetAssemblies();
        });
        services.AddControllers(opt =>
        {
            opt.Filters.Add(typeof(ValidatorActionFilter));
        })
        .AddJsonOptions(options =>
        {
            options.JsonSerializerOptions.Converters.Add(new DateTimeConverter());
            options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
        })
        .ConfigureApiBehaviorOptions(options =>
        {
            options.SuppressModelStateInvalidFilter = true;
        }); ;

        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen(c =>
        {
            c.SchemaFilter<CustomSchemaFilters>();
        });

        // Dependency Injection
        services.AddScoped(typeof(IAppLogger<>), typeof(LoggerAdapter<>));

        return services;
    }
}
