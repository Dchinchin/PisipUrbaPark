using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UrbaPark.Aplicacion.Servicio;
using UrbaPark.Dominio.Modelo.Abstracciones;
using UrbaPark.Infraestructura.AccesoDatos;
using UrbaPark.Infraestructura.AccesoDatos.Repositorio;

namespace UrbaPark.Aplicacion.ServicioImpl
{
    public class CronogramaServicioImpl : ICronogramaServicio
    {
        private readonly ICronogramaRepositorio _cronogramaRepositorio;

        public CronogramaServicioImpl(Pisip_UrbanParkContext pisip_UrbanParkContext)
        {
            _cronogramaRepositorio = new CronogramaRepositorioImpl(pisip_UrbanParkContext);
        }

        public Task CronogramaAddAsync(Bitacora TEntity)
        {
            throw new NotImplementedException();
        }

        public Task CronogramaDeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Bitacora>> CronogramaGetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Bitacora> CronogramaGetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task CronogramaUpdateAsync(Bitacora Entity)
        {
            throw new NotImplementedException();
        }

        public Task<List<Cronogramas>> ListarCronograma()
        {
            throw new NotImplementedException();
        }
    }

    
}
