using Microsoft.AspNetCore.Mvc;
using UrbaPark.Aplicacion.Abstracciones;
using UrbaPark.Aplicacion.DTO;
using Microsoft.AspNetCore.Http;

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
    public async Task<ActionResult<BitacoraDto>> Post([FromForm] CreateBitacoraDto bitacoraDto, IFormFile? imagenFile)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var createdBitacora = await _bitacoraAppService.CreateBitacoraAsync(bitacoraDto, imagenFile);
        return CreatedAtAction(nameof(Get), new { id = createdBitacora.IdBitacora }, createdBitacora);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Put(int id, [FromForm] UpdateBitacoraDto bitacoraDto, IFormFile? imagenFile)
    {

        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        try
        {
            var bitacora = await _bitacoraAppService.UpdateBitacoraAsync(id, bitacoraDto, imagenFile);
            return Ok(bitacora);
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
            await _bitacoraAppService.DeleteBitacoraAsync(id);
            return Ok(true);
        }
        catch (KeyNotFoundException)
        {
            return NotFound();
        }
    }
}
