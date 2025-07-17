using UrbaPark.Aplicacion.DTO;
using UrbaPark.Dominio.Modelo.Entidades;

namespace UrbaPark.Aplicacion.Abstracciones;

public interface IUsuarioAppService
{
    Task<IEnumerable<UsuarioDto>> GetAllUsuariosAsync();
    Task<IEnumerable<UsuarioDto>> GetFilteredUsuariosAsync(UsuarioFilterDto filter);
    Task<UsuarioDto?> GetUsuarioByIdAsync(int id);
    Task<UsuarioDto> CreateUsuarioAsync(CreateUsuarioDto usuarioDto);
    Task<UsuarioDto> UpdateUsuarioAsync(int id, UpdateUsuarioDto usuarioDto);
    Task<bool> Authenticate(AuthenticateRequestDto request);
    
    Task DeleteUsuarioAsync(int id);
    int GetCurrentUserId();
}
