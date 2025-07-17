
using UrbaPark.Aplicacion.Abstracciones;
using UrbaPark.Aplicacion.DTO;
using UrbaPark.Dominio.Modelo.Abstracciones;
using UrbaPark.Dominio.Modelo.Entidades;

namespace UrbaPark.Aplicacion.Implementaciones;

public class MantenimientoAppService : IMantenimientoAppService
{
    private readonly IMantenimientoRepositorio _mantenimientoRepositorio;
    private readonly IUsuarioAppService _usuarioAppService;

    public MantenimientoAppService(IMantenimientoRepositorio mantenimientoRepositorio,
        IUsuarioAppService usuarioAppService)
    {
        _mantenimientoRepositorio = mantenimientoRepositorio;
        _usuarioAppService = usuarioAppService;
    }

    public async Task<IEnumerable<MantenimientoDto>> GetAllMantenimientosAsync()
    {
        return await GetFilteredMantenimientosAsync(new MantenimientoFilterDto());
    }

    public async Task<IEnumerable<MantenimientoDto>> GetFilteredMantenimientosAsync(MantenimientoFilterDto filter)
    {
        var mantenimientos = await _mantenimientoRepositorio.GetAllAsync(
            m =>
                (!filter.IdMantenimiento.HasValue || m.IdMantenimiento == filter.IdMantenimiento.Value) &&
                (!filter.IdParqueadero.HasValue || m.IdParqueadero == filter.IdParqueadero.Value) &&
                (!filter.IdTipoMantenimiento.HasValue || m.IdTipoMantenimiento == filter.IdTipoMantenimiento.Value) &&
                (!filter.IdInforme.HasValue || m.IdInforme == filter.IdInforme.Value) &&
                (!filter.FechaDesde.HasValue || m.FechaInicio >= filter.FechaDesde.Value) &&
                (!filter.FechaHasta.HasValue || m.FechaFin <= filter.FechaHasta.Value) &&
                (string.IsNullOrEmpty(filter.Descripcion) ||
                 (m.Observaciones != null && m.Observaciones.Contains(filter.Descripcion))) &&
                (!filter.EstaEliminado.HasValue || m.EstaEliminado == filter.EstaEliminado.Value),
            m => m.Bitacoras
        );

        return mantenimientos.Select(m => new MantenimientoDto
        {
            IdMantenimiento = m.IdMantenimiento,
            IdUsuario = m.IdUsuario,
            IdParqueadero = m.IdParqueadero,
            IdTipoMantenimiento = m.IdTipoMantenimiento,
            IdInforme = m.IdInforme,
            FechaInicio = m.FechaInicio,
            FechaCreacion = m.FechaCreacion,
            FechaFin = m.FechaFin,
            Observaciones = m.Observaciones,
            Estado = m.Estado,
            EstaEliminado = m.EstaEliminado,
            FechaModificacion = m.FechaModificacion,
            Bitacoras = m.Bitacoras.Select(b => new BitacoraDto
            {
                IdBitacora = b.IdBitacora,
                IdMantenimiento = b.IdMantenimiento,
                FechaHora = b.FechaHora,
                Descripcion = b.Descripcion,
                ImagenUrl = b.ImagenUrl,
                EstaEliminado = b.EstaEliminado,
                FechaCreacion = b.FechaCreacion,
                FechaModificacion = b.FechaModificacion
            }).ToList()
        });
    }

    

    public async Task<MantenimientoDto?> GetMantenimientoByIdAsync(int id)
    {
        var mantenimiento = await _mantenimientoRepositorio.GetByIdAsync(id, m => m.Bitacoras);
        if (mantenimiento == null || mantenimiento.EstaEliminado == true) return null;

        return new MantenimientoDto
        {
            IdMantenimiento = mantenimiento.IdMantenimiento,
            IdUsuario = mantenimiento.IdUsuario,
            IdParqueadero = mantenimiento.IdParqueadero,
            IdTipoMantenimiento = mantenimiento.IdTipoMantenimiento,
            IdInforme = mantenimiento.IdInforme,
            FechaInicio = mantenimiento.FechaInicio,
            FechaCreacion = mantenimiento.FechaCreacion,
            FechaFin = mantenimiento.FechaFin,
            Observaciones = mantenimiento.Observaciones,
            Estado = mantenimiento.Estado,
            EstaEliminado = mantenimiento.EstaEliminado,
            FechaModificacion = mantenimiento.FechaModificacion,
            Bitacoras = mantenimiento.Bitacoras.Select(b => new BitacoraDto
            {
                IdBitacora = b.IdBitacora,
                IdMantenimiento = b.IdMantenimiento,
                FechaHora = b.FechaHora,
                Descripcion = b.Descripcion,
                ImagenUrl = b.ImagenUrl,
                EstaEliminado = b.EstaEliminado,
                FechaCreacion = b.FechaCreacion,
                FechaModificacion = b.FechaModificacion
            }).ToList()
        };
    }

    public async Task<MantenimientoDto> CreateMantenimientoAsync(CreateMantenimientoDto mantenimientoDto)
    {
        var mantenimiento = new Mantenimiento
        {
            
            IdUsuario = mantenimientoDto.IdUsuario,
            IdParqueadero = mantenimientoDto.IdParqueadero,
            IdTipoMantenimiento = mantenimientoDto.IdTipoMantenimiento,
            FechaInicio = mantenimientoDto.FechaInicio,
            FechaCreacion = DateTime.Now,
            FechaModificacion = DateTime.Now,
            FechaFin = mantenimientoDto.FechaFin,
            Observaciones = mantenimientoDto.Observaciones,
            EstaEliminado = false,
            Estado = "Pendiente"
        };

        await _mantenimientoRepositorio.AddAsync(mantenimiento);

        return new MantenimientoDto
        {
            IdMantenimiento = mantenimiento.IdMantenimiento,
            IdUsuario = mantenimiento.IdUsuario,
            IdParqueadero = mantenimiento.IdParqueadero,
            IdTipoMantenimiento = mantenimiento.IdTipoMantenimiento,
            IdInforme = mantenimiento.IdInforme,
            FechaInicio = mantenimiento.FechaInicio,
            FechaCreacion = mantenimiento.FechaCreacion,
            FechaFin = mantenimiento.FechaFin,
            Observaciones = mantenimiento.Observaciones,
            Estado = mantenimiento.Estado,
            EstaEliminado = mantenimiento.EstaEliminado,
            FechaModificacion = mantenimiento.FechaModificacion,
            Bitacoras = new List<BitacoraDto>()
        };
    }

    public async Task<MantenimientoDto> UpdateMantenimientoAsync(int id, UpdateMantenimientoDto mantenimientoDto)
    {
        var mantenimiento = await _mantenimientoRepositorio.GetByIdAsync(id, m => m.Bitacoras);
        if (mantenimiento == null) throw new KeyNotFoundException("Mantenimiento no encontrado.");

        
        mantenimiento.IdUsuario = mantenimientoDto.IdUsuario ?? mantenimiento.IdUsuario;
        mantenimiento.IdParqueadero = mantenimientoDto.IdParqueadero ?? mantenimiento.IdParqueadero;
        mantenimiento.IdTipoMantenimiento = mantenimientoDto.IdTipoMantenimiento ?? mantenimiento.IdTipoMantenimiento;
        mantenimiento.IdInforme = mantenimientoDto.IdInforme ?? mantenimiento.IdInforme;
        mantenimiento.FechaInicio = mantenimientoDto.FechaInicio ?? mantenimiento.FechaInicio;
        mantenimiento.FechaFin = mantenimientoDto.FechaFin ?? mantenimiento.FechaFin;
        mantenimiento.Observaciones = mantenimientoDto.Observaciones ?? mantenimiento.Observaciones;
        mantenimiento.FechaModificacion = DateTime.Now;

        if (!string.IsNullOrEmpty(mantenimientoDto.Estado))
        {
            if (mantenimientoDto.Estado != "Pendiente" && mantenimientoDto.Estado != "Terminado")
            {
                throw new ArgumentException("El estado solo puede ser 'Pendiente' o 'Terminado'.");
            }

            mantenimiento.Estado = mantenimientoDto.Estado;
        }

        await _mantenimientoRepositorio.UpdateAsync(mantenimiento);

        return new MantenimientoDto
        {
            IdMantenimiento = mantenimiento.IdMantenimiento,
            IdUsuario = mantenimiento.IdUsuario,
            IdParqueadero = mantenimiento.IdParqueadero,
            IdTipoMantenimiento = mantenimiento.IdTipoMantenimiento,
            IdInforme = mantenimiento.IdInforme,
            FechaInicio = mantenimiento.FechaInicio,
            FechaCreacion = mantenimiento.FechaCreacion,
            FechaFin = mantenimiento.FechaFin,
            Observaciones = mantenimiento.Observaciones,
            Estado = mantenimiento.Estado,
            EstaEliminado = mantenimiento.EstaEliminado,
            FechaModificacion = mantenimiento.FechaModificacion,
            Bitacoras = mantenimiento.Bitacoras.Select(b => new BitacoraDto
            {
                IdBitacora = b.IdBitacora,
                IdMantenimiento = b.IdMantenimiento,
                FechaHora = b.FechaHora,
                Descripcion = b.Descripcion,
                ImagenUrl = b.ImagenUrl,
                EstaEliminado = b.EstaEliminado,
                FechaCreacion = b.FechaCreacion,
                FechaModificacion = b.FechaModificacion
            }).ToList()
        };
    }

    public async Task DeleteMantenimientoAsync(int id)
    {
        var mantenimiento = await _mantenimientoRepositorio.GetByIdAsync(id);
        if (mantenimiento == null) throw new KeyNotFoundException("Mantenimiento no encontrado.");

        mantenimiento.EstaEliminado = true;
        await _mantenimientoRepositorio.UpdateAsync(mantenimiento);
    }
}