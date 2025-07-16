using Microsoft.AspNetCore.Mvc;
using UrbaPark.Aplicacion.Abstracciones;
using UrbaPark.Aplicacion.DTO;

namespace UrbaParkAPIWeb.Controllers;

[ApiController]
[Route("api/[controller]")]
public class InformesEncabezadoController : ControllerBase
{
    private readonly IInformeEncabezadoAppService _informeEncabezadoAppService;

    public InformesEncabezadoController(IInformeEncabezadoAppService informeEncabezadoAppService)
    {
        _informeEncabezadoAppService = informeEncabezadoAppService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<InformeEncabezadoDto>>> Get([FromQuery] InformeEncabezadoFilterDto filter)
    {
        var informes = await _informeEncabezadoAppService.GetFilteredInformesEncabezadoAsync(filter);
        return Ok(informes);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<InformeEncabezadoDto>> Get(int id)
    {
        var informe = await _informeEncabezadoAppService.GetInformeEncabezadoByIdAsync(id);
        if (informe == null)
        {
            return NotFound();
        }
        return Ok(informe);
    }

    [HttpPost]
    public async Task<ActionResult<InformeEncabezadoDto>> Post([FromBody] CreateInformeEncabezadoDto informeEncabezadoDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        var createdInforme = await _informeEncabezadoAppService.CreateInformeEncabezadoAsync(informeEncabezadoDto);
        return CreatedAtAction(nameof(Get), new { id = createdInforme.IdInforme }, createdInforme);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Put(int id, [FromBody] UpdateInformeEncabezadoDto informeEncabezadoDto)
    {
        if (id != informeEncabezadoDto.IdInforme)
        {
            return BadRequest("El ID del informe en la URL no coincide con el ID del cuerpo de la solicitud.");
        }

        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        try
        {
            await _informeEncabezadoAppService.UpdateInformeEncabezadoAsync(informeEncabezadoDto);
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
            await _informeEncabezadoAppService.DeleteInformeEncabezadoAsync(id);
            return NoContent();
        }
        catch (KeyNotFoundException)
        {
            return NotFound();
        }
    }
}
