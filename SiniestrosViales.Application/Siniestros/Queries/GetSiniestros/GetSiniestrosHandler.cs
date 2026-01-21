using AutoMapper;
using MediatR;
using SiniestrosViales.Application.Common.Models;
using SiniestrosViales.Application.Dtos;
using SiniestrosViales.Application.Interfaces.Repositories;

namespace SiniestrosViales.Application.Siniestros.Queries.GetSiniestros;

public class GetSiniestrosHandler : IRequestHandler<GetSiniestrosQuery, PagedResult<SiniestroDto>>
{
    private readonly ISiniestroRepository _siniestroRepository;
    private readonly IMapper _mapper;

    public GetSiniestrosHandler(
        ISiniestroRepository siniestroRepository,
        IMapper mapper)
    {
        this._siniestroRepository = siniestroRepository;
        this._mapper = mapper;
    }

    public async Task<PagedResult<SiniestroDto>> Handle(GetSiniestrosQuery request, CancellationToken cancellationToken)
    {
        var siniestros = await _siniestroRepository.GetFilterAsync(
            request.Departamento,
            request.FechaInicio,
            request.FechaFin,
            request.Page,
            request.PageSize
        );

        var siniestroDtos = _mapper.Map<List<SiniestroDto>>(siniestros.Item2);

        return new PagedResult<SiniestroDto>(
            siniestroDtos,
            siniestros.Item1,
            request.Page,
            request.PageSize);
    }
}
