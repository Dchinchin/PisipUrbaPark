using Microsoft.AspNetCore.Mvc;
using UrbaPark.Aplicacion.Abstracciones;
using UrbaPark.Aplicacion.DTO;

namespace UrbaParkAPIWeb.Controllers;

[ApiController]
[Route("api/[controller]")]
public class MantenimientoController : ControllerBase
{
    private readonly IMantenimientoAppService _mantenimientoAppService;

    public MantenimientoController(IMantenimientoAppService mantenimientoAppService)
    {
        _mantenimientoAppService = mantenimientoAppService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<MantenimientoDto>>> Get([FromQuery] MantenimientoFilterDto filter)
    {
        var mantenimientos = await _mantenimientoAppService.GetFilteredMantenimientosAsync(filter);
        return Ok(mantenimientos);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<MantenimientoDto>> Get(int id)
    {
        var mantenimiento = await _mantenimientoAppService.GetMantenimientoByIdAsync(id);
        if (mantenimiento == null)
        {
            return NotFound();
        }
        return Ok(mantenimiento);
    }

    [HttpPost]
    public async Task<ActionResult<MantenimientoDto>> Post([FromBody] CreateMantenimientoDto mantenimientoDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        var createdMantenimiento = await _mantenimientoAppService.CreateMantenimientoAsync(mantenimientoDto);
        return CreatedAtAction(nameof(Get), new { id = createdMantenimiento.IdMantenimiento }, createdMantenimiento);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Put(int id, [FromBody] UpdateMantenimientoDto mantenimientoDto)
    {
        if (id != mantenimientoDto.IdMantenimiento)
        {
            return BadRequest("El ID del mantenimiento en la URL no coincide con el ID del cuerpo de la solicitud.");
        }

        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        try
        {
            await _mantenimientoAppService.UpdateMantenimientoAsync(mantenimientoDto);
            return NoContent();
        }
        catch (KeyNotFoundException)
        {
            return NotFound();
        }
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        try
        {
            await _mantenimientoAppService.DeleteMantenimientoAsync(id);
            return NoContent();
        }
        catch (KeyNotFoundException)
        {
            return NotFound();
        }
    }
}
