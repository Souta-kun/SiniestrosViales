using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using SiniestrosViales.Infrastructure.Base;

namespace SiniestrosViales.Infrastructure.Health;

public class DbHealthCheck : IHealthCheck
{
    private readonly SiniestrosDbContext context;

    public DbHealthCheck(SiniestrosDbContext context)
    {
        this.context = context;
    }

    public async Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
    {
        try
        {
            await this.context.Database.ExecuteSqlRawAsync("SELECT 1", cancellationToken);

            return HealthCheckResult.Healthy("Database OK");
        }
        catch (Exception ex)
        {
            return HealthCheckResult.Unhealthy(ex.Message);
        }
    }
}
