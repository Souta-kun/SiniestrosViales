namespace SiniestrosViales.Application.Dtos;

public class SiniestroDto
{
    public long Id { get; init; }
    public DateTime FechaHora { get; init; }
    public string Departamento { get; init; } = default!;
    public string Ciudad { get; init; } = default!;
    public string Tipo { get; init; } = default!;
    public int VehiculosInvolucrados { get; init; }
    public int NumeroVictimas { get; init; }
    public string? Descripcion { get; init; }
}
