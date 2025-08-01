using UrbaPark.Aplicacion.DTO;

namespace UrbaPark.Aplicacion.Abstracciones;

public interface IInformeEncabezadoAppService
{
    Task<IEnumerable<InformeEncabezadoDto>> GetAllInformesEncabezadoAsync();
    Task<IEnumerable<InformeEncabezadoDto>> GetFilteredInformesEncabezadoAsync(InformeEncabezadoFilterDto filter);
    Task<InformeEncabezadoDto?> GetInformeEncabezadoByIdAsync(int id);
    Task<InformeEncabezadoDto> CreateInformeEncabezadoAsync(CreateInformeEncabezadoDto informeEncabezadoDto);
    Task<InformeEncabezadoDto> UpdateInformeEncabezadoAsync(int id, UpdateInformeEncabezadoDto informeEncabezadoDto);
    Task DeleteInformeEncabezadoAsync(int id);
}
