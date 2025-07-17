using UrbaPark.Aplicacion.DTO;
using Microsoft.AspNetCore.Http;

namespace UrbaPark.Aplicacion.Abstracciones;

public interface IDetalleInformeAppService
{
    Task<IEnumerable<DetalleInformeDto>> GetAllDetallesInformeAsync();
    Task<IEnumerable<DetalleInformeDto>> GetFilteredDetallesInformeAsync(DetalleInformeFilterDto filter);
    Task<DetalleInformeDto?> GetDetalleInformeByIdAsync(int id);
    Task<DetalleInformeDto> CreateDetalleInformeAsync(CreateDetalleInformeDto detalleInformeDto, IFormFile? archivoFile);
    Task<DetalleInformeDto> UpdateDetalleInformeAsync(int id, UpdateDetalleInformeDto detalleInformeDto, IFormFile? archivoFile);
    Task DeleteDetalleInformeAsync(int id);
}
