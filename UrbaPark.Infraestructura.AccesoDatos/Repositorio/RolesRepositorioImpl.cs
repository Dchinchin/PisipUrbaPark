using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UrbaPark.Dominio.Modelo.Abstracciones;

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
