using Microsoft.AspNetCore.Mvc;
using UrbaPark.Aplicacion.Abstracciones;
using UrbaPark.Aplicacion.DTO;

namespace UrbaParkAPIWeb.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UsuariosController : ControllerBase
{
    private readonly IUsuarioAppService _usuarioAppService;

    public UsuariosController(IUsuarioAppService usuarioAppService)
    {
        _usuarioAppService = usuarioAppService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<UsuarioDto>>> Get([FromQuery] UsuarioFilterDto filter)
    {
        var usuarios = await _usuarioAppService.GetFilteredUsuariosAsync(filter);
        return Ok(usuarios);
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<UsuarioDto>> Get(int id)
    {
        var usuario = await _usuarioAppService.GetUsuarioByIdAsync(id);
        if (usuario == null)
        {
            return NotFound();
        }
        return Ok(usuario);
    }

    [HttpPost]
    public async Task<ActionResult<UsuarioDto>> Post([FromBody] CreateUsuarioDto usuarioDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        var createdUsuario = await _usuarioAppService.CreateUsuarioAsync(usuarioDto);
        return CreatedAtAction(nameof(Get), new { id = createdUsuario.IdUsuario }, createdUsuario);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Put(int id, [FromBody] UpdateUsuarioDto usuarioDto)
    {

        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        try
        {
            var usuario = await _usuarioAppService.UpdateUsuarioAsync(id, usuarioDto);
            return Ok(usuario);
        }
        catch (KeyNotFoundException)
        {
            return NotFound();
        }
    }

    [HttpPost("autenticar")]
    public async Task<IActionResult> Authenticate([FromBody] AuthenticateRequestDto request)
    {
        var response = await _usuarioAppService.Authenticate(request);
        if (response.Autenticado)
        {
            return Ok(response);
        }
        return Unauthorized(response);
    }

    

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        try
        {
            await _usuarioAppService.DeleteUsuarioAsync(id);
            return Ok(true);
        }
        catch (KeyNotFoundException)
        {
            return NotFound();
        }
    }
}
