using Microsoft.AspNetCore.Mvc;
using UrbaPark.Aplicacion.Abstracciones;
using UrbaPark.Aplicacion.DTO;

namespace UrbaParkAPIWeb.Controllers;

[ApiController]
[Route("api/[controller]")]
public class RolesController : ControllerBase
{
    private readonly IRolAppService _rolAppService;

    public RolesController(IRolAppService rolAppService)
    {
        _rolAppService = rolAppService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<RolDto>>> Get([FromQuery] RolFilterDto filter)
    {
        var roles = await _rolAppService.GetFilteredRolesAsync(filter);
        return Ok(roles);
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<RolDto>> Get(int id)
    {
        var rol = await _rolAppService.GetRolByIdAsync(id);
        if (rol == null)
        {
            return NotFound();
        }
        return Ok(rol);
    }

    [HttpPost]
    public async Task<ActionResult<RolDto>> Post([FromBody] CreateRolDto rolDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        var createdRol = await _rolAppService.CreateRolAsync(rolDto);
        return CreatedAtAction(nameof(Get), new { id = createdRol.IdRol }, createdRol);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Put(int id, [FromBody] UpdateRolDto rolDto)
    {

        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        try
        {
            var rol = await _rolAppService.UpdateRolAsync(id, rolDto);
            return Ok(rol);
        }
        catch (KeyNotFoundException)
        {
            return NotFound();
        }
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        try
        {
            await _rolAppService.DeleteRolAsync(id);
            return Ok(true);
        }
        catch (KeyNotFoundException)
        {
            return NotFound();
        }
    }
}
