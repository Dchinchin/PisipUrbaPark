using UrbaPark.Aplicacion.DTO;

namespace UrbaPark.Aplicacion.Abstracciones;

public interface ITipoMantenimientoAppService
{
    Task<IEnumerable<TipoMantenimientoDto>> GetAllTipoMantenimientosAsync();
    Task<IEnumerable<TipoMantenimientoDto>> GetFilteredTipoMantenimientosAsync(TipoMantenimientoFilterDto filter);
    Task<TipoMantenimientoDto?> GetTipoMantenimientoByIdAsync(int id);
    Task<TipoMantenimientoDto> CreateTipoMantenimientoAsync(CreateTipoMantenimientoDto tipoMantenimientoDto);
    Task<TipoMantenimientoDto> UpdateTipoMantenimientoAsync(int id, UpdateTipoMantenimientoDto tipoMantenimientoDto);
    Task DeleteTipoMantenimientoAsync(int id);
}
