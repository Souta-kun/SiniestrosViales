using Microsoft.EntityFrameworkCore;
using SiniestrosViales.Domain.Entities;

namespace SiniestrosViales.Infrastructure.Base;

public class SiniestrosDbContext : DbContext
{
    public DbSet<Siniestro> Siniestros => Set<Siniestro>();

    public SiniestrosDbContext(DbContextOptions<SiniestrosDbContext> options) : base(options)
    {
        
    }
}
