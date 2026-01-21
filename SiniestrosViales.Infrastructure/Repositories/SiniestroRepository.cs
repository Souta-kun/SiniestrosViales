using Microsoft.EntityFrameworkCore;
using SiniestrosViales.Application.Interfaces.Repositories;
using SiniestrosViales.Domain.Entities;
using SiniestrosViales.Infrastructure.Base;

namespace SiniestrosViales.Infrastructure.Repositories;

public class SiniestroRepository : ISiniestroRepository
{
    private readonly SiniestrosDbContext _context;

    public SiniestroRepository(SiniestrosDbContext context)
    {
        _context = context;
    }

    public Task AddAsync(Siniestro s)
    {
        _context.Siniestros.Add(s);
        return _context.SaveChangesAsync();
    }

    public async Task<Tuple<int, List<Siniestro>>> GetFilterAsync(
        string? departamento,
        DateTime? fechaInicio,
        DateTime? fechaFin,
        int page = 1,
        int pageSize = 10)
    {
        var query = _context.Siniestros.AsQueryable();

        if (!string.IsNullOrEmpty(departamento))
            query = query.Where(x => x.Departamento == departamento);

        if (fechaInicio.HasValue)
            query = query.Where(x => x.FechaHora >= fechaInicio);

        if (fechaFin.HasValue)
            query = query.Where(x => x.FechaHora <= fechaFin);

        var total = await query.CountAsync();

        var items = await query
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        return Tuple.Create(total, items);
    }
}
