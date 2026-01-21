using SiniestrosViales.Domain.Entities;

namespace SiniestrosViales.Application.Interfaces.Repositories;

public interface ISiniestroRepository
{
    Task AddAsync(Siniestro siniestro);
    Task<Tuple<int, List<Siniestro>>> GetFilterAsync(
        string? Departamento,
        DateTime? FechaInicio,
        DateTime? FechaFin,
        int Page = 1,
        int PageSize = 10);
}
