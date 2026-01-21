using AutoMapper;
using FluentAssertions;
using Moq;
using SiniestrosViales.Application.Common.Models;
using SiniestrosViales.Application.Dtos;
using SiniestrosViales.Application.Interfaces.Repositories;
using SiniestrosViales.Application.Siniestros.Queries.GetSiniestros;
using SiniestrosViales.Domain.Entities;
using SiniestrosViales.Domain.Enums;

namespace SiniestrosViales.Application.Tests.Queries;

public class GetSiniestrosHandlerTests
{
    private readonly Mock<ISiniestroRepository> _repositoryMock;
    private readonly Mock<IMapper> _mapperMock;
    private readonly GetSiniestrosHandler _handler;

    public GetSiniestrosHandlerTests()
    {
        _repositoryMock = new Mock<ISiniestroRepository>();
        _mapperMock = new Mock<IMapper>();

        _handler = new GetSiniestrosHandler(_repositoryMock.Object, _mapperMock.Object);
    }

    [Fact]
    public async Task GetSiniestrosHandler_Handle_ShouldReturnSiniestros()
    {
        // Arrange
        var query = new GetSiniestrosQuery(
            Departamento: "Bolivar",
            FechaInicio: null,
            FechaFin: null,
            Page: 1,
            PageSize: 10
        );

        var siniestros = new List<Siniestro>
        {
            new(DateTime.UtcNow, "Bolivar", "Cartagena", TipoSiniestro.Atropello, 1, 2),
            new(DateTime.UtcNow, "Bolivar", "Turbaco", TipoSiniestro.Choque, 2, 2)
        };

        var pagedResult = new PagedResult<Siniestro>(siniestros, totalItems: 2, page: 1, pageSize: 10);

        var siniestrosDto = new List<SiniestroDto>
        {
            new() { FechaHora = DateTime.UtcNow, Departamento = "Bolivar", Ciudad = "Cartagena", VehiculosInvolucrados = 1, NumeroVictimas = 2 },
            new() { FechaHora = DateTime.UtcNow, Departamento = "Bolivar", Ciudad = "Turbaco", VehiculosInvolucrados = 2, NumeroVictimas = 2 }
        };

        _repositoryMock
            .Setup(r => r.GetFilterAsync(
                query.Departamento,
                query.FechaInicio,
                query.FechaFin,
                query.Page,
                query.PageSize))
            .ReturnsAsync(Tuple.Create(2, siniestros));

        _mapperMock
            .Setup(m => m.Map<List<SiniestroDto>>(siniestros))
            .Returns(siniestrosDto);

        // Act
        var result = await _handler.Handle(query, CancellationToken.None);

        // Assert
        result.Should().NotBeNull();
        result.Items.Should().NotBeNull();
        result.TotalItems.Should().Be(2);

        _repositoryMock.Verify(r => r.GetFilterAsync(
            query.Departamento,
            query.FechaInicio,
            query.FechaFin,
            query.Page,
            query.PageSize),
            Times.Once);
    }
}
