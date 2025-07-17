using Microsoft.AspNetCore.Mvc;
using UrbaPark.Aplicacion.Abstracciones;
using UrbaPark.Aplicacion.DTO;

namespace UrbaParkAPIWeb.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ParqueaderoController : ControllerBase
{
    private readonly IParqueaderoAppService _parqueaderoAppService;

    public ParqueaderoController(IParqueaderoAppService parqueaderoAppService)
    {
        _parqueaderoAppService = parqueaderoAppService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<ParqueaderoDto>>> Get([FromQuery] ParqueaderoFilterDto filter)
    {
        var parqueaderos = await _parqueaderoAppService.GetFilteredParqueaderosAsync(filter);
        return Ok(parqueaderos);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ParqueaderoDto>> Get(int id)
    {
        var parqueadero = await _parqueaderoAppService.GetParqueaderoByIdAsync(id);
        if (parqueadero == null)
        {
            return NotFound();
        }
        return Ok(parqueadero);
    }

    [HttpPost]
    public async Task<ActionResult<ParqueaderoDto>> Post([FromBody] CreateParqueaderoDto parqueaderoDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        var createdParqueadero = await _parqueaderoAppService.CreateParqueaderoAsync(parqueaderoDto);
        return CreatedAtAction(nameof(Get), new { id = createdParqueadero.IdParqueadero }, createdParqueadero);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Put(int id, [FromBody] UpdateParqueaderoDto parqueaderoDto)
    {

        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        try
        {
            var parqueadero = await _parqueaderoAppService.UpdateParqueaderoAsync(id, parqueaderoDto);
            return Ok(parqueadero);
        }
        catch (KeyNotFoundException)
        {
            return NotFound();
        }
    }

    [HttpPut("{id}/activar")]
    public async Task<IActionResult> ActivarParqueadero(int id)
    {
        try
        {
            await _parqueaderoAppService.ActivarParqueadero(id);
            return NoContent();
        }
        catch (KeyNotFoundException)
        {
            return NotFound();
        }
    }

    [HttpPut("{id}/desactivar")]
    public async Task<IActionResult> DesactivarParqueadero(int id)
    {
        try
        {
            await _parqueaderoAppService.DesactivarParqueadero(id);
            return NoContent();
        }
        catch (KeyNotFoundException)
        {
            return NotFound();
        }
    }
}
