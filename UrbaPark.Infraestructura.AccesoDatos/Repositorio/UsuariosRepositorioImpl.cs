using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UrbaPark.Dominio.Modelo.Abstracciones;

namespace UrbaPark.Infraestructura.AccesoDatos.Repositorio
{
    public class UsuariosRepositorioImpl : RepositorioImpl<Usuarios>, IUsuariosRepositorio
    {
        private readonly Pisip_UrbanParkContext _UrbanParkContext;

        public UsuariosRepositorioImpl(Pisip_UrbanParkContext dbContext) : base(dbContext)
        {
            _UrbanParkContext = dbContext;
        }
    }
}
