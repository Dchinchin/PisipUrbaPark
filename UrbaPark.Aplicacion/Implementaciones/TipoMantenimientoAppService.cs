using UrbaPark.Aplicacion.Abstracciones;
using UrbaPark.Aplicacion.DTO;
using UrbaPark.Dominio.Modelo.Abstracciones;
using UrbaPark.Dominio.Modelo.Entidades;

namespace UrbaPark.Aplicacion.Implementaciones;

public class TipoMantenimientoAppService : ITipoMantenimientoAppService
{
    private readonly ITipoMantenimientoRepositorio _tipoMantenimientoRepositorio;

    public TipoMantenimientoAppService(ITipoMantenimientoRepositorio tipoMantenimientoRepositorio)
    {
        _tipoMantenimientoRepositorio = tipoMantenimientoRepositorio;
    }

    public async Task<IEnumerable<TipoMantenimientoDto>> GetAllTipoMantenimientosAsync()
    {
        return await GetFilteredTipoMantenimientosAsync(new TipoMantenimientoFilterDto());
    }

    public async Task<IEnumerable<TipoMantenimientoDto>> GetFilteredTipoMantenimientosAsync(TipoMantenimientoFilterDto filter)
    {
        var tipos = await _tipoMantenimientoRepositorio.GetAllAsync(t =>
            (!filter.IdTipoMantenimiento.HasValue || t.IdTipo == filter.IdTipoMantenimiento.Value) &&
            (string.IsNullOrEmpty(filter.Nombre) || (t.Nombre != null && t.Nombre.Contains(filter.Nombre))) &&
            (string.IsNullOrEmpty(filter.Descripcion) || (t.Descripcion != null && t.Descripcion.Contains(filter.Descripcion))) &&
            (!filter.EstaEliminado.HasValue || t.EstaEliminado == filter.EstaEliminado.Value)
        );

        return tipos.Select(t => new TipoMantenimientoDto
        {
            IdTipo = t.IdTipo,
            Nombre = t.Nombre,
            Descripcion = t.Descripcion,
            FechaCreacion = t.FechaCreacion,
            FechaModificacion = t.FechaModificacion,
            Estado = !t.EstaEliminado
        });
    }

    public async Task<TipoMantenimientoDto?> GetTipoMantenimientoByIdAsync(int id)
    {
        var tipo = await _tipoMantenimientoRepositorio.GetByIdAsync(id);
        if (tipo == null || tipo.EstaEliminado) return null;

        return new TipoMantenimientoDto
        {
            IdTipo = tipo.IdTipo,
            Nombre = tipo.Nombre,
            Descripcion = tipo.Descripcion,
            FechaCreacion = tipo.FechaCreacion,
            FechaModificacion = tipo.FechaModificacion
        };
    }

    public async Task<TipoMantenimientoDto> CreateTipoMantenimientoAsync(CreateTipoMantenimientoDto tipoMantenimientoDto)
    {
        var tipo = new TipoMantenimiento
        {
            Nombre = tipoMantenimientoDto.Nombre,
            Descripcion = tipoMantenimientoDto.Descripcion,
            FechaCreacion = DateTime.Now,
            FechaModificacion = DateTime.Now
        };

        await _tipoMantenimientoRepositorio.AddAsync(tipo);

        return new TipoMantenimientoDto
        {
            IdTipo = tipo.IdTipo,
            Nombre = tipo.Nombre,
            Descripcion = tipo.Descripcion,
            FechaCreacion = tipo.FechaCreacion,
            FechaModificacion = tipo.FechaModificacion
        };
    }

    public async Task<TipoMantenimientoDto> UpdateTipoMantenimientoAsync(int id, UpdateTipoMantenimientoDto tipoMantenimientoDto)
    {
        var tipo = await _tipoMantenimientoRepositorio.GetByIdAsync(id);
        if (tipo == null) throw new KeyNotFoundException("Tipo de Mantenimiento no encontrado.");

        tipo.Nombre = tipoMantenimientoDto.Nombre ?? tipo.Nombre;
        tipo.Descripcion = tipoMantenimientoDto.Descripcion ?? tipo.Descripcion;
        tipo.FechaModificacion = DateTime.Now;

        await _tipoMantenimientoRepositorio.UpdateAsync(tipo);

        return new TipoMantenimientoDto
        {
            IdTipo = tipo.IdTipo,
            Nombre = tipo.Nombre,
            Descripcion = tipo.Descripcion,
            FechaCreacion = tipo.FechaCreacion,
            FechaModificacion = tipo.FechaModificacion
        };
    }

    public async Task DeleteTipoMantenimientoAsync(int id)
    {
        var tipo = await _tipoMantenimientoRepositorio.GetByIdAsync(id);
        if (tipo == null) throw new KeyNotFoundException("Tipo de Mantenimiento no encontrado.");

        tipo.EstaEliminado = true;
        await _tipoMantenimientoRepositorio.UpdateAsync(tipo);
    }
}
