using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using SiniestrosViales.Api.Middlewares;
using SiniestrosViales.Application.Interfaces.Repositories;
using SiniestrosViales.Application.Mappings;
using SiniestrosViales.Application.Siniestros.Commands.CreateSiniestro;
using SiniestrosViales.Infrastructure.Base;
using SiniestrosViales.Infrastructure.Health;
using SiniestrosViales.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<SiniestrosDbContext>(opts =>
    opts.UseSqlServer(builder.Configuration.GetConnectionString("SiniestrosDb"))
);

builder.Services.AddHealthChecks().AddCheck<DbHealthCheck>("database", HealthStatus.Unhealthy);

builder.Services.AddScoped<ISiniestroRepository, SiniestroRepository>();
builder.Services.AddMediatR(
    cfg => cfg.RegisterServicesFromAssembly(typeof(CreateSiniestroCommand).Assembly)
);
builder.Services.AddAutoMapper(typeof(SiniestroProfile).Assembly);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapHealthChecks("health");

app.UseMiddleware<ExceptionMiddleware>();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
