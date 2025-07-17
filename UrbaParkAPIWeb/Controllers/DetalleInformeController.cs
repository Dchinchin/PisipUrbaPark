using Microsoft.AspNetCore.Mvc;
using UrbaPark.Aplicacion.Abstracciones;
using UrbaPark.Aplicacion.DTO;
using Microsoft.AspNetCore.Http;

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

    [HttpGet("{id:int}")]
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
    public async Task<ActionResult<DetalleInformeDto>> Post([FromForm] CreateDetalleInformeDto detalleInformeDto, IFormFile? archivoFile)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var createdDetalle = await _detalleInformeAppService.CreateDetalleInformeAsync(detalleInformeDto, archivoFile);
        return CreatedAtAction(nameof(Get), new { id = createdDetalle.IdDetInfo }, createdDetalle);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Put(int id, [FromForm] UpdateDetalleInformeDto detalleInformeDto, IFormFile? archivoFile)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        try
        {
            var detalle = await _detalleInformeAppService.UpdateDetalleInformeAsync(id, detalleInformeDto, archivoFile);
            return Ok(detalle);
        }
        catch (KeyNotFoundException)
        {
            return NotFound();
        }
    }
}
