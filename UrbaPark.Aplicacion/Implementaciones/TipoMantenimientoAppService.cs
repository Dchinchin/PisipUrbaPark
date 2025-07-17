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
            (!filter.IdTipoMantenimiento.HasValue || t.Id == filter.IdTipoMantenimiento.Value) &&
            (string.IsNullOrEmpty(filter.Nombre) || (t.Nombre != null && t.Nombre.Contains(filter.Nombre))) &&
            (string.IsNullOrEmpty(filter.Descripcion) || (t.Descripcion != null && t.Descripcion.Contains(filter.Descripcion)))
        );

        return tipos.Select(t => new TipoMantenimientoDto
        {
            Id = t.Id,
            Nombre = t.Nombre,
            Descripcion = t.Descripcion
        });
    }

    public async Task<TipoMantenimientoDto?> GetTipoMantenimientoByIdAsync(int id)
    {
        var tipo = await _tipoMantenimientoRepositorio.GetByIdAsync(id);
        if (tipo == null) return null;

        return new TipoMantenimientoDto
        {
            Id = tipo.Id,
            Nombre = tipo.Nombre,
            Descripcion = tipo.Descripcion
        };
    }

    public async Task<TipoMantenimientoDto> CreateTipoMantenimientoAsync(CreateTipoMantenimientoDto tipoMantenimientoDto)
    {
        var tipo = new TipoMantenimiento
        {
            Nombre = tipoMantenimientoDto.Nombre,
            Descripcion = tipoMantenimientoDto.Descripcion
        };

        await _tipoMantenimientoRepositorio.AddAsync(tipo);

        return new TipoMantenimientoDto
        {
            Id = tipo.Id,
            Nombre = tipo.Nombre,
            Descripcion = tipo.Descripcion
        };
    }

    public async Task<TipoMantenimientoDto> UpdateTipoMantenimientoAsync(int id, UpdateTipoMantenimientoDto tipoMantenimientoDto)
    {
        var tipo = await _tipoMantenimientoRepositorio.GetByIdAsync(id);
        if (tipo == null) throw new KeyNotFoundException("Tipo de Mantenimiento no encontrado.");

        tipo.Nombre = tipoMantenimientoDto.Nombre ?? tipo.Nombre;
        tipo.Descripcion = tipoMantenimientoDto.Descripcion ?? tipo.Descripcion;

        await _tipoMantenimientoRepositorio.UpdateAsync(tipo);

        return new TipoMantenimientoDto
        {
            Id = tipo.Id,
            Nombre = tipo.Nombre,
            Descripcion = tipo.Descripcion
        };
    }

    public async Task DeleteTipoMantenimientoAsync(int id)
    {
        await _tipoMantenimientoRepositorio.DeleteAsync(id);
    }
}
