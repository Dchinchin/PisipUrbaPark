using UrbaPark.Aplicacion.Abstracciones;
using UrbaPark.Aplicacion.DTO;
using UrbaPark.Dominio.Modelo.Abstracciones;
using UrbaPark.Dominio.Modelo.Entidades;

namespace UrbaPark.Aplicacion.Implementaciones;

public class DetalleInformeAppService : IDetalleInformeAppService
{
    private readonly IDet_InfoEncaRepositorio _detalleInformeRepositorio;

    public DetalleInformeAppService(IDet_InfoEncaRepositorio detalleInformeRepositorio)
    {
        _detalleInformeRepositorio = detalleInformeRepositorio;
    }

    public async Task<IEnumerable<DetalleInformeDto>> GetAllDetallesInformeAsync()
    {
        return await GetFilteredDetallesInformeAsync(new DetalleInformeFilterDto());
    }

    public async Task<IEnumerable<DetalleInformeDto>> GetFilteredDetallesInformeAsync(DetalleInformeFilterDto filter)
    {
        var detalles = await _detalleInformeRepositorio.GetAllAsync(d =>
            (!filter.IdDetalleInforme.HasValue || d.id_detInfo == filter.IdDetalleInforme.Value) &&
            (!filter.IdInforme.HasValue || d.id_informe == filter.IdInforme.Value) &&
            (string.IsNullOrEmpty(filter.Descripcion) || d.descripcion.Contains(filter.Descripcion))
        );

        return detalles.Select(d => new DetalleInformeDto
        {
            IdDetInfo = d.id_detInfo,
            IdInforme = d.id_informe,
            Descripcion = d.descripcion,
            ArchivoUrl = d.archivo_url
        });
    }

    public async Task<DetalleInformeDto?> GetDetalleInformeByIdAsync(int id)
    {
        var detalle = await _detalleInformeRepositorio.GetByIdAsync(id);
        if (detalle == null) return null;

        return new DetalleInformeDto
        {
            IdDetInfo = detalle.id_detInfo,
            IdInforme = detalle.id_informe,
            Descripcion = detalle.descripcion,
            ArchivoUrl = detalle.archivo_url
        };
    }

    public async Task<DetalleInformeDto> CreateDetalleInformeAsync(CreateDetalleInformeDto detalleInformeDto)
    {
        var detalle = new Detalle_Informe
        {
            id_informe = detalleInformeDto.IdInforme,
            descripcion = detalleInformeDto.Descripcion,
            archivo_url = detalleInformeDto.ArchivoUrl
        };

        await _detalleInformeRepositorio.AddAsync(detalle);

        return new DetalleInformeDto
        {
            IdDetInfo = detalle.id_detInfo,
            IdInforme = detalle.id_informe,
            Descripcion = detalle.descripcion,
            ArchivoUrl = detalle.archivo_url
        };
    }

    public async Task UpdateDetalleInformeAsync(UpdateDetalleInformeDto detalleInformeDto)
    {
        var detalle = await _detalleInformeRepositorio.GetByIdAsync(detalleInformeDto.IdDetInfo);
        if (detalle == null) throw new KeyNotFoundException("Detalle de Informe no encontrado.");

        detalle.id_informe = detalleInformeDto.IdInforme ?? detalle.id_informe;
        detalle.descripcion = detalleInformeDto.Descripcion ?? detalle.descripcion;
        detalle.archivo_url = detalleInformeDto.ArchivoUrl ?? detalle.archivo_url;

        await _detalleInformeRepositorio.UpdateAsync(detalle);
    }

    public async Task DeleteDetalleInformeAsync(int id)
    {
        await _detalleInformeRepositorio.DeleteAsync(id);
    }
}
