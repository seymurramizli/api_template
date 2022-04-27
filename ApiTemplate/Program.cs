using ApiTemplate;
using ApiTemplate.Data;
using ApiTemplate.Entities;
using Common;
using Common.Filters;
using Common.Middleware;
using FluentValidation.AspNetCore;
using Microsoft.EntityFrameworkCore;
using Serilog;
using Serilog.Sanitizer.Extensions;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddDbContext<DbContext, ApplicationDbContext>(options =>
                 options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Add services to the container.
builder.Services.AddCommon(builder.Configuration);

builder.Services.InjectDependencies(builder.Configuration);

builder.Services.AddHttpContextAccessor();
builder.Services.AddHttpClient();
builder.Services.AddControllers(opt =>
{
    opt.Filters.Add(typeof(ValidatorActionFilter));
}).AddFluentValidation(fvc =>
{
    fvc.RegisterValidatorsFromAssemblyContaining<Program>();
    fvc.DisableDataAnnotationsValidation = true;
});


builder.Host.UseSerilog((hostingContext, services, loggerConfiguration) => loggerConfiguration
                    .Sanitize().Structured().ByRemoving<Department>(x => x.Name)
                    .Continue()
                    .ReadFrom.Configuration(hostingContext.Configuration)
                    .Enrich.FromLogContext());

builder.WebHost.UseContentRoot(Directory.GetCurrentDirectory())
                .UseIISIntegration()
                .UseIIS();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<ErrorHandlerMiddleware>();
app.UseMiddleware<RequestMiddleware>();

app.UseAuthorization();

app.MapControllers();

app.Run();
