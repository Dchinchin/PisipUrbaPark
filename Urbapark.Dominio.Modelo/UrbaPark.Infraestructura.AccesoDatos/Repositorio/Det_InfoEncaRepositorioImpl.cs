using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UrbaPark.Dominio.Modelo.Abstracciones;

namespace UrbaPark.Infraestructura.AccesoDatos.Repositorio
{
    public class Det_InfoEncaRepositorioImpl : RepositorioImpl<Informes_Encabezado>, IInfo_EncaRepositorio
    {
        private readonly Pisip_UrbanParkContext _UrbanParkContext;

        public Det_InfoEncaRepositorioImpl(Pisip_UrbanParkContext dbContext) : base(dbContext)
        {
            _UrbanParkContext = dbContext;
        }
    }
}
