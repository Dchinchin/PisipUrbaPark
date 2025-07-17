using UrbaPark.Aplicacion.Abstracciones;
using UrbaPark.Aplicacion.DTO;
using UrbaPark.Dominio.Modelo.Abstracciones;
using UrbaPark.Dominio.Modelo.Entidades;
using Microsoft.AspNetCore.Http;

namespace UrbaPark.Aplicacion.Implementaciones;

public class InformeEncabezadoAppService : IInformeEncabezadoAppService
{
    private readonly IInfo_EncaRepositorio _informeEncabezadoRepositorio;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public InformeEncabezadoAppService(IInfo_EncaRepositorio informeEncabezadoRepositorio, IHttpContextAccessor httpContextAccessor)
    {
        _informeEncabezadoRepositorio = informeEncabezadoRepositorio;
        _httpContextAccessor = httpContextAccessor;
    }

    private string? GetAbsoluteUrl(string? relativeUrl)
    {
        if (string.IsNullOrEmpty(relativeUrl)) return null;
        var request = _httpContextAccessor.HttpContext?.Request;
        if (request == null) return relativeUrl; // Fallback if HttpContext is not available
        return $"{request.Scheme}://{request.Host}{relativeUrl}";
    }

    public async Task<IEnumerable<InformeEncabezadoDto>> GetAllInformesEncabezadoAsync()
    {
        return await GetFilteredInformesEncabezadoAsync(new InformeEncabezadoFilterDto());
    }

    public async Task<IEnumerable<InformeEncabezadoDto>> GetFilteredInformesEncabezadoAsync(InformeEncabezadoFilterDto filter)
    {
        var informes = await _informeEncabezadoRepositorio.GetAllWithDetailsAsync(i =>
            (!filter.IdInforme.HasValue || i.IdInforme == filter.IdInforme.Value) &&
            (!filter.IdUsuario.HasValue || i.IdMantenimiento == filter.IdUsuario.Value) && // Assuming IdUsuario in filter maps to IdMantenimiento in entity for filtering purposes, as IdUsuario is not directly in Informes_Encabezado
            (!filter.FechaDesde.HasValue || i.Fecha >= filter.FechaDesde.Value) &&
            (!filter.FechaHasta.HasValue || i.Fecha <= filter.FechaHasta.Value)
        );

        return informes.Select(i => new InformeEncabezadoDto
        {
            IdInforme = i.IdInforme,
            IdMantenimiento = i.IdMantenimiento,
            Fecha = i.Fecha,
            Detalles = i.Detalle_Informe?.Select(d => new DetalleInformeDto
            {
                IdDetInfo = d.id_detInfo,
                IdInforme = d.id_informe,
                Descripcion = d.descripcion,
                ArchivoUrl = GetAbsoluteUrl(d.archivo_url)
            }).ToList(),
            Bitacoras = i.Bitacoras?.Select(b => new BitacoraDto
            {
                IdBitacora = b.IdBitacora,
                IdInforme = b.IdInforme,
                IdMantenimiento = b.IdMantenimiento,
                FechaHora = b.FechaHora,
                Descripcion = b.Descripcion,
                ImagenUrl = GetAbsoluteUrl(b.ImagenUrl)
            }).ToList()
        });
    }

    public async Task<InformeEncabezadoDto?> GetInformeEncabezadoByIdAsync(int id)
    {
        var informe = await _informeEncabezadoRepositorio.GetByIdWithDetailsAsync(id);
        if (informe == null) return null;

        return new InformeEncabezadoDto
        {
            IdInforme = informe.IdInforme,
            IdMantenimiento = informe.IdMantenimiento,
            Fecha = informe.Fecha,
            Detalles = informe.Detalle_Informe?.Select(d => new DetalleInformeDto
            {
                IdDetInfo = d.id_detInfo,
                IdInforme = d.id_informe,
                Descripcion = d.descripcion,
                ArchivoUrl = GetAbsoluteUrl(d.archivo_url)
            }).ToList()
        };
    }

    public async Task<InformeEncabezadoDto> CreateInformeEncabezadoAsync(CreateInformeEncabezadoDto informeEncabezadoDto)
    {
        var informe = new Informes_Encabezado
        {
            IdMantenimiento = informeEncabezadoDto.IdMantenimiento,
            Fecha = informeEncabezadoDto.Fecha
        };

        await _informeEncabezadoRepositorio.AddAsync(informe);

        return new InformeEncabezadoDto
        {
            IdInforme = informe.IdInforme,
            IdMantenimiento = informe.IdMantenimiento,
            Fecha = informe.Fecha
        };
    }

    public async Task<InformeEncabezadoDto> UpdateInformeEncabezadoAsync(int id, UpdateInformeEncabezadoDto informeEncabezadoDto)
    {
        var informe = await _informeEncabezadoRepositorio.GetByIdAsync(id);
        if (informe == null) throw new KeyNotFoundException("Informe de Encabezado no encontrado.");

        informe.IdMantenimiento = informeEncabezadoDto.IdMantenimiento ?? informe.IdMantenimiento;
        informe.Fecha = informeEncabezadoDto.Fecha ?? informe.Fecha;

        await _informeEncabezadoRepositorio.UpdateAsync(informe);

        return new InformeEncabezadoDto
        {
            IdInforme = informe.IdInforme,
            IdMantenimiento = informe.IdMantenimiento,
            Fecha = informe.Fecha
        };
    }

    public async Task DeleteInformeEncabezadoAsync(int id)
    {
        await _informeEncabezadoRepositorio.DeleteAsync(id);
    }
}