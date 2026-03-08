using Microsoft.AspNetCore.Mvc;
using SinistroManager.Services;
using SinistroManager.Enums;

namespace SinistroManager.Controllers;

[ApiController]
[Route("api/[controller]")]
public class SinistrosController : ControllerBase
{
    private readonly SinistroService _service;

    public SinistrosController(SinistroService service)
    {
        _service = service;
    }

    [HttpPost("{id}/aprovar")]
    public async Task<IActionResult> Aprovar(Guid id, [FromQuery] UserRole role)
    {
        try
        {
            await _service.AprovarAsync(id, role);
            return Ok();
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(ex.Message);
        }
        catch (UnauthorizedAccessException)
        {
            return Forbid();
        }
    }
}