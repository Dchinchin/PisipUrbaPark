using UrbaPark.Dominio.Modelo.Abstracciones;
using UrbaPark.Dominio.Modelo.Entidades;

namespace UrbaPark.Infraestructura.AccesoDatos.Repositorio
{
    public class Info_EncaRepositorioImpl : RepositorioImpl<Informes_Encabezado>, IInfo_EncaRepositorio
    {
        private readonly Pisip_UrbanParkContext _UrbanParkContext;

        public Info_EncaRepositorioImpl(Pisip_UrbanParkContext dbContext) : base(dbContext)
        {
            _UrbanParkContext = dbContext;
        }
    }
}
