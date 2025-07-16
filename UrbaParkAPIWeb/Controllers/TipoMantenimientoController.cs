using Microsoft.AspNetCore.Mvc;
using UrbaPark.Aplicacion.Abstracciones;
using UrbaPark.Aplicacion.DTO;

namespace UrbaParkAPIWeb.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TipoMantenimientoController : ControllerBase
{
    private readonly ITipoMantenimientoAppService _tipoMantenimientoAppService;

    public TipoMantenimientoController(ITipoMantenimientoAppService tipoMantenimientoAppService)
    {
        _tipoMantenimientoAppService = tipoMantenimientoAppService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<TipoMantenimientoDto>>> Get([FromQuery] TipoMantenimientoFilterDto filter)
    {
        var tipos = await _tipoMantenimientoAppService.GetFilteredTipoMantenimientosAsync(filter);
        return Ok(tipos);
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<TipoMantenimientoDto>> Get(int id)
    {
        var tipo = await _tipoMantenimientoAppService.GetTipoMantenimientoByIdAsync(id);
        if (tipo == null)
        {
            return NotFound();
        }
        return Ok(tipo);
    }

    [HttpPost]
    public async Task<ActionResult<TipoMantenimientoDto>> Post([FromBody] CreateTipoMantenimientoDto tipoMantenimientoDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        var createdTipo = await _tipoMantenimientoAppService.CreateTipoMantenimientoAsync(tipoMantenimientoDto);
        return CreatedAtAction(nameof(Get), new { id = createdTipo.Id }, createdTipo);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Put(int id, [FromBody] UpdateTipoMantenimientoDto tipoMantenimientoDto)
    {
        if (id != tipoMantenimientoDto.Id)
        {
            return BadRequest("El ID del tipo de mantenimiento en la URL no coincide con el ID del cuerpo de la solicitud.");
        }

        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        try
        {
            await _tipoMantenimientoAppService.UpdateTipoMantenimientoAsync(tipoMantenimientoDto);
            return NoContent();
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
            await _tipoMantenimientoAppService.DeleteTipoMantenimientoAsync(id);
            return NoContent();
        }
        catch (KeyNotFoundException)
        {
            return NotFound();
        }
    }
}
