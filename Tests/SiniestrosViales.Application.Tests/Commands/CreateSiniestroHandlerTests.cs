using FluentAssertions;
using Moq;
using SiniestrosViales.Application.Interfaces.Repositories;
using SiniestrosViales.Application.Siniestros.Commands.CreateSiniestro;
using SiniestrosViales.Domain.Entities;
using SiniestrosViales.Domain.Enums;

namespace SiniestrosViales.Application.Tests.Commands;

public class CreateSiniestroHandlerTests
{
    private readonly Mock<ISiniestroRepository> _repositoryMock;
    private readonly CreateSiniestroHandler _handler;

    public CreateSiniestroHandlerTests()
    {
        _repositoryMock = new Mock<ISiniestroRepository>();
        _handler = new CreateSiniestroHandler(_repositoryMock.Object);
    }

    [Fact]
    public async Task CreateSiniestroHandler_Handler_ShouldCreateSiniestroAndReturnId()
    {
        // Arrange
        var command = new CreateSiniestroCommand(
            FechaHora: DateTime.UtcNow,
            Departamento: "Atlántico",
            Ciudad: "Barranquilla",
            Tipo: TipoSiniestro.Volcamiento,
            Vehiculos: 1,
            Victimas: 2
        );

        _repositoryMock
            .Setup(r => r.AddAsync(It.IsAny<Siniestro>()))
            .Returns(Task.CompletedTask);

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        result.Should().NotBe(null);

        _repositoryMock.Verify(
            r => r.AddAsync(It.IsAny<Siniestro>()),
            Times.Once
        );
    }
}
