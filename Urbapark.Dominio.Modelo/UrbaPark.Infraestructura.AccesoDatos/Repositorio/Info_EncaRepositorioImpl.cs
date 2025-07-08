using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UrbaPark.Dominio.Modelo.Abstracciones;

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
