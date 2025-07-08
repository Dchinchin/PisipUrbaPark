using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UrbaPark.Dominio.Modelo.Abstracciones;

namespace UrbaPark.Infraestructura.AccesoDatos.Repositorio
{
    public class BitacoraRepositorioImpl : RepositorioImpl<Bitacora>, IBitacoraRepositorio
    {
        private readonly Pisip_UrbanParkContext _UrbanParkContext;

        public BitacoraRepositorioImpl(Pisip_UrbanParkContext dbContext) : base(dbContext)
        {
            _UrbanParkContext = dbContext;
        }

        public Task<List<Bitacora>> ListarBitacora()
        {
            throw new NotImplementedException();
        }
    }
}
