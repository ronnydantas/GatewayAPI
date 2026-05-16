using Domain.UseCases.UpdadePerson;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GatewayAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PersonController : ControllerBase
{
    private readonly IMediator _mediator;

    public PersonController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [Authorize]
    [HttpPut("update-register/{id}")]
    public async Task<IActionResult> UpdateRegister(string id, [FromBody] PersonCommand cliente)
    {
        cliente.Id = id;
        var user = await _mediator.Send(cliente);

        return Ok(user);
    }
}
