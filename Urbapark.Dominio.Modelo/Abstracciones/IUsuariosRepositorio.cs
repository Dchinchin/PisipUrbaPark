using UrbaPark.Dominio.Modelo.Entidades;

namespace UrbaPark.Dominio.Modelo.Abstracciones
{
    public interface IUsuariosRepositorio : IRepositorio <Usuarios>
    {
        Task<Usuarios?> GetByEmailAsync(string email);
    }
}
