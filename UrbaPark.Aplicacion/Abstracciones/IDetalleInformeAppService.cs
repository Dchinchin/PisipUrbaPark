using UrbaPark.Aplicacion.DTO;

namespace UrbaPark.Aplicacion.Abstracciones;

public interface IDetalleInformeAppService
{
    Task<IEnumerable<DetalleInformeDto>> GetAllDetallesInformeAsync();
    Task<IEnumerable<DetalleInformeDto>> GetFilteredDetallesInformeAsync(DetalleInformeFilterDto filter);
    Task<DetalleInformeDto?> GetDetalleInformeByIdAsync(int id);
    Task<DetalleInformeDto> CreateDetalleInformeAsync(CreateDetalleInformeDto detalleInformeDto);
    Task UpdateDetalleInformeAsync(UpdateDetalleInformeDto detalleInformeDto);
    Task DeleteDetalleInformeAsync(int id);
}
