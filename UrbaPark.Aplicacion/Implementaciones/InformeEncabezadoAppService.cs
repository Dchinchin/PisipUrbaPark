using Microsoft.AspNetCore.Http;
using UrbaPark.Aplicacion.Abstracciones;
using UrbaPark.Aplicacion.DTO;
using UrbaPark.Dominio.Modelo.Abstracciones;
using UrbaPark.Dominio.Modelo.Entidades;


namespace UrbaPark.Aplicacion.Implementaciones;

public class InformeEncabezadoAppService : IInformeEncabezadoAppService
{
    private readonly IInfo_EncaRepositorio _informeEncabezadoRepositorio;
    private readonly IMantenimientoRepositorio _mantenimientoRepositorio;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public InformeEncabezadoAppService(IInfo_EncaRepositorio informeEncabezadoRepositorio, IMantenimientoRepositorio mantenimientoRepositorio)
    {
        _informeEncabezadoRepositorio = informeEncabezadoRepositorio;
        
        _mantenimientoRepositorio = mantenimientoRepositorio;
    }

    

    public async Task<IEnumerable<InformeEncabezadoDto>> GetAllInformesEncabezadoAsync()
    {
        return await GetFilteredInformesEncabezadoAsync(new InformeEncabezadoFilterDto());
    }

    public async Task<IEnumerable<InformeEncabezadoDto>> GetFilteredInformesEncabezadoAsync(InformeEncabezadoFilterDto filter)
    {
        var informes = await _informeEncabezadoRepositorio.GetAllWithDetailsAsync(i =>
            (!filter.IdInforme.HasValue || i.IdInforme == filter.IdInforme.Value) &&
            (!filter.IdUsuario.HasValue || i.IdUsuario == filter.IdUsuario.Value) &&
            (!filter.FechaDesde.HasValue || i.FechaCreacion >= filter.FechaDesde.Value) &&
            (!filter.FechaHasta.HasValue || i.FechaCreacion <= filter.FechaHasta.Value) &&
            (!filter.EstaEliminado.HasValue || i.EstaEliminado == filter.EstaEliminado.Value)
        );

        var informeDtos = new List<InformeEncabezadoDto>();
        foreach (var i in informes)
        {
            informeDtos.Add(new InformeEncabezadoDto
            {
                IdInforme = i.IdInforme,
                IdUsuario = i.IdUsuario,
                Titulo = i.Titulo,
                FechaCreacion = i.FechaCreacion,
                FechaModificacion = i.FechaModificacion,
                Estado = i.EstaEliminado ? "Inactivo" : "Activo",
                Mantenimientos = i.Mantenimientos?.Select(m => new MantenimientoDto
                {
                    IdMantenimiento = m.IdMantenimiento,
                    IdUsuario = m.IdUsuario,
                    IdParqueadero = m.IdParqueadero,
                    IdTipoMantenimiento = m.IdTipoMantenimiento
                }).ToList(),
                Detalles = i.Detalle_Informe?.Select(d => new DetalleInformeDto
                {
                    IdDetInfo = d.IdDetInfo,
                    IdInforme = d.IdInforme,
                    Descripcion = d.Descripcion,
                    ArchivoUrl = GetAbsoluteUrl(d.ArchivoUrl),
                    FechaCreacion = d.FechaCreacion,
                    FechaModificacion = d.FechaModificacion,
                    Estado = d.EstaEliminado ? "Inactivo" : "Activo"
                }).ToList(),
            });
        }
        return informeDtos;
    }

    public async Task<InformeEncabezadoDto?> GetInformeEncabezadoByIdAsync(int id)
    {
        var informe = await _informeEncabezadoRepositorio.GetByIdWithDetailsAsync(id);
        if (informe == null || informe.EstaEliminado) return null;

        return new InformeEncabezadoDto
        {
            IdInforme = informe.IdInforme,
            IdUsuario = informe.IdUsuario,
            Titulo = informe.Titulo,
            FechaCreacion = informe.FechaCreacion,
            FechaModificacion = informe.FechaModificacion,
            Estado = informe.EstaEliminado ? "Inactivo" : "Activo",
            Mantenimientos = informe.Mantenimientos?.Select(m => new MantenimientoDto
            {
                IdMantenimiento = m.IdMantenimiento,
                IdUsuario = m.IdUsuario,
                IdParqueadero = m.IdParqueadero,
                IdTipoMantenimiento = m.IdTipoMantenimiento
            }).ToList(),
            Detalles = informe.Detalle_Informe?.Select(d => new DetalleInformeDto
            {
                IdDetInfo = d.IdDetInfo,
                IdInforme = d.IdInforme,
                Descripcion = d.Descripcion,
                ArchivoUrl = GetAbsoluteUrl(d.ArchivoUrl),
                FechaCreacion = d.FechaCreacion,
                FechaModificacion = d.FechaModificacion
            }).ToList(),
        };
    }

    public async Task<InformeEncabezadoDto> CreateInformeEncabezadoAsync(CreateInformeEncabezadoDto informeEncabezadoDto)
    {
        var informe = new Informes_Encabezado
        {
            IdUsuario = informeEncabezadoDto.IdUsuario,
            Titulo = informeEncabezadoDto.Titulo,
            FechaCreacion = DateTime.Now,
            FechaModificacion = DateTime.Now
        };

        await _informeEncabezadoRepositorio.AddAsync(informe);

        return new InformeEncabezadoDto
        {
            IdInforme = informe.IdInforme,
            IdUsuario = informe.IdUsuario,
            Titulo = informe.Titulo,
            FechaCreacion = informe.FechaCreacion,
            FechaModificacion = informe.FechaModificacion
        };
    }

    public async Task<InformeEncabezadoDto> UpdateInformeEncabezadoAsync(int id, UpdateInformeEncabezadoDto informeEncabezadoDto)
    {
        var informe = await _informeEncabezadoRepositorio.GetByIdAsync(id);
        if (informe == null) throw new KeyNotFoundException("Informe de Encabezado no encontrado.");

        informe.Titulo = informeEncabezadoDto.Titulo ?? informe.Titulo;
        informe.FechaModificacion = DateTime.Now;

        await _informeEncabezadoRepositorio.UpdateAsync(informe);

        return new InformeEncabezadoDto
        {
            IdInforme = informe.IdInforme,
            IdUsuario = informe.IdUsuario,
            Titulo = informe.Titulo,
            FechaCreacion = informe.FechaCreacion,
            FechaModificacion = informe.FechaModificacion
        };
    }

    public async Task DeleteInformeEncabezadoAsync(int id)
    {
        var informe = await _informeEncabezadoRepositorio.GetByIdAsync(id);
        if (informe == null) throw new KeyNotFoundException("Informe de Encabezado no encontrado.");

        informe.EstaEliminado = true;
        await _informeEncabezadoRepositorio.UpdateAsync(informe);
    }
    
    private string? GetAbsoluteUrl(string? relativeUrl)
    {
        if (string.IsNullOrEmpty(relativeUrl)) return null;
        var request = _httpContextAccessor.HttpContext?.Request;
        if (request == null) return relativeUrl; // Fallback if HttpContext is not available
        return $"{request.Scheme}://{request.Host}{relativeUrl}";
    }
}