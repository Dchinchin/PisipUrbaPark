using UrbaPark.Dominio.Modelo.Abstracciones;
using UrbaPark.Dominio.Modelo.Entidades;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace UrbaPark.Infraestructura.AccesoDatos.Repositorio
{
    public class Info_EncaRepositorioImpl : RepositorioImpl<Informes_Encabezado>, IInfo_EncaRepositorio
    {
        private readonly Pisip_UrbanParkContext _UrbanParkContext;

        public Info_EncaRepositorioImpl(Pisip_UrbanParkContext dbContext) : base(dbContext)
        {
            _UrbanParkContext = dbContext;
        }

        public async Task<Informes_Encabezado?> GetByIdWithDetailsAsync(int id)
        {
            return await _UrbanParkContext.Informes_Encabezado
                .Include(i => i.Detalle_Informe)
                .FirstOrDefaultAsync(i => i.IdInforme == id);
        }

        public async Task<IEnumerable<Informes_Encabezado>> GetAllWithDetailsAsync(Expression<Func<Informes_Encabezado, bool>> filter)
        {
            return await _UrbanParkContext.Informes_Encabezado
                .Include(i => i.Detalle_Informe)
                .Where(filter)
                .ToListAsync();
        }
    }
}
