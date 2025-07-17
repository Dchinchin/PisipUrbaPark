using UrbaPark.Aplicacion.DTO;

namespace UrbaPark.Aplicacion.Abstracciones;

public interface IParqueaderoAppService
{
    Task<IEnumerable<ParqueaderoDto>> GetAllParqueaderosAsync();
    Task<IEnumerable<ParqueaderoDto>> GetFilteredParqueaderosAsync(ParqueaderoFilterDto filter);
    Task<ParqueaderoDto?> GetParqueaderoByIdAsync(int id);
    Task<ParqueaderoDto> CreateParqueaderoAsync(CreateParqueaderoDto parqueaderoDto);
    Task<ParqueaderoDto> UpdateParqueaderoAsync(int id, UpdateParqueaderoDto parqueaderoDto);
    
    Task DeleteParqueaderoAsync(int id);
}
