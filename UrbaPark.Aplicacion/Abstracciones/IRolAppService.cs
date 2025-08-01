using UrbaPark.Aplicacion.DTO;

namespace UrbaPark.Aplicacion.Abstracciones;

public interface IRolAppService
{
    Task<IEnumerable<RolDto>> GetAllRolesAsync();
    Task<IEnumerable<RolDto>> GetFilteredRolesAsync(RolFilterDto filter);
    Task<RolDto?> GetRolByIdAsync(int id);
    Task<RolDto> CreateRolAsync(CreateRolDto rolDto);
    Task<RolDto> UpdateRolAsync(int id, UpdateRolDto rolDto);
    Task DeleteRolAsync(int id);
}
