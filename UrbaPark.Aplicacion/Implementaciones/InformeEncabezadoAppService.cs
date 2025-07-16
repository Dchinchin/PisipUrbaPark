using UrbaPark.Aplicacion.Abstracciones;
using UrbaPark.Aplicacion.DTO;
using UrbaPark.Dominio.Modelo.Abstracciones;
using UrbaPark.Dominio.Modelo.Entidades;

namespace UrbaPark.Aplicacion.Implementaciones;

public class InformeEncabezadoAppService : IInformeEncabezadoAppService
{
    private readonly IInfo_EncaRepositorio _informeEncabezadoRepositorio;

    public InformeEncabezadoAppService(IInfo_EncaRepositorio informeEncabezadoRepositorio)
    {
        _informeEncabezadoRepositorio = informeEncabezadoRepositorio;
    }

    public async Task<IEnumerable<InformeEncabezadoDto>> GetAllInformesEncabezadoAsync()
    {
        return await GetFilteredInformesEncabezadoAsync(new InformeEncabezadoFilterDto());
    }

    public async Task<IEnumerable<InformeEncabezadoDto>> GetFilteredInformesEncabezadoAsync(InformeEncabezadoFilterDto filter)
    {
        var informes = await _informeEncabezadoRepositorio.GetAllAsync(i =>
            (!filter.IdInforme.HasValue || i.IdInforme == filter.IdInforme.Value) &&
            (!filter.IdUsuario.HasValue || i.IdMantenimiento == filter.IdUsuario.Value) && // Assuming IdUsuario in filter maps to IdMantenimiento in entity for filtering purposes, as IdUsuario is not directly in Informes_Encabezado
            (!filter.FechaDesde.HasValue || i.Fecha >= filter.FechaDesde.Value) &&
            (!filter.FechaHasta.HasValue || i.Fecha <= filter.FechaHasta.Value)
        );

        return informes.Select(i => new InformeEncabezadoDto
        {
            IdInforme = i.IdInforme,
            IdMantenimiento = i.IdMantenimiento,
            Fecha = i.Fecha
        });
    }

    public async Task<InformeEncabezadoDto?> GetInformeEncabezadoByIdAsync(int id)
    {
        var informe = await _informeEncabezadoRepositorio.GetByIdAsync(id);
        if (informe == null) return null;

        return new InformeEncabezadoDto
        {
            IdInforme = informe.IdInforme,
            IdMantenimiento = informe.IdMantenimiento,
            Fecha = informe.Fecha
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

    public async Task UpdateInformeEncabezadoAsync(UpdateInformeEncabezadoDto informeEncabezadoDto)
    {
        var informe = await _informeEncabezadoRepositorio.GetByIdAsync(informeEncabezadoDto.IdInforme);
        if (informe == null) throw new KeyNotFoundException("Informe de Encabezado no encontrado.");

        informe.IdMantenimiento = informeEncabezadoDto.IdMantenimiento ?? informe.IdMantenimiento;
        informe.Fecha = informeEncabezadoDto.Fecha ?? informe.Fecha;

        await _informeEncabezadoRepositorio.UpdateAsync(informe);
    }

    public async Task DeleteInformeEncabezadoAsync(int id)
    {
        await _informeEncabezadoRepositorio.DeleteAsync(id);
    }
}
