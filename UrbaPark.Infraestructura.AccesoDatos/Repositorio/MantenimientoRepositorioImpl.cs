using UrbaPark.Dominio.Modelo.Abstracciones;
using UrbaPark.Dominio.Modelo.Entidades;

namespace UrbaPark.Infraestructura.AccesoDatos.Repositorio
{
    public class MantenimientoRepositorioImpl : RepositorioImpl<Mantenimiento>, IMantenimientoRepositorio
    {
        private readonly Pisip_UrbanParkContext _UrbanParkContext;

        public MantenimientoRepositorioImpl(Pisip_UrbanParkContext dbContext) : base(dbContext)
        {
            _UrbanParkContext = dbContext;
        }
    }
}
