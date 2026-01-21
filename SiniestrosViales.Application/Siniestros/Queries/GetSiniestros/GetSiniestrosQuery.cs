using MediatR;
using SiniestrosViales.Application.Common.Models;
using SiniestrosViales.Application.Dtos;

namespace SiniestrosViales.Application.Siniestros.Queries.GetSiniestros;

public record GetSiniestrosQuery(
    string? Departamento,
    DateTime? FechaInicio,
    DateTime? FechaFin,
    int Page = 1,
    int PageSize = 10
) : IRequest<PagedResult<SiniestroDto>>;