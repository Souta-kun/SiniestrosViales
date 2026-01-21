using SiniestrosViales.Domain.Enums;

namespace SiniestrosViales.Domain.Entities;

public class Siniestro
{
    public long Id { get; private set; }
    public DateTime FechaHora { get; private set; }
    public string Departamento { get; private set; }
    public string Ciudad { get; private set; }
    public TipoSiniestro Tipo { get; private set; }
    public int VehiculosInvolucrados { get; private set; }
    public int NumeroVictimas { get; private set; }
    public string? Descripcion { get; private set; }

    private Siniestro() { }

    public Siniestro(
        DateTime fechaHora,
        string departamento,
        string ciudad,
        TipoSiniestro tipo,
        int vehiculos,
        int victimas,
        string? descripcion = null)
    {
        if (string.IsNullOrWhiteSpace(departamento))
            throw new ArgumentException("El departamento es requerido.");

        if (string.IsNullOrWhiteSpace(ciudad))
            throw new ArgumentException("La ciudad es requerido.");

        if (vehiculos < 0)
            throw new ArgumentException("El número de vehículos no puede ser negativo.");

        if (victimas < 0)
            throw new ArgumentException("El número de víctimas no puede ser negativo.");

        if (Enum.IsDefined(typeof(TipoSiniestro), tipo) == false)
            throw new ArgumentException("El tipo de siniestro no es válido.");

        FechaHora = fechaHora;
        Departamento = departamento;
        Ciudad = ciudad;
        Tipo = tipo;
        VehiculosInvolucrados = vehiculos;
        NumeroVictimas = victimas;
        Descripcion = descripcion;
    }
}
