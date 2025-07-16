using UrbaPark.Aplicacion.DTO;

namespace UrbaPark.Aplicacion.Abstracciones;

public interface IMantenimientoAppService
{
    Task<IEnumerable<MantenimientoDto>> GetAllMantenimientosAsync();
    Task<IEnumerable<MantenimientoDto>> GetFilteredMantenimientosAsync(MantenimientoFilterDto filter);
    Task<MantenimientoDto?> GetMantenimientoByIdAsync(int id);
    Task<MantenimientoDto> CreateMantenimientoAsync(CreateMantenimientoDto mantenimientoDto);
    Task UpdateMantenimientoAsync(UpdateMantenimientoDto mantenimientoDto);
    Task DeleteMantenimientoAsync(int id);
}
