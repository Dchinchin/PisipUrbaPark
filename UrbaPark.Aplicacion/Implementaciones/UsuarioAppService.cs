using UrbaPark.Aplicacion.Abstracciones;
using UrbaPark.Aplicacion.DTO;
using UrbaPark.Dominio.Modelo.Abstracciones;
using UrbaPark.Dominio.Modelo.Entidades;
using UrbaPark.Dominio.Servicio.Abstracciones;
using Microsoft.AspNetCore.Http;

namespace UrbaPark.Aplicacion.Implementaciones;

public class UsuarioAppService : IUsuarioAppService
{
    private readonly IUsuariosRepositorio _usuariosRepositorio;
    private readonly IHashService _hashService;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public UsuarioAppService(IUsuariosRepositorio usuariosRepositorio, IHashService hashService, IHttpContextAccessor httpContextAccessor)
    {
        _usuariosRepositorio = usuariosRepositorio;
        _hashService = hashService;
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task<IEnumerable<UsuarioDto>> GetAllUsuariosAsync()
    {
        return await GetFilteredUsuariosAsync(new UsuarioFilterDto());
    }

    public async Task<IEnumerable<UsuarioDto>> GetFilteredUsuariosAsync(UsuarioFilterDto filter)
    {
        var usuarios = await _usuariosRepositorio.GetAllAsync(u =>
            (!filter.IdUsuario.HasValue || u.IdUsuario == filter.IdUsuario.Value) &&
            (!filter.IdRol.HasValue || u.IdRol == filter.IdRol.Value) &&
            (string.IsNullOrEmpty(filter.NombreUsuario) || (u.Nombre.Contains(filter.NombreUsuario) || u.Apellido.Contains(filter.NombreUsuario))) &&
            (string.IsNullOrEmpty(filter.CorreoElectronico) || u.Correo.Contains(filter.CorreoElectronico)) &&
            (!filter.EstaEliminado.HasValue || filter.EstaEliminado.Value == u.EstaEliminado) &&
            !u.EstaEliminado
        );

        return usuarios.Select(u => new UsuarioDto
        {
            IdUsuario = u.IdUsuario,
            IdRol = u.IdRol,
            Nombre = u.Nombre,
            Apellido = u.Apellido,
            Correo = u.Correo,
            EstaEliminado = u.EstaEliminado,
            Cedula = u.Cedula,
            FechaCreacion = u.FechaCreacion,
            FechaModificacion = u.FechaModificacion,
            ContrasenaActualizada = u.ContrasenaActualizada
        });
    }

    public async Task<UsuarioDto?> GetUsuarioByIdAsync(int id)
    {
        var usuario = await _usuariosRepositorio.GetByIdAsync(id);
        if (usuario == null || usuario.EstaEliminado) return null;

        return new UsuarioDto
        {
            IdUsuario = usuario.IdUsuario,
            IdRol = usuario.IdRol,
            Nombre = usuario.Nombre,
            Apellido = usuario.Apellido,
            Correo = usuario.Correo,
            EstaEliminado = usuario.EstaEliminado,
            Cedula = usuario.Cedula,
            FechaCreacion = usuario.FechaCreacion,
            FechaModificacion = usuario.FechaModificacion,
            ContrasenaActualizada = usuario.ContrasenaActualizada
        };
    }

    public async Task<UsuarioDto> CreateUsuarioAsync(CreateUsuarioDto usuarioDto)
    {
        var usuario = new Usuarios
        {
            IdRol = usuarioDto.IdRol,
            Nombre = usuarioDto.Nombre,
            Apellido = usuarioDto.Apellido,
            Correo = usuarioDto.Correo,
            Cedula = usuarioDto.Cedula,
            Contrasena = usuarioDto.Contrasena,
            FechaCreacion = DateTime.Now,
            FechaModificacion = DateTime.Now,
            ContrasenaActualizada = false
        };

        await _usuariosRepositorio.AddAsync(usuario);

        return new UsuarioDto
        {
            IdUsuario = usuario.IdUsuario,
            IdRol = usuario.IdRol,
            Nombre = usuario.Nombre,
            Apellido = usuario.Apellido,
            Correo = usuario.Correo,
            EstaEliminado = usuario.EstaEliminado,
            Cedula = usuario.Cedula,
            FechaCreacion = usuario.FechaCreacion,
            FechaModificacion = usuario.FechaModificacion,
            ContrasenaActualizada = usuario.ContrasenaActualizada
        };
    }

    public async Task<UsuarioDto> UpdateUsuarioAsync(int id, UpdateUsuarioDto usuarioDto)
    {
        var usuario = await _usuariosRepositorio.GetByIdAsync(id);
        if (usuario == null) throw new KeyNotFoundException("Usuario no encontrado.");

        usuario.IdRol = usuarioDto.IdRol ?? usuario.IdRol;
        usuario.Nombre = usuarioDto.Nombre ?? usuario.Nombre;
        usuario.Apellido = usuarioDto.Apellido ?? usuario.Apellido;
        usuario.Correo = usuarioDto.Correo ?? usuario.Correo;
        usuario.EstaEliminado = usuarioDto.EstaEliminado ?? usuario.EstaEliminado;
        usuario.Cedula = usuarioDto.Cedula ?? usuario.Cedula;
        usuario.FechaModificacion = DateTime.Now;

        if (usuarioDto.ContrasenaActualizada.HasValue)
        {
            usuario.ContrasenaActualizada = usuarioDto.ContrasenaActualizada.Value;
        }

        if (!string.IsNullOrEmpty(usuarioDto.Contrasena))
        {
            usuario.Contrasena = usuarioDto.Contrasena; 
        }

        await _usuariosRepositorio.UpdateAsync(usuario);

        return new UsuarioDto
        {
            IdUsuario = usuario.IdUsuario,
            IdRol = usuario.IdRol,
            Nombre = usuario.Nombre,
            Apellido = usuario.Apellido,
            Correo = usuario.Correo,
            EstaEliminado = usuario.EstaEliminado,
            Cedula = usuario.Cedula,
            FechaCreacion = usuario.FechaCreacion,
            FechaModificacion = usuario.FechaModificacion,
            ContrasenaActualizada = usuario.ContrasenaActualizada
        };
    }

    public async Task DeleteUsuarioAsync(int id)
    {
        var usuario = await _usuariosRepositorio.GetByIdAsync(id);
        if (usuario == null) throw new KeyNotFoundException("Usuario no encontrado.");

        usuario.EstaEliminado = true;
        await _usuariosRepositorio.UpdateAsync(usuario);
    }

    public async Task<AuthenticationResponseDto> Authenticate(AuthenticateRequestDto request)
    {
        var user = await _usuariosRepositorio.GetByEmailAsync(request.Correo);
        if (user == null || user.EstaEliminado)
        {
            return new AuthenticationResponseDto { Autenticado = false, ContrasenaActualizada = false };
        }

        bool isAuthenticated = _hashService.VerifyPassword(request.Contrasena, user.Contrasena);

        return new AuthenticationResponseDto
        {
            Autenticado = isAuthenticated,
            ContrasenaActualizada = user.ContrasenaActualizada
        };
    }

    public int GetCurrentUserId()
    {
        var userIdClaim = _httpContextAccessor.HttpContext?.User?.Claims.FirstOrDefault(c => c.Type == "id");
        if (userIdClaim != null && int.TryParse(userIdClaim.Value, out int userId))
        {
            return userId;
        }
        return 0;
    }
}