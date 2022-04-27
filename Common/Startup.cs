﻿using Common.Converters;
using Common.Filters;
using Common.Logging;
using System.Text.Json;

namespace Common;

public static class Startup
{
    public static IServiceCollection AddCommon(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddAutoMapper(typeof(Startup));
        services.AddControllers(opt =>
        {
            opt.Filters.Add(typeof(ValidatorActionFilter));
        })
            .AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.Converters.Add(new DateTimeConverter());
                options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
            });

        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();

        // Dependency Injection
        services.AddScoped(typeof(IAppLogger<>), typeof(LoggerAdapter<>));

        return services;
    }
}
