using Domain.DTOs;
using Domain.UseCases.Identitys;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace GatewayAPI.Controllers;


[ApiController]
[Route("api/[controller]")]
public class IdentityController : ControllerBase
{
    private readonly IMediator _mediator;

    public IdentityController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] SignUpCommand model)
    {
        var user = await _mediator.Send(model);

        return Ok(user);
    }
}
