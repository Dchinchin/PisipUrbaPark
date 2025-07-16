using UrbaPark.Aplicacion.Abstracciones;
using UrbaPark.Aplicacion.DTO;
using UrbaPark.Dominio.Modelo.Abstracciones;
using UrbaPark.Dominio.Modelo.Entidades;

namespace UrbaPark.Aplicacion.Implementaciones;

public class MantenimientoAppService : IMantenimientoAppService
{
    private readonly IMantenimientoRepositorio _mantenimientoRepositorio;

    public MantenimientoAppService(IMantenimientoRepositorio mantenimientoRepositorio)
    {
        _mantenimientoRepositorio = mantenimientoRepositorio;
    }

    public async Task<IEnumerable<MantenimientoDto>> GetAllMantenimientosAsync()
    {
        return await GetFilteredMantenimientosAsync(new MantenimientoFilterDto());
    }

    public async Task<IEnumerable<MantenimientoDto>> GetFilteredMantenimientosAsync(MantenimientoFilterDto filter)
    {
        var mantenimientos = await _mantenimientoRepositorio.GetAllAsync(m =>
            (!filter.IdMantenimiento.HasValue || m.IdMantenimiento == filter.IdMantenimiento.Value) &&
            (!filter.IdParqueadero.HasValue || m.IdParqueadero == filter.IdParqueadero.Value) &&
            (!filter.IdTipoMantenimiento.HasValue || m.IdTipomantenimiento == filter.IdTipoMantenimiento.Value) &&
            (!filter.FechaDesde.HasValue || m.FechaInicio >= filter.FechaDesde.Value) &&
            (!filter.FechaHasta.HasValue || m.FechaFin <= filter.FechaHasta.Value) &&
            (string.IsNullOrEmpty(filter.Descripcion) || (m.Observaciones != null && m.Observaciones.Contains(filter.Descripcion)))
        );

        return mantenimientos.Select(m => new MantenimientoDto
        {
            IdMantenimiento = m.IdMantenimiento,
            IdUsuario = m.IdUsuario,
            IdParqueadero = m.IdParqueadero,
            IdTipomantenimiento = m.IdTipomantenimiento,
            FechaInicio = m.FechaInicio,
            FechaCreacion = m.FechaCreacion,
            FechaFin = m.FechaFin,
            Observaciones = m.Observaciones
        });
    }

    public async Task<MantenimientoDto?> GetMantenimientoByIdAsync(int id)
    {
        var mantenimiento = await _mantenimientoRepositorio.GetByIdAsync(id);
        if (mantenimiento == null) return null;

        return new MantenimientoDto
        {
            IdMantenimiento = mantenimiento.IdMantenimiento,
            IdUsuario = mantenimiento.IdUsuario,
            IdParqueadero = mantenimiento.IdParqueadero,
            IdTipomantenimiento = mantenimiento.IdTipomantenimiento,
            FechaInicio = mantenimiento.FechaInicio,
            FechaCreacion = mantenimiento.FechaCreacion,
            FechaFin = mantenimiento.FechaFin,
            Observaciones = mantenimiento.Observaciones
        };
    }

    public async Task<MantenimientoDto> CreateMantenimientoAsync(CreateMantenimientoDto mantenimientoDto)
    {
        var mantenimiento = new Mantenimiento
        {
            IdUsuario = mantenimientoDto.IdUsuario,
            IdParqueadero = mantenimientoDto.IdParqueadero,
            IdTipomantenimiento = mantenimientoDto.IdTipomantenimiento,
            FechaInicio = mantenimientoDto.FechaInicio,
            FechaCreacion = mantenimientoDto.FechaCreacion,
            FechaFin = mantenimientoDto.FechaFin,
            Observaciones = mantenimientoDto.Observaciones
        };

        await _mantenimientoRepositorio.AddAsync(mantenimiento);

        return new MantenimientoDto
        {
            IdMantenimiento = mantenimiento.IdMantenimiento,
            IdUsuario = mantenimiento.IdUsuario,
            IdParqueadero = mantenimiento.IdParqueadero,
            IdTipomantenimiento = mantenimiento.IdTipomantenimiento,
            FechaInicio = mantenimiento.FechaInicio,
            FechaCreacion = mantenimiento.FechaCreacion,
            FechaFin = mantenimiento.FechaFin,
            Observaciones = mantenimiento.Observaciones
        };
    }

    public async Task UpdateMantenimientoAsync(UpdateMantenimientoDto mantenimientoDto)
    {
        var mantenimiento = await _mantenimientoRepositorio.GetByIdAsync(mantenimientoDto.IdMantenimiento);
        if (mantenimiento == null) throw new KeyNotFoundException("Mantenimiento no encontrado.");

        mantenimiento.IdUsuario = mantenimientoDto.IdUsuario ?? mantenimiento.IdUsuario;
        mantenimiento.IdParqueadero = mantenimientoDto.IdParqueadero ?? mantenimiento.IdParqueadero;
        mantenimiento.IdTipomantenimiento = mantenimientoDto.IdTipomantenimiento ?? mantenimiento.IdTipomantenimiento;
        mantenimiento.FechaInicio = mantenimientoDto.FechaInicio ?? mantenimiento.FechaInicio;
        mantenimiento.FechaCreacion = mantenimientoDto.FechaCreacion ?? mantenimiento.FechaCreacion;
        mantenimiento.FechaFin = mantenimientoDto.FechaFin ?? mantenimiento.FechaFin;
        mantenimiento.Observaciones = mantenimientoDto.Observaciones ?? mantenimiento.Observaciones;

        await _mantenimientoRepositorio.UpdateAsync(mantenimiento);
    }

    public async Task DeleteMantenimientoAsync(int id)
    {
        await _mantenimientoRepositorio.DeleteAsync(id);
    }
}
