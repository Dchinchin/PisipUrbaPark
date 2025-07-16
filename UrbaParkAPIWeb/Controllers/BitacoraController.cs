using Microsoft.AspNetCore.Mvc;
using UrbaPark.Aplicacion.Abstracciones;
using UrbaPark.Aplicacion.DTO;

namespace UrbaParkAPIWeb.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BitacoraController : ControllerBase
{
    private readonly IBitacoraAppService _bitacoraAppService;

    public BitacoraController(IBitacoraAppService bitacoraAppService)
    {
        _bitacoraAppService = bitacoraAppService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<BitacoraDto>>> Get([FromQuery] BitacoraFilterDto filter)
    {
        var bitacoras = await _bitacoraAppService.GetFilteredBitacorasAsync(filter);
        return Ok(bitacoras);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<BitacoraDto>> Get(int id)
    {
        var bitacora = await _bitacoraAppService.GetBitacoraByIdAsync(id);
        if (bitacora == null)
        {
            return NotFound();
        }
        return Ok(bitacora);
    }

    [HttpPost]
    public async Task<ActionResult<BitacoraDto>> Post([FromBody] CreateBitacoraDto bitacoraDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        var createdBitacora = await _bitacoraAppService.CreateBitacoraAsync(bitacoraDto);
        return CreatedAtAction(nameof(Get), new { id = createdBitacora.IdBitacora }, createdBitacora);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Put(int id, [FromBody] UpdateBitacoraDto bitacoraDto)
    {
        if (id != bitacoraDto.IdBitacora)
        {
            return BadRequest("El ID de la bitácora en la URL no coincide con el ID del cuerpo de la solicitud.");
        }

        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        try
        {
            await _bitacoraAppService.UpdateBitacoraAsync(bitacoraDto);
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
            await _bitacoraAppService.DeleteBitacoraAsync(id);
            return NoContent();
        }
        catch (KeyNotFoundException)
        {
            return NotFound();
        }
    }
}
