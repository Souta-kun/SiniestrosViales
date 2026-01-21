using MediatR;
using SiniestrosViales.Application.Interfaces.Repositories;
using SiniestrosViales.Domain.Entities;

namespace SiniestrosViales.Application.Siniestros.Commands.CreateSiniestro;

public class CreateSiniestroHandler : IRequestHandler<CreateSiniestroCommand, long>
{
    private readonly ISiniestroRepository _siniestroRepository;

    public CreateSiniestroHandler(ISiniestroRepository siniestroRepository)
    {
        this._siniestroRepository = siniestroRepository;
    }

    public async Task<long> Handle(CreateSiniestroCommand request, CancellationToken ct)
    {
        var siniestro = new Siniestro(
            request.FechaHora,
            request.Departamento,
            request.Ciudad,
            request.Tipo,
            request.Vehiculos,
            request.Victimas,
            request.Descripcion
        );

        await _siniestroRepository.AddAsync(siniestro);
        return siniestro.Id;
    }
}
