using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UrbaPark.Dominio.Modelo.Abstracciones;



namespace UrbaPark.Infraestructura.AccesoDatos.Repositorio
{
    public class RepositorioImpl<T> : IRepositorio<T> where T : class
    {
        private readonly Pisip_UrbanParkContext _dbContext;
        private readonly DbSet<T> _dbSet;


        //constructor
        public RepositorioImpl(Pisip_UrbanParkContext dBContext)
        {
            _dbContext = dBContext;
            _dbSet = dBContext.Set<T>();
        }
        public virtual async Task AddAsync(T TEntity)
        {
            try
            {
                await _dbSet.AddAsync(TEntity);
                await _dbContext.SaveChangesAsync();
            }
            catch (Exception e)
            {
                throw new Exception("Error: No se pudo insertar Datos " + e);
            }
        }

        public async Task DeleteAsync(int id)
        {
            try
            {
                var entity = await GetByIdAsync(id);
                if (entity != null)
                {
                    _dbSet.Remove(entity);
                    await _dbContext.SaveChangesAsync();
                }
            }
            catch (Exception e)
            {
                throw new Exception("Error: No se pudo eliminar Datos " + e.Message);
            }
        }

        public async Task<IEnumerable<T>> GetAllAsync(System.Linq.Expressions.Expression<Func<T, bool>>? filter = null, params System.Linq.Expressions.Expression<Func<T, object>>[] includes)
        {
            try
            {
                IQueryable<T> query = _dbSet;

                foreach (var include in includes)
                {
                    query = query.Include(include);
                }

                if (filter != null)
                {
                    query = query.Where(filter);
                }
                return await query.ToListAsync();
            }
            catch (Exception e)
            {
                throw new Exception("Error: No se pudo listar Datos " + e.Message);
            }
        }

        public async Task<T?> GetByIdAsync(int id, params System.Linq.Expressions.Expression<Func<T, object>>[] includes)
        {
            try
            {
                IQueryable<T> query = _dbSet;

                foreach (var include in includes)
                {
                    query = query.Include(include);
                }

                var keyName = _dbContext.Model.FindEntityType(typeof(T)).FindPrimaryKey().Properties.Select(x => x.Name).Single();
                return await query.FirstOrDefaultAsync(e => EF.Property<int>(e, keyName) == id);
            }
            catch (Exception e)
            {
                throw new Exception("Error: No se pudo buscar por id los Datos " + e.Message);
            }
        }

        public virtual async Task UpdateAsync(T Entity)
        {
            try
            {
                _dbSet.Update(Entity);
                await _dbContext.SaveChangesAsync();
            }
            catch (Exception e)
            {
                throw new Exception("Error: No se pudo actualizar Datos " + e.Message);
            }
        }


    }
}
