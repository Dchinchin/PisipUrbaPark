using Microsoft.AspNetCore.Mvc;
using UrbaPark.Aplicacion.Abstracciones;
using UrbaPark.Aplicacion.DTO;

namespace UrbaParkAPIWeb.Controllers;

[ApiController]
[Route("api/[controller]")]
public class DetalleInformeController : ControllerBase
{
    private readonly IDetalleInformeAppService _detalleInformeAppService;

    public DetalleInformeController(IDetalleInformeAppService detalleInformeAppService)
    {
        _detalleInformeAppService = detalleInformeAppService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<DetalleInformeDto>>> Get([FromQuery] DetalleInformeFilterDto filter)
    {
        var detalles = await _detalleInformeAppService.GetFilteredDetallesInformeAsync(filter);
        return Ok(detalles);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<DetalleInformeDto>> Get(int id)
    {
        var detalle = await _detalleInformeAppService.GetDetalleInformeByIdAsync(id);
        if (detalle == null)
        {
            return NotFound();
        }
        return Ok(detalle);
    }

    [HttpPost]
    public async Task<ActionResult<DetalleInformeDto>> Post([FromBody] CreateDetalleInformeDto detalleInformeDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        var createdDetalle = await _detalleInformeAppService.CreateDetalleInformeAsync(detalleInformeDto);
        return CreatedAtAction(nameof(Get), new { id = createdDetalle.IdDetInfo }, createdDetalle);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Put(int id, [FromBody] UpdateDetalleInformeDto detalleInformeDto)
    {
        if (id != detalleInformeDto.IdDetInfo)
        {
            return BadRequest("El ID del detalle de informe en la URL no coincide con el ID del cuerpo de la solicitud.");
        }

        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        try
        {
            await _detalleInformeAppService.UpdateDetalleInformeAsync(detalleInformeDto);
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
            await _detalleInformeAppService.DeleteDetalleInformeAsync(id);
            return NoContent();
        }
        catch (KeyNotFoundException)
        {
            return NotFound();
        }
    }
}
