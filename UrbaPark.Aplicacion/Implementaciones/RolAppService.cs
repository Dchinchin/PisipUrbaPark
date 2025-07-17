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
            (!filter.IdRol.HasValue || r.IdRol == filter.IdRol.Value) &&
            (string.IsNullOrEmpty(filter.NombreRol) || r.NombreRol.Contains(filter.NombreRol)) &&
            (!filter.EstaEliminado.HasValue || r.EstaEliminado == filter.EstaEliminado.Value)
        );

        return roles.Select(r => new RolDto
        {
            IdRol = r.IdRol,
            NombreRol = r.NombreRol,
            Descripcion = r.Descripcion,
            FechaCreacion = r.FechaCreacion,
            FechaModificacion = r.FechaModificacion
        });
    }

    public async Task<RolDto?> GetRolByIdAsync(int id)
    {
        var rol = await _rolesRepositorio.GetByIdAsync(id);
        if (rol == null || rol.EstaEliminado) return null;

        return new RolDto
        {
            IdRol = rol.IdRol,
            NombreRol = rol.NombreRol,
            Descripcion = rol.Descripcion,
            FechaCreacion = rol.FechaCreacion,
            FechaModificacion = rol.FechaModificacion
        };
    }

    public async Task<RolDto> CreateRolAsync(CreateRolDto rolDto)
    {
        var rol = new Roles
        {
            NombreRol = rolDto.NombreRol,
            Descripcion = rolDto.Descripcion,
            FechaCreacion = DateTime.Now,
            FechaModificacion = DateTime.Now
        };

        await _rolesRepositorio.AddAsync(rol);

        return new RolDto
        {
            IdRol = rol.IdRol,
            NombreRol = rol.NombreRol,
            Descripcion = rol.Descripcion,
            FechaCreacion = rol.FechaCreacion,
            FechaModificacion = rol.FechaModificacion
        };
    }

    public async Task<RolDto> UpdateRolAsync(int id, UpdateRolDto rolDto)
    {
        var rol = await _rolesRepositorio.GetByIdAsync(id);
        if (rol == null) throw new KeyNotFoundException("Rol no encontrado.");

        rol.NombreRol = rolDto.NombreRol ?? rol.NombreRol;
        rol.Descripcion = rolDto.Descripcion ?? rol.Descripcion;
        rol.FechaModificacion = DateTime.Now;

        await _rolesRepositorio.UpdateAsync(rol);

        return new RolDto
        {
            IdRol = rol.IdRol,
            NombreRol = rol.NombreRol,
            Descripcion = rol.Descripcion,
            FechaCreacion = rol.FechaCreacion,
            FechaModificacion = rol.FechaModificacion
        };
    }

    public async Task DeleteRolAsync(int id)
    {
        var rol = await _rolesRepositorio.GetByIdAsync(id);
        if (rol == null) throw new KeyNotFoundException("Rol no encontrado.");

        rol.EstaEliminado = true;
        await _rolesRepositorio.UpdateAsync(rol);
    }
}
