using Microsoft.AspNetCore.Http;
using UrbaPark.Aplicacion.Abstracciones;
using UrbaPark.Aplicacion.DTO;
using UrbaPark.Dominio.Modelo.Abstracciones;
using UrbaPark.Dominio.Modelo.Entidades;

namespace UrbaPark.Aplicacion.Implementaciones;

public class MantenimientoAppService : IMantenimientoAppService
{
    private readonly IMantenimientoRepositorio _mantenimientoRepositorio;
    private readonly IUsuarioAppService _usuarioAppService;
    private readonly IHttpContextAccessor _httpContextAccessor;

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
                (!filter.IdTipoMantenimiento.HasValue || m.IdTipomantenimiento == filter.IdTipoMantenimiento.Value) &&
                (!filter.FechaDesde.HasValue || m.FechaInicio >= filter.FechaDesde.Value) &&
                (!filter.FechaHasta.HasValue || m.FechaFin <= filter.FechaHasta.Value) &&
                (string.IsNullOrEmpty(filter.Descripcion) ||
                 (m.Observaciones != null && m.Observaciones.Contains(filter.Descripcion))),
            m => m.Informes_Encabezados
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
            Observaciones = m.Observaciones,
            InformesEncabezado = m.Informes_Encabezados?.Select(ie => new InformeEncabezadoDto
            {
                IdInforme = ie.IdInforme,
                IdMantenimiento = ie.IdMantenimiento,
                Fecha = ie.Fecha,
                Detalles = ie.Detalle_Informe?.Select(di => new DetalleInformeDto
                {
                    IdDetInfo = di.id_detInfo,
                    IdInforme = di.id_informe,
                    Descripcion = di.descripcion,
                    ArchivoUrl = GetAbsoluteUrl(di.archivo_url)
                }).ToList(),
                Bitacoras = ie.Bitacoras?.Select(b => new BitacoraDto
                {
                    IdBitacora = b.IdBitacora,
                    IdInforme = b.IdInforme,
                    IdMantenimiento = b.IdMantenimiento,
                    FechaHora = b.FechaHora,
                    Descripcion = b.Descripcion,
                    ImagenUrl = GetAbsoluteUrl(b.ImagenUrl)
                }).ToList()
            }).ToList()
        });
    }

    private string? GetAbsoluteUrl(string? relativeUrl)
    {
        if (string.IsNullOrEmpty(relativeUrl)) return null;
        var request = _httpContextAccessor.HttpContext?.Request;
        if (request == null) return relativeUrl; // Fallback if HttpContext is not available
        return $"{request.Scheme}://{request.Host}{relativeUrl}";
    }

    public async Task<MantenimientoDto?> GetMantenimientoByIdAsync(int id)
    {
        var mantenimiento = await _mantenimientoRepositorio.GetByIdAsync(id, m => m.Informes_Encabezados);
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
            Observaciones = mantenimiento.Observaciones,
            InformesEncabezado = mantenimiento.Informes_Encabezados?.Select(ie => new InformeEncabezadoDto
            {
                IdInforme = ie.IdInforme,
                IdMantenimiento = ie.IdMantenimiento,
                Fecha = ie.Fecha,
                Detalles = ie.Detalle_Informe?.Select(di => new DetalleInformeDto
                {
                    IdDetInfo = di.id_detInfo,
                    IdInforme = di.id_informe,
                    Descripcion = di.descripcion,
                    ArchivoUrl = GetAbsoluteUrl(di.archivo_url)
                }).ToList(),
                Bitacoras = ie.Bitacoras?.Select(b => new BitacoraDto
                {
                    IdBitacora = b.IdBitacora,
                    IdInforme = b.IdInforme,
                    IdMantenimiento = b.IdMantenimiento,
                    FechaHora = b.FechaHora,
                    Descripcion = b.Descripcion,
                    ImagenUrl = GetAbsoluteUrl(b.ImagenUrl)
                }).ToList()
            }).ToList()
        };
    }

    public async Task<MantenimientoDto> CreateMantenimientoAsync(CreateMantenimientoDto mantenimientoDto)
    {
        if (mantenimientoDto.IdUsuario != 0) // Assuming 0 means no user assigned, or adjust based on actual DTO
        {
            var usuario = await _usuarioAppService.GetUsuarioByIdAsync(mantenimientoDto.IdUsuario);
            if (usuario == null || usuario.Estado == "Inactivo")
            {
                throw new InvalidOperationException("El usuario asignado no existe o no está activo.");
            }
        }

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

    public async Task<MantenimientoDto> UpdateMantenimientoAsync(int id, UpdateMantenimientoDto mantenimientoDto)
    {
        var mantenimiento = await _mantenimientoRepositorio.GetByIdAsync(id);
        if (mantenimiento == null) throw new KeyNotFoundException("Mantenimiento no encontrado.");

        if (mantenimientoDto.IdUsuario.HasValue)
        {
            var usuario = await _usuarioAppService.GetUsuarioByIdAsync(mantenimientoDto.IdUsuario.Value);
            if (usuario == null || usuario.Estado == "Inactivo")
            {
                throw new InvalidOperationException("El usuario asignado no existe o no está activo.");
            }
        }

        mantenimiento.IdUsuario = mantenimientoDto.IdUsuario ?? mantenimiento.IdUsuario;
        mantenimiento.IdParqueadero = mantenimientoDto.IdParqueadero ?? mantenimiento.IdParqueadero;
        mantenimiento.IdTipomantenimiento = mantenimientoDto.IdTipomantenimiento ?? mantenimiento.IdTipomantenimiento;
        mantenimiento.FechaInicio = mantenimientoDto.FechaInicio ?? mantenimiento.FechaInicio;
        mantenimiento.FechaCreacion = mantenimientoDto.FechaCreacion ?? mantenimiento.FechaCreacion;
        mantenimiento.FechaFin = mantenimientoDto.FechaFin ?? mantenimiento.FechaFin;
        mantenimiento.Observaciones = mantenimientoDto.Observaciones ?? mantenimiento.Observaciones;

        await _mantenimientoRepositorio.UpdateAsync(mantenimiento);

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

    public async Task DeleteMantenimientoAsync(int id)
    {
        await _mantenimientoRepositorio.DeleteAsync(id);
    }
}