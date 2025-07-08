using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UrbaPark.Dominio.Modelo.Abstracciones;

namespace UrbaPark.Infraestructura.AccesoDatos.Repositorio
{
    public class CronogramaRepositorioImpl : RepositorioImpl<Cronogramas>, ICronogramaRepositorio
    {
        private readonly Pisip_UrbanParkContext _UrbanParkContext;

        public CronogramaRepositorioImpl(Pisip_UrbanParkContext dbContext) : base(dbContext)
        {
            _UrbanParkContext = dbContext;
        }

        public Task<List<Cronogramas>> ListarCronogramaActivo()
        {
            throw new NotImplementedException();
        }
    }
}
