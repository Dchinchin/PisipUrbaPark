using UrbaPark.Aplicacion.DTO;
using UrbaPark.Dominio.Modelo.Entidades;

namespace UrbaPark.Aplicacion.Abstracciones;

public interface IUsuarioAppService
{
    Task<IEnumerable<UsuarioDto>> GetAllUsuariosAsync();
    Task<IEnumerable<UsuarioDto>> GetFilteredUsuariosAsync(UsuarioFilterDto filter);
    Task<UsuarioDto?> GetUsuarioByIdAsync(int id);
    Task<UsuarioDto> CreateUsuarioAsync(CreateUsuarioDto usuarioDto);
    Task UpdateUsuarioAsync(UpdateUsuarioDto usuarioDto);
    Task DeleteUsuarioAsync(int id);
}
