using UrbaPark.Aplicacion.Abstracciones;
using UrbaPark.Aplicacion.DTO;
using UrbaPark.Dominio.Modelo.Abstracciones;
using UrbaPark.Dominio.Modelo.Entidades;
using UrbaPark.Dominio.Servicio.Abstracciones;

namespace UrbaPark.Aplicacion.Implementaciones;

public class UsuarioAppService : IUsuarioAppService
{
    private readonly IUsuariosRepositorio _usuariosRepositorio;
    private readonly IHashService _hashService;

    public UsuarioAppService(IUsuariosRepositorio usuariosRepositorio, IHashService hashService)
    {
        _usuariosRepositorio = usuariosRepositorio;
        _hashService = hashService;
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
            (string.IsNullOrEmpty(filter.Estado) || u.Estado.Contains(filter.Estado))
        );

        return usuarios.Select(u => new UsuarioDto
        {
            IdUsuario = u.IdUsuario,
            IdRol = u.IdRol,
            Nombre = u.Nombre,
            Apellido = u.Apellido,
            Correo = u.Correo,
            Estado = u.Estado,
            Cedula = u.Cedula
        });
    }

    public async Task<UsuarioDto?> GetUsuarioByIdAsync(int id)
    {
        var usuario = await _usuariosRepositorio.GetByIdAsync(id);
        if (usuario == null) return null;

        return new UsuarioDto
        {
            IdUsuario = usuario.IdUsuario,
            IdRol = usuario.IdRol,
            Nombre = usuario.Nombre,
            Apellido = usuario.Apellido,
            Correo = usuario.Correo,
            Estado = usuario.Estado,
            Cedula = usuario.Cedula
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
            Estado = usuarioDto.Estado,
            Cedula = usuarioDto.Cedula,
            Contrasena = usuarioDto.Contrasena // The repository will hash this
        };

        await _usuariosRepositorio.AddAsync(usuario);

        return new UsuarioDto
        {
            IdUsuario = usuario.IdUsuario,
            IdRol = usuario.IdRol,
            Nombre = usuario.Nombre,
            Apellido = usuario.Apellido,
            Correo = usuario.Correo,
            Estado = usuario.Estado,
            Cedula = usuario.Cedula
        };
    }

    public async Task UpdateUsuarioAsync(UpdateUsuarioDto usuarioDto)
    {
        var usuario = await _usuariosRepositorio.GetByIdAsync(usuarioDto.IdUsuario);
        if (usuario == null) throw new KeyNotFoundException("Usuario no encontrado.");

        usuario.IdRol = usuarioDto.IdRol ?? usuario.IdRol;
        usuario.Nombre = usuarioDto.Nombre ?? usuario.Nombre;
        usuario.Apellido = usuarioDto.Apellido ?? usuario.Apellido;
        usuario.Correo = usuarioDto.Correo ?? usuario.Correo;
        usuario.Estado = usuarioDto.Estado ?? usuario.Estado;
        usuario.Cedula = usuarioDto.Cedula ?? usuario.Cedula;

        if (!string.IsNullOrEmpty(usuarioDto.Contrasena))
        {
            usuario.Contrasena = usuarioDto.Contrasena; // The repository will hash this
        }

        await _usuariosRepositorio.UpdateAsync(usuario);
    }

    public async Task DeleteUsuarioAsync(int id)
    {
        await _usuariosRepositorio.DeleteAsync(id);
    }

    public async Task<bool> Authenticate(AuthenticateRequestDto request)
    {
        var user = await _usuariosRepositorio.GetByEmailAsync(request.Correo);
        return user != null && _hashService.VerifyPassword(request.Contrasena, user.Contrasena);
    }
}
