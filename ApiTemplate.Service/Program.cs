using ApiTemplate;
using ApiTemplate.Data;
using Common;
using Common.Contract;
using Common.Filters;
using Common.Middleware;
using FluentValidation.AspNetCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Serilog;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddDbContext<DbContext, DefaultContext>(options =>
                 options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Add services to the container.
builder.Services.AddCommon(builder.Configuration);

builder.Services.InjectDependencies(builder.Configuration);

builder.Services.AddHttpContextAccessor();
builder.Services.AddHttpClient();
builder.Services.AddFluentValidation(fvc =>
{
    fvc.RegisterValidatorsFromAssemblyContaining<Program>();
    fvc.DisableDataAnnotationsValidation = true;
});

builder.Services
    .AddHealthChecks()
    .AddDbContextCheck<DefaultContext>();

builder.Host.UseSerilog((hostingContext, services, loggerConfiguration) => loggerConfiguration
                    .ReadFrom.Configuration(hostingContext.Configuration)
                    .Enrich.FromLogContext());
builder.WebHost.UseContentRoot(Directory.GetCurrentDirectory())
                .UseIISIntegration()
                .UseIIS();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsProduction())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<ErrorHandlerMiddleware>();
//app.UseMiddleware<RequestMiddleware>();

app.UseAuthorization();

app.MapControllers();

app.MapHealthChecks("health");

app.UseSerilogRequestLogging(options =>
{
    options.MessageTemplate =
        "{RemoteIpAddress} {RequestScheme}:{RequestHost} {RequestMethod} {RequestPath} responded {StatusCode} in {Elapsed:0.0000} ms";
    options.EnrichDiagnosticContext = (
        diagnosticContext,
        httpContext) =>
    {
        diagnosticContext.Set("RequestHost", httpContext.Request.Host.Value);
        diagnosticContext.Set("RequestScheme", httpContext.Request.Scheme);
        diagnosticContext.Set("RemoteIpAddress", httpContext.Connection.RemoteIpAddress);
    };
});

app.Run();
