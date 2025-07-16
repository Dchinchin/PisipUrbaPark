using UrbaPark.Dominio.Modelo.Abstracciones;
using UrbaPark.Dominio.Modelo.Entidades;

namespace UrbaPark.Infraestructura.AccesoDatos.Repositorio
{
    public class RolesRepositorioImpl : RepositorioImpl<Roles>, IRolesRepositorio
    {
        private readonly Pisip_UrbanParkContext _UrbanParkContext;

        public RolesRepositorioImpl(Pisip_UrbanParkContext dbContext) : base(dbContext)
        {
           _UrbanParkContext = dbContext;
        }

    
    }
}
