using MediatR;
using SiniestrosViales.Domain.Enums;

namespace SiniestrosViales.Application.Siniestros.Commands.CreateSiniestro;

public record CreateSiniestroCommand(
    DateTime FechaHora,
    string Departamento,
    string Ciudad,
    TipoSiniestro Tipo,
    int Vehiculos,
    int Victimas,
    string? Descripcion = null
) : IRequest<long>;