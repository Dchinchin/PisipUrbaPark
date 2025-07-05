using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UrbaPark.Dominio.Modelo.Abstracciones;

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
