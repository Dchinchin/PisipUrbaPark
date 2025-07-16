using UrbaPark.Dominio.Modelo.Abstracciones;
using UrbaPark.Dominio.Modelo.Entidades;

namespace UrbaPark.Infraestructura.AccesoDatos.Repositorio
{
    public class ParqueaderoRepositorioImpl : RepositorioImpl<Parquadero>, IParqueaderoRepositorio
    {
        private readonly Pisip_UrbanParkContext _UrbanParkContext;

        public ParqueaderoRepositorioImpl(Pisip_UrbanParkContext dbContext) : base(dbContext)
        {
            _UrbanParkContext = dbContext;
        }
    }
}
