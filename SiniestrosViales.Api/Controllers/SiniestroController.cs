using MediatR;
using Microsoft.AspNetCore.Mvc;
using SiniestrosViales.Application.Common.Models;
using SiniestrosViales.Application.Siniestros.Commands.CreateSiniestro;
using SiniestrosViales.Application.Siniestros.Queries.GetSiniestros;

namespace SiniestrosViales.Api.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class SiniestroController : ControllerBase
{
    private readonly IMediator _mediator;

    public SiniestroController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateSiniestroCommand cmd)
    {
        var id = await _mediator.Send(cmd);

        return Ok(ApiResult.SuccessResult(id));
    }

    [HttpGet]
    public async Task<IActionResult> Get([FromQuery] GetSiniestrosQuery query)
    {
        var result = await _mediator.Send(query);

        return Ok(ApiResult.SuccessResult(result));
    }
}
