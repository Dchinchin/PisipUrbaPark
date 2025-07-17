using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UrbaPark.Dominio.Modelo.Abstracciones
{
    public interface IRepositorio<T> where T : class
    {
        Task AddAsync(T TEntity); //insertar
        Task UpdateAsync(T Entity);//actualizar
        Task DeleteAsync(int id);//eliminar por ID
        Task<IEnumerable<T>> GetAllAsync(System.Linq.Expressions.Expression<Func<T, bool>>? filter = null, params System.Linq.Expressions.Expression<Func<T, object>>[] includes);
        Task<T?> GetByIdAsync(int id, params System.Linq.Expressions.Expression<Func<T, object>>[] includes);
    }
}
