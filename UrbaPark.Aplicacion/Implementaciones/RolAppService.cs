using UrbaPark.Aplicacion.Abstracciones;
using UrbaPark.Aplicacion.DTO;
using UrbaPark.Dominio.Modelo.Abstracciones;
using UrbaPark.Dominio.Modelo.Entidades;

namespace UrbaPark.Aplicacion.Implementaciones;

public class RolAppService : IRolAppService
{
    private readonly IRolesRepositorio _rolesRepositorio;

    public RolAppService(IRolesRepositorio rolesRepositorio)
    {
        _rolesRepositorio = rolesRepositorio;
    }

    public async Task<IEnumerable<RolDto>> GetAllRolesAsync()
    {
        return await GetFilteredRolesAsync(new RolFilterDto());
    }

    public async Task<IEnumerable<RolDto>> GetFilteredRolesAsync(RolFilterDto filter)
    {
        var roles = await _rolesRepositorio.GetAllAsync(r =>
            (!filter.IdRol.HasValue || r.id_rol == filter.IdRol.Value) &&
            (string.IsNullOrEmpty(filter.NombreRol) || r.nombre_rol.Contains(filter.NombreRol))
        );

        return roles.Select(r => new RolDto
        {
            IdRol = r.id_rol,
            NombreRol = r.nombre_rol,
            Descripcion = r.descripcion
        });
    }

    public async Task<RolDto?> GetRolByIdAsync(int id)
    {
        var rol = await _rolesRepositorio.GetByIdAsync(id);
        if (rol == null) return null;

        return new RolDto
        {
            IdRol = rol.id_rol,
            NombreRol = rol.nombre_rol,
            Descripcion = rol.descripcion
        };
    }

    public async Task<RolDto> CreateRolAsync(CreateRolDto rolDto)
    {
        var rol = new Roles
        {
            nombre_rol = rolDto.NombreRol,
            descripcion = rolDto.Descripcion
        };

        await _rolesRepositorio.AddAsync(rol);

        return new RolDto
        {
            IdRol = rol.id_rol,
            NombreRol = rol.nombre_rol,
            Descripcion = rol.descripcion
        };
    }

    public async Task UpdateRolAsync(UpdateRolDto rolDto)
    {
        var rol = await _rolesRepositorio.GetByIdAsync(rolDto.IdRol);
        if (rol == null) throw new KeyNotFoundException("Rol no encontrado.");

        rol.nombre_rol = rolDto.NombreRol ?? rol.nombre_rol;
        rol.descripcion = rolDto.Descripcion ?? rol.descripcion;

        await _rolesRepositorio.UpdateAsync(rol);
    }

    public async Task DeleteRolAsync(int id)
    {
        await _rolesRepositorio.DeleteAsync(id);
    }
}
