using UrbaPark.Dominio.Modelo.Entidades;

using System.Linq.Expressions;

namespace UrbaPark.Dominio.Modelo.Abstracciones
{
    public interface IInfo_EncaRepositorio: IRepositorio<Informes_Encabezado>
    {
        Task<Informes_Encabezado?> GetByIdWithDetailsAsync(int id);
        Task<IEnumerable<Informes_Encabezado>> GetAllWithDetailsAsync(Expression<Func<Informes_Encabezado, bool>> filter);
    }
}
